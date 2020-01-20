using Doctor.Connection;
using Doctor.Conversion;
using Doctor.PacketHandling;
using Doctor.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doctor
{
    public partial class DetailDoctorForm : Form
    {
        private ServerConnection serverConnection;
        private PageConversion pageConversion;

        private ClientServerWorker clientServerWorker;
        private PacketHandler packetHandler;

        private Patient patient;
        
        public DetailDoctorForm(Patient patient, ServerConnection serverConnection)
        {
            InitializeComponent();

            this.patient = patient;
            this.serverConnection = serverConnection;
            this.packetHandler = new PacketHandler();

            InitializeDefaultEvents();
            SetDefaultValues();
            SetupDoctorSync();
        }

        private void InitializeDefaultEvents()
        {
            this.FormClosing += (s, e) =>
            {
                if (this.serverConnection.Connected)
                {
                    clientServerWorker.Stop();
                    this.serverConnection.SendWithNoResponse($"Doctor/Close\r\n");
                }
            };
        }

        public void SetDefaultValues()
        {
            lblName.Text = patient.Name;
            lblBirthDate.Text = patient.Age + "";
            lblGender.Text = patient.Gender;
            lblPantiëntKey.Text = patient.Id;
        }

        private async void SetupDoctorSync()
        {
            bool connectToClient = await ConnectToClient();
            if (connectToClient)
                StartWorker();
        }

        private void ToggleControls(bool value)
        {
            for (int i = 0; i < this.Controls.Count; i++)
                this.Controls[i].Enabled = value;
        }

        private async Task<bool> ConnectToClient()
        {
            try
            {
                ToggleControls(false);

                string responseRaw = await this.serverConnection.SendWithResponse($"Doctor/ConnectToClient\r\n{patient.Id}");
                Tuple<string[], PacketType> responsePacket = packetHandler.HandlePacket(responseRaw);
                if (responsePacket.Item2 == PacketType.Status)
                {
                    if (packetHandler.IsStatusOk(responsePacket))
                    {
                        ToggleControls(true);
                        AppendMessage($"Systeem: Verbonden met client: {patient.Name} - {patient.Id}");

                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void StartWorker()
        {
            this.clientServerWorker = new ClientServerWorker(this.serverConnection);
            this.clientServerWorker.StatusReceived += ClientServerWorker_StatusReceived;
            this.clientServerWorker.BikeReceived += ClientServerWorker_BikeReceived;
            this.clientServerWorker.ClientDisconnectReceived += ClientServerWorker_ClientDisconnectReceived;
            this.clientServerWorker.BroadcastReceived += ClientServerWorker_BroadcastReceived;
            this.clientServerWorker.MessageReceived += ClientServerWorker_MessageReceived;
            this.clientServerWorker.Run();
        }

        private void ClientServerWorker_StatusReceived(StatusArgs args)
        {
            // Obsolute until further notice
        }

        private void ClientServerWorker_BikeReceived(BikeArgs args)
        {
            pageConversion = new PageConversion();
            pageConversion.Page10Received += (e) =>
            {

            };
            pageConversion.Page19Received += (e) =>
            {
                int instandpowerLSB = e.Data[5];
                int instandpowerMSB = e.Data[6];
                int work1 = (((instandpowerMSB | 0b11110000) ^ 0b11110000) << 8) | instandpowerLSB;

                int currSpeed = (int)Math.Round(work1 * 6.1182972778676, 0);

                //DrawSpeedOnChart(chrtSpeedIndexCounter, this.currSpeed);
            };
            pageConversion.Page50Received += (e) =>
            {

            };

            byte[] data = args.Data.ParseRepString();
            pageConversion.RegisterData(data.SubArray(4, args.Data.Length - 4));
        }

        private void ClientServerWorker_ClientDisconnectReceived(EventArgs args)
        {
            this.Invoke((MethodInvoker)delegate
            {
                AppendMessage("Systeem: De patient heeft de verbinding gesloten");
                clientServerWorker.Stop();
            });
        }

        private void ClientServerWorker_MessageReceived(MessageArgs args)
        {
            this.Invoke((MethodInvoker)delegate 
            {
                AppendMessage($"{patient.Name}: {args.Message}");
            });
        }

        private void ClientServerWorker_BroadcastReceived(BroadcastArgs args)
        {
            this.Invoke((MethodInvoker)delegate
            {
                AppendMessage($"Broadcast: {args.Message}");
            });
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Als je de sessie afsluit wordt de huidige data opgeslagen en de sessie afgesloten."+ "\r\n" + "Weet je zeker dat je de sessie stoppen?",
                               "Stop Sessie",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void StartSesion_Click(object sender, EventArgs e)
        {

        }

        private void StopSession_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Als je de sessie afsluit wordt de huidige data opgeslagen en de sessie afgesloten." + "\r\n" + "Weet je zeker dat je de sessie stoppen?",
                               "Stop Sessie",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void EmergencyBreak_Click(object sender, EventArgs e)
        {
            // Ook de VR pause aanroepen & misschien de tekst van de panel aanpassen
            // Doctor/Stop

            if (this.serverConnection.Connected)
            {
                this.serverConnection.SendWithNoResponse($"Doctor/StopVR\r\n");
                AppendMessage("Systeem: De VR is gestopt");
            }
        }

        private void AppendMessage(string message)
        {
            tbMessageHistory.AppendText($"[{DateTime.Now.ToShortTimeString()}] {message}\n");
            tbMessageHistory.AppendText(Environment.NewLine);
        }

        private void TextBoxSendMessage_Enter(object sender, EventArgs e)
        {
            if (tbTextBoxSendMessage.Text == "Stuur bericht ...")
            {
                tbTextBoxSendMessage.Text = "";
                tbTextBoxSendMessage.ForeColor = Color.Black;
            }
        }

        private void TbTextBoxSendMessage_Leave(object sender, EventArgs e)
        {
            if (tbTextBoxSendMessage.Text == "")
            {
                tbTextBoxSendMessage.Text = "Stuur bericht ...";
                tbTextBoxSendMessage.ForeColor = Color.Silver;
            }
        }

        private void TbTextBoxSendMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(tbTextBoxSendMessage.Text))
                {
                    AppendMessage($"Doctor: {tbTextBoxSendMessage.Text}");
                    this.serverConnection.SendWithNoResponse($"Doctor/Message\r\n{tbTextBoxSendMessage.Text.Trim()}");
                    tbTextBoxSendMessage.Text = "";
                }
            }
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            ClientHistoryForm clientHistoryForm = new ClientHistoryForm();
            clientHistoryForm.ShowDialog();
        }

        private void buttonResistance_Click(object sender, EventArgs e)
        {
            DetailDoctorForm detailDoctorForm = new DetailDoctorForm(patient, serverConnection);
            int resistanceValue = detailDoctorForm.trackBarResistance.Value;

            // Moet via het sturen naar de server die het dan weer stuurt naar de client
            // Je hebt als doctor geen directe connectie met de bike
            //
            //BleBikeHandler ble = new BleBikeHandler();
            //ble.ChangeResistance(resistanceValue);

        }

        /*private void writechHearthRate(Patient patient)
        {
            int i = 0;
            foreach (double hearbeat in patient.heartbeat)
            {
                chHeartRate.Series["VO2Now"].Points.AddXY(i, patient.heartrate);
                i++;
            }
        }
        private void writechBikeSpeed(Patient patient)
        {
            int i = 0;
            foreach (double hearbeat in patient.heartbeat)
            {
                chBikeSpeed.Series["VO2Now"].Points.AddXY(i, Bikespeed);
                i++;
            }
        }*/
    }
}
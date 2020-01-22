using Doctor.Connection;
using Doctor.Conversion;
using Doctor.PacketHandling;
using Doctor.Utils;
using Doctor.Utils.DataHolders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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

        private List<byte[]> historyBikeData;
        private List<byte[]> historyHeartData;

        private Patient patient;
        private int chrtSpeedIndexCounter = 1;
        private int chrtBpmIndexCounter = 1;

        private int travelledDistance;
        private byte travelledDistanceRawPrev;
        private byte travelledDistanceStartingValue;
        private bool started = true;

        public DetailDoctorForm(Patient patient, ServerConnection serverConnection)
        {
            InitializeComponent();

            this.patient = patient;
            this.serverConnection = serverConnection;
            this.packetHandler = new PacketHandler();

            this.historyBikeData = new List<byte[]>();
            this.historyHeartData = new List<byte[]>();

            InitializeDefaultEvents();
            SetDefaultValues();
            SetupDoctorSync();
        }

        private void InitializeDefaultEvents()
        {
            this.FormClosing += async(s, e) =>
            {
                if (this.serverConnection.Connected)
                {
                    if (MessageBox.Show("Als je de sessie afsluit wordt de huidige data opgeslagen en de sessie afgesloten." + 
                        "\r\n" + "Weet je zeker dat je de sessie stoppen?", "Stop Sessie", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        clientServerWorker.Stop();
                        Thread.Sleep(1000);

                        HistoryItem newHistoryItem = new HistoryItem(this.historyBikeData, this.historyHeartData);
                        string json = JsonConvert.SerializeObject(newHistoryItem);

                        string rawResponse = await this.serverConnection.SendWithResponse($"Doctor/AddClientHistory\r\n{this.patient.Id}\r\n{DateTime.Now}\r\n{json}");
                        if (packetHandler.IsStatusOk(packetHandler.HandlePacket(rawResponse)))
                        {
                            if (MessageBox.Show("De data is opgeslagen", "Data opslag", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                this.serverConnection.SendWithNoResponse($"Doctor/Close\r\n");
                        }
                        else
                        {
                            if (MessageBox.Show("Probleem tijdens het verkrijgen van een response", "Data opslag", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                                this.serverConnection.SendWithNoResponse($"Doctor/Close\r\n");
                        }
                    }
                    else
                        e.Cancel = true;
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
            this.clientServerWorker.SyncDataReceived += ClientServerWorker_SyncDataReceived;
            this.clientServerWorker.ClientDisconnectReceived += ClientServerWorker_ClientDisconnectReceived;
            this.clientServerWorker.BroadcastReceived += ClientServerWorker_BroadcastReceived;
            this.clientServerWorker.MessageReceived += ClientServerWorker_MessageReceived;
            this.clientServerWorker.Run();
        }

        private void ClientServerWorker_StatusReceived(StatusArgs args)
        {
            // Obsolute until further notice
        }

        private void ClientServerWorker_SyncDataReceived(SyncDataArgs args)
        {
            pageConversion = new PageConversion();
            pageConversion.Page10Received += (e) =>
            {
                byte[] internalBikeData = args.BikeData.ParseRepString();

                if (started)
                {
                    travelledDistanceStartingValue = internalBikeData[7];
                    started = false;
                }

                int t = internalBikeData[7] - travelledDistanceRawPrev;
                if (t < 0)
                {
                    t += 256;
                }
                travelledDistance += t;
                travelledDistanceRawPrev = (byte)travelledDistance;

                this.Invoke((MethodInvoker)delegate
                {
                    lblDistance.Text = $"Distance: {travelledDistance - travelledDistanceStartingValue}m";
                });
            };
            pageConversion.Page19Received += (e) =>
            {
                int lsb = e.Data[4];
                int msb = e.Data[5];
                int work1 = lsb + (msb << 8);

                int currSpeed = (int)Math.Round((double)(work1 / 1000), 0);
                DrawSpeedOnChart(chrtSpeedIndexCounter, currSpeed);
            };

            byte[] bikeData = args.BikeData.ParseRepString();
            byte[] heartData = args.HeartData.ParseRepString();

            if (bikeData.Length > 0)
                pageConversion.RegisterData(bikeData.SubArray(4, bikeData.Length - 4));

            if (heartData.Length > 0)
                DrawHeartRateOnChart(chrtBpmIndexCounter, heartData[1]);
            else
                DrawHeartRateOnChart(chrtBpmIndexCounter, 0);

            this.historyBikeData.Add(bikeData);
            this.historyHeartData.Add(heartData);

            chrtBpmIndexCounter++;
            chrtSpeedIndexCounter++;
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

        private void DrawSpeedOnChart(int time, int speed)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    chBikeSpeed.Series["BikeSpeed"].Points.AddXY(time, speed);
                });
            }
            catch (InvalidOperationException) { }
        }

        private void DrawHeartRateOnChart(int time, int heartRate)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    chHeartRate.Series["BPM"].Points.AddXY(time, heartRate);
                });
            }
            catch (InvalidOperationException) { }
        }

        private void StopSession_Click(object sender, EventArgs e)
        {
            this.Close();
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
            ClientHistoryForm clientHistoryForm = new ClientHistoryForm(this.patient.Id);
            clientHistoryForm.ShowDialog();
        }

        private void buttonResistance_Click(object sender, EventArgs e)
        {
            AppendMessage($"Systeem: Fiets weerstand veranderd naar {trackBarResistance.Value} watt");
            this.serverConnection.SendWithNoResponse($"Doctor/Resistance\r\n{trackBarResistance.Value}");

        }

        private void TrackBarResistance_ValueChanged(object sender, EventArgs e)
        {
            lblResistance.Text = $"Resistance: {trackBarResistance.Value}w";
        }
    }
}
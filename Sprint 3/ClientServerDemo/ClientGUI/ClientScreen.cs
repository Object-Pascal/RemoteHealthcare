using Client.Json_Structure;
using ClientGUI.Bluetooth;
using ClientGUI.Connection;
using ClientGUI.Json_Structure.Serializables.Sub_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class ClientScreen : Form
    {
        private ServerConnectionVR serverConnectionVR;
        private JsonPacketBuilder jsonPacketBuilder;

        private ServerConnection serverConnection;
        private ClientServerWorker clientServerWorker;
        private BleBikeHandler bleBikeHandler;
        private BleHeartHandler bleHeartHandler;

        private int phase = 0;

        private string currentSessionId;

        private List<string> bleBikeList;

        private int seconds = 0;
        private int minutes = 0;
        private int phaseTime = 120;
        private int phaseTimeMin;
        private int phaseTimeSec;
        private int totalSeconds = 0;
        private int resistance = 0;




        public ClientScreen(ServerConnectionVR serverConnectionVR, ServerConnection serverConnection, string currentSessionId)
        {
            InitializeComponent();

            this.serverConnectionVR = serverConnectionVR;
            this.jsonPacketBuilder = new JsonPacketBuilder();
            this.serverConnection = serverConnection;
            this.currentSessionId = currentSessionId;

            InitializeDefaultEvents();
            InitializeDeclarations();
            LoadBikes(); 
            ToggleControls(false);
            StartWorker();
        }

        private void InitializeDeclarations()
        {
            this.bleHeartHandler = new BleHeartHandler();
            this.bleBikeHandler = new BleBikeHandler(); 
        }

        private async void LoadBikes()
        {
            this.bleBikeList = await this.bleBikeHandler.RetrieveBleBikes("Tacx");
            await bleHeartHandler.InitBleHeart();
            this.bleBikeList.ForEach(x => selectBike.Items.Add(x));
           
        }

        private void ChangeResistanceDown()
        {
            if (resistance > 0)
            {
                resistance--;
                bleBikeHandler.ChangeResistance(resistance);
            }
        }

        private void InitializeDefaultEvents()
        {
            this.FormClosing += (s, e) =>
            {
                if (this.serverConnection.Connected)
                {
                    clientServerWorker.Stop();
                    this.serverConnection.SendWithNoResponse($"Client/Close\r\n");
                }
            };
        }

        private async Task<bool> ReInitializeConnection()
        {
            return await serverConnectionVR.Connect("145.48.6.10", 6666);
        }

        private void ToggleControls(bool value)
        {
            for (int i = 0; i < this.Controls.Count; i++)
                this.Controls[i].Enabled = value;
        }

        private async void StartWorker()
        {
            if (this.serverConnection.Connected)
            {
                this.clientServerWorker = new ClientServerWorker(this.serverConnection);
                this.clientServerWorker.StatusReceived += ClientServerWorker_StatusReceived;
                this.clientServerWorker.DoctorDisconnectReceived += ClientServerWorker_DoctorDisconnectReceived;
                this.clientServerWorker.ResistanceReceived += ClientServerWorker_ResistanceReceived;
                this.clientServerWorker.BroadcastReceived += ClientServerWorker_BroadcastReceived;
                this.clientServerWorker.MessageReceived += ClientServerWorker_MessageReceived;
                this.clientServerWorker.StopReceived += ClientServerWorker_StopReceived;
                this.clientServerWorker.Run();
            }
            else
            {
                if (MessageBox.Show("De server is op het moment niet beschikbaar, wil opnieuw proberen te verbinden?", "Server niet beschikbaar", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    bool connected = await ReInitializeConnection();
                    if (connected)
                        StartWorker();
                }
            }
        }

        private void ClientServerWorker_StatusReceived(StatusArgs args)
        {
            if (args.Status == "ready")
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    ToggleControls(true);
                }));

                // Pakket verzenden: Client/Bike\r\nBIKE_BYTES
                // Bike byte structuur: EX: [164,9,78,5,25,174,0,106,26,0,96,32,97]

                // VR Starten
                // Bike Starten
                // Etc.
            }
        }

        private void ClientServerWorker_DoctorDisconnectReceived(EventArgs args)
        {
            // Bepaalde systemen uitzetten: bike, vr etc.

            this.Invoke((MethodInvoker)delegate
            {
                AppendMessage("Systeem: De doctor heeft de verbinding gesloten");
                clientServerWorker.Stop();
            });
        }

        private void ClientServerWorker_MessageReceived(MessageArgs args)
        {
            this.Invoke((MethodInvoker)delegate
            {
                AppendMessage($"Doctor: {args.Message}");
            });
        }

        private void ClientServerWorker_ResistanceReceived(ResistanceArgs args)
        {
            byte resistance;
            if (byte.TryParse(args.Resistance, out resistance))
            {
                // Resistance veranderen
            }
        }

        private void ClientServerWorker_BroadcastReceived(BroadcastArgs args)
        {
            this.Invoke((MethodInvoker)delegate
            {
                AppendMessage($"Broadcast: {args.Message}");
            });
        }

        private void ClientServerWorker_StopReceived(EventArgs args)
        {
            // VR Stop uitvoeren
            this.Invoke((MethodInvoker)delegate
            {
                Tuple<string, StopData> stopVRResponse = jsonPacketBuilder.BuildStopPacket();
                AppendMessage("Systeem: De doctor heeft de VR gestopt");
            });
        }

        private void AppendMessage(string message)
        {
            tbMessageHistory.AppendText($"[{DateTime.Now.ToShortTimeString()}] {message}\n");
            tbMessageHistory.AppendText(Environment.NewLine);
        }

        private void TxtSendMessage_Enter(object sender, EventArgs e)
        {
            if (txtSendMessage.Text == "Stuur bericht ...")
            {
                txtSendMessage.Text = "";
                txtSendMessage.ForeColor = Color.Black;
            }
        }

        private void TxtSendMessage_Leave(object sender, EventArgs e)
        {
            if (txtSendMessage.Text == "")
            {
                txtSendMessage.Text = "Stuur bericht ...";
                txtSendMessage.ForeColor = Color.Silver;
            }
        }

        private void TxtSendMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSendMessage.Text))
                {
                    AppendMessage($"You: {txtSendMessage.Text}");
                    this.serverConnection.SendWithNoResponse($"Client/Message\r\n{txtSendMessage.Text.Trim()}");
                    txtSendMessage.Text = "";
                }
            }
        }

        private void drawSpeedOnChart(int time, int speed)
        {
            chart1.Series["BikeSpeed"].Points.AddXY(time, speed);
        }

        private void drawHeartRateOnChart(int time, int heartRate)
        {
            chart1.Series["HeartRate"].Points.AddXY(time, heartRate); 
        }
        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void SelectBike_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateData();
            totalSeconds++;
            phaseTime--;
            this.phaseTimeMin = phaseTime / 60;
            this.phaseTimeSec = phaseTime & 60;
            this.seconds = totalSeconds % 60;
            this.minutes = totalSeconds / 60;

           
            if (this.seconds < 10)
            {
                timePassed.Text = "0" + this.minutes + ":0" + this.seconds;
            }
            else
            {
                timePassed.Text = "0" + this.minutes + ":" + this.seconds;
            }

            drawSpeedOnChart(totalSeconds, Int32.Parse(bleBikeHandler.bikeData));
            drawHeartRateOnChart(totalSeconds, Int32.Parse(bleHeartHandler.heartData)); 
        }

        private async void Start_Click(object sender, EventArgs e)
        {
            if (selectBike.SelectedIndex != null)
            {
                await this.bleHeartHandler.InitBleHeart();
                bleHeartHandler.Connect("Decathlon Dual HR", "Heartrate");
                bleBikeHandler.Connect(selectBike.SelectedItem.ToString(), "6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
                await bleHeartHandler.DataAsync();
                await bleBikeHandler.DataAsync();
                bleBikeHandler.ChangeResistance(0);
                Initialize_Time();

            }
            else
            {
                timePassed.Text = "Select bike first"; 
            }
            
        }

        private async void UpdateData()
        {
            await bleBikeHandler.DataAsync();
            await bleHeartHandler.DataAsync();
        }

        private void Initialize_Time()
        {
            if (timePassed.Text == "00:00") { phase++; }
            time.Interval = 300;
            time.Tick += new EventHandler(Timer1_Tick);
            time.Start();
        }

       
    }
}
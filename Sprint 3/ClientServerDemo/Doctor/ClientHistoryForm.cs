using Doctor.Connection;
using Doctor.Conversion;
using Doctor.PacketHandling;
using Doctor.Utils;
using Doctor.Utils.DataHolders;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace Doctor
{
    public partial class ClientHistoryForm : Form
    {
        private string patientId;
        private bool started;

        private int chrtSpeedIndexCounter = 1;
        private int chrtBpmIndexCounter = 1;

        private int travelledDistance;
        private byte travelledDistanceRawPrev;
        private byte travelledDistanceStartingValue;

        private ServerConnection serverConnection;
        private PacketHandler packetHandler;

        private PageConversion pageConversion;
        private ClientData currClientData;

        public ClientHistoryForm(string patientId)
        {
            InitializeComponent();
            SetupNewConnection();

            this.started = true;
            this.patientId = patientId;
            this.currClientData = null;
        }

        public async void SetupNewConnection()
        {
            ToggleControls(false);

            this.serverConnection = new ServerConnection();
            this.packetHandler = new PacketHandler();

            bool connected = await this.serverConnection.Connect("127.0.0.1", 25545);
            if (connected)
                ToggleControls(true);
        }

        private void ToggleControls(bool value)
        {
            for (int i = 0; i < this.Controls.Count; i++)
                this.Controls[i].Enabled = value;
        }

        private async void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string rawResponse = await this.serverConnection.SendWithResponse($"Doctor/GetClientHistory\r\n{this.patientId}\r\n{dateTimePicker1.Value.ToString()}");
            Tuple<string[], PacketType> response = packetHandler.HandlePacket(rawResponse);

            if (response.Item1.Length == 3)
            {
                string rawJson = response.Item1[2];
                currClientData = JsonConvert.DeserializeObject<ClientData>(rawJson);

                int travelledDistance = 0;
                for (int i = 0; i < currClientData.bikeData.Count; i++)
                {
                    byte[] internalBikeData = currClientData.bikeData[i];

                    if (internalBikeData.Length > 0)
                    {
                        pageConversion = new PageConversion();
                        pageConversion.Page10Received += (args) =>
                        {
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

                            travelledDistance = travelledDistance - travelledDistanceStartingValue;
                        };
                        pageConversion.Page19Received += (args) =>
                        {
                            int lsb = internalBikeData[4];
                            int msb = internalBikeData[5];
                            int work1 = lsb + (msb << 8);

                            int currSpeed = (int)Math.Round((double)(work1 / 1000), 0);
                            DrawSpeedOnChart(chrtSpeedIndexCounter, currSpeed);
                        };

                        pageConversion.RegisterData(internalBikeData.SubArray(4, internalBikeData.Length - 4));
                        chrtSpeedIndexCounter++;
                    }
                    else
                        DrawSpeedOnChart(chrtSpeedIndexCounter, 0);
                }

                for (int i = 0; i < currClientData.heartRateData.Count; i++)
                {
                    byte[] internalHeartData = currClientData.bikeData[i];

                    if (internalHeartData.Length > 0)
                    {
                        DrawHeartRateOnChart(chrtBpmIndexCounter, internalHeartData[1]);
                    }
                    else
                        DrawHeartRateOnChart(chrtBpmIndexCounter, 0);

                    chrtBpmIndexCounter++;
                }
            }
        }

        private void DrawSpeedOnChart(int time, int speed)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    chrtData.Series["BikeSpeed"].Points.AddXY(time, speed);
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
                    chrtData.Series["BPM"].Points.AddXY(time, heartRate);
                });
            }
            catch (InvalidOperationException) { }
        }
    }
}
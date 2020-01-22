using Client.Json_Structure;
using ClientGUI.Bluetooth;
using ClientGUI.Connection;
using ClientGUI.Conversion;
using ClientGUI.Sub_Objects;
using ClientGUI.Utils;
using ClientGUI.Utils.DataHolders;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class ClientScreen : Form
    {
        private string name;
        private string id;

        private ServerConnection serverConnection;
        private ClientServerWorker clientServerWorker;

        private ServerConnectionVR serverConnectionVR;
        private JsonPacketBuilder jsonPacketBuilder;
        private VRData runningVrData;

        private PageConversion pageConversion;
        private BleBikeHandler bleBikeHandler;
        private BleHeartHandler bleHeartHandler;

        private Timer syncTimer;

        private int chrtSpeedIndexCounter = 1;
        private DateTime previousTimeBike;
        private int chrtBpmIndexCounter = 1;
        private DateTime previousTimeBpm;

        private int travelledDistance;
        private byte travelledDistanceRawPrev;
        private byte travelledDistanceStartingValue;
        private bool started = true;

        private int phase = 0;

        private int seconds = 0;
        private int minutes = 0;
        private int phaseTime = 120;
        private int phaseTimeMin;
        private int phaseTimeSec;
        private int totalSeconds = 0;

        private int currResistance = 0;
        private int currDistance = 0;
        private int currSpeed = 0;
        private byte[] currHeartData = new byte[0];
        private byte[] currBikeData = new byte[0];

        private int currBpm = 0;

        public ClientScreen(string name, string id, ServerConnectionVR serverConnectionVR, ServerConnection serverConnection, string currentSessionId, BleHeartHandler bleHeartHandler, BleBikeHandler bleBikeHandler)
        {
            InitializeComponent();

            this.name = name;
            this.id = id;

            this.serverConnectionVR = serverConnectionVR;
            this.jsonPacketBuilder = new JsonPacketBuilder();
            this.serverConnection = serverConnection;

            this.pageConversion = new PageConversion();
            this.bleHeartHandler = bleHeartHandler;
            this.bleBikeHandler = bleBikeHandler;

            this.runningVrData = new VRData();
            this.runningVrData.currentSessionId = currentSessionId;

            InitializeDefaultEvents();
            ToggleControls(false);
            StartWorker();
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

        private async void ClientServerWorker_StatusReceived(StatusArgs args)
        {
            if (args.Status == "ready")
            {
                AppendMessage("Systeem: Doctor verbonden");

                this.Invoke(new MethodInvoker(delegate {
                    ToggleControls(true);
                }));

                StartVR();

                if (this.bleHeartHandler != null && this.bleBikeHandler != null)
                {
                    bool heartInitComplete = await bleHeartHandler.InitBleHeart();
                    bool bikeInitComplete = await bleBikeHandler.InitBleBike();

                    if (heartInitComplete && bikeInitComplete)
                    {
                        this.Invoke(new MethodInvoker(delegate {
                            syncTimer = new Timer();
                            syncTimer.Interval = 1000;
                            syncTimer.Tick += SyncTimer_Tick;
                            syncTimer.Start();
                        }));

                        bleBikeHandler.ChangeResistance(5);

                        previousTimeBpm = DateTime.Now;
                        this.bleHeartHandler.SubscriptionValueChanged += BleHeartHandler_SubscriptionValueChanged;

                        previousTimeBike = DateTime.Now;
                        this.bleBikeHandler.SubscriptionValueChanged += BleBikeHandler_SubscriptionValueChanged;

                        AppendMessage("Systeem: Verbinden met de hartslag monitor...");
                        int heartErrorCode = await bleHeartHandler.Connect("Decathlon Dual HR", "Heartrate");

                        AppendMessage($"Systeem: Verbinden met de fiets {this.bleBikeHandler.deviceName}...");
                        int bikeErrorCode = await bleBikeHandler.Connect("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");

                        if (heartErrorCode == 1)
                            AppendMessage("Systeem: Verbinding met de hartslag monitor niet mogelijk: Error code 1");
                        else
                            AppendMessage("Systeem: Verbonden met de hartslag monitor");

                        if (bikeErrorCode == 1)
                            AppendMessage("Systeem: Verbinding met de fiets niet mogelijk: Error code 1");
                        else
                        {
                            AppendMessage($"Systeem: Verbonden met de fiets {this.bleBikeHandler.deviceName}");
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Text = $"{this.Text} - {this.bleBikeHandler.deviceName}";
                            }));
                        }
                    }
                    else if (!heartInitComplete)
                        AppendMessage("Systeem: Verbinding met de hartslag monitor niet mogelijk: Error code 1");
                    else if (!bikeInitComplete)
                        AppendMessage("Systeem: Verbinding met de fiets niet mogelijk: Error code 1");
                }
                else
                    AppendMessage("Systeem: Verbinding met de fiets en de hartslag monitor niet mogelijk: Initialisation failure");
            }
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            if (!bleHeartHandler.IsConnected && !bleBikeHandler.IsConnected)
                return;

            byte[] finalBikeData = new byte[0];
            byte[] finalHeartData = new byte[0];

            if (this.currHeartData.Length > 0)
                finalHeartData = this.currHeartData;

            if (this.currBikeData.Length > 0)
                finalBikeData = this.currBikeData;

            DrawSpeedOnChart(chrtSpeedIndexCounter, this.currSpeed);
            AddAllPanels(this.runningVrData.BikePanelId, (int)Math.Round(this.currSpeed / 3.6, 0), this.currBpm, this.currDistance);
            chrtSpeedIndexCounter++;

            DrawHeartRateOnChart(chrtBpmIndexCounter, this.currBpm);
            //AddAllPanels(this.runningVrData.BikePanelId, (int)Math.Round(this.currSpeed / 3.6, 0), this.currBpm, this.currDistance);
            chrtBpmIndexCounter++;

            // Pakket verzenden: Client/SyncData\r\nBIKE_BYTES\r\nHEART_BYTES
            this.serverConnection.SendWithNoResponse($"Client/SyncData\r\n{this.id}\r\n{finalBikeData.ToRepString()}\r\n{finalHeartData.ToRepString()}");
        }

        private void BleHeartHandler_SubscriptionValueChanged(Avans.TI.BLE.BLESubscriptionValueChangedEventArgs args)
        {
            if (args.Data.Length == 6)
            {
                this.currHeartData = args.Data;
                this.currBpm = args.Data[1];
            }
        }

        private void BleBikeHandler_SubscriptionValueChanged(Avans.TI.BLE.BLESubscriptionValueChangedEventArgs args)
        {
            pageConversion = new PageConversion();
            pageConversion.Page10Received += (e) =>
            {
                if (started)
                {
                    travelledDistanceStartingValue = args.Data[7];
                    started = false;
                }

                int t = args.Data[7] - travelledDistanceRawPrev;
                if (t < 0)
                {
                    t += 256;
                }
                travelledDistance += t;
                travelledDistanceRawPrev = (byte)travelledDistance;
                this.currDistance = travelledDistance - travelledDistanceStartingValue;
            };
            pageConversion.Page19Received += (e) =>
            {
                int lsb = e.Data[4];
                int msb = e.Data[5];
                int work1 = lsb + (msb << 8);
                

                pageConversion.RegisterData(args.Data.SubArray(4, args.Data.Length - 4));
                AddAllPanels(this.runningVrData.BikePanelId, (int)Math.Round(this.currSpeed / 3.6, 0), this.currBpm, this.currDistance);

                this.currSpeed = (int)Math.Round((double)(work1 / 1000), 0);
            };
            this.currBikeData = args.Data;
            pageConversion.RegisterData(args.Data.SubArray(4, args.Data.Length - 4));
        }

        private void ClientServerWorker_DoctorDisconnectReceived(EventArgs args)
        {
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
                if (resistance > 0)
                {
                    this.currResistance = resistance;
                    bleBikeHandler.ChangeResistance(resistance);

                    AppendMessage($"Systeem: Fiets weerstand veranderd naar {resistance} watt");
                }
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
            this.Invoke((MethodInvoker)delegate
            {
                Tuple<string, JObject> stopResponse = SendToTunnel(jsonPacketBuilder.BuildStopPacket().Item1);
                AppendMessage("Systeem: De doctor heeft de VR simulatie gestopt");
            });
        }

        private void AppendMessage(string message)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tbMessageHistory.AppendText($"[{DateTime.Now.ToShortTimeString()}] {message}\n");
                tbMessageHistory.AppendText(Environment.NewLine);
            });
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

        private void DrawSpeedOnChart(int time, int speed)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    chart1.Series["BikeSpeed"].Points.AddXY(time, speed);
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
                    chart1.Series["BPM"].Points.AddXY(time, heartRate);
                });
            }
            catch (InvalidOperationException) { }
        }

        private Tuple<string, JObject> SendToTunnel(string packet)
        {
            return serverConnectionVR.TransferSendableResponse(jsonPacketBuilder.BuildSendTunnelPacket(this.runningVrData.currentTunnelId, packet).Item1);
        }

        private void StartVR()
        {
            if (serverConnectionVR.IsConnected)
            {
                AppendMessage("Systeem: De VR omgeving wordt opgezet");

                OpenTunnel();
                ResetVRScene();

                LoadTerrainAndDeleteGroundPlane(256, 256);
                AddObjectSurroundings();
                AddPanel("BikePanel");
                AddAndShowAndFollowRoute();

                if (runningVrData.BikePanelId != null)
                {
                    string addPanelPacket = jsonPacketBuilder.BuildSendTunnelPacket(this.runningVrData.currentTunnelId, jsonPacketBuilder.BuildPanelPacket(runningVrData.BikePanelId, "distance", 0, 0, 10).Item1).Item1;
                    serverConnectionVR.TransferSendableResponse(addPanelPacket);

                    AddAllPanels(runningVrData.BikePanelId, 0, 0, 0);

                    Tuple<string, JObject> cameraNode = SendToTunnel(jsonPacketBuilder.BuildFindNodePacket("Camera").Item1);
                    string cameraId = (cameraNode.Item2.SelectToken("data.data.data") as JArray)[0].SelectToken("uuid").ToString();
                    SendToTunnel(jsonPacketBuilder.BuildUpdateNodePacket(runningVrData.BikePanelId, cameraId, 0.5, 0.3, 1.2, -0.5, -40, 0, 0).Item1);
                }
            }
            else
                AppendMessage("Systeem: De applicatie is niet verbonden met de VR-server");
        }

        private void OpenTunnel()
        {
            Tuple<string, JObject> openTunnelResponse = serverConnectionVR.TransferSendableResponse(jsonPacketBuilder.BuildTunnelPacket(this.runningVrData.currentSessionId, "banaantje").Item1);
            this.runningVrData.currentTunnelId = openTunnelResponse.Item2.SelectToken("data.id").ToString();
        }

        private string AddPanel(string name)
        {
            string panelAddJson = jsonPacketBuilder.BuildPanelAddPacket(name, new int[] { 1, 1 }, new int[] { 250, 250 }, new int[] { 1, 1, 1, 1 }).Item1;
            string sendPanelAddJson = jsonPacketBuilder.BuildSendTunnelPacket(this.runningVrData.currentTunnelId, panelAddJson).Item1;
            Tuple<string, JObject> panelAddResponse = serverConnectionVR.TransferSendableResponse(sendPanelAddJson);

            runningVrData.BikePanelId = panelAddResponse.Item2.SelectToken("data.data.data.uuid").ToString();
            return panelAddResponse.Item2.SelectToken("data.data.data.uuid").ToString();
        }

        private void ClearPanel(string panelId)
        {
            string clearPanelPacket = jsonPacketBuilder.BuildSendTunnelPacket(this.runningVrData.currentTunnelId, @"{""id"":""scene/panel/clear"",""data"":{""id"":""" + panelId + @"""}}").Item1;
            serverConnectionVR.TransferSendableResponse(clearPanelPacket);
        }
     
        private void AddAndShowAndFollowRoute()
        {
            string[] segments = new Uri(Directory.GetCurrentDirectory()).Segments;
            string folderPath = segments.SubArray(0, segments.Length - 2).ToFullString() + @"Bike\Mountain_Bike\OBJ";
            string objectPath = folderPath.Replace("%20", " ").Replace("/", @"\").Remove(0, 1) + @"\Mountain_Bike.obj";

            RouteNode[] routeArray = new RouteNode[] {
                new RouteNode(new int[]{ -120, 0, -120 },new int[] { -80, 0, -80}),
                new RouteNode(new int[]{-50, 0, -80}, new int[] {0, 0, -50}),
                new RouteNode(new int[]{ 0, 0, 0 }, new int[]{ 50, 0, 0 }),
                new RouteNode(new int[]{ 80, 0, 50 }, new int[]{ 100, 0, 50 })
            };

            Tuple<string, JObject> addroute = SendToTunnel(jsonPacketBuilder.BuildRouteAddPacket(routeArray).Item1);
            string routeId = addroute.Item2.SelectToken("data.data.data.uuid").ToString();
            SendToTunnel(jsonPacketBuilder.BuildRouteShowPacket(true).Item1);
            Tuple<string, JObject> addBike = SendToTunnel(jsonPacketBuilder.BuildModelLoadPacket("bike", objectPath, 0, 0, 0, 0.1, true, false, "animationname").Item1);
            SendToTunnel(jsonPacketBuilder.BuildRouteFollowPacket(addroute.Item2.SelectToken("data.data.data.uuid").ToString(), addBike.Item2.SelectToken("data.data.data.uuid").ToString(), 2.0f, 1f, "XYZ", 1.0f).Item1);
            RoadAdd(routeId);

            Tuple<string, JObject> cameraNode = SendToTunnel(jsonPacketBuilder.BuildFindNodePacket("Camera").Item1);
            string cameraId = (cameraNode.Item2.SelectToken("data.data.data") as JArray)[0].SelectToken("uuid").ToString();
            Tuple<string, JObject> updateCamera = SendToTunnel(jsonPacketBuilder.BuildUpdateNodePacket(cameraId, addBike.Item2.SelectToken("data.data.data.uuid").ToString(), 7, 0, 0, 0, 0, 0, 0).Item1);
        }

        private void LoadTerrainAndDeleteGroundPlane(int width, int lenght)
        {
            int sizeHeightMap = width * lenght;
            float[] heightmap = new float[sizeHeightMap];
            int i = 0;
            int writePlace = 0;
            int timesTheHeightChanges = width / 20;

            for (int k = 0; k < 8; k++)
            {
                for (int j = 0; j < (lenght * (width / 8)); j++)
                {
                    heightmap[writePlace] = i;
                    writePlace++;
                }
                i++;
            }

            SendToTunnel(jsonPacketBuilder.BuildTerrainPacket(width, lenght, heightmap).Item1);
            SendToTunnel(jsonPacketBuilder.BuildTerrainNodePacket("terrain", -128, 0, -128, 1, true).Item1);
        }

        private void ResetVRScene()
        {
            SendToTunnel(jsonPacketBuilder.BuildSceneResetPacket().Item1);
        }

        private void RoadAdd(string routeId)
        {
            SendToTunnel(jsonPacketBuilder.BuildRoadAddPacket(routeId).Item1);
        }

        private void AddObjectSurroundings()
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-100, 0);
                int z = random.Next(-120, -110);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 0, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-100, 0);
                int z = random.Next(-120, -110);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 0, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-90, -70);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree3.obj", x, 1, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-90, -70);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 1, z);
            }


            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-60, -50);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 2, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-60, -50);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 2, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -50);
                int z = random.Next(-30, -10);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree2.obj", x, 2, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -80);
                int z = random.Next(-60, -50);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 2, z);
            }


            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -80);
                int z = random.Next(-60, -50);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 3, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -50);
                int z = random.Next(-30, -10);
                AddObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 3, z);
            }
        }

        private void AddObject(string objectPath, int x, int y, int z)
        {
            SendToTunnel(jsonPacketBuilder.BuildModelLoadPacket("object", objectPath, x, y, z, 1, true, false, "animationname").Item1);
        }

        private void AddAllPanels(string id, int speed, int heartrate, int distance)
        {
            ClearPanel(id);
            AddPanel(id, $"{speed}m/s", 5, 25, 32);
            AddPanel(id, $"{heartrate}bpm", 95, 25, 32);
            AddPanel(id, $"{distance}m", 185, 25, 32);

            SwapPanel(id);
        }

        private void SwapPanel(string id)
        {
            SendToTunnel(jsonPacketBuilder.BuildSwapPanelPacket(id).Item1);
        }

        private void AddPanel(string id, string text, int x, int y, double size)
        {
            SendToTunnel(jsonPacketBuilder.BuildPanelPacket(id, text, x, y, size).Item1);
        }
    }
}
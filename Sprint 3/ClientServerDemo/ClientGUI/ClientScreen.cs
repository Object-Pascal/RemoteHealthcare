using Client.Json_Structure;
using ClientGUI.Bluetooth;
using ClientGUI.Connection;
using ClientGUI.Json_Structure.Serializables.Sub_Objects;
using ClientGUI.Sub_Objects;
using ClientGUI.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class ClientScreen : Form
    {
        private string currentSessionId;
        private ServerConnectionVR serverConnectionVR;
        private JsonPacketBuilder jsonPacketBuilder;

        private ServerConnection serverConnection;
        private ClientServerWorker clientServerWorker;
        private BleBikeHandler bleBikeHandler;
        private BleHeartHandler bleHeartHandler;

        private int phase = 0;

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
                Tuple<string, JObject> stopResponse = SendToTunnel(jsonPacketBuilder.BuildStopPacket().Item1);
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

            string panelAddJson = jsonPacketBuilder.BuildPanelAddPacket("Boeie", new int[] { 1, 1 }, new int[] { 250, 250 }, new int[] { 1, 1, 1, 1 }).Item1;
            string sendPanelAddJson = jsonPacketBuilder.BuildSendTunnelPacket(currentSessionId, panelAddJson).Item1;
            Tuple<string, JObject> panelAddResponse = serverConnection.TransferSendableResponse(sendPanelAddJson);

            string panelAddId = panelAddResponse.Item2.SelectToken("data.data.data.uuid").ToString();

            ClearPanel(panelAddId);
            addAllPanels(panelAddId, 20, Int32.Parse(bleBikeHandler.bikeData), Int32.Parse(bleHeartHandler.heartData), (5, 25), (95, 25), (185, 25)) ;

            drawSpeedOnChart(totalSeconds, Int32.Parse(bleBikeHandler.bikeData));
            drawHeartRateOnChart(totalSeconds, Int32.Parse(bleHeartHandler.heartData)); 
        }

        private async void Start_Click(object sender, EventArgs e)
        {
            if (selectBike.SelectedItem != null)
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

        private Tuple<string, JObject> SendToTunnel(string packet)
        {
            return serverConnectionVR.TransferSendableResponse(jsonPacketBuilder.BuildSendTunnelPacket(this.currentSessionId, packet).Item1);
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
            startVR(); 
            time.Start();
        }

        private void startVR()
        {
            string panelAddJson = jsonPacketBuilder.BuildPanelAddPacket("Boeie", new int[] { 1, 1 }, new int[] { 250, 250 }, new int[] { 1, 1, 1, 1 }).Item1;
            string sendPanelAddJson = jsonPacketBuilder.BuildSendTunnelPacket(currentSessionId, panelAddJson).Item1;
            Tuple<string, JObject> panelAddResponse = serverConnection.TransferSendableResponse(sendPanelAddJson);

            string panelAddId = panelAddResponse.Item2.SelectToken("data.data.data.uuid").ToString();

            string panelJson = jsonPacketBuilder.BuildPanelPacket(panelAddId, "distance", 0, 0, 10).Item1;
            string sendJson = jsonPacketBuilder.BuildSendTunnelPacket(currentSessionId, panelJson).Item1;

            Tuple<string, JObject> panelResponse = serverConnection.TransferSendableResponse(sendJson);

            addAllPanels(panelAddId, 50, 50, 50, (5, 25), (95, 25), (185, 25));

            //Zorgt dat het Panel in beeld blijft staan:
            Tuple<string, JObject> cameraNode = SendToTunnel(jsonPacketBuilder.BuildFindNodePacket("Camera").Item1);
            string cameraId = (cameraNode.Item2.SelectToken("data.data.data") as JArray)[0].SelectToken("uuid").ToString();
            Tuple<string, JObject> updateCamera = SendToTunnel(jsonPacketBuilder.BuildUpdateNodePacket(panelAddId, cameraId, 0.5, 0.3, 1.2, -0.5, -40, 0, 0).Item1);

            // ff wat simuleren
            // Thread.Sleep(3000);

            ClearPanel(panelAddId);
            addAllPanels(panelAddId, 20, 30, 40, (5, 25), (95, 25), (185, 25));
        }

        private void ClearPanel(string panelId)
        {
            string clearPanelJsonRaw = @"{""id"":""scene/panel/clear"",""data"":{""id"":""" + panelId + @"""}}";
            //Wat is desination? 
            string clearPanelPacket = jsonPacketBuilder.BuildSendTunnelPacket(currentSessionId, clearPanelJsonRaw).Item1;
            Tuple<string, JObject> panelClearResponse = serverConnection.TransferSendableResponse(clearPanelPacket);
            // De response boeit nog even niet zo zeer
        }

        private void AddRoute_Click(object sender, EventArgs e)
        {
            //resetVRScene();
            // addAndShowAndFollowRoute1("data/NetworkEngine/models/cars/white/car_white.obj");
            loadTerrainAndDeleteGroundPlane(256, 256);
            // addAndShowAndFollowRoute2("data/NetworkEngine/models/cars/white/car_white.obj");
            //string path = @"C:\Users\joelle\Desktop\meeeeee3\RemoteHealthcare\Sprint 3\ClientServerDemo\ClientGUI\Bike\Mountain_Bike\OBJ\Mountain_Bike.obj";
            //                C:\Users\joelle\Desktop\meeeeee3\RemoteHealthcare\Sprint 3\ClientServerDemo\ClientGUI\Bike\Mountain_Bike\OBJ\Mountain_Bike.obj

            string[] segments = new Uri(Directory.GetCurrentDirectory()).Segments;
            string folderPath = segments.SubArray(0, segments.Length - 2).ToFullString() + @"Bike\Mountain_Bike\OBJ";
            string path = folderPath.Replace("%20", " ").Replace("/", @"\").Remove(0, 1) + @"\Mountain_Bike.obj";
            addAndShowAndFollowRoute2(path);


            addObjectsInSurroundings();

        }

       

        private void addAndShowAndFollowRoute1(string objectPath)
        {
            RouteNode[] routeArray = new RouteNode[] {  new RouteNode(new int[]{ 0, 0, 0 },new int[] { 5, 0, -5 }) ,
                                                        new RouteNode(new int[]{ 50, 0, 0 },new int[] { 5, 0, 5 }) ,
                                                        new RouteNode(new int[]{ 50, 0, 50 },new int[] { -5, 0, 5 }) ,
                                                        new RouteNode(new int[]{ 0, 0, 50 },new int[] { -5, 0, -5 })};

            Tuple<string, JObject> addroute = SendToTunnel(jsonPacketBuilder.BuildRouteAddPacket(routeArray).Item1);
            Console.WriteLine(addroute);
            Tuple<string, JObject> showRoute = SendToTunnel(jsonPacketBuilder.BuildRouteShowPacket(true).Item1);
            Tuple<string, JObject> addBike = SendToTunnel(jsonPacketBuilder.BuildModelLoadPacket("bike", objectPath, 0, 0, 0, 1, true, false, "animationname").Item1);
            Tuple<string, JObject> followRoute = SendToTunnel(jsonPacketBuilder.BuildRouteFollowPacket(addroute.Item2.SelectToken("data.data.data.uuid").ToString(), addBike.Item2.SelectToken("data.data.data.uuid").ToString(), 1.0f, 0.0f, "XYZ", 0.0f).Item1);
        }
        //Deze methode voegt een route toe en laat hier een object over heen rijden

        private void loadTerrainAndDeleteGroundPlane(int width, int lenght)
        {
            int sizeHeightMap = width * lenght;
            float[] heightmap = new float[sizeHeightMap];
            int i = 0;
            int writePlace = 0;
            int timesTheHeightChanges = width / 20;

            //TODO: fix deze logica naar iets mooiers

            for (int k = 0; k < 8; k++)
            {
                for (int j = 0; j < (lenght * (width / 8)); j++)
                {
                    heightmap[writePlace] = i;
                    writePlace++;
                }
                i++;
            }






            Tuple<string, JObject> addTerrain = SendToTunnel(jsonPacketBuilder.BuildTerrainPacket(width, lenght, heightmap).Item1);
            Tuple<string, JObject> AddTerrainNode = SendToTunnel(jsonPacketBuilder.BuildTerrainNodePacket("terrain", -128, 0, -128, 1, true).Item1);
        }
        //Deze methode voegt een terrein toe met hoogte verschillen
        private void addAndShowAndFollowRoute2(string objectPath)
        {

            RouteNode[] routeArray = new RouteNode[] {  new RouteNode(new int[]{ -120, 0, -120 },new int[] { -80, 0, -80}),
                                                        new RouteNode(new int[]{-50, 0, -80}, new int[] {0, 0, -50}),
                                                        new RouteNode(new int[]{ 0, 0, 0 }, new int[]{ 50, 0, 0 }),
                                                        new RouteNode(new int[]{ 80, 0, 50 }, new int[]{ 100, 0, 50 }),
                                                       };

            /*Tuple<string, JObject> addroute = SendToTunnel(jsonPacketBuilder.BuildRouteAddPacket(routeArray).Item1);
            string routeId = addroute.Item2.SelectToken("data.data.data.uuid").ToString();
            Tuple<string, JObject> showRoute = SendToTunnel(jsonPacketBuilder.BuildRouteShowPacket(true).Item1);
            Tuple<string, JObject> addBike = SendToTunnel(jsonPacketBuilder.BuildModelLoadPacket("bike", objectPath, 0, 0, 0, 0.1, true, false, "animationname").Item1);
            Tuple<string, JObject> followRoute = SendToTunnel(jsonPacketBuilder.BuildRouteFollowPacket(addroute.Item2.SelectToken("data.data.data.uuid").ToString(), addBike.Item2.SelectToken("data.data.data.uuid").ToString(), 2.0f, 1f, "XYZ", 1.0f).Item1);
            roadAdd(routeId);*/

            // De camera volgt de auto door de volgende code:
            Tuple<string, JObject> cameraNode = SendToTunnel(jsonPacketBuilder.BuildFindNodePacket("Camera").Item1);

            string cameraId = (cameraNode.Item2.SelectToken("data.data.data") as JArray)[0].SelectToken("uuid").ToString();
            //Tuple<string, JObject> updateCamera = SendToTunnel(jsonPacketBuilder.BuildUpdateNodePacket(cameraId, addBike.Item2.SelectToken("data.data.data.uuid").ToString(), 7, 0, 0, 0, 0, 0, 0).Item1);
        }
        //Deze methode voegt een route toe en laat hier een object over heen rijden. Deze route loopt over de hoogtemap van loadTerrainAndDeleteGroundPlane
        private void resetVRScene()
        {
            Tuple<string, JObject> resetTerrain = SendToTunnel(jsonPacketBuilder.BuildSceneResetPacket().Item1);
        }
        //Deze methode reset de VR Scene
        private void roadAdd(string routeId)
        {
            Tuple<string, JObject> roadAdd = SendToTunnel(jsonPacketBuilder.BuildRoadAddPacket(routeId).Item1);
        }
        //Deze methode laat een weg over de route lopen

        private void addObjectsInSurroundings()
        {

            //for (int i = 10; i < 100; i = i +10)
            //{
            //    addObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", -120 + i, 0, -100);
            //    addObject("data/NetworkEngine/models/trees/fantasy/tree3.obj", -120 + i, 1, -80);
            //    addObject("data/NetworkEngine/models/trees/fantasy/tree2.obj", -120 + i, 2, -60);
            //    addObject("data/NetworkEngine/models/trees/fantasy/tree3.obj", -120 + i, 3, -40);
            //    addObject("data/NetworkEngine/models/trees/fantasy/tree2.obj", -120 + i, 4, -20);
            //}

            Random random = new Random();


            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-100, 0);
                int z = random.Next(-120, -110);
                addObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 0, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-100, 0);
                int z = random.Next(-120, -110);
                addObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 0, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-90, -70);
                addObject("data/NetworkEngine/models/trees/fantasy/tree3.obj", x, 1, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-90, -70);
                addObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 1, z);
            }


            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-60, -50);
                addObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 2, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 120);
                int z = random.Next(-60, -50);
                addObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 2, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -50);
                int z = random.Next(-30, -10);
                addObject("data/NetworkEngine/models/trees/fantasy/tree2.obj", x, 2, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -80);
                int z = random.Next(-60, -50);
                addObject("data/NetworkEngine/models/trees/fantasy/tree1.obj", x, 2, z);
            }


            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -80);
                int z = random.Next(-60, -50);
                addObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 3, z);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(-120, -50);
                int z = random.Next(-30, -10);
                addObject("data/NetworkEngine/models/trees/fantasy/tree4.obj", x, 3, z);
            }
        }

        private void addObject(string objectPath, int x, int y, int z)
        {
            Tuple<string, JObject> addObject = SendToTunnel(jsonPacketBuilder.BuildModelLoadPacket("object", objectPath, x, y, z, 1, true, false, "animationname").Item1);
        }

        private void addAllPanels(string id, int speed, int heartrate, int meters, (int, int) speed2, (int, int) heartrate2, (int, int) meters2)
        {
            addPanel(id, $"{speed}m/s", speed2.Item1, speed2.Item2, 32);
            addPanel(id, $"{heartrate}bpm", heartrate2.Item1, heartrate2.Item2, 32);
            addPanel(id, $"{meters}m", meters2.Item1, meters2.Item2, 32);

            swapPanel(id);
        }

        private void swapPanel(string id)
        {
            Tuple<string, JObject> swapPanel = SendToTunnel(jsonPacketBuilder.BuildSwapPanelPacket(id).Item1);
        }

        private void addPanel(string id, string text, int x, int y, double size)
        {
            Tuple<string, JObject> addPanel = SendToTunnel(jsonPacketBuilder.BuildPanelPacket(id, text, x, y, size).Item1);
        }
    }
}
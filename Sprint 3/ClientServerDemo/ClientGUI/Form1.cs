using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.RegularExpressions;
//using static System.Net.Mime.MediaTypeNames;
using ClientGUI.Conversion;
using ClientGUI.Utils;
using ClientGUI.Sim;
using ClientGUI.Connection;
using Newtonsoft.Json.Linq;
using Client.Json_Structure;
using ClientGUI.Bluetooth;
using ClientGUI.Sub_Objects;

namespace ClientGUI
{
    public partial class Form1 : Form
    {
        private JsonPacketBuilder jsonPacketBuilder;
        private ServerConnection serverConnection;
        private bool connected;

        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";

        private static PageConversion pageConversion;
        private int travelledDistance;
        private byte travelledDistanceRawPrev;
        private byte travelledDistanceStartingValue;
        private bool started;

        private BleBikeHandler bleBikeHandler;
        private BleHeartHandler bleHeartHandler;

        private List<string> bleBikeList;
        private List<string> bleHeartList;

        private Dictionary<string, string> users;

        public Form1()
        {
            InitializeComponent();
            users = new Dictionary<string, string>();
            jsonPacketBuilder = new JsonPacketBuilder();
            serverConnection = new ServerConnection();
            connected = false;
            Connect();

            Simulator sim = new Simulator(@"C:\Users\kjcox\Documents\School\Periode 2.1\Proftaak\Git\RemoteHealthcare\Sprint 1\FietsData_4sep.txt");
            sim.DataReceived += (args) =>
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        //lblSimPercentage.Text = $"{Math.Round(args.Percentage, 1)}%";
                    });
                }
                catch (ObjectDisposedException) { }


                byte[] receivedDataSubset = args.DataLine.SubArray(4, args.DataLine.Length - 2 - 4);
                pageConversion.RegisterData(receivedDataSubset);
            };

            sim.Ended += () =>
            {
                // SimEnded event is still invoked from the running Simulator code on an asynchronous Task
                this.Invoke((MethodInvoker)delegate
                {
                    //lstBikes.Enabled = true;
                    //lstHearts.Enabled = true;
                    //btnSimulator.Enabled = true;
                });
            };

            //TcpClient client = new TcpClient();
            //client.Connect("localhost", 80);

            //stream = client.GetStream();

            //stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

            // Console.WriteLine("login\r\nJoëlle\r\nJoëlle\r\n\r\n");

            //Console.WriteLine("Do you want to make account then type account do you wanna login then type login");

            //string choice = Console.ReadLine();

            /*if (choice.Equals("account")) {

                Console.WriteLine("choose a name");
                string name = Console.ReadLine();
                Console.WriteLine("choose a password");
                string wachtwoord = Console.ReadLine();

                Write("Account\r\n" + name + "\r\n" + wachtwoord);
            }else if (choice.Equals("login"))
            {
                Console.WriteLine("your name");
                string name = Console.ReadLine();
                Console.WriteLine("your password");
                string wachtwoord = Console.ReadLine();

                Write("login\r\n" + name + "\r\n" + wachtwoord);
            }
            else
            {
                Console.WriteLine("wrong answer");
                //Application.Restart();
                //Application.Exit(); 
            }*/
            //string name = "";
            //string wachtwoord = "";

            //Write("login\r\n" + name + "\r\n" + wachtwoord + "\r\n\r\n");
            ////Write("login\r\nJoëlle\r\nJoëlle\r\n\r\n");
            //writeVr("12");

            //while (true)
            //{

            //    string line = Console.ReadLine();
            //    Write($"broadcast\r\n{line}\r\n\r\n");
            //}
        }

        private async void Connect()
        {
            connected = await serverConnection.Connect("145.48.6.10", 6666);
        }

        private void RegisterBleBikeEvents()
        {
            pageConversion = new PageConversion();
            pageConversion.Page10Received += (args) =>
            {
                if (started)
                {
                    travelledDistanceStartingValue = args.Data[3];
                    started = false;
                }

                int t = args.Data[3] - travelledDistanceRawPrev;
                if (t < 0)
                {
                    t += 256;
                }
                travelledDistance += t;
                travelledDistanceRawPrev = (byte)travelledDistance;
                writeFiets(travelledDistance.ToString());

                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        //  lblDistance.Text = $"{travelledDistance - travelledDistanceStartingValue}m";
                    });
                }
                catch (ObjectDisposedException) { }

            };

            pageConversion.Page19Received += (args) =>
            {

            };

            pageConversion.Page50Received += (args) =>
            {

            };
        }

        private void Write(string v)
        {
            stream.Write(Encoding.ASCII.GetBytes(v), 0, v.Length);
            stream.Flush();

        }
        //dit is voor het schrijven van data. 

        private void writeVr(string v)
        {
            Write($"vr\r\n{v}\r\n\r\n");
        }
        //Kirsten hiermee kan je de vr data naar de server versturen hij leest hem daar nu nog alleen maar uit en print het uit

        private void writeFiets(string v)
        {
            Write($"fiets\r\n{v}\r\n\r\n");
        }
        //thijs hiermee kan je de fiets data naar de server versturen hij leest hem daar nu nog alleen maar uit en print het uit

        private void OnRead(IAsyncResult ar)
        {
            Console.WriteLine("got data");
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += Encoding.ASCII.GetString(buffer, 0, receivedBytes);

            while (totalBuffer.Contains("\r\n\r\n"))
            {

                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                Console.WriteLine(packet);
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);

                string[] data = Regex.Split(packet, "\r\n");
                handlePacket(data);
            }
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);


        }
        //hier kijkt hij wanneer er data binnenkomt

        private void handlePacket(string[] data)
        {
            switch (data[0])
            {
                case "login":
                    Console.WriteLine($"Je bent ingelogd: {data[1]}");
                    break;
                   
                case "message":
                    Console.WriteLine($"Bericht van {data[1]}: {data[2]}");
                    break;
                default:
                    Console.WriteLine("Onbekend pakketje");
                    break;
            }
        }
        // hier kijkt hij wat voor data er binnenkomt. 

        private void Button1_Click(object sender, EventArgs e)
        {
            Tuple<string, JObject> sessionResponse = serverConnection.TransferSendableResponse(jsonPacketBuilder.BuildSessionPacket().Item1);
            
            JArray data = sessionResponse.Item2.SelectToken("data") as JArray;
            foreach (JObject j in data)
            {
                if (!users.Keys.Contains(j.SelectToken("clientinfo.user").ToString())) 
                    users.Add(j.SelectToken("clientinfo.user").ToString(), j.SelectToken("id").ToString());
            }
            btnLoadSessions.Enabled = false;
        }

        string destination;
        private void Button1_Click_1(object sender, EventArgs e)
        {
            Tuple<string, JObject> openTunnelResponse = serverConnection.TransferSendableResponse(jsonPacketBuilder.BuildTunnelPacket(users["marle"], "banaantje").Item1);

            destination = openTunnelResponse.Item2.SelectToken("data.id").ToString();

            string panelAddJson = jsonPacketBuilder.BuildPanelAddPacket("Boeie", new int[] { 1, 1 }, new int[] { 100, 100 }, new int[] { 1, 1, 1, 1 }).Item1;

            string sendPanelAddJson = jsonPacketBuilder.BuildSendTunnelPacket(destination, panelAddJson).Item1;

            Tuple<string, JObject> panelAddResponse = serverConnection.TransferSendableResponse(sendPanelAddJson);

            string clearPanelJsonRaw = @"{""id"":""scene/panel/clear"",""data"":{""id"":""" + panelAddResponse.Item2.SelectToken("data.data.data.uuid") + @"""}}";
            string sendJson1 = jsonPacketBuilder.BuildSendTunnelPacket(destination, clearPanelJsonRaw).Item1;
            Tuple<string, JObject> panelClearResponse = serverConnection.TransferSendableResponse(sendJson1);

            string panelJson = jsonPacketBuilder.BuildPanelPacket(panelAddResponse.Item2.SelectToken("data.data.data.uuid").ToString(), "distance", 0, 0, 10).Item1;
            string sendJson = jsonPacketBuilder.BuildSendTunnelPacket(destination, panelJson).Item1;

            Tuple<string, JObject> panelResponse = serverConnection.TransferSendableResponse(sendJson);


        }

        private void AddRoute_Click(object sender, EventArgs e)
        {
            resetVRScene();
            // addAndShowAndFollowRoute1("data/NetworkEngine/models/cars/white/car_white.obj");
            loadTerrainAndDeleteGroundPlane(256,256);
            addAndShowAndFollowRoute2("data/NetworkEngine/models/cars/white/car_white.obj");
            roadAdd();



        }

        private Tuple<string, JObject> SendToTunnel(string packet)
        {
            Console.WriteLine(packet);
            return serverConnection.TransferSendableResponse(jsonPacketBuilder.BuildSendTunnelPacket(destination, packet).Item1);
        }

        private void addAndShowAndFollowRoute1(string objectPath)
        {
            RouteNode[] routeArray = new RouteNode[] {  new RouteNode(new int[]{ 0, 0, 0 },new int[] { 5, 0, -5 }) ,
                                                        new RouteNode(new int[]{ 50, 0, 0 },new int[] { 5, 0, 5 }) ,
                                                        new RouteNode(new int[]{ 50, 0, 50 },new int[] { -5, 0, 5 }) ,
                                                        new RouteNode(new int[]{ 0, 0, 50 },new int[] { -5, 0, -5 })};

            Tuple<string, JObject> addroute = SendToTunnel(jsonPacketBuilder.BuildRouteAddPacket(routeArray).Item1);
            Tuple<string, JObject> showRoute = SendToTunnel(jsonPacketBuilder.BuildRouteShowPacket(true).Item1);
            Tuple<string, JObject> addBike = SendToTunnel(jsonPacketBuilder.BuildModelLoadPacket("bike", objectPath, 0, 0, 0, 0.01, true, false, "animationname").Item1);
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

            Tuple<string, JObject> addroute = SendToTunnel(jsonPacketBuilder.BuildRouteAddPacket(routeArray).Item1);
            Tuple<string, JObject> showRoute = SendToTunnel(jsonPacketBuilder.BuildRouteShowPacket(true).Item1);
            Tuple<string, JObject> addBike = SendToTunnel(jsonPacketBuilder.BuildModelLoadPacket("bike", objectPath, 0, 0, 0, 0.01, true, false, "animationname").Item1);
            Tuple<string, JObject> followRoute = SendToTunnel(jsonPacketBuilder.BuildRouteFollowPacket(addroute.Item2.SelectToken("data.data.data.uuid").ToString(), addBike.Item2.SelectToken("data.data.data.uuid").ToString(), 1.0f, 1f, "XYZ",1.0f).Item1);

        }
        //Deze methode voegt een route toe en laat hier een object over heen rijden. Deze route loopt over de hoogtemap van loadTerrainAndDeleteGroundPlane
        private void resetVRScene()
        {
            Tuple<string, JObject> resetTerrain = SendToTunnel(jsonPacketBuilder.BuildSceneReset().Item1);
        }
        //Deze methode reset de VR Scene
        private void roadAdd()
        {
            Tuple<string, JObject> roadAdd = SendToTunnel(jsonPacketBuilder.BuildRoadAdd().Item1);
        }
        //Deze methode laat een weg over de route lopen
    }
}
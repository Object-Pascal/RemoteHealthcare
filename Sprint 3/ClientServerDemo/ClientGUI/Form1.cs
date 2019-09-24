using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.RegularExpressions;
//using static System.Net.Mime.MediaTypeNames;
using System.Windows;
using ClientGUI.Conversion;
using ClientGUI.Utils;
using ClientGUI.Sim;
using ClientGUI.Connection;
using Newtonsoft.Json.Linq;
using Client.Json_Structure;

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

        public Form1()
        {
            InitializeComponent();

            jsonPacketBuilder = new JsonPacketBuilder();
            serverConnection = new ServerConnection();
            connected = false;
            Connect();

            Simulator sim = new Simulator(@"C:\Users\thijz\Desktop\TI jaar 2\project 2.1\RemoteHealthcare\Sprint 1\FietsData_4sep.txt");
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

                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        //lblDistance.Text = $"{travelledDistance - travelledDistanceStartingValue}m";
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

        string id = "";
        private void Button1_Click(object sender, EventArgs e)
        {
            Tuple<string, JObject> sessionResponse = serverConnection.TransferSendableResponse(jsonPacketBuilder.BuildSessionPacket().Item1);
          
            JArray data = sessionResponse.Item2.SelectToken("data") as JArray;
            foreach (JObject j in data)
            {
                id = j.SelectToken("id").ToString();
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string panelJson = jsonPacketBuilder.BuildPanelPacket("speed", "distance", 100, 100, 100).Item1;

            Tuple<string, JObject> openTunnelResponse = serverConnection.TransferSendableResponse(jsonPacketBuilder.BuildTunnelPacket(id, "banaantje").Item1);

            string sendJson = jsonPacketBuilder.BuildSendTunnelPacket("DEST", panelJson).Item1;
            Tuple<string, JObject> panelResponse = serverConnection.TransferSendableResponse(sendJson);
        }
    }
}
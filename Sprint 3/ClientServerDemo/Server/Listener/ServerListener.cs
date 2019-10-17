using Server.IO;
using Server.IO.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Server.Listener
{
    class ServerListener
    {
        private TcpListener tcpListener;

        private PacketHandler packetHandler;

        List<TcpClient> connectedClients;
        private Dictionary<TcpClient, Thread> clientThreads;

        private X509Certificate2 serverCertificate;
        private Dictionary<TcpClient, SslStream> clientStreams;

        public ServerListener(string certificatePath, string ipv4, int port)
        {
            this.tcpListener = new TcpListener(new IPEndPoint(IPAddress.Parse(ipv4), port));

            this.packetHandler = new PacketHandler();

            this.connectedClients = new List<TcpClient>();
            this.clientThreads = new Dictionary<TcpClient, Thread>();

            this.clientStreams = new Dictionary<TcpClient, SslStream>();
            this.serverCertificate = new X509Certificate2(certificatePath, "banaantje");
        }

        private void SetupSslStreamForClient(TcpClient client)
        {
            Console.WriteLine($"\t> Creating SSL stream for client... {client.Client.RemoteEndPoint.ToString()}");

            SslStream sslStream = new SslStream(client.GetStream(), false);
            sslStream.AuthenticateAsServer(this.serverCertificate, false, true);

            clientStreams.Add(client, sslStream);

            Console.WriteLine($"\t> Created SSL stream for client {client.Client.RemoteEndPoint.ToString()}");
        }

        public void Start()
        {
            try
            {
                Console.WriteLine("Starting server...");
                tcpListener.Start();
                Console.WriteLine("Server running");
                Console.WriteLine();

                WaitForClients();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ocurred while starting server:\n" + ex.InnerException + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void Stop() => tcpListener.Stop();

        public async void WaitForClients()
        {
            TcpClient clientConnected = null;
            try
            {
                clientConnected = await tcpListener.AcceptTcpClientAsync();
                connectedClients.Add(clientConnected);

                Console.WriteLine($"\t> Client {clientConnected.Client.RemoteEndPoint.ToString()} connected");
                Console.WriteLine();

                SetupSslStreamForClient(clientConnected);
                Console.WriteLine();

                Thread clientThread = new Thread(async() =>
                {
                    bool running = true;
                    while (running)
                    {
                        TcpClient clientInThread = null;
                        try
                        {
                            clientInThread = clientConnected;
                            byte[] packetLengthBytes = ReadFromStream(clientInThread, 4);

                            if (packetLengthBytes.Length == 4)
                            {
                                int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

                                byte[] packetData = ReadFromStream(clientInThread, packetLength);
                                string responsepacket = Encoding.UTF8.GetString(packetData);

                                Tuple<string, PacketType> packetBundle = packetHandler.HandlePacket(responsepacket);

                                switch (packetBundle.Item2)
                                {
                                    case PacketType.ClientStatus:
                                        Console.WriteLine($"\t> Client Status packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.ClientLogin:
                                        Console.WriteLine($"\t> Client LogIn packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.ClientLogout:
                                        Console.WriteLine($"\t> Client LogOut packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        clientInThread.Close();
                                        connectedClients.Remove(clientInThread);

                                        Console.WriteLine($"\t\t> Client {clientInThread.Client.RemoteEndPoint.ToString()} disconnected");
                                        break;
                                    case PacketType.ClientVr:
                                        Console.WriteLine($"\t> Client VR packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.ClientBike:
                                        Console.WriteLine($"\t> Client Bike packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.ClientMessage:
                                        Console.WriteLine($"\t> Client Message packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;

                                    case PacketType.DoctorLogin:
                                        Console.WriteLine($"\t> Doctor Login packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        string testUsername = "Pascal Stoop";
                                        string testPassword = "123";

                                        string[] data = Regex.Split(packetBundle.Item1, "\r\n");

                                        if (data.Length == 2)
                                        {
                                            if (testUsername == data[0] && testPassword == data[1])
                                            {
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nok");
                                            }
                                            else
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        }
                                        SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        break;
                                    case PacketType.DoctorLogout:
                                        Console.WriteLine($"\t> Doctor Logout packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        clientInThread.Close();
                                        connectedClients.Remove(clientInThread);

                                        Console.WriteLine($"\t\t> Client {clientInThread.Client.RemoteEndPoint.ToString()} disconnected");

                                        break;
                                    case PacketType.DoctorDataGet:
                                        Console.WriteLine($"\t> Doctor DataGet packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        if (packetBundle.Item1 == "ALL_CLIENTS")
                                        {
                                            ClientCollection clientCollection = await ClientDataSaver.LoadClients();
                                            string packet = "Server/DataGet\r\n";

                                            for (int i = 0; i < clientCollection.clients.Count; i++)
                                            {
                                                packet += clientCollection.clients[i].Name + "//" + clientCollection.clients[i].Id;
                                            }

                                            SendWithNoResponse(clientInThread, packet);
                                        }

                                        break;
                                    case PacketType.DoctorDataSave:
                                        Console.WriteLine($"\t> Doctor DataSave packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        //TODO: Client data opslaan

                                        break;
                                    case PacketType.DoctorAddClientHistory:
                                        Console.WriteLine($"\t> Doctor AddClientHistory packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // Packet opstelling: "Doctor/AddClientHistory\r\n"[CLIENT_ID]\r\n[SHORT_DATE]\r\n[JSON_DATA]

                                        break;
                                    case PacketType.DoctorBroadcast:
                                        Console.WriteLine($"\t> Doctor Broadcast packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");
                                        connectedClients.ForEach(x =>
                                        {
                                            string responseFromClient = SendWithResponse(x, $"broadcast\r\n{packetBundle.Item1}", 3000);
                                            if (responseFromClient != null)
                                            {
                                                Tuple<string, PacketType> responseFromClientPacketBundle = packetHandler.HandlePacket(responseFromClient);
                                                if (responseFromClientPacketBundle.Item2 == PacketType.DoctorStatus)
                                                {
                                                    if (!packetHandler.IsStatusOk(responseFromClientPacketBundle.Item1))
                                                        Console.WriteLine("\t> Doctor did not respond with status ok!");
                                                }
                                            }
                                            else
                                                Console.WriteLine("\t> Doctor did not send a response");
                                        });
                                        break;
                                    case PacketType.UnknownPacket:
                                        Console.WriteLine($"\t> Unknown packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.EmptyPacket:
                                        Console.WriteLine($"\t> Empty packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");
                                        break;
                                    default:
                                        break;
                                }
                                Console.WriteLine("\t\t> Handled packet");
                            }
                            else
                                running = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\t> Error ocurred from connecting client: {clientConnected.Client.RemoteEndPoint.ToString()}\n" + ex.InnerException + "\n" + ex.Message + "\n" + ex.StackTrace);

                            if (clientInThread != null)
                                clientStreams.Remove(clientInThread);

                            running = false;
                        }
                    }
                });

                // Threads per client bijhouden voor latere cleanup
                clientThreads.Add(clientConnected, clientThread);
                clientThread.Start();

                WaitForClients();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ocurred during client wait!\n" + ex.InnerException + ex.Message + "\n" + ex.StackTrace);

                if (clientConnected != null)
                    clientStreams.Remove(clientConnected);
            }
        }

        public string SendWithResponse(TcpClient client, string packet, int readTimeout = Timeout.Infinite)
        {
            byte[] packetLengthBytes = BitConverter.GetBytes(packet.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(packet);

            WriteToStream(client, packetLengthBytes);
            WriteToStream(client, dataBytes);

            byte[] packetLengthData = ReadFromStream(client, 4, readTimeout);
            int packetLength = BitConverter.ToInt32(packetLengthData, 0);

            byte[] data = ReadFromStream(client, packetLength, readTimeout);
            string responseDataRaw = Encoding.UTF8.GetString(data);

            return responseDataRaw;
        }

        public void SendWithNoResponse(TcpClient client, string packet)
        {
            byte[] packetLengthBytes = BitConverter.GetBytes(packet.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(packet);

            WriteToStream(client, packetLengthBytes);
            WriteToStream(client, dataBytes);
        }

        public byte[] ReadFromStream(TcpClient client, int packetLength, int readTimeout = Timeout.Infinite)
        {
            try
            {
                SslStream sslStream = clientStreams[client];
                sslStream.ReadTimeout = readTimeout;

                byte[] receivedBuff = new byte[packetLength];
                int readPosition = 0;

                while (readPosition < packetLength)
                {
                    readPosition += sslStream.Read(receivedBuff, readPosition, packetLength - readPosition);
                }
                return receivedBuff;
            }
            catch (Exception ex)
            {
                if (!client.Client.Connected)
                {
                    if (clientStreams.ContainsKey(client))
                        clientStreams.Remove(client);

                    Console.WriteLine($"\t> Client disconnected {client.Client.RemoteEndPoint.ToString()}");
                }
                else
                {
                    Console.WriteLine($"\t> Error ocurred during read: {client.Client.RemoteEndPoint.ToString()}\n" + ex.InnerException + "\n" + ex.Message + "\n" + ex.StackTrace);
                }
                return new byte[] { };
            }
        }

        private async void WriteToStream(TcpClient client, byte[] value)
        {
            try
            {
                SslStream stream = clientStreams[client];

                await stream.WriteAsync(value, 0, value.Length);
                await stream.FlushAsync();
            }
            catch (Exception) { }
        }
    }
}
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
            this.serverCertificate = new X509Certificate2(certificatePath, "bruh");
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
                                    case PacketType.Status:
                                        Console.WriteLine($"\t> Status packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.Login:
                                        Console.WriteLine($"\t> LogIn packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.Logout:
                                        Console.WriteLine($"\t> LogOut packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        clientInThread.Close();
                                        connectedClients.Remove(clientInThread);

                                        Console.WriteLine($"\t\t> Client {clientInThread.Client.RemoteEndPoint.ToString()} disconnected");
                                        break;
                                    case PacketType.DataGet:
                                        Console.WriteLine($"\t> ClientDataGet packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        if (packetBundle.Item1 == "ALL_CLIENT_DATA")
                                        {
                                            ClientCollection clientCollection = await ClientDataSaver.LoadClients();
                                            string packet = "Server/DataGet\r\n";

                                            for (int i = 0; i < clientCollection.clients.Length; i++)
                                            {
                                                packet += clientCollection.clients[i].Name + "//" + clientCollection.clients[i].Id;
                                            }

                                            SendWithNoResponse(clientInThread, packet);
                                        }

                                        break;
                                    case PacketType.DataSave:
                                        Console.WriteLine($"\t> ClientDataSave packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        //TODO: Client data opslaan naar de client

                                        break;
                                    case PacketType.Broadcast:
                                        Console.WriteLine($"\t> Broadcast packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");
                                        connectedClients.ForEach(x =>
                                        {
                                            string responseFromClient = SendWithResponse(x, $"broadcast\r\n{packetBundle.Item1}", 3000);
                                            if (responseFromClient != null)
                                            {
                                                Tuple<string, PacketType> responseFromClientPacketBundle = packetHandler.HandlePacket(responseFromClient);
                                                if (responseFromClientPacketBundle.Item2 == PacketType.Status)
                                                {
                                                    if (!packetHandler.IsStatusOk(responseFromClientPacketBundle.Item1))
                                                        Console.WriteLine("\t> Client did not respond with status ok!");
                                                }
                                            }
                                            else
                                                Console.WriteLine("\t> Client did not send a response");
                                        });
                                        break;
                                    case PacketType.Vr:
                                        Console.WriteLine($"\t> VR packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.Bike:
                                        Console.WriteLine($"\t> Bike packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        break;
                                    case PacketType.Message:
                                        Console.WriteLine($"\t> Message packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

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
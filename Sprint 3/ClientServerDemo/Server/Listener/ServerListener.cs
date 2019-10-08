using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

        public string login { get; set;  }

        public string password { get; set; }

        public ServerListener(string certificate, string ipv4, int port)
        {
            this.tcpListener = new TcpListener(new IPEndPoint(IPAddress.Parse(ipv4), port));

            this.packetHandler = new PacketHandler();
            this.connectedClients = new List<TcpClient>();

            this.serverCertificate = X509Certificate2.CreateFromCertFile(certificate);
        }

        private void SetupSslStreamForClient(TcpClient client)
        {
            Console.WriteLine($"Creating SSL stream for client {client.Client.RemoteEndPoint.ToString()}");

            SslStream sslStream = new SslStream(client.GetStream(), false);
            sslStream.AuthenticateAsServer(this.serverCertificate, false, true);

            clientStreams.Add(client, sslStream);
        }

        public void Start()
        {
            try
            {
                Console.WriteLine("Starting server...");
                tcpListener.Start();
                Console.WriteLine("Starting server complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ocurred in server!\n" + ex.InnerException + ex.Message + "\n" + ex.StackTrace);
            }
        }
        public void Stop() => tcpListener.Stop();

        public async void WaitForClients()
        {
            try
            {
                TcpClient clientConnected = await tcpListener.AcceptTcpClientAsync();
                connectedClients.Add(clientConnected);

                SetupSslStreamForClient(clientConnected);

                Console.WriteLine($"Client {clientConnected.Client.RemoteEndPoint.ToString()} connected");

                Thread clientThread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            byte[] packetLengthBytes = ReadFromStream(clientConnected, 4);
                            int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

                            byte[] packetData = ReadFromStream(clientConnected, packetLength);
                            string responsepacket = Encoding.UTF8.GetString(packetData);

                            Tuple<string, PacketType> packetBundle = packetHandler.HandlePacket(responsepacket);

                            Console.WriteLine($"Handling {packetBundle.Item2.ToString()} packet from {clientConnected.Client.RemoteEndPoint.ToString()}");

                            switch (packetBundle.Item2)
                            {
                                case PacketType.Login:
                                    // Login functies hanteren

                                    Console.WriteLine($"Client {clientConnected.Client.RemoteEndPoint.ToString()} logged in");
                                    break;
                                case PacketType.Logout:
                                    // Logout functies hanteren

                                    Console.WriteLine($"Client {clientConnected.Client.RemoteEndPoint.ToString()} logged out");

                                    clientConnected.Close();
                                    connectedClients.Remove(clientConnected);

                                    Console.WriteLine($"Client {clientConnected.Client.RemoteEndPoint.ToString()} disconnected");
                                    break;
                                case PacketType.Broadcast:
                                    connectedClients.ForEach(x =>
                                    {
                                        string responseFromClient = Send(x, $"broadcast\r\n{packetBundle.Item1}", 3000);
                                        if (responseFromClient != null)
                                        {
                                            Tuple<string, PacketType> responseFromClientPacketBundle = packetHandler.HandlePacket(responseFromClient);
                                            if (responseFromClientPacketBundle.Item2 == PacketType.Status)
                                            {
                                                if (!packetHandler.IsStatusOk(responseFromClientPacketBundle.Item1))
                                                    throw new Exception("Client did not respond with status ok!");
                                            }
                                        }
                                        else
                                            throw new Exception("Client did not send a response!");
                                    });
                                    break;
                                case PacketType.Vr:
                                    break;
                                case PacketType.Bike:
                                    break;
                                case PacketType.Message:
                                    break;
                                case PacketType.UnknownPacket:
                                    Console.WriteLine($"Uknown packet received from client {clientConnected.Client.RemoteEndPoint.ToString()}");
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error ocurred in client! {clientConnected.Client.RemoteEndPoint.ToString()}\n" + ex.InnerException + "\n" + ex.Message + "\n" + ex.StackTrace);
                            break;
                        }
                    }
                });

                // Even extra de threads bijhouden voor later misschien een cleanup
                clientThreads.Add(clientConnected, clientThread);
                clientThread.Start();

                // Write status connect ok
                // Send(clientConnected, Encoding.UTF8.GetBytes("Connect\r\nsucces"));

                WaitForClients();
            }
            catch (Exception ex)
            {
                // WriteToStream(clientConnected, Encoding.UTF8.GetBytes("Connect\r\nfailed"));
                Console.WriteLine($"Error ocurred during client wait!\n" + ex.InnerException + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public string Send(TcpClient client, string value, int readTimeout = Timeout.Infinite)
        {
            byte[] packetLengthBytes = BitConverter.GetBytes(value.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(value);

            WriteToStream(client, packetLengthBytes);
            WriteToStream(client, dataBytes);

            byte[] packetLengthData = ReadFromStream(client, 4, readTimeout);
            int packetLength = BitConverter.ToInt32(packetLengthData, 0);

            byte[] data = ReadFromStream(client, packetLength, readTimeout);
            string responseDataRaw = Encoding.UTF8.GetString(data);

            return responseDataRaw;
        }

        public byte[] ReadFromStream(TcpClient client, int packetLength, int readTimeout = Timeout.Infinite)
        {
            try
            {
                SslStream stream = clientStreams[client];
                stream.ReadTimeout = readTimeout;

                byte[] receivedBuff = new byte[packetLength];
                int readPosition = 0;

                while (readPosition < packetLength)
                {
                    readPosition += stream.Read(receivedBuff, readPosition, packetLength - readPosition);
                }
                return receivedBuff;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ocurred during read! {client.Client.RemoteEndPoint.ToString()}\n" + ex.InnerException + "\n" + ex.Message + "\n" + ex.StackTrace);
                return null;
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
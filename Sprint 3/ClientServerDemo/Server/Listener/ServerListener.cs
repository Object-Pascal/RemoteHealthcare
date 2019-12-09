﻿using Server.IO;
using Server.IO.Data;
using System;
using System.Linq;
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

        private IODataHandler iODataHandler;
        private List<TcpClient> connectedClients;
        private Dictionary<TcpClient, Thread> clientThreads;

        private X509Certificate2 serverCertificate;
        private Dictionary<TcpClient, SslStream> clientStreams;

        private Dictionary<string, TcpClient> clientForClientId;
        private Dictionary<TcpClient, TcpClient> clientForDoctor;

        public ServerListener(string certificatePath, string ipv4, int port)
        {
            this.tcpListener = new TcpListener(new IPEndPoint(IPAddress.Parse(ipv4), port));

            this.packetHandler = new PacketHandler();

            this.iODataHandler = new IODataHandler();
            this.connectedClients = new List<TcpClient>();
            this.clientThreads = new Dictionary<TcpClient, Thread>();

            this.clientForClientId = new Dictionary<string, TcpClient>();
            this.clientForDoctor = new Dictionary<TcpClient, TcpClient>();

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

                                Tuple<string[], PacketType> packetBundle = packetHandler.HandlePacket(responsepacket);

                                switch (packetBundle.Item2)
                                {
                                    case PacketType.ClientStatus:
                                        Console.WriteLine($"\t> Client Status packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // Obsolete until further notice

                                        break;
                                    case PacketType.ClientLogin:
                                        Console.WriteLine($"\t> Client LogIn packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        //Client/Login\r\n123
                                        if (packetBundle.Item1.Length == 1)
                                        {
                                            string clientId = packetBundle.Item1[0];

                                            if (!clientForClientId.ContainsKey(clientId) && !clientForClientId.ContainsValue(clientInThread))
                                            {
                                                int id;
                                                if (int.TryParse(clientId, out id))
                                                {
                                                    ClientCollection clientCollection = IODataHandler.LoadClients();
                                                    if (clientCollection.clients.Any(x => x.Id == id))
                                                    {
                                                        clientForClientId.Add(clientId, clientInThread);
                                                        SendWithNoResponse(clientInThread, "Server/Status\r\nok");
                                                    }
                                                    else
                                                        SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                                }
                                                else
                                                    SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                            }
                                            else
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        }

                                        break;
                                    case PacketType.ClientLogout:
                                        Console.WriteLine($"\t> Client LogOut packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        clientInThread.Close();
                                        connectedClients.Remove(clientInThread);

                                        Console.WriteLine($"\t\t> Client {clientInThread.Client.RemoteEndPoint.ToString()} disconnected");
                                        break;
                                    case PacketType.ClientVr:
                                        Console.WriteLine($"\t> Client VR packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // Obsolete until further notice

                                        break;
                                    case PacketType.ClientBike:
                                        Console.WriteLine($"\t> Client Bike packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // TODO Realtime bike data tunneling to connected Doctor

                                        break;
                                    case PacketType.ClientMessage:
                                        Console.WriteLine($"\t> Client Message packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        //Client/Message\r\ntext
                                        if (clientForDoctor.ContainsKey(clientInThread))
                                        {
                                            string clientMessage = packetBundle.Item1[0];

                                            TcpClient doctorFromClient = clientForDoctor[clientInThread];
                                            SendWithNoResponse(doctorFromClient, $"Server/Message\r\n{clientMessage}");
                                        }
                                       
                                        break;
                                    case PacketType.DoctorLogin:
                                        Console.WriteLine($"\t> Doctor Login packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        string[] loginData = packetBundle.Item1;

                                        if (loginData.Length == 2)
                                        {
                                            Doctor doctor = await iODataHandler.GetDoctorAsync(loginData[0], loginData[1]);
                                            if (doctor != null)
                                            {
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nok");
                                            }
                                            else
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        }
                                        break;
                                    case PacketType.DoctorLogout:
                                        Console.WriteLine($"\t> Doctor Logout packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        connectedClients.Remove(clientInThread);
                                        clientInThread.Close();

                                        Console.WriteLine($"\t\t> Client {clientInThread.Client.RemoteEndPoint.ToString()} disconnected");
                                        running = false;

                                        break;
                                    case PacketType.DoctorDataGet:
                                        Console.WriteLine($"\t> Doctor DataGet packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        if (packetBundle.Item1[0] == "ALL_CLIENTS")
                                        {
                                            ClientCollection clientCollection = IODataHandler.LoadClients();
                                            string packet = "Server/DataGet\r\n";

                                            for (int i = 0; i < clientCollection.clients.Count; i++)
                                            {
                                                packet += clientCollection.clients[i].Name + "//" + clientCollection.clients[i].Id + "//" + clientCollection.clients[i].Birthdate + "//" + clientCollection.clients[i].Gender + "//";
                                            }

                                            SendWithNoResponse(clientInThread, packet);
                                        }
                                        else
                                            SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        break;
                                    case PacketType.DoctorDataSave:
                                        Console.WriteLine($"\t> Doctor DataSave packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // Obsolete until further notice

                                        break;
                                    case PacketType.DoctorAddNewClient:
                                        Console.WriteLine($"\t> Doctor AddNewClient packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // EG: Doctor/AddNewClient\r\nNAME
                                        string[] newClientData = packetBundle.Item1;

                                        if (newClientData.Length == 1)
                                        {
                                            int newId = await iODataHandler.ProvideNewClientId();                                 
                                            if (newId > -1)
                                            {
                                                Client newClient = new Client();
                                                newClient.Name = newClientData[0];
                                                newClient.Id = newId;

                                                bool saved = await iODataHandler.AddClientAsync(newClient);

                                                if (saved)
                                                    SendWithNoResponse(clientInThread, "Server/Status\r\nok");
                                                else
                                                    SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                            }
                                            else
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        }

                                        break;
                                    case PacketType.DoctorAddClientHistory:
                                        Console.WriteLine($"\t> Doctor AddClientHistory packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // EG: Doctor/AddClientHistory\r\nCLIENT_ID\r\nDATETIME\r\nHEARTRATE_BYTES\r\nBIKE_BYTES
                                        string[] saveData = packetBundle.Item1;

                                        if (saveData.Length == 4)
                                        {
                                            int idParseResult = 0;
                                            if (int.TryParse(saveData[0], out idParseResult))
                                            {
                                                Client client = await iODataHandler.GetClientAsync(idParseResult);
                                                if (client != null)
                                                {
                                                    ClientData newClientDataEntry = new ClientData();
                                                    newClientDataEntry.clientId = client.Id;

                                                    DateTime dateParseResult;
                                                    if (DateTime.TryParse(saveData[1], out dateParseResult))
                                                    {
                                                        newClientDataEntry.Date = dateParseResult;

                                                        string[] heartRateDataLines = Regex.Split(saveData[2], "//");
                                                        if (heartRateDataLines.Length > 0)
                                                        {
                                                            for (int i = 0; i < heartRateDataLines.Length; i++)
                                                            {
                                                                newClientDataEntry.heartRateData.Add(heartRateDataLines[i]);
                                                            }
                                                        }

                                                        string[] bikeDataLines = Regex.Split(saveData[3], "//");
                                                        if (bikeDataLines.Length > 0)
                                                        {
                                                            for (int i = 0; i < bikeDataLines.Length; i++)
                                                            {
                                                                newClientDataEntry.bikeData.Add(bikeDataLines[i]);
                                                            }
                                                        }

                                                        bool saved = await iODataHandler.AddClientDataAsync(newClientDataEntry);
                                                        if (saved)
                                                            SendWithNoResponse(clientInThread, "Server/Status\r\nok");
                                                        else
                                                            SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                                    }
                                                    else
                                                        SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                                }
                                                else
                                                    SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                            }
                                            else
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        }
                                        else
                                            SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");

                                        break;
                                    case PacketType.DoctorGetClientHistory:
                                        Console.WriteLine($"\t> Doctor AddClientHistory packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        // EG: Doctor/GetClientHistory\r\nCLIENT_ID\r\nDATE
                                        string[] getData = packetBundle.Item1;

                                        if (getData.Length == 2)
                                        {
                                            int idParseResult = 0;
                                            if (int.TryParse(getData[0], out idParseResult))
                                            {
                                                DateTime dateParseResult;
                                                if (DateTime.TryParse(getData[1], out dateParseResult))
                                                {
                                                    // Filter based on year, month and day (TODO possible time check implemented later)
                                                    ClientData clientData = await iODataHandler.GetClientDataAsync(idParseResult, dateParseResult);

                                                    string heartRateBytesRaw = "";
                                                    for (int i = 0; i < clientData.heartRateData.Count; i++)
                                                        heartRateBytesRaw += clientData.heartRateData[i] + "//";

                                                    string bikeBytesRaw = "";
                                                    for (int i = 0; i < clientData.bikeData.Count; i++)
                                                        bikeBytesRaw += clientData.bikeData[i] + "//";

                                                    // EG: Server/GetClientHistory\r\nDATETIME\r\nHEARTRATE_BYTES\r\nBIKE_BYTES
                                                    string packet = $"Server/GetClientHistory\r\n{clientData.Date.ToString()}\r\n{heartRateBytesRaw}\r\n{bikeBytesRaw}";
                                                    SendWithNoResponse(clientInThread, packet);
                                                }

                                            }
                                            else
                                                SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");
                                        }
                                        else
                                            SendWithNoResponse(clientInThread, "Server/Status\r\nnotok");

                                        break;
                                    case PacketType.DoctorBroadcast:
                                        Console.WriteLine($"\t> Doctor Broadcast packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        TcpClient[] clColl = clientForClientId.Values.ToArray();
                                        for (int i = 0; i < clColl.Length; i++)
                                        {
                                            SendWithNoResponse(clColl[i], $"broadcast\r\n{packetBundle.Item1}");
                                        }  
                                        break;
                                    case PacketType.DoctorMessage:
                                        Console.WriteLine($"\t> Doctor Message packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        //Doctor/Message\r\n123\r\ntext
                                        if (packetBundle.Item1.Length == 2)
                                        {
                                            string id = packetBundle.Item1[0];
                                            string doctorMessage = packetBundle.Item1[1];

                                            if (clientForClientId.ContainsKey(id))
                                            {
                                                TcpClient clientFromId = clientForClientId[id];
                                                SendWithNoResponse(clientFromId, $"Server/Message\r\n{doctorMessage}");
                                            }
                                        }
                                        break;
                                    case PacketType.DoctorConnectToClient:
                                        Console.WriteLine($"\t> Doctor Message packet received from {clientInThread.Client.RemoteEndPoint.ToString()}");

                                        //Doctor/ConnectToClient\r\n123
                                        if (packetBundle.Item1.Length == 1)
                                        {
                                            if (clientForClientId.ContainsKey(packetBundle.Item1[0]))
                                            {
                                                TcpClient selectedPatientClient = clientForClientId[packetBundle.Item1[0]];
                                                clientForDoctor.Add(selectedPatientClient, clientInThread);
                                            }
                                        }
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
                            try
                            {
                                Console.WriteLine($"\t> Error ocurred from connecting client: {clientConnected.Client.RemoteEndPoint.ToString()}\n" + ex.InnerException + "\n" + ex.Message + "\n" + ex.StackTrace);

                                if (clientInThread != null)
                                    clientStreams.Remove(clientInThread);

                                running = false;
                            }
                            catch (ObjectDisposedException)
                            {
                                // client cleared
                            }
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
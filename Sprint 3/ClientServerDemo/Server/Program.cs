using Server.IO;
using Server.IO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientServerDemo
{
    class Program
    {
        private static TcpListener listener;
        private static List<Server> clients = new List<Server>();
        public static List<Tuple<string, string>> accounts { get; set; }
        static void Main(string[] args)
        {
            // Testje om clients in te laden (dit moet later eigenlijk in de packethandling van de server worden toegepast ofcourse)
            TestLoadClients();
            Console.ReadKey();

            //Start();
        }

        private static async void TestLoadClients()
        {
            ClientDataSaver clientDataSaver = new ClientDataSaver();
            ClientCollection clientCollection = await clientDataSaver.LoadClients();

            // Voorbeeld print
            for (int i = 0; i < clientCollection.clients.Length; i++)
            {
                Console.WriteLine($"Name: {clientCollection.clients[i].Name} | Id: {clientCollection.clients[i].Id}");
            }
        }

        private static void Start()
        {
            Console.WriteLine("Starting server...");
            accounts = new List<Tuple<string, string>>();
            accounts.Add(Tuple.Create("josa", "123")); 

            listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();
            Console.WriteLine("Server started on port 80");

            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.ReadKey();
        }

        private static void OnConnect(IAsyncResult ar)
        {
            TcpClient newTcpClient = listener.EndAcceptTcpClient(ar);
            Server connectedClient = new Server(newTcpClient);

            Console.WriteLine("New client connected");
            clients.Add(new Server(newTcpClient));

            Console.WriteLine("clients connected");
            foreach(Server c in clients)
            {
                Console.WriteLine(c.login);
            }

            Wait(connectedClient);
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        public static void Wait(Server connectedClient)
        {
            byte[] packetLengthData = connectedClient.ReadFromStream(4);
            int packetLength = BitConverter.ToInt32(packetLengthData, 0);

            Console.WriteLine();
            Console.WriteLine("Received packet length: " + packetLength);

            byte[] data = connectedClient.ReadFromStream(packetLength);
            string responseDataRaw = Encoding.UTF8.GetString(data);

            Console.WriteLine();
            Console.WriteLine("Received packet: " + responseDataRaw);

            Wait(connectedClient);
        }

        public static void Broadcast(Server client, string v)
        {
            foreach(Server c in clients)
            {
                Console.WriteLine($"message\r\n{client.login}\r\n{v}");
                c.Send($"message\r\n{client.login}\r\n{v}");
            }    
        }
        //hier verstuurd hij de standaard messages die hebben wij niet echt nodig maar die stonden nog in johans code die kunnen wij uiteindelijk weghalen 
        //de verschillende clients en hun data moeten nog opgeslagen kunnen worden maar ik weet nog niet hoe ik dit het beste aan kan pakken. 
    }
}
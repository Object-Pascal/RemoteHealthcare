using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        TcpListener listener;
        private List<Client> clients = new List<Client>();
        public List<Tuple<string, string>> accounts { get; set; }  

        Program()
        {
            accounts = new List<Tuple<string, string>>();
            accounts.Add(Tuple.Create("josa", "123")); 
            //hier kan je meer accounts toevoegen 
            listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.ReadKey();
        }
        // hier start hij met uitlezen

        private void OnConnect(IAsyncResult ar)
        {
            var newTcpClient = listener.EndAcceptTcpClient(ar);
            Console.WriteLine("New client connected");
            clients.Add(new Client(newTcpClient, this));

            Console.WriteLine("clients connected");
            foreach(var c in clients)
            {
                Console.WriteLine(c.login);
            }
            

            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }
        //hier laat hij de verschillende clients connecten 

        internal void Broadcast(Client client, string v)
        {
            foreach(var c in clients)
            {
                Console.WriteLine($"message\r\n{client.login}\r\n{v}\r\n\r\n");
                c.Write($"message\r\n{client.login}\r\n{v}\r\n\r\n");
            }    
        }
        //hier verstuurd hij de standaard messages die hebben wij niet echt nodig maar die stonden nog in johans code die kunnen wij uiteindelijk weghalen 

        //de verschillende clients en hun data moeten nog opgeslagen kunnen worden maar ik weet nog niet hoe ik dit het beste aan kan pakken. 
    }
}

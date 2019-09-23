using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientServerDemo
{
    class Client
    {
        private TcpClient tcpClient;
        private Program program;
        private NetworkStream stream;

        private byte[] buffer = new byte[1024];

        string totalBuffer = "";

        public string login { get; set;  }

        public string password { get; set; }

        public Client(TcpClient tcpClient, Program program)
        {
            this.tcpClient = tcpClient;
            this.program = program;

            this.stream = tcpClient.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void OnRead(IAsyncResult ar)
        {
            Console.WriteLine("got data");
           
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += System.Text.Encoding.ASCII.GetString(buffer, 0, receivedBytes);

            while(totalBuffer.Contains("\r\n\r\n"))
            {
                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n")+4);

                string[] data = Regex.Split(packet, "\r\n");
                handlePacket(data);
            }

            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

        }
        // hiermee kijkt hij wanneer er data binnenkomt

        private void handlePacket(string[] data)
        {
            switch(data[0])
            {
                case "login":

                    foreach(Tuple<string, string> c in program.accounts)
                    {
                        if(c.Item1 == data[1] && c.Item2 == data[2])
                        {
                            this.password = c.Item2;
                            this.login = c.Item1;
                            Write("login\r\nok\r\n\r\n" + data[1]);
                            break; 
                        }
                    }
                    Write("login\r\nniet ok\r\n\r\n");

                    break; 
               
                case "broadcast":
                    program.Broadcast(this, data[1]);
                    break;
                case "vr":
                    Console.WriteLine(data[1]);
                    break;
                    // hier komt de vr data binnen
                case "fiets":
                    Console.WriteLine(data[1]);
                    break; 
                    //hier komt de fiets data binnen
                default:
                    Write("Dat snap ik niet\r\n\r\n");
                    Console.WriteLine("Unknown packet");
                    break;
            }
        }
        // hier bepaald die wat voor soort pakket er binnen komt

        public void Write(string v)
        {
            stream.Write(System.Text.Encoding.ASCII.GetBytes(v), 0, v.Length);
            stream.Flush();
        }
        //hiermee kan je data schrijven 
    }
}

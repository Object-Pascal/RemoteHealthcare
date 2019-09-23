using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;
using System.Windows; 

namespace Client
{
    class Program
    {
        private static NetworkStream stream;
        private static byte[] buffer = new byte[1024];
        static string totalBuffer = "";

        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 80);

            stream = client.GetStream();

            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

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
            Console.WriteLine("your name");
            string name = Console.ReadLine();
            Console.WriteLine("your password");
            string wachtwoord = Console.ReadLine();
            Console.ReadKey(); 

            Write("login\r\n" + name + "\r\n" + wachtwoord + "\r\n\r\n");



            //Write("login\r\nJoëlle\r\nJoëlle\r\n\r\n");
            writeVr("12"); 

            while (true)
            {

                string line = Console.ReadLine();
                Write($"broadcast\r\n{line}\r\n\r\n");
            }
        }
        // deze methode is vooral voor het inloggen

        private static void Write(string v)
        {
            stream.Write(System.Text.Encoding.ASCII.GetBytes(v), 0, v.Length);
            stream.Flush();

        }
        //dit is voor het schrijven van data. 

        private static void writeVr(string v)
        {
            Write($"vr\r\n{v}\r\n\r\n"); 
        }
        //Kirsten hiermee kan je de vr data naar de server versturen hij leest hem daar nu nog alleen maar uit en print het uit
        
        private static void writeFiets(string v)
        {
            Write($"fiets\r\n{v}\r\n\r\n");
        }
        //thijs hiermee kan je de fiets data naar de server versturen hij leest hem daar nu nog alleen maar uit en print het uit






        private static void OnRead(IAsyncResult ar)
        {
            Console.WriteLine("got data");
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += System.Text.Encoding.ASCII.GetString(buffer, 0, receivedBytes);

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

        private static void handlePacket(string[] data)
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
    }
}

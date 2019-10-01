using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientServerDemo
{
    class Server
    {
        // TODO: Ombouwen naar het asynchroon afhandelen van clients en doktersapplicaties (TcpListener en aparte threads)

        private TcpClient tcpClient;
        private NetworkStream stream;

        private byte[] buffer = new byte[1024];

        string totalBuffer = "";

        public string login { get; set;  }

        public string password { get; set; }

        public Server(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.stream = tcpClient.GetStream();
        }

        private void handlePacket(string[] data)
        {
            switch(data[0])
            {
                case "login":

                    foreach(Tuple<string, string> c in Program.accounts)
                    {
                        if(c.Item1 == data[1] && c.Item2 == data[2])
                        {
                            this.password = c.Item2;
                            this.login = c.Item1;

                            Send("login\r\nok");
                            break; 
                        }
                    }
                    Send("login\r\nniet ok");
                    break;         
                case "broadcast":
                    Program.Broadcast(this, data[1]);
                    break;
                case "vr":
                    Console.WriteLine(data[1]);
                    break;
                case "fiets":
                    Console.WriteLine(data[1]);
                    break; 
                default:
                    Send("Dat snap ik niet");
                    Console.WriteLine("Unknown packet");
                    break;
            }
        }

        public void Send(string value)
        {
            byte[] packetLengthBytes = BitConverter.GetBytes(value.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(value);

            WriteToStream(packetLengthBytes);
            WriteToStream(dataBytes);

            byte[] packetLengthData = ReadFromStream(4);
            int packetLength = BitConverter.ToInt32(packetLengthData, 0);

            byte[] data = ReadFromStream(packetLength);
            string responseDataRaw = Encoding.UTF8.GetString(data);
            
            if (responseDataRaw.Contains("\r\n"))
                handlePacket(Regex.Split(responseDataRaw, "\r\n"));
        }

        public byte[] ReadFromStream(int packetLength)
        {
            try
            {
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
                Console.WriteLine("Error!\n" + ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }

        private async void WriteToStream(byte[] value)
        {
            try
            {
                await stream.WriteAsync(value, 0, value.Length);
                await stream.FlushAsync();
            }
            catch (Exception) { }
        }

        //public async void ReadFromStream()
        //{
        //    try
        //    {
        //        Console.WriteLine("Data received");
        //        int receivedBytes = await stream.ReadAsync();
        //        totalBuffer += System.Text.Encoding.ASCII.GetString(buffer, 0, receivedBytes);

        //        while (totalBuffer.Contains("\r\n\r\n"))
        //        {
        //            string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
        //            totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);

        //            string[] data = Regex.Split(packet, "\r\n");
        //            handlePacket(data);
        //        }

        //        stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        //    }
        //    catch (Exception) { }
        //}
    }
}
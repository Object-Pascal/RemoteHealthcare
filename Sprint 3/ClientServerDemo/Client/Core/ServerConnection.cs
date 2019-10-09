using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.Connection
{
    class ServerConnection
    {
        private TcpClient client;

        public ServerConnection()
        {
            client = new TcpClient();
        }

        public async Task<bool> Connect(string ip, int port)
        {
            try
            {
                await client.ConnectAsync(ip, port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async void Send(string value)
        {
            byte[] packetLengthBytes = BitConverter.GetBytes(value.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(value);

            WriteToStream(packetLengthBytes);
            WriteToStream(dataBytes);

            byte[] packetLengthData = await ReadFromStream(4);
            int packetLength = BitConverter.ToInt32(packetLengthData, 0);

            byte[] data = await ReadFromStream(packetLength);
            string responseDataRaw = Encoding.UTF8.GetString(data);

            if (responseDataRaw.Contains("\r\n"))
                handlePacket(Regex.Split(responseDataRaw, "\r\n"));
        }

        private async Task<byte[]> ReadFromStream(int packetLength)
        {
            try
            {
                byte[] receivedBuff = new byte[packetLength];
                int readPosition = 0;

                while (readPosition < packetLength)
                {
                    readPosition += await client.GetStream().ReadAsync(receivedBuff, readPosition, packetLength - readPosition);
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
                await client.GetStream().WriteAsync(value, 0, value.Length);
                await client.GetStream().FlushAsync();
            }
            catch (Exception) { }
        }

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
    }
}
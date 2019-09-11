using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VrDemo.Connection
{
    class TunnelConnection
    {
        private TcpClient client;

        public TunnelConnection()
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

        public string OpenSession(string data)
        {
            byte[] prefix = new byte[] { 0x18, 0x00, 0x00, 0x00 };
            byte[] dataBytes = Encoding.UTF8.GetBytes(data.ToCharArray(), 0, data.Length);

            Send(prefix.Concat(dataBytes).ToArray());
            return ReceiveResponse();
        }

        public string CreateTunnel(string data)
        {
            byte[] prefix = new byte[] { 0x18, 0x00, 0x00, 0x00 };
            byte[] dataBytes = Encoding.UTF8.GetBytes(data.ToCharArray(), 0, data.Length);

            Send(prefix.Concat(dataBytes).ToArray());
            return ReceiveResponse();
        }

        private string ReceiveResponse()
        {
            try
            {
                byte[] recBuff = new Byte[256];
                int bytes = client.GetStream().Read(recBuff, 0, recBuff.Length);

                byte[] received = new byte[bytes];
                for (int i = 0; i < bytes; i++)
                    received[i] = recBuff[i];

                return Encoding.UTF8.GetString(received);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!\n" + ex.Message + "\n" + ex.StackTrace);
                return null;
            }

        }

        private void Send(byte[] message)
        {
            try
            {
                client.GetStream().Write(message, 0, message.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
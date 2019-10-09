using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Doctor

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

        /// <summary>
        /// Sends JSON data and returns the response from the server
        /// </summary>
        public Tuple<string, JObject> TransferSendableResponse(string sendableRaw)
        {
            byte[] prefix = BitConverter.GetBytes(sendableRaw.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(sendableRaw);

            Send(prefix); 
            Send(dataBytes);

            byte[] jsonPacketLengthData = ReceiveResponse(4);
            int jsonPacketLength = BitConverter.ToInt32(jsonPacketLengthData, 0);

            byte[] jsonData = ReceiveResponse(jsonPacketLength);
            string jsonRaw = Encoding.UTF8.GetString(jsonData);

            return new Tuple<string, JObject>(jsonRaw, JObject.Parse(jsonRaw));
        }

        /// <summary>
        /// Sends JSON data and doesn't wait for a response
        /// </summary>
        public void TransferSendableNoResponse(string sendableRaw)
        {
            byte[] prefix = BitConverter.GetBytes(sendableRaw.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(sendableRaw);

            Send(prefix);
            Send(dataBytes);
        }

        public Tuple<string, JObject> TransferToTunnel(string tunnelSendableRaw, string tunnelDataSendableRaw)
        {
            return TransferSendableResponse(tunnelSendableRaw);
        }

        private byte[] ReceiveResponse(int packetLength)
        {
            try
            {
                byte[] receivedBuff = new byte[packetLength];
                int readPosition = 0;

                while (readPosition < packetLength)
                {
                    readPosition += client.GetStream().Read(receivedBuff, readPosition, packetLength - readPosition);
                }

                return receivedBuff;
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
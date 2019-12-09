using Newtonsoft.Json.Linq;
using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI.Connection
{
    public class ServerConnection
    {
        private TcpClient client;
        private SslStream sslStream;

        public bool Connected
        {
            get => client.Connected;
        }

        public ServerConnection()
        {
            client = new TcpClient();
        }

        public async Task<bool> Connect(string ip, int port)
        {
            try
            {
                await client.ConnectAsync(ip, port);
                sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate));

                try
                {
                    sslStream.AuthenticateAsClient(ip);
                }
                catch (AuthenticationException)
                {
                    Console.WriteLine("Authentication failed - closing the connection.");
                    client.Close();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while starting the connection:\n" + ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while closing the connection:\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            return false;
        }

        public async Task<string> SendWithResponse(string packet)
        {
            byte[] length = BitConverter.GetBytes(packet.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(packet);

            Send(length);  
            Send(dataBytes);

            byte[] packetLengthData = await ReceiveResponse(4);
            int packetLength = BitConverter.ToInt32(packetLengthData, 0);

            byte[] responseData = await ReceiveResponse(packetLength);
            string response = Encoding.UTF8.GetString(responseData);

            return response;
        }

        public void SendWithNoResponse(string packet)
        {
            byte[] length = BitConverter.GetBytes(packet.Length);
            byte[] dataBytes = Encoding.UTF8.GetBytes(packet);

            Send(length);
            Send(dataBytes);
        }

        public async Task<string> WaitForResponse()
        {
            byte[] packetLengthData = await ReceiveResponse(4);
            int packetLength = BitConverter.ToInt32(packetLengthData, 0);

            byte[] responseData = await ReceiveResponse(packetLength);
            string response = Encoding.UTF8.GetString(responseData);

            return response;
        }

        private async Task<byte[]> ReceiveResponse(int packetLength)
        {
            try
            {
                byte[] receivedBuff = new byte[packetLength];
                int readPosition = 0;

                while (readPosition < packetLength)
                {
                    readPosition += await sslStream.ReadAsync(receivedBuff, readPosition, packetLength - readPosition);
                }

                return receivedBuff;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading from stream:\n" + ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }

        private async void Send(byte[] message)
        {
            try
            {
                await sslStream.WriteAsync(message, 0, message.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading writing to stream:\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
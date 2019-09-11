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
    }
}
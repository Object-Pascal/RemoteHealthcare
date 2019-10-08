using Server.IO;
using Server.IO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Server.Listener;

namespace Server
{
    class Program
    {
        private static ServerListener server;

        static void Main(string[] args)
        {
            server = new ServerListener("", "127.0.0.1", 8080);
            server.Start();
        }
    }
}
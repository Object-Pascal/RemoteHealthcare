using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VrDemo.Connection;
using VrDemo.IO;

namespace VrDemo
{
    class Program
    {
        private static TunnelConnection tunnelConnection;
        static void Main(string[] args)
        {
            tunnelConnection = new TunnelConnection();

            // 
            //Console.Write("Sendable name: ");
            //string input = Console.ReadLine();
            //string sessionData = LoadSession().Result;
            //Console.Write(sessionData);
            //Console.ReadKey();

            bool connected = Connect().Result;
            if (connected)
                Console.WriteLine("Connected");
            else
                Console.WriteLine("Unable to connect");

            Console.ReadKey();
        }

        private static async Task<string> LoadSession()
        {
            return (await JsonLoader.LoadSendable("Session")).Item1;
        }

        private static async Task<bool> Connect()
        {
            return await tunnelConnection.Connect("145.48.6.10", 6666);
        }
    }
}
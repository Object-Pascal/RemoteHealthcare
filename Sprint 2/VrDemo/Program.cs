using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VrDemo.Connection;
using VrDemo.Data;
using VrDemo.IO;
using VrDemo.Utils;

namespace VrDemo
{
    class Program
    {
        private static ServerConnection serverConnection;
        static void Main(string[] args)
        {
            serverConnection = new ServerConnection();

            // Packets voorladen in json files
            string sessionData = LoadSession().Result;

            // Connecten met de server enzo
            bool connected = Connect().Result;
            if (connected)
                Console.WriteLine("Connected");
            else
                Console.WriteLine("Unable to connect");

            Console.WriteLine();

            Tuple<string, JObject> sessionJson = serverConnection.TransferSendable(sessionData.ToCleanPacketString());

            JArray data = sessionJson.Item2.SelectToken("data") as JArray;
            List<Session> sessions = new List<Session>();
            foreach (JObject j in data)
            {
                string id = j.SelectToken("id").ToString();
                string host = j.SelectToken("clientinfo.host").ToString();
                string user = j.SelectToken("clientinfo.user").ToString();
                string file = j.SelectToken("clientinfo.file").ToString();
                string renderer = j.SelectToken("clientinfo.renderer").ToString();

                sessions.Add(new Session(id, host, user, file, renderer));
            }

            Console.WriteLine("Session used:");
            Console.WriteLine(sessions.Last().ToString());
            Console.WriteLine();

            Tunnel tunnel = null;
            try
            {
                string tunnelData = LoadTunnel().Result.Replace("[SESSION_ID]", sessions.Last().id);

                Tuple<string, JObject> tunnelJson = serverConnection.TransferSendable(tunnelData.ToCleanPacketString());
                tunnel = new Tunnel(tunnelJson.Item2.SelectToken("data.id").ToString(), tunnelJson.Item2.SelectToken("data.status").ToString());

                if (tunnel.status.ToLower() == "ok")
                {
                    Console.WriteLine($"Tunnel created: {tunnel.ToString()}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to create tunnel:\n{e.Message}\n{e.StackTrace}");
            }
            Console.ReadKey();
        }

        private static async Task<string> LoadSession()
        {
            return (await JsonLoader.LoadSendable("Session")).Item1;
        }

        private static async Task<string> LoadTunnel()
        {
            return (await JsonLoader.LoadSendable("Tunnel")).Item1;
        }

        private static async Task<bool> Connect()
        {
            return await serverConnection.Connect("145.48.6.10", 6666);
        }
    }
}
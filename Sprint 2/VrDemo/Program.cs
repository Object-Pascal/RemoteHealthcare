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
            string sessionData = LoadSendable("Session").Result;

            // Connecten met de server enzo
            bool connected = Connect().Result;
            if (connected)
                Console.WriteLine("Connected");
            else
                Console.WriteLine("Unable to connect");

            Console.WriteLine();

            Tuple<string, JObject> sessionJson = serverConnection.TransferSendableResponse(sessionData.ToCleanPacketString());

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

            string selectedUser = "passi";
            if (sessions.Any(x => x.user.ToLower().Contains(selectedUser)))
            {
                Session usedSession = sessions.Where(x => x.user.ToLower() == selectedUser).First();

                Console.WriteLine("Session used:");
                Console.WriteLine(usedSession.ToString());
                Console.WriteLine();

                Tunnel tunnel = null;
                try
                {
                    string tunnelData = LoadSendable("Tunnel").Result.Replace("[SESSION_ID]", usedSession.id).Replace("[SESSION_KEY]", "banaantje");

                    Tuple<string, JObject> tunnelJson = serverConnection.TransferSendableResponse(tunnelData.ToCleanPacketString());
                    tunnel = new Tunnel(tunnelJson.Item2.SelectToken("data.id").ToString(), tunnelJson.Item2.SelectToken("data.status").ToString());

                    if (tunnel.status.ToLower() == "ok")
                    {
                        Console.WriteLine($"Tunnel created: {tunnel.ToString()}");
                    }

                    // Als het goed is 0,0,0,0,0,0 : yaw, pitch, roll, x, y, z
                    string terrain = LoadSendable("Terrain").Result.Replace("[TERRAIN_HEIGHTS]", "[ 0, 0, 0, 0, 0, 0 ]");
                    string skyBoxTime = LoadSendable("SkyBoxTime").Result.Replace(@"""[SKYBOX_TIME]""", "0");
                    string skyBoxUpdate = LoadSendable("SkyBoxUpdate").Result;
                    string sendTunnel = LoadSendable("SendTunnel").Result.Replace("[TUNNEL_ID]", tunnel.id);

                    //serverConnection.TransferToTunnel(sendTunnel, terrain);
                    serverConnection.TransferToTunnel(sendTunnel, skyBoxTime);
                    //serverConnection.TransferToTunnel(sendTunnel, skyBoxUpdate);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unable to create tunnel:\n{e.Message}\n{e.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine($@"No session host for ""{selectedUser}"" found");
            }
            Console.ReadKey();
        }

        private static async Task<string> LoadSendable(string name)
        {
            return (await JsonLoader.LoadSendable(name)).Item1;
        }

        private static async Task<bool> Connect()
        {
            return await serverConnection.Connect("145.48.6.10", 6666);
        }
    }
}
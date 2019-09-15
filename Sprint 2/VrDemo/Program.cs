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

            Tuple<string, JObject> sessionJson = serverConnection.TransferSendableResponse(sessionData.MinifyJson());

            JArray data = sessionJson.Item2.SelectToken("data") as JArray;
            List<Session> sessions = new List<Session>();
            List<Route> routes = new List<Route>();
            foreach (JObject j in data)
            {
                string id = j.SelectToken("id").ToString();
                string host = j.SelectToken("clientinfo.host").ToString();
                string user = j.SelectToken("clientinfo.user").ToString();
                string file = j.SelectToken("clientinfo.file").ToString();
                string renderer = j.SelectToken("clientinfo.renderer").ToString();
                

                sessions.Add(new Session(id, host, user, file, renderer));
                Console.WriteLine(sessions.Last().ToString());
            }

            string selectedUser = "voyager";
            if (sessions.Any(x => x.host.ToLower().Contains(selectedUser)))
            {
                Session usedSession = sessions.Where(x => x.host.ToLower() == selectedUser).First();

                Console.WriteLine("Session used:");
                Console.WriteLine(usedSession.ToString());
                Console.WriteLine();

                Tunnel tunnel = null;
                try
                {
                    string tunnelData = LoadSendable("Tunnel").Result.Replace("[SESSION_ID]", usedSession.id).Replace("[SESSION_KEY]", "banaantje");

                    Tuple<string, JObject> tunnelJson = serverConnection.TransferSendableResponse(tunnelData.MinifyJson());
                    tunnel = new Tunnel(tunnelJson.Item2.SelectToken("data.id").ToString(), tunnelJson.Item2.SelectToken("data.status").ToString());

                    if (tunnel.status.ToLower() == "ok")
                    {
                        Console.WriteLine($"Tunnel created: {tunnel.ToString()}");
                    }

                    // Als je dit wilt aanpassen moet dit ook in Terrain.json
                    float[,] heights = new float[32, 32];
                    for (int x = 0; x < 32; x++)
                        for (int y = 0; y < 32; y++)
                            heights[x, y] = 2 + (float)(Math.Cos(x / 5.0) + Math.Cos(y / 5.0));

                    string heightsRaw = heights.Cast<float>().ToArray().ToSimpleString();

                    string sendTunnel = LoadSendable("SendTunnel").Result.Replace("[TUNNEL_DEST]", tunnel.id);

                    string terrain = LoadSendable("Terrain").Result.Replace(@"""[TERRAIN_HEIGHTS]""", heightsRaw);
                    string terrainNode = LoadSendable("TerrainNode").Result.Replace("[TERRAIN_NODE_NAME]", "floor");

                    string skyBoxTime = LoadSendable("SkyBoxTime").Result.Replace(@"""[SKYBOX_TIME]""", "0");
                    //string followRoute = LoadSendable("RouteFollow").Result.Replace("[ROUTE_ID]", ).Replace("[ROTATION]", "XZ");
                    //string skyBoxUpdate = LoadSendable("SkyBoxUpdate").Result;
                    //string deleteTerrain = LoadSendable("Update").Result.Replace("scene/terrain/update", "scene/terrain/delete");
                    //string updateTerrain = LoadSendable("Update").Result;

                    string treelaod = LoadSendable("Treeload").Result.Replace("[TREE-LOAD]", "tree"); 
                    

                    Tuple<string, JObject> resp1 = serverConnection.TransferToTunnel(sendTunnel, terrain);
                    Tuple<string, JObject> resp2 = serverConnection.TransferToTunnel(sendTunnel, terrainNode);
                    Tuple<string, JObject> resp3 = serverConnection.TransferToTunnel(sendTunnel, treelaod); 
                    Tuple<string, JObject> resp4 = serverConnection.TransferToTunnel(sendTunnel, skyBoxTime); 
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
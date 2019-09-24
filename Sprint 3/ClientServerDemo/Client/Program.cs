using Client.Json_Structure;
using Client.Json_Structure.Serializables;
using Client.Json_Structure.Serializables.Sub_Objects;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonPacketBuilder jsonPacketBuilder = new JsonPacketBuilder();

            Tuple<string, Treeload> treeloadPacketBundle = jsonPacketBuilder.BuildTreeloadPacket("testName", "data/NetworkEngine/models/cars/white/car_white.obj", -16, 0, -16, 1, true, false, "animationname");
            Tuple<string, Json_Structure.Serializables.Terrain> terrainNodePacketBundle = jsonPacketBuilder.BuildTerrainPacket(5, 5, new float[] { 1.1f, 1.2f, 1.3f });
            Tuple<string, SkyBoxUpdate> skyBoxUpdatePacketBundle = jsonPacketBuilder.BuildSkyBoxUpdatePacket("static");
            Tuple<string, SkyBoxTime> skyBoxTimePacketBundle = jsonPacketBuilder.BuildSkyboxTimePacket("12");
            Tuple<string, RouteShow> routeShowPacketBundle = jsonPacketBuilder.BuildRouteShowPacket(true);
            Tuple<string, RouteAdd> routeAddPacketBundle = jsonPacketBuilder.BuildRouteAddPacket(new RouteNode[]
            {
                new RouteNode() {
                    pos = new int[] { 0, 0, 0 },
                    dir = new int[] { 5, 0, -5 }
                },
                new RouteNode() {
                    pos = new int[] { 50, 0, 0 },
                    dir = new int[] { 5, 0, 5 }
                },
                new RouteNode() {
                    pos = new int[] { 50, 0, 50 },
                    dir = new int[] { -5, 0, 5 }
                },
                new RouteNode() {
                    pos = new int[] { 0, 0, 50 },
                    dir = new int[] { -5, 0, -5 }
                }
            });
            Tuple<string, RouteFollow> routFollowPacketBundle = jsonPacketBuilder.BuildRouteFollowPacket("test", "test", "1.0", 0.0f, "XYZ", 1.0f, true);
            Tuple<string, DeleteNode> deleteNodePacketBundle = jsonPacketBuilder.BuildDeleteNodePacket("test");
            Tuple<string> sendTunnelPacketBundle = jsonPacketBuilder.BuildSendTunnelPacket("test", deleteNodePacketBundle.Item1);        
        }
    }
}
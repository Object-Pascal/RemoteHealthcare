﻿using Client.Json_Structure.Serializables;
using Client.Json_Structure.Serializables.Sub_Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Client.Json_Structure
{
    public class JsonPacketBuilder
    {
        // TODO: SendTunnel

        public Tuple<string, Session> BuildSessionPacket()
        {
            Session obj = new Session()
            {
                id = "session/list"
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, Session>(json, obj);
        }

        public Tuple<string, Tunnel> BuildTunnelPacket(string sessionId, string sessionKey)
        {
            Tunnel obj = new Tunnel()
            {
                id = "tunnel/create",
                data = new Data1()
                {
                    session = sessionId,
                    key = sessionKey
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, Tunnel>(json, obj);
        }

        public Tuple<string> BuildSendTunnelPacket(string tunnelDest, string packet)
        {
            JObject dataObject = new JObject();
            dataObject.Add("dest", tunnelDest);
            dataObject.Add("data", JObject.Parse(packet));

            JObject finalObject = new JObject();
            finalObject.Add("id", "tunnel/send");
            finalObject.Add("data", dataObject);

            return new Tuple<string>(finalObject.ToString());
        }

        public Tuple<string, DeleteNode> BuildDeleteNodePacket(string nodeID)
        {
            DeleteNode obj = new DeleteNode()
            {
                id = "scene/node/delete",
                data = new Data10()
                {
                    id = nodeID
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, DeleteNode>(json, obj);
        }

        public Tuple<string, SkyBoxTime> BuildSkyboxTimePacket(string time)
        {
            SkyBoxTime obj = new SkyBoxTime()
            {
                id = "scene/skybox/settime",
                data = new Data5()
                {
                    time = time   
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, SkyBoxTime>(json, obj);
        }

        public Tuple<string, SkyBoxUpdate> BuildSkyBoxUpdatePacket(string type)
        {
            SkyBoxUpdate obj = new SkyBoxUpdate()
            {
                id = "scene/skybox/update",
                data = new Data6()
                {
                    type = type,
                    files = new Files()
                    {
                        xpos = "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_rt.png",
                        xneg = "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_lf.png",
                        ypos = "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_up.png",
                        yneg = "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_dn.png",
                        zpos = "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_bk.png",
                        zneg = "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_ft.png"
                    }
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, SkyBoxUpdate>(json, obj);
        }

        public Tuple<string, Update> BuildUpdatePacket()
        {
            Update obj = new Update()
            {
                id = "scene/terrain/update"
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, Update>(json, obj);
        }

        public Tuple<string, Serializables.Terrain> BuildTerrainPacket(int width, int height, float[] heightMap)
        {
            Serializables.Terrain obj = new Serializables.Terrain()
            {
                id = "scene/terrain/add",
                data = new Data4()
                {
                    size = new int[] { width, height },
                    heights = heightMap
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, Serializables.Terrain>(json, obj);
        }

        public Tuple<string, TerrainNode> BuildTerrainNodePacket(string name, int x, int y, int z, int scale, bool smoothnormals)
        {
            TerrainNode obj = new TerrainNode()
            {
                id = "scene/node/add",
                data = new Data3()
                {
                    name = name,
                    components = new Components2()
                    {
                        transform = new Transform()
                        {
                            position = new int[] { x, y, x },
                            scale = scale,
                            rotation = new int[] { 0, 0, 0, }
                        },
                        terrain = new Serializables.Sub_Objects.Terrain()
                        {
                            smoothnormals = smoothnormals
                        }
                    }
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, TerrainNode>(json, obj);
        }

        public Tuple<string, RouteAdd> BuildRouteAddPacket(RouteNode[] routes)
        {
            RouteAdd obj = new RouteAdd()
            {
                id = "route/add",
                data = new Data8()
                {
                    nodes = routes
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, RouteAdd>(json, obj);
        }

        public Tuple<string, RouteShow> BuildRouteShowPacket(bool show)
        {
            RouteShow obj = new RouteShow()
            {
                id = "route/show",
                data = new Data7()
                {
                    show = show
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, RouteShow>(json, obj);
        }

        public Tuple<string, RouteFollow> BuildRouteFollowPacket(string routeID, string nodeID, string speed, float offset, string rotate, float smoothing, bool followHeight)
        {
            RouteFollow obj = new RouteFollow()
            {
                id = "route/follow",
                data = new Data9()
                {
                    route = routeID,
                    node = nodeID,
                    speed = speed,
                    offset = offset,
                    rotate = rotate,
                    smoothing = smoothing,
                    followHeight = followHeight,
                    rotateOffset = new int[] { 0, 0, 0 },
                    positionOffset = new int[] { 0, 0, 0 }
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, RouteFollow>(json, obj);
        }

        public Tuple<string, Treeload> BuildTreeloadPacket(string name, string file, int x, int y, int z, int scale, bool cullbackfaces, bool animated, string animation)
        {
            Treeload obj = new Treeload()
            {
                id = "scene/node/add",
                data = new Data2()
                {
                    name = name,
                    components = new Components1()
                    {
                        transform = new Transform()
                        {
                            position = new int[] { x, y, z },
                            scale = scale,
                            rotation = new int[] { 0, 0, 0 }
                        }
                    },
                    model = new Model()
                    {
                        file = file,
                        cullbackfaces = cullbackfaces,
                        animated = animated,
                        animation = animation
                    }
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, Treeload>(json, obj);
        }
    }
}
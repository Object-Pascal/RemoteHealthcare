using ClientGUI.Json_Structure.Serializables;
using ClientGUI.Json_Structure.Serializables.Sub_Objects;
using ClientGUI.Sub_Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Client.Json_Structure
{
    public class JsonPacketBuilder
    {
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

        public Tuple<string, Panel> BuildPanelPacket(string id, string text, int x, int y, double size)
        {
            Panel obj = new Panel()
            {
                id = "scene/panel/drawtext",
                data = new Data11()
                {
                    id = id,
                    text = text,
                    position = new int[] { x, y },
                    size = size
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, Panel>(json, obj);
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

        public Tuple<string, ClientGUI.Json_Structure.Serializables.Terrain> BuildTerrainPacket(int width, int height, float[] heightMap)
        {
            ClientGUI.Json_Structure.Serializables.Terrain obj = new ClientGUI.Json_Structure.Serializables.Terrain()
            {
                id = "scene/terrain/add",
                data = new Data4()
                {
                    size = new int[] { width, height },
                    heights = heightMap
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, ClientGUI.Json_Structure.Serializables.Terrain>(json, obj);
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
                        terrain = new ClientGUI.Sub_Objects.Terrain()
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

        public Tuple<string, RouteFollow> BuildRouteFollowPacket(string routeID, string nodeID, float speed, float offset, string rotate, float smoothing)
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
                    followHeight = true,
                    rotateOffset = new int[] { 0, 0, 0 },
                    positionOffset = new int[] { 0, 0, 0 }
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, RouteFollow>(json, obj);
        }

        public Tuple<string, ModelLoad> BuildModelLoadPacket(string name, string file, int x, int y, int z, double scale, bool cullbackfaces, bool animated, string animation)
        {
            ModelLoad obj = new ModelLoad()
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
                        },
                        model = new Model()
                        {
                            file = file,
                            cullbackfaces = cullbackfaces,
                            animated = animated,
                            animation = animation
                        }
                    }

                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, ModelLoad>(json, obj);
        }

        public Tuple<string, PanelAdd> BuildPanelAddPacket(string name,  int[] size, int[] resolution, int[] background)
        {
            PanelAdd obj = new PanelAdd()
            {
                id = "scene/node/add",
                data = new Data12()
                {
                    name = name,
                    components = new Components3()
                    {
                        panel = new Panel1()
                        {
                            size = size,
                            resolution = resolution,
                            background = background,
                            castShadow = false
                        }
                    }                  
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, PanelAdd>(json, obj);
        }

        public Tuple<string, SceneReset> BuildSceneResetPacket()
        {
            SceneReset obj = new SceneReset()
            {
                id = "scene/reset",
                data = new Data10()
                {
                }
            };
            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, SceneReset>(json, obj);

        }

        public Tuple<string, RoadAdd> BuildRoadAddPacket(string routeId)
        {
            RoadAdd obj = new RoadAdd()
            {
                id = "scene/road/add",
                data = new DataRoad()
                {
                    route = routeId,
                    diffuse = "data/NetworkEngine/textures/tarmac_diffuse.png",
                    normal = "data/NetworkEngine/textures/tarmac_normale.png",
                    specular = "data/NetworkEngine/textures/tarmac_specular.png",
                    heightoffset = 0.01f
                    //hier wordt de weg aangemaakt
                }

            };

            

            string json = JsonConvert.SerializeObject(obj);
            return new Tuple<string, RoadAdd>(json, obj);
        }

      
    }
}
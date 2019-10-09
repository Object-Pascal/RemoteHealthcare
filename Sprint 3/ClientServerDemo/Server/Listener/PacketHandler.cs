using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Listener
{
    public class PacketHandler
    {
        public Tuple<string, PacketType> HandlePacket(string packet)
        {
            string[] lines = Regex.Split(packet, "\r\n");
            if (lines.Length > 1)
            {
                switch (lines[0])
                {
                    case "Client/Status":
                        return new Tuple<string, PacketType>(lines[1], PacketType.Status);
                    case "Client/DataGet":
                        return new Tuple<string, PacketType>(lines[1], PacketType.DataGet);
                    case "Client/DataSave":
                        return new Tuple<string, PacketType>(lines[1], PacketType.DataSave);
                    case "Client/LogIn":
                        return new Tuple<string, PacketType>(lines[1], PacketType.Login);
                    case "Client/LogOut":
                        return new Tuple<string, PacketType>(lines[1], PacketType.Logout);
                    case "Client/Broadcast":
                        return new Tuple<string, PacketType>(lines[1], PacketType.Broadcast);
                    case "Client/VR":
                        return new Tuple<string, PacketType>(lines[1], PacketType.Vr);
                    case "Client/Bike":
                        return new Tuple<string, PacketType>(lines[1], PacketType.Bike);
                    case "Client/message":
                        return new Tuple<string, PacketType>(lines[1], PacketType.Message);
                    default:
                        return new Tuple<string, PacketType>(lines[1], PacketType.UnknownPacket);
                }
            }
            else
                return new Tuple<string, PacketType>(string.Empty, PacketType.EmptyPacket);
        }

        public bool IsStatusOk(string packet)
        {
            string[] lines = Regex.Split(packet, "\r\n");

            if (lines.Length > 0)
            {
                if (lines[1] == "ok")
                    return true;
            }
            return false;
        }
    }
}
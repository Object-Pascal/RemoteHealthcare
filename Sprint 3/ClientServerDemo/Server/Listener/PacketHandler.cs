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
            switch (lines[0])
            {
                case "status":
                    return new Tuple<string, PacketType>(lines[0], PacketType.Status);
                case "clientdataget":
                    return new Tuple<string, PacketType>(lines[0], PacketType.ClientDataGet);
                case "clientdatasave":
                    return new Tuple<string, PacketType>(lines[0], PacketType.ClientDataSave);
                case "login":
                    return new Tuple<string, PacketType>(lines[0], PacketType.Login);
                case "logout":
                    return new Tuple<string, PacketType>(lines[0], PacketType.Logout);
                case "broadcast":
                    return new Tuple<string, PacketType>(lines[0], PacketType.Broadcast);
                case "vr":
                    return new Tuple<string, PacketType>(lines[0], PacketType.Vr);
                case "bike":
                    return new Tuple<string, PacketType>(lines[0], PacketType.Bike);
                case "message":
                    return new Tuple<string, PacketType>(lines[0], PacketType.Message);
                default:
                    // De packet kan ook leeg zijn maar either way is het een unkown packet
                    return new Tuple<string, PacketType>(lines[0], PacketType.UnknownPacket);
            }
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
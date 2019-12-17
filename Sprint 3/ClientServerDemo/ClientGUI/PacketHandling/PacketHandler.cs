using System;
using System.Text.RegularExpressions;

namespace ClientGUI.PacketHandling
{
    public class PacketHandler
    {
        public Tuple<string[], PacketType> HandlePacket(string packet)
        {
            string[] lines = Regex.Split(packet, "\r\n");
            if (lines.Length > 1)
            {
                switch (lines[0])
                {
                    case "Server/Status":
                        return new Tuple<string[], PacketType>(lines, PacketType.Status);
                    case "Server/DoctorDisconnect":
                        return new Tuple<string[], PacketType>(lines, PacketType.DoctorDisconnect);
                    case "Server/Resistance":
                        return new Tuple<string[], PacketType>(lines, PacketType.Resistance);
                    case "Server/Broadcast":
                        return new Tuple<string[], PacketType>(lines, PacketType.Broadcast);
                    case "Server/Message":
                        return new Tuple<string[], PacketType>(lines, PacketType.Message);
                    case "Server/Stop":
                        return new Tuple<string[], PacketType>(lines, PacketType.StopVR);
                    default:
                        return new Tuple<string[], PacketType>(lines, PacketType.UnknownPacket);
                }
            }
            else
                return new Tuple<string[], PacketType>(new string[] { }, PacketType.EmptyPacket);
        }

        public bool IsStatusOk(Tuple<string[], PacketType> packet)
        {
            if (packet.Item2 == PacketType.Status)
            {
                if (packet.Item1.Length == 2)
                {
                    if (packet.Item1[1] == "ok")
                        return true;
                }
            }
            return false;
        }
    }
}
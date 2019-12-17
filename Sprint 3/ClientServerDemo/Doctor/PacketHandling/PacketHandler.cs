using System;
using System.Text.RegularExpressions;

namespace Doctor.PacketHandling
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
                    case "Server/ClientDisconnect":
                        return new Tuple<string[], PacketType>(lines, PacketType.ClientDisconnect);
                    case "Server/Broadcast":
                        return new Tuple<string[], PacketType>(lines, PacketType.Broadcast);
                    case "Server/Message":
                        return new Tuple<string[], PacketType>(lines, PacketType.Message);
                    case "Server/DataGet":
                        return new Tuple<string[], PacketType>(lines, PacketType.DataGet);
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
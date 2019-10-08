using System;
using System.Text.RegularExpressions;

namespace Doctor.PacketHandling
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
                    case "Server/DataGet":
                        return new Tuple<string, PacketType>(lines[1], PacketType.DataGet);
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
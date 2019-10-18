using Server.Utils;
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
        public Tuple<string[], PacketType> HandlePacket(string packet)
        {
            string[] lines = Regex.Split(packet, "\r\n");

            if (lines.Length > 1)
            {
                switch (lines[0])
                {
                    case "Client/Status":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientStatus);
                    case "Client/LogIn":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientLogin);
                    case "Client/LogOut":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientLogout);
                    case "Client/VR":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientVr);
                    case "Client/Bike":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientBike);
                    case "Client/Message":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientMessage);

                    case "Doctor/Status":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorStatus);
                    case "Doctor/LogIn":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorLogin);
                    case "Doctor/LogOut":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorLogout);
                    case "Doctor/DataGet":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorDataGet);
                    case "Doctor/DataSave":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorDataSave);
                    case "Doctor/AddNewClient":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorAddNewClient);
                    case "Doctor/AddClientHistory":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorAddClientHistory);
                    case "Doctor/GetClientHistory":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorGetClientHistory);
                    case "Doctor/Broadcast":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorLogin);
                    case "Doctor/Message":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorLogout);
                    default:
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.UnknownPacket);
                }
            }
            else
                return new Tuple<string[], PacketType>(new string[] { string.Empty }, PacketType.EmptyPacket);
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
﻿using Server.Utils;
using System;
using System.Text.RegularExpressions;

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
                    case "Client/Login":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientLogin);
                    case "Client/Logout":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientLogout);
                    case "Client/VR":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientVr);
                    case "Client/SyncData":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientSyncData);
                    case "Client/Close":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientClose);
                    case "Client/Message":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.ClientMessage);

                    case "Doctor/Status":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorStatus);
                    case "Doctor/LogIn":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorLogin);
                    case "Doctor/LogOut":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorLogout);
                    case "Doctor/StopVR":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorStopVR);
                    case "Doctor/Resistance":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorResistance);
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
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorBroadcast);
                    case "Doctor/Close":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorClose);
                    case "Doctor/Message":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorMessage);
                    case "Doctor/ConnectToClient":
                        return new Tuple<string[], PacketType>(lines.SubArray(1, lines.Length - 1), PacketType.DoctorConnectToClient);
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
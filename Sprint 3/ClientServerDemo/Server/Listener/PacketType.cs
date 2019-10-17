﻿namespace Server.Listener
{
    public enum PacketType
    {
        UnknownPacket,
        EmptyPacket,

        ClientStatus,
        ClientLogin,
        ClientLogout,
        ClientVr,
        ClientBike,
        ClientMessage,

        DoctorStatus,
        DoctorDataGet,
        DoctorDataSave,
        DoctorAddClientHistory,
        DoctorLogin,
        DoctorLogout,
        DoctorBroadcast,
        DoctorMessage
    }
}
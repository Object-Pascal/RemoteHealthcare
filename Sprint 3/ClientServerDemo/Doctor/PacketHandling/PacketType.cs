﻿namespace Doctor.PacketHandling
{
    public enum PacketType
    {
        Status,
        ClientDisconnect,
        Broadcast,
        Message,
        DataGet,
        UnknownPacket,
        EmptyPacket,
        SyncData,
        Heart
    }
}
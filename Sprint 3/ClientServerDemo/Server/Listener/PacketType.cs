namespace Server.Listener
{
    public enum PacketType
    {
        Status,
        DataGet,
        DataSave,
        Login,
        Logout,
        Broadcast,
        Vr,
        Bike,
        Message,
        UnknownPacket,
        EmptyPacket
    }
}
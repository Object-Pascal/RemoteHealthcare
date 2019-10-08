namespace Server.Listener
{
    public enum PacketType
    {
        Status,
        ClientDataGet,
        ClientDataSave,
        Login,
        Logout,
        Broadcast,
        Vr,
        Bike,
        Message,
        UnknownPacket
    }
}
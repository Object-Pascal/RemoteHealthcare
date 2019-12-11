namespace Doctor.PacketHandling
{
    public enum PacketType
    {
        Status,
        Broadcast,
        Message,
        DataGet,
        UnknownPacket,
        EmptyPacket
    }
}
namespace Server.Listener
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
        DoctorResistance,
        DoctorDataGet,
        DoctorDataSave,
        DoctorAddNewClient,
        DoctorAddClientHistory,
        DoctorGetClientHistory,
        DoctorLogin,
        DoctorLogout,
        DoctorBroadcast,
        DoctorMessage,
        DoctorConnectToClient,
        ClientClose,
        DoctorClose
    }
}
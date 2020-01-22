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
        ClientSyncData,
        ClientHeart,
        ClientMessage,
        ClientClose,

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
        DoctorClose,
        DoctorStopVR
    }
}
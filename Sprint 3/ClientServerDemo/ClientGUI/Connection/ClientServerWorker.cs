using System;

namespace ClientGUI.Connection
{
    public class ClientServerWorker
    {
        private ServerConnection conn;

        public event BroadcastHandler BroadcastReceived;
        public delegate void BroadcastHandler(BroadcastArgs args);

        public ClientServerWorker(ServerConnection conn)
        {
            // TODO: Server packets ontvangen -> ontleden -> event callback geven
        }
    }

    public class BroadcastArgs : EventArgs
    {
        public string Message;

        public BroadcastArgs(string message)
        {
            this.Message = message;
        }
    }
}
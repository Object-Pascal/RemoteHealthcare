using ClientGUI.PacketHandling;
using System;
using System.Threading.Tasks;

namespace ClientGUI.Connection
{
    public class ClientServerWorker
    {
        public bool IsRunning { get; private set; }

        private ServerConnection conn;
        private PacketHandler packetHandler;

        public event StatusHandler StatusReceived;
        public delegate void StatusHandler(StatusArgs args);

        public event ResistanceHandler ResistanceReceived;
        public delegate void ResistanceHandler(ResistanceArgs args);

        public event BroadcastHandler BroadcastReceived;
        public delegate void BroadcastHandler(BroadcastArgs args);

        public event MessageHandler MessageReceived;
        public delegate void MessageHandler(MessageArgs args);

        public event StopHandler StopReceived;
        public delegate void StopHandler(EventArgs args);

        public event DoctorDisconnectHandler DoctorDisconnectReceived;
        public delegate void DoctorDisconnectHandler(EventArgs args);

        public ClientServerWorker(ServerConnection conn)
        {
            this.conn = conn;
            this.packetHandler = new PacketHandler();
        }

        public async void Run()
        {
            IsRunning = true;
            await Task.Run(async() =>
            {
                while (IsRunning)
                {
                    try
                    {
                        string response = await conn.WaitForResponse();
                        Tuple<string[], PacketType> packet = packetHandler.HandlePacket(response);

                        switch (packet.Item2)
                        {
                            case PacketType.Status:
                                StatusReceived?.Invoke(new StatusArgs(packet.Item1[1]));
                                break;
                            case PacketType.DoctorDisconnect:
                                DoctorDisconnectReceived?.Invoke(new EventArgs());
                                break;
                            case PacketType.Resistance:
                                ResistanceReceived?.Invoke(new ResistanceArgs(packet.Item1[1]));
                                break;
                            case PacketType.Broadcast:
                                BroadcastReceived?.Invoke(new BroadcastArgs(packet.Item1[1]));
                                break;
                            case PacketType.Message:
                                MessageReceived?.Invoke(new MessageArgs(packet.Item1[1]));
                                break;
                            case PacketType.StopVR:
                                StopReceived?.Invoke(new EventArgs());
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        IsRunning = false;
                    }
                }
            });
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }

    public class StatusArgs : EventArgs
    {
        public string Status;

        public StatusArgs(string status)
        {
            this.Status = status;
        }
    }

    public class ResistanceArgs : EventArgs
    {
        public string Resistance;

        public ResistanceArgs(string resistance)
        {
            this.Resistance = resistance;
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

    public class MessageArgs : EventArgs
    {
        public string Message;

        public MessageArgs(string message)
        {
            this.Message = message;
        }
    }
}
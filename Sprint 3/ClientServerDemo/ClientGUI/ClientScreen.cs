using ClientGUI.Connection;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class ClientScreen : Form
    {
        private ServerConnectionVR serverConnectionVR;
        private ServerConnection serverConnection;
        private ClientServerWorker clientServerWorker;

        private string currentSessionId;

        public ClientScreen(ServerConnectionVR serverConnectionVR, ServerConnection serverConnection, string currentSessionId)
        {
            InitializeComponent();

            this.serverConnectionVR = serverConnectionVR;
            this.serverConnection = serverConnection;
            this.currentSessionId = currentSessionId;

            lblWait.Left = (this.ClientSize.Width - lblWait.Width) / 2;
            lblWait.Top = (this.ClientSize.Height - lblWait.Height) / 2;

            StartWorker();
        }

        private void StartWorker()
        {
            if (this.serverConnection.Connected)
            {
                this.clientServerWorker = new ClientServerWorker(this.serverConnection);
                this.clientServerWorker.StatusReceived += ClientServerWorker_StatusReceived;
                this.clientServerWorker.BroadcastReceived += ClientServerWorker_BroadcastReceived;
                this.clientServerWorker.MessageReceived += ClientServerWorker_MessageReceived;
                this.clientServerWorker.StopReceived += ClientServerWorker_StopReceived;
            }
        }

        private void ClientServerWorker_StatusReceived(StatusArgs args)
        {
            if (args.Status == "ready")
            {
                // VR Starten
                // Bike Starten
                // Etc.
            }
        }

        private void ClientServerWorker_MessageReceived(MessageArgs args)
        {
            
        }

        private void ClientServerWorker_BroadcastReceived(BroadcastArgs args)
        {
            
        }

        private void ClientServerWorker_StopReceived(EventArgs args)
        {

        }
    }
}
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
        private ServerConnection serverConnectionVR;
        private ClientServerWorker clientServerWorker;

        private string currentSessionId;

        public ClientScreen(ServerConnection serverConnectionVR, string currentSessionId)
        {
            InitializeComponent();

            this.serverConnectionVR = serverConnectionVR;
            this.currentSessionId = currentSessionId;

            StartWorker();
        }

        private async void StartWorker()
        {
            bool checkCert = await CheckCertificate();
            if (checkCert)
            {
                ServerConnection sc = new ServerConnection();

                bool connected = await sc.Connect("80.115.121.54", 25545);
                if (connected)
                {
                    this.clientServerWorker = new ClientServerWorker(sc);
                }
            }
        }

        private async Task<bool> CheckCertificate()
        {
            return await Task.Run(() =>
            {
                try
                {
                    string certFile = Directory.GetCurrentDirectory() + "\\certificate.cer";
                    if (File.Exists(certFile))
                    {
                        X509Certificate2 cert = new X509Certificate2(certFile);
                        using (X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser))
                        {
                            store.Open(OpenFlags.ReadWrite);

                            if (!store.Certificates.Contains(cert))
                            {
                                store.Add(cert);
                            }                        
                        }
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}

using Client.Json_Structure;
using ClientGUI.Bluetooth;
using ClientGUI.Connection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class SessionScreen : Form
    {
        private string Name;
        private string Id;

        private JsonPacketBuilder jsonPacketBuilder;
        private ServerConnection serverConnection;

        private bool vrConnected;
        private ServerConnectionVR serverConnectionVR;
        private BleHeartHandler bleHeartHandler;
        private BleBikeHandler bleBikeHandler;

        private Dictionary<string, string> users;

        public SessionScreen()
        {
            InitializeComponent();

            InitializeDefaultValues();
            InitializeDefaultEvents();
            InitializeSessionContext();
            InitializeLogin();
        }

        private void InitializeDefaultValues()
        {
            this.users = new Dictionary<string, string>();
            this.vrConnected = false;
            this.jsonPacketBuilder = new JsonPacketBuilder();
            this.serverConnectionVR = new ServerConnectionVR();
        }

        private void InitializeDefaultEvents()
        {
            this.FormClosing += (s, e) =>
            {
                if (this.serverConnection != null)
                {
                    this.serverConnection.SendWithNoResponse($"Client/Logout\r\n");
                    System.Threading.Thread.Sleep(1000);
                }
            };
        }

        private void InitializeSessionContext()
        {
            lstbSessions.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Refresh", (s, e) => RefreshSessions())
            });
        }

        private void InitializeLogin()
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.FormClosing += (s, e) =>
            {
                if (!loginScreen.IsLoggedIn)
                {
                    if (this.serverConnection != null)
                    {
                        this.serverConnection.SendWithNoResponse($"Client/Logout\r\n");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            };

            loginScreen.LoggedIn += (e) =>
            {
                this.Name = e.Name;
                this.Id = e.Id;

                this.serverConnection = e.ServerConnection;
                this.bleHeartHandler = e.BleHeartHandler;
                this.bleBikeHandler = e.BleBikeHandler;            
                InitializeSessions();
            };
            loginScreen.ShowDialog();
        }

        private async Task<bool> InitializeConnection()
        {
            btnSelectSession.Enabled = false;            
            return await serverConnectionVR.Connect("145.48.6.10", 6666);
        }

        private async void InitializeSessions()
        {
            this.vrConnected = await InitializeConnection();
            if (this.vrConnected)
            {
                Tuple<string, JObject> sessionResponse = serverConnectionVR.TransferSendableResponse(jsonPacketBuilder.BuildSessionPacket().Item1);

                JArray data = sessionResponse.Item2.SelectToken("data") as JArray;
                foreach (JObject j in data)
                {
                    if (!users.Keys.Contains(j.SelectToken("clientinfo.user").ToString()))
                    {
                        users.Add(j.SelectToken("clientinfo.user").ToString(), j.SelectToken("id").ToString());
                        lstbSessions.Items.Add($"{j.SelectToken("id").ToString()}: {j.SelectToken("clientinfo.user").ToString()}");
                    }
                }
                btnSelectSession.Enabled = true;
            }
            else
            {
                if (MessageBox.Show("Kan niet verbinden met de VR server", "Waarschuwing", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                    InitializeSessions();
            }
        }

        private void RefreshSessions()
        {
            if (this.vrConnected)
            {
                lstbSessions.Items.Clear();

                Tuple<string, JObject> sessionResponse = serverConnectionVR.TransferSendableResponse(jsonPacketBuilder.BuildSessionPacket().Item1);

                JArray data = sessionResponse.Item2.SelectToken("data") as JArray;
                foreach (JObject j in data)
                {
                    if (!users.Keys.Contains(j.SelectToken("clientinfo.user").ToString()))
                    {
                        users.Add(j.SelectToken("clientinfo.user").ToString(), j.SelectToken("id").ToString());
                        lstbSessions.Items.Add($"{j.SelectToken("id").ToString()}: {j.SelectToken("clientinfo.user").ToString()}");
                    }
                }
                btnSelectSession.Enabled = true;
            }
            else
            {
                if (MessageBox.Show("Kan niet verbinden met de VR server", "Waarschuwing", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                    InitializeSessions();
            }
        }

        private void BtnSelectSession_Click(object sender, EventArgs e)
        {
            if (lstbSessions.SelectedItem != null)
            {
                string selectedSessionId = Regex.Split(lstbSessions.SelectedItem.ToString(), ":")[0];

                ClientScreen clientScreen = new ClientScreen(this.Name, this.Id, this.serverConnectionVR, this.serverConnection, selectedSessionId, this.bleHeartHandler, this.bleBikeHandler);
                clientScreen.FormClosing += (s, a) => this.Show();

                this.Hide();
                clientScreen.ShowDialog();
            }
        }
    }
}
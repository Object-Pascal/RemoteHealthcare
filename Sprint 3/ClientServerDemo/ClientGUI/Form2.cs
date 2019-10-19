using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Json_Structure;
using ClientGUI.Bluetooth;
using ClientGUI.Connection;
using ClientGUI.Sub_Objects;
using Newtonsoft.Json.Linq;

namespace ClientGUI
{
    public partial class Form2 : Form
    {
        private JsonPacketBuilder jsonPacketBuilder;
        private ServerConnection serverConnection;       

        private Dictionary<string, string> users;

        public Form2()
        {
            InitializeComponent();

            users = new Dictionary<string, string>();
            jsonPacketBuilder = new JsonPacketBuilder();
            serverConnection = new ServerConnection();
            
            InitializeLogin();
        }

        private void InitializeLogin()
        {
            LoginScreen loginScreen = new LoginScreen();            
            loginScreen.LoggedIn += (e) =>
            {
                BleHeartHandler bleHeart = loginScreen.bleHeartHandler;
                BleBikeHandler bleBike = loginScreen.bleBikeHandler;
                
                InitializeSessions();
            };
            loginScreen.ShowDialog();
        }

        private async Task<bool> InitializeConnection()
        {
            btnSelectSession.Enabled = false;            
            return await serverConnection.Connect("145.48.6.10", 6666);
        }

        private async void InitializeSessions()
        {
            bool connected = await InitializeConnection();
            if (connected)
            {
                Tuple<string, JObject> sessionResponse = serverConnection.TransferSendableResponse(jsonPacketBuilder.BuildSessionPacket().Item1);

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
                // Form starten die jouw snelheid, bpm enz laat zien en die ook verbonden is met de VR
            }
        }
    }
}
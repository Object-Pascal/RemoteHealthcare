using Doctor.PacketHandling;
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


namespace Doctor
{
    public partial class DokterForm : Form
    {
        private LoginScreen loginScreen;
        private ServerConnection serverConnection;
        private PacketHandler packetHandler;

        private FlowLayoutPanel panel;
        private List<Patient> selectedPatients;
        private List<Patient> availablePatients;

        public DokterForm()
        {
            InitializeComponent();

            loginScreen = new LoginScreen();
            this.packetHandler = new PacketHandler();

            this.selectedPatients = new List<Patient>();
            this.availablePatients = new List<Patient>();

            loginScreen.LoggedIn += LoginScreen_LoggedIn;
            loginScreen.FalseLogin += LoginScreen_FalseLogin;
            loginScreen.ShowDialog();

            this.FormClosing += (s, e) =>
            {
                this.serverConnection.SendWithNoResponse($"Doctor/LogOut\r\n");
                System.Threading.Thread.Sleep(1000);
            };
        }

        private async void InitializeClientList()
        {
            //send: Doctor/DataGet\r\nALL_CLIENTS
            //response: Server/DataGet\r\nNAME//ID//NAME//ID//NAME//ID//NAME//ID//NAME//ID//...
            string responsePacket = await this.serverConnection.SendWithResponse($"Doctor/DataGet\r\nALL_CLIENTS");
            Tuple<string, PacketType> handledPacket = packetHandler.HandlePacket(responsePacket);

            if (handledPacket.Item2 == PacketType.DataGet)
            {
                string[] clientsRaw = Regex.Split(handledPacket.Item1, "//").Where(x => x != "").ToArray();

                for (int i = 0; i < clientsRaw.Length; i += 4)
                {
                    availablePatients.Add(new Patient(clientsRaw[i], clientsRaw[i + 1], clientsRaw[i + 2], clientsRaw[i + 3]));
                    availableListBox.Items.Add(clientsRaw[i]);
                }
            }
        }

        private void LoginScreen_LoggedIn(LogInArgs args)
        {
            this.serverConnection = args.ServerConnection;
            InitializeClientList();
            loginScreen.Close();
        }

        private void LoginScreen_FalseLogin()
        {
            MessageBox.Show("False login credentials");
            Application.Exit();
        }

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selectedIndexes = availableListBox.SelectedIndices;
            int[] selectedIndexesArray = new int[selectedIndexes.Count];
            selectedIndexes.CopyTo(selectedIndexesArray, 0);

            int count = selectedIndexes.Count;

            foreach (int i in selectedIndexesArray)
            {
                selectedPatients.Add(availablePatients[i]);
                selectedListBox.Items.Add(availablePatients[i].toString());
                addBtnToFlowpanel(availablePatients[i]);
                availableListBox.Items.Remove(availablePatients[i].toString());
            }

            selectedPatients.ForEach(x => availablePatients.Remove(x));
        }

        private void DeselectBtn_Click(object sender, EventArgs e) 
        {
            ListBox.SelectedIndexCollection selectedIndexes = selectedListBox.SelectedIndices;
            int[] selectedIndexesArray = new int[selectedIndexes.Count];
            selectedIndexes.CopyTo(selectedIndexesArray, 0);

            int count = selectedIndexes.Count;

            foreach (int i in selectedIndexesArray)
            {
                availablePatients.Add(selectedPatients[i]);
                availableListBox.Items.Add(selectedPatients[i].toString());
                removeBtnFromFlowpanel(selectedPatients[i]);
                selectedListBox.Items.Remove(selectedPatients[i].toString());
            }

            availablePatients.ForEach(x => selectedPatients.Remove(x));
        }

        private void BroadcastTextBox_Enter(object sender, EventArgs e)
        {
            if (BroadcastTextBox.Text == "Typ het uitzendbericht:")
            {
                BroadcastTextBox.Text = "";

                BroadcastTextBox.ForeColor = Color.Black;

                BroadcastTextBox.Font = new Font(BroadcastTextBox.Font, FontStyle.Regular);
            }
        }

        private void BroadcastTextBox_Leave(object sender, EventArgs e)
        {
            if (BroadcastTextBox.Text == "")
            {
                BroadcastTextBox.Text = "Typ het uitzendbericht:";
                BroadcastTextBox.ForeColor = Color.Gray;
                BroadcastTextBox.Font = new Font(BroadcastTextBox.Font, FontStyle.Italic);
            }
        }

        private void BroadcastBtn_Click(object sender, EventArgs e)
        {
            //Send message to server for broadcasting

            string broadcastmessage = BroadcastTextBox.Text;
            this.serverConnection.SendWithNoResponse($"Doctor/Broadcast\r\n{broadcastmessage}\r\n");

            //Clears textbox
            BroadcastTextBox.Clear();
            BroadcastTextBox.Text = "Typ het uitzendbericht:";
            BroadcastTextBox.ForeColor = Color.Gray;
            BroadcastTextBox.Font = new Font(BroadcastTextBox.Font, FontStyle.Italic);

        }

        private void addBtnToFlowpanel(Patient p)
        {
            Button btn = new Button();
            btn.Height = 82;
            btn.Width = 147;
            btn.Text = $"Naam: {p.Name} \nLeeftijd: {p.Age}\nGeslacht: {p.Gender}\nBpm: {p.Bpm}";
            btn.FlatStyle = FlatStyle.Popup;
            btn.TextAlign = ContentAlignment.TopLeft;
            btn.BackColor = Color.White;
            panel.Controls.Add(btn);
            btn.Tag = p;
            btn.Click += new EventHandler(button_click);
        }

        private async void button_click(object sender, EventArgs e)
        {
            //send selected patient back to server
            //open detailed information form

            Button b = sender as Button;
            DetailDoctorForm detail = new DetailDoctorForm((Patient)b.Tag, this.serverConnection);
            detail.Show();

        }

        private void removeBtnFromFlowpanel(Patient p)
        {

            foreach (Button b in panel.Controls)
            {
                if (b.Text.Contains(p.Name))
                {
                    LayoutPanelClient.Controls.Remove(b);
                }
            }
        }

        private async void RefreshBttn_Click(object sender, EventArgs e)
        {
            //same as setup methods only clears the lists first.
            string responsePacket = await this.serverConnection.SendWithResponse($"Doctor/DataGet\r\nALL_CLIENTS");
            Tuple<string, PacketType> handledPacket = packetHandler.HandlePacket(responsePacket);

            if (handledPacket.Item2 == PacketType.DataGet)
            {
                string[] clientsRaw = Regex.Split(handledPacket.Item1, "//").Where(x => x != "").ToArray();

                availablePatients.Clear();
                availableListBox.Items.Clear();

                for (int i = 0; i < clientsRaw.Length; i += 4)
                {
                    availablePatients.Add(new Patient(clientsRaw[i], clientsRaw[i + 1], clientsRaw[i + 2], clientsRaw[i + 3]));
                    availableListBox.Items.Add(clientsRaw[i]);
                }
            }

        }
    } 
}
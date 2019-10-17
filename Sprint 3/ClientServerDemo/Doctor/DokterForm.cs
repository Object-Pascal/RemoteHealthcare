using Doctor.PacketHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 


namespace Doctor
{
    public partial class DokterForm : Form
    {
        private LoginScreen loginScreen;
        private ServerConnection serverConnection;
        private bool serverConnected;
        private PacketHandler packetHandler;

        private FlowLayoutPanel panel;
        private List<Patient> selectedPatients;
        private List<Patient> availablePatients;

        public DokterForm()
        {
            InitializeComponent();

            loginScreen = new LoginScreen();

            loginScreen.LoggedIn += LoginScreen_LoggedIn;
            loginScreen.FalseLogin += LoginScreen_FalseLogin;
            loginScreen.ShowDialog();
        }

        private void LoginScreen_LoggedIn()
        {
            loginScreen.Close();
        }

        private void LoginScreen_FalseLogin()
        {
            MessageBox.Show("False login credentials");
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
            btn.Click += new EventHandler(button_click);
        }

        private void button_click(object sender, EventArgs e)
        {
            //send selected patient back to server
            //open detailed information form

            BroadcastTextBox.Text = "WELLOE DIT WERKT";
        }

        private void removeBtnFromFlowpanel(Patient p)
        {
            Button remove;

            foreach (Button b in panel.Controls)
            {
                if (b.Text.Contains(p.Name))
                {
                    LayoutPanelClient.Controls.Remove(b);
                }
            }
        }

        private void testDataAvailablePatients()
        {
            availablePatients.Add(new Patient("Pascal", 20, "Man", "0000"));
            availablePatients.Add(new Patient("Maarten", 20, "Man", "0000"));
            availablePatients.Add(new Patient("Thijs", 21, "Man", "0000"));
            availablePatients.Add(new Patient("Joelle", 20, "Vrouw", "0000"));
            availablePatients.Add(new Patient("Marleen", 20, "Vrouw", "0000"));
            availablePatients.Add(new Patient("Kirsten", 20, "Vrouw", "0000"));
        }
    } 
}
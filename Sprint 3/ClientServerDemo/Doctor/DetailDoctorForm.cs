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
    public partial class DetailDoctorForm : Form
    {
        private ServerConnection serverConnection;
        //private bool serverConnected;
        //private PacketHandler packetHandler;

        private Patient patient;
        public DetailDoctorForm(Patient patient, ServerConnection serverConnection)
        {
            InitializeComponent();
            SetDefaultValues(patient, serverConnection);
        }

        public void SetDefaultValues(Patient patient, ServerConnection serverConnection)
        {
            this.patient = patient;
            this.serverConnection = serverConnection;

            lblName.Text = patient.Name;
            lblBirthDate.Text = patient.Age + "";     
            lblGender.Text = patient.Gender;
            lblPantiëntKey.Text = patient.Id;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Als je de sessie afsluit wordt de huidige data opgeslagen en de sessie afgesloten."+ "\r\n" + "Weet je zeker dat je de sessie stoppen?",
                               "Stop Sessie",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void StartSesion_Click(object sender, EventArgs e)
        {

        }
        private void StopSession_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Als je de sessie afsluit wordt de huidige data opgeslagen en de sessie afgesloten." + "\r\n" + "Weet je zeker dat je de sessie stoppen?",
                               "Stop Sessie",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void EmergencyBreak_Click(object sender, EventArgs e)
        {

        }
      
        private void TextBoxSendMessage_Enter(object sender, EventArgs e)
        {
            if (tbTextBoxSendMessage.Text == "Stuur bericht ...")
            {
                tbTextBoxSendMessage.Text = "";

                tbTextBoxSendMessage.ForeColor = Color.Black;
            }
        }

        private void TextBoxSendMessage_Leave(object sender, EventArgs e)
        {
            if (tbTextBoxSendMessage.Text == "")
            {
                tbTextBoxSendMessage.Text = "Stuur bericht ...";
                tbTextBoxSendMessage.ForeColor = Color.Silver;
            }
        }

        private void TextBoxSendMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbMessageHistory.SelectedText += "Doctor: " + tbTextBoxSendMessage.Text + "\r\n";
                tbTextBoxSendMessage.Text = "";
            }
        }

        private void SendPrivateMessage_Click(object sender, EventArgs e)
        {
            tbMessageHistory.SelectedText += "Doctor: " + tbTextBoxSendMessage.Text + "\r\n";
            tbTextBoxSendMessage.Text = "Stuur bericht ...";
            tbTextBoxSendMessage.ForeColor = Color.Silver;
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            ClientHistoryForm clientHistoryForm = new ClientHistoryForm();
            clientHistoryForm.ShowDialog();
        }

        /*private void writechHearthRate(Patient patient)
        {
            int i = 0;
            foreach (double hearbeat in patient.heartbeat)
            {
                chHeartRate.Series["VO2Now"].Points.AddXY(i, patient.heartrate);
                i++;
            }
        }
        private void writechBikeSpeed(Patient patient)
        {
            int i = 0;
            foreach (double hearbeat in patient.heartbeat)
            {
                chBikeSpeed.Series["VO2Now"].Points.AddXY(i, Bikespeed);
                i++;
            }
        }*/
    }
}

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
        public DetailDoctorForm()
        {
            InitializeComponent();
            SetDefaultValues();
        }

        public void SetDefaultValues()
        {
            lblName.Text =          "Name:              Thijs van der Velden";
            lblBirthDate.Text =     "Birthdate:         16-05-1998";
            lblGender.Text =        "Gender:            Male";
            lblPantiëntKey.Text =   "PantiëntKey:    2131200";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Name_Click(object sender, EventArgs e)
        {
            //lblName.Text = Name;
        }

        private void BirthDate_Click(object sender, EventArgs e)
        {
            //lblBirthDate.Text = BirthDate;
        }
        private void Gender_Click(object sender, EventArgs e)
        {
            //lblGender.Text = Gender;
        }
        private void PantiëntKey_Click(object sender, EventArgs e)
        {
            //lblPantiëntKey.Text = PantiëntKey;
        }

        private void HeartRate_Click(object sender, EventArgs e)
        {

        }

        private void BikeSpeed_Click(object sender, EventArgs e)
        {

        }
      
        private void StartSesion_Click(object sender, EventArgs e)
        {

        }
        private void StopSession_Click(object sender, EventArgs e)
        {

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

    }
}

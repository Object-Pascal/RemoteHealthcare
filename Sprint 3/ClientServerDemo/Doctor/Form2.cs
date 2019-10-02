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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            SetDefaultValues();
        }

        public void SetDefaultValues()
        {
            lblName.Text = "Name";
            lblBirthDate.Text = "Birthdate";
            lblGender.Text = "Gender";
            lblPantiëntKey.Text = "PantiëntKey";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Name_Click(object sender, EventArgs e)
        {

        }

        private void BirthDate_Click(object sender, EventArgs e)
        {

        }
        private void Gender_Click(object sender, EventArgs e)
        {

        }
        private void PantiëntKey_Click(object sender, EventArgs e)
        {

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

        private void SendPrivateMessage_Click(object sender, EventArgs e)
        {

        }
    }
}

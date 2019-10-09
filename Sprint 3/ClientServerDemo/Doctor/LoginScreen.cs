using System;
using Avans.TI.BLE;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Doctor
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {

        }

        private void Name_Enter(object sender, EventArgs e)
        {
            if (txtDoctorName.Text == "Naam")
            {
                txtDoctorName.Text = "";
                txtDoctorName.ForeColor = Color.Black;
            }

        }

        private void Name_Leave(object sender, EventArgs e)
        {
            if (txtDoctorName.Text == "")
            {
                txtDoctorName.Text = "Naam";
                txtDoctorName.ForeColor = Color.Silver;
            }
        }

        private void PatientNumber_Enter(object sender, EventArgs e)
        {
            if (txtDoctorPassword.Text == "Wachtwoord")
            {
                txtDoctorPassword.Text = "";
                txtDoctorPassword.ForeColor = Color.Black;
            }
        }

        private void PatientNumber_Leave(object sender, EventArgs e)
        {
            if (txtDoctorPassword.Text == "")
            {
                txtDoctorPassword.Text = "Wachtwoord";
                txtDoctorPassword.ForeColor = Color.Silver;
            }
        }

    }
}
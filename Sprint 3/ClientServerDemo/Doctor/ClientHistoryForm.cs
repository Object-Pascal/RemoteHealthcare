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
    public partial class ClientHistoryForm : Form
    {
        public ClientHistoryForm()
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

        private void LblName_Click(object sender, EventArgs e)
        {

        }

        private void LblGender_Click(object sender, EventArgs e)
        {

        }

        private void LblBirthDate_Click(object sender, EventArgs e)
        {

        }

        private void LblPantiëntKey_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

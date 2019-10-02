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
        private FlowLayoutPanel panel;

        public DokterForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            panel = LayoutPanelClient;


        }

        private void Name_Enter(object sender, EventArgs e)
        {
            if (BroadcastTextBox.Text == "Typ het uitzendbericht:")
            {
                BroadcastTextBox.Text = "";

                BroadcastTextBox.ForeColor = Color.Black;
            }

        }

        private void Name_Leave(object sender, EventArgs e)
        {
            if (BroadcastTextBox.Text == "")
            {
                BroadcastTextBox.Text = "Typ het uitzendbericht:";
                BroadcastTextBox.ForeColor = Color.Silver;
            }
        }

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            //Client to selected patient list

            //Client added to flowpanel
        }
    }
}

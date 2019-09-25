using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private bool RoomExist(string roomID)
        {
            return true;
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (RoomExist(roomName.Text))
            {

            } else
            {
                this.noRoom.Visible = true;
            }
        }

        private void Name_Enter(object sender, EventArgs e)
        {
            if (name.Text == "Name")
            {
                name.Text = "";

                name.ForeColor = Color.Black;
            }

        }

        private void Name_Leave(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                name.Text = "Name";
                name.ForeColor = Color.Silver;
            }
        }

        private void RoomName_Enter(object sender, EventArgs e)
        {
            if (roomName.Text == "Room name")
            {
                roomName.Text = "";

                roomName.ForeColor = Color.Black;
            }
        }

        private void Roomname_Leave(object sender, EventArgs e)
        {
            if (roomName.Text == "")
            {
                roomName.Text = "Room name";

                roomName.ForeColor = Color.Silver;
            }
        }

    }
}

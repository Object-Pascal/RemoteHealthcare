using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientGUI.Bluetooth;

namespace ClientGUI
{
    public partial class LoginScreen : Form
    {

        private BleBikeHandler bleBikeHandler;
        private BleHeartHandler bleHeartHandler;

        private List<string> bleBikeList;
        private List<string> bleHeartList;
        public LoginScreen()
        {
            InitializeComponent();
            InitializeDeclarations();
        }

        private void InitializeDeclarations()
        {
            this.bleBikeHandler = new BleBikeHandler();
            this.bleHeartHandler = new BleHeartHandler();
        }

        private async void LoadBikes()
        {
            this.bleBikeList = await this.bleBikeHandler.RetrieveBleBikes("Tacx");
            this.bleBikeList.ForEach(x => selectBike.Items.Add(x));
        }

        private bool RoomExist(string roomID)
        {
            return true;
        }

        private bool BikeExist(string bikeID)
        {
            return true;
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (RoomExist(patientNumber.Text))
            {

            } else
            {
                this.unknownNumber.Visible = true;
            }
        }

        private void Name_Enter(object sender, EventArgs e)
        {
            if (name.Text == "Naam")
            {
                name.Text = "";

                name.ForeColor = Color.Black;
            }

        }

        private void Name_Leave(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                name.Text = "Naam";
                name.ForeColor = Color.Silver;
            }
        }

        private void PatientNumber_Enter(object sender, EventArgs e)
        {
            if (patientNumber.Text == "Patiëntnummer")
            {
                patientNumber.Text = "";

                patientNumber.ForeColor = Color.Black;
            }
        }

        private void PatientNumber_Leave(object sender, EventArgs e)
        {
            if (patientNumber.Text == "")
            {
                patientNumber.Text = "Patiëntnummer";

                patientNumber.ForeColor = Color.Silver;
            }
        }

    }
}

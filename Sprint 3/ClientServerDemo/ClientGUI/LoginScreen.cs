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
using ClientGUI.Conversion;
using ClientGUI.Utils;

namespace ClientGUI
{
    public partial class LoginScreen : Form
    {

       private static PageConversion pageConversion;
        private BleBikeHandler bleBikeHandler;
        private BleHeartHandler bleHeartHandler;

        private List<string> bleBikeList;
        private List<string> bleHeartList;
        private ConnectServer connect;
        private bool started;

        public LoginScreen()
        {
            connect = new ConnectServer();
            InitializeComponent();
            InitializeDeclarations();
            LoadBikes();
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

        private void SelectBike_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = selectBike.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedItem))
            {
                this.bleBikeHandler.Connect(selectedItem, "6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
                this.started = true;

                this.bleBikeHandler.SubscriptionValueChanged += (args) =>
                {
                    byte[] receivedDataSubset = args.Data.SubArray(4, args.Data.Length - 2 - 4);
                    pageConversion.RegisterData(receivedDataSubset);
                };
            }
        }

        private bool PatientExist(string patientID)
        {
            return true;
        }

 

        private void Login_Click(object sender, EventArgs e)
        {
            if (PatientExist(patientNumber.Text))
            {
                connect.Connect();
                connect.sendPatient(new Patient(name.Text, patientNumber.Text));

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

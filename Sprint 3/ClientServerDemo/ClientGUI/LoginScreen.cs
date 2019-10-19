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
using ClientGUI.Bluetooth;
using ClientGUI.Conversion;
using ClientGUI.Utils;
using System.Threading;

namespace ClientGUI
{
    public partial class LoginScreen : Form
    {

        private static PageConversion pageConversion;

        public BleBikeHandler bleBikeHandler;
        public BleHeartHandler bleHeartHandler;

        private BLE bleBike;
        private BLE bleHeart;

        private List<string> bleBikeList;
        private List<string> bleHeartList;
        private bool started;

        public event LoggedInHandler LoggedIn;
        public delegate void LoggedInHandler(LogInArgs args);

        public LoginScreen()
        {
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
        private bool PatientExist(string patientID)
        {
            return true;
        }


        private void Login_Click(object sender, EventArgs e)
        {
            //if (selectBike.SelectedItem != null)
            if (true)
            {
                if (PatientExist(patientNumber.Text))
                {
                    //bleHeartHandler.Connect("Decathlon Dual HR", "Heartrate");
                    //bleBikeHandler.Connect(selectBike.SelectedItem.ToString(), "6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");

                    this.LoggedIn?.Invoke(new LogInArgs(tbName.Text));
                    this.Close();
                }
                else
                {
                    this.unknownNumber.Text = "Patiëntnummer bestaat niet!";
                    this.unknownNumber.Visible = true;                    
            }
            }
            else
            {
                this.unknownNumber.Text = "     Geen fiets geselecteerd!";
                this.unknownNumber.Visible = true;
            }
        }

        private void Name_Enter(object sender, EventArgs e)
        {
            if (tbName.Text == "Naam")
            {
                tbName.Text = "";

                tbName.ForeColor = Color.Black;
            }

        }

        private void Name_Leave(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                tbName.Text = "Naam";
                tbName.ForeColor = Color.Silver;
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

    public class LogInArgs : EventArgs
    {
        public string Name;

        public LogInArgs(string name)
        {
            this.Name = name;
        }
    }
}
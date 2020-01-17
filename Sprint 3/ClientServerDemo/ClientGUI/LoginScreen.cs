using Avans.TI.BLE;
using ClientGUI.Bluetooth;
using ClientGUI.Connection;
using ClientGUI.PacketHandling;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class LoginScreen : Form
    {
        public BleBikeHandler bleBikeHandler;
        public BleHeartHandler bleHeartHandler;

        private BLE bleBike;
        private BLE bleHeart;

        private List<string> bleBikeList;
        private List<string> bleHeartList;

        private ClientServerWorker clientServerWorker;
        private ServerConnection serverConnection;
        private PacketHandler packetHandler;

        public bool IsLoggedIn { get; private set; }
        public event LoggedInHandler LoggedIn;
        public delegate void LoggedInHandler(LogInArgs args);

        public LoginScreen()
        {
            InitializeComponent();
            InitializeDefaultValues();

            StartServer();
            LoadBikes();
        }

        private void InitializeDefaultValues()
        {
            this.bleBikeHandler = new BleBikeHandler();
            this.bleHeartHandler = new BleHeartHandler();

            this.IsLoggedIn = false;
            this.packetHandler = new PacketHandler();
            this.serverConnection = new ServerConnection();
        }

        private async void LoadBikes()
        {
            this.bleBikeList = await this.bleBikeHandler.RetrieveBleBikes("Tacx");
            this.bleBikeList.ForEach(x => selectBike.Items.Add(x));
            selectBike.Items.Add("Bike Simulator");
        }

        private async void StartServer()
        {
            bool checkCert = await CheckCertificate();
            if (checkCert)
            {
                login.Enabled = false;
                tbName.Enabled = false;
                tbPatientNumber.Enabled = false;

                bool connected = await this.serverConnection.Connect("80.115.121.54", 25545);
                if (connected)
                {
                    login.Enabled = true;
                    tbName.Enabled = true;
                    tbPatientNumber.Enabled = true;
                }
            }
        }

        private async Task<bool> PatientExist(string patientName, string patientID)
        {
            try
            {
                string responseRaw = await this.serverConnection.SendWithResponse($"Client/Login\r\n{patientID}\r\n{patientName}");
                Tuple<string[], PacketType> responsePacket = packetHandler.HandlePacket(responseRaw);
                if (responsePacket.Item2 == PacketType.Status)
                {
                    if (packetHandler.IsStatusOk(responsePacket))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        private async void Login_Click(object sender, EventArgs e)
        {
            if (selectBike.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(tbName.Text) && !string.IsNullOrEmpty(tbPatientNumber.Text))
                {
                    this.IsLoggedIn = await PatientExist(tbName.Text.Trim(), tbPatientNumber.Text.Trim());
                    if (this.IsLoggedIn)
                    {
                        this.bleBikeHandler.SetDeviceName(selectBike.SelectedItem.ToString());
                        this.LoggedIn?.Invoke(new LogInArgs(tbName.Text, this.serverConnection, this.bleHeartHandler, this.bleBikeHandler));
                        this.Close();
                    }
                    else
                    {
                        this.unknownNumber.Text = "Kan niet inloggen met de ingevoerde gegevens!";
                        this.unknownNumber.Visible = true;
                    }
                }
            }
            else
            {
                this.unknownNumber.Text = "     Geen fiets geselecteerd!";
                this.unknownNumber.Visible = true;
            }
        }

        private async Task<bool> CheckCertificate()
        {
            return await Task.Run(() =>
            {
                try
                {
                    string certFile = Directory.GetCurrentDirectory() + "\\certificate.cer";
                    if (File.Exists(certFile))
                    {
                        X509Certificate2 cert = new X509Certificate2(certFile);
                        using (X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser))
                        {
                            store.Open(OpenFlags.ReadWrite);

                            if (!store.Certificates.Contains(cert))
                            {
                                store.Add(cert);
                            }
                        }
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
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
            if (tbPatientNumber.Text == "Patiëntnummer")
            {
                tbPatientNumber.Text = "";

                tbPatientNumber.ForeColor = Color.Black;
            }
        }

        private void PatientNumber_Leave(object sender, EventArgs e)
        {
            if (tbPatientNumber.Text == "")
            {
                tbPatientNumber.Text = "Patiëntnummer";

                tbPatientNumber.ForeColor = Color.Silver;
            }
        }

    }

    public class LogInArgs : EventArgs
    {
        public string Name;
        public ServerConnection ServerConnection;
        public BleHeartHandler BleHeartHandler;
        public BleBikeHandler BleBikeHandler;

        public LogInArgs(string name, ServerConnection serverConnection, BleHeartHandler bleHeartHandler, BleBikeHandler bleBikeHandler)
        {
            this.Name = name;
            this.ServerConnection = serverConnection;
            this.BleHeartHandler = bleHeartHandler;
            this.BleBikeHandler = bleBikeHandler;
        }
    }
}
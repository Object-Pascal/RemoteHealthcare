﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Doctor.PacketHandling;

namespace Doctor
{
    public partial class LoginScreen : Form
    {
        private ServerConnection serverConnection;
        private bool serverConnected;
        private PacketHandler packetHandler;

        private bool isLoggedIn;

        public event LoggedInHandler LoggedIn;
        public delegate void LoggedInHandler();

        public event FalseLoggedInHandler FalseLogin;
        public delegate void FalseLoggedInHandler();

        public LoginScreen()
        {
            InitializeComponent();
            InitializeServerConnection();
            this.isLoggedIn = false;

            if (this.serverConnected)
            {
                this.FormClosing += (s, e) =>
                {
                    serverConnection.Disconnect();
                };
            }
        }

        private async void InitializeServerConnection()
        {
            this.packetHandler = new PacketHandler();
            this.serverConnection = new ServerConnection();

            txtDoctorName.Enabled = false;
            txtDoctorPassword.Enabled = true;
            login.Enabled = false;

            this.serverConnected = await serverConnection.Connect("80.115.121.54", 30000);

            txtDoctorName.Enabled = true;
            txtDoctorPassword.Enabled = true;
            login.Enabled = true;
        }

        private async void Login_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDoctorName.Text) && !string.IsNullOrEmpty(txtDoctorPassword.Text))
            {
                string responsePacket = await this.serverConnection.SendWithResponse($"Doctor/LogIn\r\n{txtDoctorName.Text}\r\n{txtDoctorPassword.Text}");
                Tuple<string, PacketType> handledPacket = packetHandler.HandlePacket(responsePacket);

                if (handledPacket.Item2 == PacketType.Status)
                {
                    if (handledPacket.Item1 == "ok")
                    { 
                        lblUnknownLogin.Visible = false;
                        this.LoggedIn?.Invoke();
                    }
                    else
                    {
                        lblUnknownLogin.Visible = true;
                        this.FalseLogin?.Invoke();
                    }
                }
            }
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
using FietsDemoUI.Bluetooth;
using FietsDemoUI.Core.Conversion;
using FietsDemoUI.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FietsDemoUI
{
    public partial class MainForm : Form
    {
        private static PageConversion pageConversion;

        private int travelledDistance;
        private byte travelledDistanceRawPrev;
        private byte travelledDistanceStartingValue;
        private bool started;

        private BleBikeHandler bleBikeHandler;
        private BleHeartHandler bleHeartHandler;

        private List<string> bleBikeList;
        private List<string> bleHeartList;

        public MainForm()
        {
            InitializeComponent();
            InitializeDeclarations();

            RegisterBleBikeEvents();

            LoadBikes();
            //LoadHearts();
        }

        private void InitializeDeclarations()
        {
            this.bleBikeHandler = new BleBikeHandler();
            this.bleHeartHandler = new BleHeartHandler();
        }

        private async void LoadBikes()
        {
            this.bleBikeList = await this.bleBikeHandler.RetrieveBleBikes("Tacx");
            this.bleBikeList.ForEach(x => lstBikes.Items.Add(x));
        }

        private void BtnSimulator_Click(object sender, EventArgs e)
        {
            this.bleBikeHandler.ConnectSim("FietsData_4sep.txt");
            this.started = true;

            lstBikes.Enabled = false;
            lstHearts.Enabled = false;

            this.bleBikeHandler.SimValueChanged += (args) =>
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblSimPercentage.Text = $"{Math.Round(args.Percentage, 1)}%";
                    });
                }
                catch (ObjectDisposedException) { }


                byte[] receivedDataSubset = args.DataLine.SubArray(4, args.DataLine.Length - 2 - 4);
                pageConversion.RegisterData(receivedDataSubset);
            };

            this.bleBikeHandler.SimEnded += () =>
            {
                lstBikes.Enabled = true;
                lstHearts.Enabled = true;
            };
        }

        private void LstBikes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = lstBikes.SelectedItem as string;
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

        private void RegisterBleBikeEvents()
        {
            pageConversion = new PageConversion();
            pageConversion.Page10Received += (args) =>
            {
                if (started)
                {
                    travelledDistanceStartingValue = args.Data[3];
                    started = false;
                }

                int t = args.Data[3] - travelledDistanceRawPrev;
                if (t < 0)
                {
                    t += 256;
                }
                travelledDistance += t;
                travelledDistanceRawPrev = (byte)travelledDistance;

                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblDistance.Text = $"{travelledDistance - travelledDistanceStartingValue}m";
                    });
                }
                catch (ObjectDisposedException) { }

            };

            pageConversion.Page19Received += (args) =>
            {

            };

            pageConversion.Page50Received += (args) =>
            {

            };
        }
    }
}

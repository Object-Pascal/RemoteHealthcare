using Client.Bluetooth;
using Client.Core.Conversion;
using Client.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class BikeData
    {
        private static PageConversion pageConversion;

        private int travelledDistance;
        private byte travelledDistanceRawPrev;
        private byte travelledDistanceStartingValue;
        private bool started;

        private BleBikeHandler bleBikeHandler;
       // private BleHeartHandler bleHeartHandler;

        private List<string> bleBikeList;
        private List<string> bleHeartList;

        public BikeData()
        {
            InitializeDeclarations();

            RegisterBleBikeEvents();

            LoadBikes();
            //LoadHearts();
        }

        private void InitializeDeclarations()
        {
            this.bleBikeHandler = new BleBikeHandler();
        //    this.bleHeartHandler = new BleHeartHandler();
        }

        private async void LoadBikes()
        {
            this.bleBikeList = await this.bleBikeHandler.RetrieveBleBikes("Tacx");
        }

        public List<string> getBikes()
        {
            return bleBikeList;
        }

        private void StartSim(object sender, EventArgs e)
        {
            this.bleBikeHandler.ConnectSim("FietsData_4sep.txt");
            this.started = true;

          //  lstHearts.Enabled = false;

        }

        private void ConnectBike(object sender, String bikeNumber)
        {

                this.bleBikeHandler.Connect(bikeNumber, "6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
                this.started = true;

                this.bleBikeHandler.SubscriptionValueChanged += (args) =>
                {
                    byte[] receivedDataSubset = args.Data.SubArray(4, args.Data.Length - 2 - 4);
                    pageConversion.RegisterData(receivedDataSubset);
                };
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

  //              pageConversion.Page19Received += (args) =>
  //          {
  //
  //          };
  //
  //              pageConversion.Page50Received += (args) =>
  //              {
  //
  //              };
            };
           }
    }
}
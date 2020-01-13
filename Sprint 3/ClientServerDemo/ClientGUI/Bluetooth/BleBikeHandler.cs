using Avans.TI.BLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientGUI.Bluetooth
{
    public class BleBikeHandler
    {
        public BLE bleBike { get; private set; }
        public string bikeData { get; set; }

        public event SubscriptionHandler SubscriptionValueChanged;
        public delegate void SubscriptionHandler(BLESubscriptionValueChangedEventArgs args);

        public event SimHandler SimValueChanged;
        public delegate void SimHandler(DataReceivedArgs args);

        public event SimEndedHandler SimEnded;
        public delegate void SimEndedHandler();
        public int errorCode;
        public int percent;
        public int workload;

        public async Task<bool> InitBleBike()
        {
            return await Task.Run(() =>
            {
                try
                {
                    this.bleBike = new BLE();
                    Thread.Sleep(1000);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<List<string>> RetrieveBleBikes(string filter = "NO_FILTER")
        {
            if (bleBike == null)
            {
                bool completed = await InitBleBike();
                if (!completed)
                    return null;
            }
            return filter == "NO_FILTER" ? bleBike.ListDevices().ToList() : bleBike.ListDevices().Where(x => x.Contains(filter)).ToList();
        }

        public async Task DataAsync()
        {
            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");
        }

        public async void ConnectBike()
        {
            errorCode = 0;
            var services = bleBike.GetServices;
            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }

            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");
        }

        private void BleBike_SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs e)
        {

            if (e.Data[4] == 0x19)
            {
                bikeData = $"{e.Data[6]}";
                int instandpowerLSB = e.Data[5];
                int instandpowerMSB = e.Data[6];
                int work1 = (((instandpowerMSB | 0b11110000) ^ 0b11110000) << 8) | instandpowerLSB;
                workload = (int)(work1 * 6.1182972778676);

            }
        }
        public async void Connect(string deviceName, string serviceName)
        {
            int errorCode = 0;
            errorCode = errorCode = await this.bleBike.OpenDevice(deviceName);

            var services = bleBike.GetServices;
            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }
            errorCode = await this.bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");
        }

        public async void ChangeResistance(int percentage)
        {
            this.percent = percentage;
            byte resistance = (byte)percent;
            byte[] output = new byte[13];
            output[0] = 0x4A; // Sync bit;
            output[1] = 0x09; // Message Length
            output[2] = 0x4E; // Message type
            output[3] = 0x05; // Message type
            output[4] = 0x30; // Data Type
            output[11] = resistance;
            output[12] = 0xFF;
            await this.bleBike.WriteCharacteristic("6e40fec3-b5a3-f393-e0a9-e50e24dcca9e", output);
        }
    }

    public class DataReceivedArgs : EventArgs
    {
        public byte[] DataLine { get; private set; }

        public DataReceivedArgs(byte[] DataLine)
        {
            this.DataLine = DataLine;
        }
    }
}
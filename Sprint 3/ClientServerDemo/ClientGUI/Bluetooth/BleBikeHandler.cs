using Avans.TI.BLE;
using ClientGUI.Sim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientGUI.Bluetooth
{
    public class BleBikeHandler
    {
        public BLE BleBike { get; private set; }

        public event SubscriptionHandler SubscriptionValueChanged;
        public delegate void SubscriptionHandler(BLESubscriptionValueChangedEventArgs args);

        public event SimHandler SimValueChanged;
        public delegate void SimHandler(DataReceivedArgs args);

        public event SimEndedHandler SimEnded;
        public delegate void SimEndedHandler();

        public async Task<bool> InitBleBike()
        {
            return await Task.Run(() =>
            {
                try
                {
                    this.BleBike = new BLE();
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
            if (BleBike == null)
            {
                bool completed = await InitBleBike();
                if (!completed)
                    return null;
            }
            return filter == "NO_FILTER" ? BleBike.ListDevices().ToList() : BleBike.ListDevices().Where(x => x.Contains(filter)).ToList();
        }

        public async void Connect(string deviceName, string serviceName)
        {
            // "Tacx Flux 00438"
            int errorCode = await this.BleBike.OpenDevice(deviceName);
            errorCode = await this.BleBike.SetService(serviceName);

            // "6e40fec1-b5a3-f393-e0a9-e50e24dcca9e"
            this.BleBike.SubscriptionValueChanged += (s, e) => SubscriptionValueChanged?.Invoke(e);
            errorCode = await this.BleBike.SubscribeToCharacteristic(serviceName);
        }

        public void ConnectSim(string fileName)
        {
            Simulator bleBikeSim = new Simulator(fileName);
            bleBikeSim.DataReceived += (e) => SimValueChanged?.Invoke(e);
            bleBikeSim.Ended += () => SimEnded?.Invoke();
            bleBikeSim.Start();
        }

        public async void WriteCharacteristic(string serciveName, byte[] bytes)
        {
            await this.BleBike.WriteCharacteristic(serciveName, bytes);
        }
    }
}
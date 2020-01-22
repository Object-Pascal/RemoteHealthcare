using Avans.TI.BLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientGUI.Bluetooth
{
    public class BleHeartHandler
    {
        public BLE bleHeart { get; private set; }
        public bool IsConnected { get; set; }

        public event SubscriptionHandler SubscriptionValueChanged;
        public delegate void SubscriptionHandler(BLESubscriptionValueChangedEventArgs args);

        public BleHeartHandler()
        {
            this.IsConnected = false;
        }

        public async Task<bool> InitBleHeart()
        {
            return await Task.Run(() =>
            {
                try
                {
                    this.bleHeart = new BLE();
                    Thread.Sleep(1000);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<int> Connect(string deviceName, string serviceName)
        {
            int errorCode = 0;
            await this.bleHeart.OpenDevice("Decathlon Dual HR");
            errorCode = await this.bleHeart.SetService("HeartRate");

            // "HeartRateMeasurement"
            bleHeart.SubscriptionValueChanged += (s, e) => this.SubscriptionValueChanged?.Invoke(e);
            await this.bleHeart.SubscribeToCharacteristic("HeartRateMeasurement");

            if (errorCode == 0)
                this.IsConnected = true;

            return errorCode;
        }
    }
}
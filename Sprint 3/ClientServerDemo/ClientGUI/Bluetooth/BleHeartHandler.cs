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
        public BLE BleHeart { get; private set; }

        public event SubscriptionHandler SubscriptionValueChanged;
        public delegate void SubscriptionHandler(BLESubscriptionValueChangedEventArgs args);

        public async Task<bool> InitBleHeart()
        {
            return await Task.Run(() =>
            {
                try
                {
                    this.BleHeart = new BLE();
                    Thread.Sleep(1000);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<List<string>> RetrieveBleBikes(string filter)
        {
            if (BleHeart == null)
            {
                bool completed = await InitBleHeart();
                if (!completed)
                    return null;
            }
            return BleHeart.ListDevices().Where(x => x.Contains(filter)).ToList();
        }

        public async void Connect(string deviceName, string serviceName)
        {
            // "Decathlon Dual HR"
            // "HeartRate"
            try
            {
                int errorCode = await this.BleHeart.OpenDevice(deviceName);
                errorCode = await this.BleHeart.SetService(serviceName);

            // "HeartRateMeasurement"
            this.BleHeart.SubscriptionValueChanged += (s, e) => SubscriptionValueChanged?.Invoke(e);
            errorCode = await this.BleHeart.SubscribeToCharacteristic(serviceName);
            }
            catch (Exception)
            {

            }
        }
    }
}
using Avans.TI.BLE;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FietsDataSim
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int errorCode = 0;
            BLE bleBike = new BLE();
            BLE bleHeart = new BLE();
            Thread.Sleep(1000); // We need some time to list available devices

            // List available devices
            List<String> bleBikeList = bleBike.ListDevices();
            Console.WriteLine("Devices found: ");
            foreach (var name in bleBikeList)
            {
                Console.WriteLine($"Device: {name}");
            }

            // Connecting
            errorCode = errorCode = await bleBike.OpenDevice("Tacx Flux 00438");

            Console.WriteLine($"Errorcode: {errorCode}");

            var services = bleBike.GetServices;
            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }

            // Set service
            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            // __TODO__ error check

            // Subscribe
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");

            Console.Read();
        }

        private static void BleBike_SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs e)
        {
            SimpleLog.Log("FietsData.txt", $"{e.Data[0]}, {e.Data[1]}, {e.Data[2]}, {e.Data[3]}, {e.Data[4]}, {e.Data[5]}, {e.Data[6]}, {e.Data[7]}, {e.Data[8]}, {e.Data[9]}, {e.Data[10]}, {e.Data[11]}, {e.Data[12]}");
        }
    }
}
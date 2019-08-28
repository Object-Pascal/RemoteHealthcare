using Avans.TI.BLE;
using FietsDemo.Core.Conversion;
using FietsDemo.Core.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FietsDemo
{
    class Program
    {
        static int travelledDistance;

        static byte travelledDistanceRawPrev;
        static bool started;

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
            foreach(var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }

            // Set service
            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            // __TODO__ error check

            // Subscribe
            started = true;
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");

            // Heart rate
            errorCode =  await bleHeart.OpenDevice("Decathlon Dual HR");

            await bleHeart.SetService("HeartRate");

            bleHeart.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            await bleHeart.SubscribeToCharacteristic("HeartRateMeasurement");
            
            Console.Read();
        }

        private static void BleBike_SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs e)
        {
            byte[] receivedDataSubset = e.Data.SubArray(4, e.Data.Length - 2 - 4);
            string convertedString = BitConverter.ToString(receivedDataSubset);

            //Console.WriteLine(convertedString);

            // TODO: Page conversie toepassen
            PageConversion pageConversion = new PageConversion();

            string value = receivedDataSubset[0].ToString("X");
            switch (value)
            {
                case "10":
                    if (started)
                    {
                        travelledDistanceRawPrev = receivedDataSubset[3];
                        started = false;
                    }

                    Program.travelledDistance += (receivedDataSubset[3] - travelledDistance) - travelledDistanceRawPrev;
                    Console.WriteLine($"Received value:                 {receivedDataSubset[3]}");
                    Console.WriteLine($"Travelled distance previous:    {Program.travelledDistanceRawPrev}");
                    Console.WriteLine($"Travelled distance:             {Program.travelledDistance}");
                    break;
                case "19":
                    break;
                case "50":
                    break;
            }

            //Console.WriteLine("Received from {0}: {1}, {2}", e.ServiceName,
            //BitConverter.ToString(SubArray<byte>(e.Data, 4, e.Data.Length - 2 - 4)).Replace("-", " "),
            //Encoding.UTF8.GetString(e.Data));
        }
    }
}
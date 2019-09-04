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
        private static PageConversion pageConversion;

        private static int travelledDistance;
        private static byte travelledDistanceRawPrev;
        private static byte travelledDistanceRawRawPrev;
        private static byte travelledDistanceStartingValue;
        private static bool started;

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
            started = true;
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");
            RegisterBleBikeEvents();

            // Heart rate
            errorCode =  await bleHeart.OpenDevice("Decathlon Dual HR");

            await bleHeart.SetService("HeartRate");

            bleHeart.SubscriptionValueChanged += BleHeart_SubscriptionValueChanged;
            await bleHeart.SubscribeToCharacteristic("HeartRateMeasurement");
            
            Console.Read();
        }

        private static void RegisterBleBikeEvents()
        {
            pageConversion = new PageConversion();
            pageConversion.Page10Received += (args) =>
            {
                if (started)
                {
                    travelledDistanceStartingValue = args.Data[3];
                    started = false;
                }

                travelledDistance += args.Data[3] - travelledDistanceRawPrev;
                travelledDistanceRawPrev = (byte)travelledDistance;

                Console.WriteLine($"Received value:                 {args.Data[3]}");
                Console.WriteLine($"PreviousValue:                  {travelledDistanceRawPrev}");
                Console.WriteLine($"Travelled starting value:       {travelledDistanceRawPrev}");
                Console.WriteLine($"Travelled distance:             {travelledDistance}");
            };

            pageConversion.Page19Received += (args) =>
            {

            };

            pageConversion.Page50Received += (args) =>
            {

            };
        }

        private static void BleHeart_SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs e)
        {
            byte[] receivedDataSubset = e.Data;
            if (e.Data.Length == 6)
            {
                //Console.WriteLine($"Heartrate data received: {receivedDataSubset[0]}, {receivedDataSubset[1]}, {receivedDataSubset[2]}, {receivedDataSubset[3]}, {receivedDataSubset[4]}, {receivedDataSubset[5]}");
            }
        }

        private static void BleBike_SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs e)
        {
            byte[] receivedDataSubset = e.Data.SubArray(4, e.Data.Length - 2 - 4);
            pageConversion.RegisterData(receivedDataSubset);

            //Console.WriteLine("Received from {0}: {1}, {2}", e.ServiceName,
            //BitConverter.ToString(SubArray<byte>(e.Data, 4, e.Data.Length - 2 - 4)).Replace("-", " "),
            //Encoding.UTF8.GetString(e.Data));
        }
    }
}
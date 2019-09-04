using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FietsDemo.Core.Simulator
{
    public class Simulator
    {
        public string TargetDataFile { get; private set; }
        public string[] DataLines { get; private set; }

        private bool StopToken;

        public event DataReceivedHandler DataReceived;
        public delegate void DataReceivedHandler(DataReceivedArgs args);

        public Simulator(string TargetDataFile)
        {
            this.TargetDataFile = TargetDataFile;
            this.StopToken = false;
            LoadLines();
        }

        public async void Start()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < DataLines.Length; i++)
                {
                    if (StopToken)
                    {
                        StopToken = false;
                        break;
                    }

                    byte[] dataLine = ParseLineBike(DataLines[i]);
                    DataReceived?.Invoke(new DataReceivedArgs(dataLine));

                    Thread.Sleep(100);
                }
            });
        }

        private byte[] ParseLineBike(string DataLineRaw)
        {
            string[] RawLines = DataLineRaw.Split(',');
            return new byte[] { byte.Parse(RawLines[0]), byte.Parse(RawLines[1]), byte.Parse(RawLines[2]), byte.Parse(RawLines[3]), byte.Parse(RawLines[4]), byte.Parse(RawLines[5]), byte.Parse(RawLines[6]), byte.Parse(RawLines[7]), byte.Parse(RawLines[8]), byte.Parse(RawLines[9]), byte.Parse(RawLines[10]), byte.Parse(RawLines[11]), byte.Parse(RawLines[12]) };
        }

        private byte[] ParseLineHeart(string DataLineRaw)
        {
            string[] RawLines = DataLineRaw.Split(',');
            return new byte[] { byte.Parse(RawLines[0]), byte.Parse(RawLines[1]), byte.Parse(RawLines[2]), byte.Parse(RawLines[3]), byte.Parse(RawLines[4]), byte.Parse(RawLines[5]) };
        }

        public void Stop() => this.StopToken = true;
        public void LoadLines() => this.DataLines = File.ReadAllLines(this.TargetDataFile);
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
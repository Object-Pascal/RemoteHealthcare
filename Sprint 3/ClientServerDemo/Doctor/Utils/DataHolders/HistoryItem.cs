using System.Collections.Generic;

namespace Doctor.Utils.DataHolders
{
    public class HistoryItem
    {
        public List<byte[]> BikeData { get; set; }
        public List<byte[]> HeartData { get; set; }

        public HistoryItem(List<byte[]> BikeData, List<byte[]> HeartData)
        {
            this.BikeData = BikeData;
            this.HeartData = HeartData;
        }
    }
}
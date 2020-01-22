using System;
using System.Collections.Generic;

namespace Doctor.Utils.DataHolders
{
    public class ClientData
    {
        public int clientId { get; set; }
        public DateTime Date { get; set; }
        public List<byte[]> heartRateData { get; set; }
        public List<byte[]> bikeData { get; set; }
    }
}
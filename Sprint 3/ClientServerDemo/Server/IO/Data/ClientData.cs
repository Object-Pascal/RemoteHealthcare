using System;
using System.Collections.Generic;

namespace Server.IO.Data
{
    public class ClientData
    {
        public int clientId { get; set; }
        public DateTime Date { get; set; }
        public List<string> heartRateData { get; set; }
        public List<string> bikeData { get; set; }
    }
}
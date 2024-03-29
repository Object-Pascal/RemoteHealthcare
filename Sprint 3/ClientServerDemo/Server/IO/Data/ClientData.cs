﻿using System;
using System.Collections.Generic;

namespace Server.IO.Data
{
    public class ClientData
    {
        public int clientId { get; set; }
        public DateTime Date { get; set; }
        public List<byte[]> heartRateData { get; set; }
        public List<byte[]> bikeData { get; set; }
    }
}
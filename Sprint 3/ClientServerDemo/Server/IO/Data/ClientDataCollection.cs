﻿using System.Collections.Generic;

namespace Server.IO.Data
{
    public class ClientDataCollection
    {
        public List<ClientData> clientDataEntries { get; set; }

        public ClientDataCollection()
        {
            clientDataEntries = new List<ClientData>();
        }
    }
}
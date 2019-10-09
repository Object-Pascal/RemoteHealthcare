using System;

namespace Server.IO.Data
{
    public class ClientCollection
    {
        public Client[] clients { get; set; }

        internal object OfType<T>()
        {
            throw new NotImplementedException();
        }
    }
}
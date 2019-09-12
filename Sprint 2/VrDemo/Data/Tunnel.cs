using System.Text;

namespace VrDemo.Data
{
    class Tunnel
    {
        public string id { get; private set; }
        public string status { get; private set; }

        public Tunnel(string id, string status)
        {
            this.id = id;
            this.status = status;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ID: {id}");
            sb.AppendLine($"status: {status}");
            return sb.ToString();
        }
    }
}

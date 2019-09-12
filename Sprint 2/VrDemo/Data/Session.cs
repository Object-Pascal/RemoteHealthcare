using System.Text;

namespace VrDemo.Data
{
    public class Session
    {
        public string id { get; private set; }
        public string host { get; private set; }
        public string user { get; private set; }
        public string file { get; private set; }
        public string renderer { get; private set; }

        public Session(string id, string host, string user, string file, string renderer)
        {
            this.id = id;
            this.host = host;
            this.user = user;
            this.file = file;
            this.renderer = renderer;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ID: {id}");
            sb.AppendLine($"Host: {host}");
            sb.AppendLine($"User: {user}");
            sb.AppendLine($"File: {file}");
            sb.AppendLine($"Renderer: {host}");
            return sb.ToString();
        }
    }
}
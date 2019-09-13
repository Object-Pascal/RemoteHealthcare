using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrDemo.Data
{
    public class Route
    {
        public string routeid { get; private set; }
        public string nodeid { get; private set; }

        public Route(string routeid, string nodeid)
        {
            this.routeid = routeid;
            this.nodeid = nodeid;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"route_ID: {routeid}");
            sb.AppendLine($"node_ID: {nodeid}");
            return sb.ToString();
        }
    }
}

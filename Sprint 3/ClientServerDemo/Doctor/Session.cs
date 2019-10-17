using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor
{
    class Session
    {
        private List<String> heartrate { get; set; }
        private List<String> distance { get; set; }
        private List<String> speed { get; set; }
        private int ellapsedTime { get; set; }

        public void updateData(string heartrate, string distance, string speed)
        {
            this.distance.Add(distance);
            this.heartrate.Add(heartrate);
            this.speed.Add(speed);
        }

    }
}

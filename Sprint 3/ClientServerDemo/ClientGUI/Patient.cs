using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI
{
    class Patient
    {

        private string name;
        private string patientID;

        public Patient(string name, string patientID)
        {
            this.name = name;
            this.patientID = patientID;
        }
    }
}

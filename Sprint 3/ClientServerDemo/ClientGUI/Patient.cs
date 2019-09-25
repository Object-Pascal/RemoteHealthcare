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
        private decimal patientID;

        public Patient(string name, decimal patientID)
        {
            this.name = name;
            this.patientID = patientID;
        }
    }
}

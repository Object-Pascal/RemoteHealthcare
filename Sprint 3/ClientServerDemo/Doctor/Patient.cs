using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor
{
    class Patient
    {
        public Patient(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Bpm = 0;
        }

        public String Name { get; private set; }

        public int Age { get; set; }

        public String Gender { get; set; }

        public int Bpm { get; set; }

        public List<Session> sessions { get; set; }

        public String toString()
        {
            return $"{Name} - FietsId " ;
        }
    }
}

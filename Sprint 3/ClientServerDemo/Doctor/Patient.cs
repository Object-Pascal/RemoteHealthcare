using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor
{
    public class Patient
    {
        public Patient(string name, string id, string age, string gender)
        {
            Name = name;
            Id = id;
            Age = age;
            Gender = gender;
            Bpm = 0;
        }

        public String Name { get; private set; }

        public string Age { get; set; }
        public string Id { get; set; }

        public String Gender { get; set; }

        public int Bpm { get; set; }

        public List<Session> sessions { get; set; }

        public String toString()
        {
            return $"{Name} - FietsId " ;
        }
    }
}

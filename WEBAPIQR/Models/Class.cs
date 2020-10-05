using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Class
    {
        public Class()
        {
            Professorattendance = new HashSet<Professorattendance>();
            Schedule = new HashSet<Schedule>();
            Studentattendance = new HashSet<Studentattendance>();
        }

        public string IdClass { get; set; }
        public string Name { get; set; }
        public int FloorNumber { get; set; }

        public virtual ICollection<Professorattendance> Professorattendance { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<Studentattendance> Studentattendance { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Level
    {
        public Level()
        {
            Schedule = new HashSet<Schedule>();
            Student = new HashSet<Student>();
        }

        public int IdLevel { get; set; }
        public int Year { get; set; }
        public string Department { get; set; }

        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}

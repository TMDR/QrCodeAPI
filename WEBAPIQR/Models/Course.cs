using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Course
    {

        public string IdCourse { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int TotalDuration { get; set; }

        public virtual ICollection<Enrollment> Enrollment { get; set; }
        public virtual ICollection<Instruction> Instruction { get; set; }
        public virtual ICollection<Studentattendance> Studentattendance { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollment = new HashSet<Enrollment>();
            Studentattendance = new HashSet<Studentattendance>();
        }

        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int IdLevel { get; set; }

        public virtual Level IdLevelNavigation { get; set; }
        public virtual Person IdStudentNavigation { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
        public virtual ICollection<Studentattendance> Studentattendance { get; set; }
    }
}

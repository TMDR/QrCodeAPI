using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPIQR.Models
{
    public class responseStudentAttendance
    {
        public int IdStudentAttendance { get; set; }
        public string IdClass { get; set; }
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int IdLevel { get; set; }
        public DateTime Date { get; set; }
        public int Grade { get; set; }
        public string IdCourse { get; set; }

        public responseStudentAttendance(Studentattendance sa,Student s)
        {
            IdStudentAttendance = sa.IdStudentAttendance;
            IdClass = sa.IdClass;
            IdStudent = sa.IdStudent;
            FirstName = s.FirstName;
            LastName = s.LastName;
            Email = s.Email;
            Mobile = s.Mobile;
            IdLevel = s.IdLevel;
            Date = sa.Date;
            Grade = sa.Grade;
            IdCourse = sa.IdCourse;
        }

        public responseStudentAttendance(Studentattendance sa, Student s,DateTime dtSchedule)
        {
            IdStudentAttendance = sa.IdStudentAttendance;
            IdClass = sa.IdClass;
            IdStudent = sa.IdStudent;
            FirstName = s.FirstName;
            LastName = s.LastName;
            Email = s.Email;
            Mobile = s.Mobile;
            IdLevel = s.IdLevel;
            Date = dtSchedule;
            Grade = sa.Grade;
            IdCourse = sa.IdCourse;
        }

        public responseStudentAttendance()
        {
        }
    }
}

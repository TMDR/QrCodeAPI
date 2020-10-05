using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Studentattendance
    {
        public int IdStudentAttendance { get; set; }
        public string IdClass { get; set; }
        public int IdStudent { get; set; }
        public DateTime Date { get; set; }
        public int Grade { get; set; }
        public string IdCourse { get; set; }

        public virtual Class IdClassNavigation { get; set; }
        public virtual Course IdCourseNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }

        public Studentattendance(string idClass, int idStudent, DateTime date, int grade, string idCourse)
        {
            IdClass = idClass;
            IdStudent = idStudent;
            Date = date;
            Grade = grade;
            IdCourse = idCourse;
        }
    }
}

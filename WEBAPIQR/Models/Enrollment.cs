using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Enrollment
    {
        public int Idenrollment { get; set; }
        public string IdCourse { get; set; }
        public int IdStudent { get; set; }

        public virtual Course IdCourseNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Professorattendance
    {
        public int IdProfessorAttendance { get; set; }
        public string IdClass { get; set; }
        public int IdProfessor { get; set; }
        public DateTime Date { get; set; }

        public virtual Class IdClassNavigation { get; set; }
        public virtual Professor IdProfessorNavigation { get; set; }

        public Professorattendance(string idClass, int idProfessor, DateTime date)
        {
            IdClass = idClass;
            IdProfessor = idProfessor;
            Date = date;
        }
    }
}

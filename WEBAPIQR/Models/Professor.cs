using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Professor
    {
        public Professor()
        {
            Instruction = new HashSet<Instruction>();
            Professorattendance = new HashSet<Professorattendance>();
        }

        public int IdProfessor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public virtual Person IdProfessorNavigation { get; set; }
        public virtual ICollection<Instruction> Instruction { get; set; }
        public virtual ICollection<Professorattendance> Professorattendance { get; set; }
    }
}

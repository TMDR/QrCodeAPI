using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Instruction
    {
        public int IdInstruction { get; set; }
        public int IdProfessor { get; set; }
        public string IdCourse { get; set; }

        public virtual Course IdCourseNavigation { get; set; }
        public virtual Professor IdProfessorNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class responseStudent
    {
        
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int IdLevel { get; set; }
        public int type { get; set; }

        public responseStudent(Student s,int type)
        {
            IdStudent = s.IdStudent;
            FirstName = s.FirstName;
            LastName = s.LastName;
            Email = s.Email;
            Mobile = s.Mobile;
            IdLevel = s.IdLevel;
            this.type = type;
        }

        public responseStudent()
        {
        }
    }
}

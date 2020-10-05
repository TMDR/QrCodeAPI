using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class responseProfessor
    {
        public int IdProfessor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int type { get; set; }

        public responseProfessor()
        {
        }

        public responseProfessor(Professor P,int type)
        {
            IdProfessor = P.IdProfessor;
            FirstName = P.FirstName;
            LastName = P.LastName;
            Email = P.Email;
            Mobile = P.Mobile;
            Address = P.Address;
            this.type = type;
        }
    }
}

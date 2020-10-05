using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class responseAdmin
    {

        public int IdPerson { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }

        public responseAdmin()
        {
        }

        public responseAdmin(Person p)
        {
            IdPerson = p.IdPerson;
            UserName = p.UserName;
            Password = p.Password;
            Type = p.Type;
        }
    }
}

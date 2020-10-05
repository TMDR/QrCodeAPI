using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Person
    {
        public int IdPerson { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }

        public virtual Professor Professor { get; set; }
        public virtual Student Student { get; set; }
    }
}

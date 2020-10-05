using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class StringClass
    {
        public string str { get; set; }

        public StringClass()
        {
        }

        public StringClass(string str)
        {
            this.str = str;
        }
    }
}

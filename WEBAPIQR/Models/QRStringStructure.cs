using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public partial class QRStringStructure
    {
        public string idClass { get; set; }
        public string idCourse { get; set; }
        public int idProfessor { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        public QRStringStructure()
        {
        }

        public QRStringStructure(string idClass, string idCourse, int idProfessor, string day, string time)
        {
            this.idClass = idClass;
            this.idCourse = idCourse;
            this.idProfessor = idProfessor;
            Day = day;
            Time = time;
        }
    }
}

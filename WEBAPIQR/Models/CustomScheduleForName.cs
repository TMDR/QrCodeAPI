using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class CustomScheduleForName
    {
        public int IdSchedule { get; set; }
        public string NameClass { get; set; }
        public string IdClass { get; set; }
        public string IdCourse { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string NameCourse { get; set; }
        public int IdLevel { get; set; }

        public CustomScheduleForName(int idSchedule, string nameClass, string idClass, string idCourse, string day, string time, string nameCourse, int idLevel)
        {
            IdSchedule = idSchedule;
            NameClass = nameClass;
            IdClass = idClass;
            IdCourse = idCourse;
            Day = day;
            Time = time;
            NameCourse = nameCourse;
            IdLevel = idLevel;
        }
    }
}

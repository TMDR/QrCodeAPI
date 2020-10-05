using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Schedule
    {
        public int IdSchedule { get; set; }
        public string IdClass { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string IdCourse { get; set; }
        public int IdLevel { get; set; }

        public virtual Class IdClassNavigation { get; set; }
        public virtual Level IdLevelNavigation { get; set; }
    }
}

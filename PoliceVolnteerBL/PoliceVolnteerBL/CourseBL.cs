using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerBL
{
    class CourseBL
    {
        public int CourseCode { get; set; }
        public string CourseName { get; set; }
        public DateTime CourseDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string NameOfInstructor { get; set; }
        public string IsRequeired { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }

    }
}

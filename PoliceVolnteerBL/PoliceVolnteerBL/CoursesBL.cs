using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceVolnteerDAL;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerBL
{
    public class CoursesBL
    {
        public List<CourseBL> CourseList { get; set; }

        public CoursesBL()
        {
            this.CourseList = new List<CourseBL>();
            DataRowCollection drc = CourseDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                CourseList.Add(new CourseBL((int)drc[i]["CourseCode"]));
            }
        }

        /*public ActivitysBL()
        {
            this.ActivityList = new List<ActivityBL>();
            DataRowCollection drc = ActivityDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ActivityList.Add(new ActivityBL((int)drc[i]["ActivityCode"]));
            }
        }*/
    }
}

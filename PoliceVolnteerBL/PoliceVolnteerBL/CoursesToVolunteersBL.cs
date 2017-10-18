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
    public class CoursesToVolunteersBL
    {
        public List<CoursesToVolunteerBL> CoursesToVolunteerList { get; set; }

        public CoursesToVolunteersBL()
        {
            this.CoursesToVolunteerList = new List<CoursesToVolunteerBL>();
            DataRowCollection drc = CoursesToVolunteerDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                CoursesToVolunteerList.Add(new CoursesToVolunteerBL((string)drc[i]["PhoneNumber"], (int)drc[i]["CourseCode"], false));
            }
        }
    }
}

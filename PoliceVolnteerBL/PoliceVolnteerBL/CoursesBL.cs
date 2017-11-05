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

        public int SumOfCoursesInPeriod(DateTime from, DateTime to)
        {
            int sum = 0;
            foreach (CourseBL index in CourseList)
                if (from.CompareTo(index.StartTime) != 1 && to.CompareTo(index.FinishTime) != -1)
                    sum++;
            return sum;
        }

        public int SumOfParticipantsAllInPeriod(DateTime from, DateTime to)
        {
            HashSet<int> courseCodeSetInPeroid = new HashSet<int>();
            foreach (CourseBL index in CourseList)
                if (from.CompareTo(index.StartTime) != 1 && to.CompareTo(index.FinishTime) != -1)
                    courseCodeSetInPeroid.Add(index.CourseCode);

            DataTable coursesToVoluteer = CoursesToVolunteerDAL.GetTable().Tables[0];

            HashSet<string> phoneNumberSet = new HashSet<string>();
            for (int i = 0; i < coursesToVoluteer.Rows.Count; i++)
                if (courseCodeSetInPeroid.Contains((int)coursesToVoluteer.Rows[i]["CourseCode"]))
                    phoneNumberSet.Add((string)coursesToVoluteer.Rows[i]["PhoneNumber"]);

            return phoneNumberSet.Count;
        }

    }
}

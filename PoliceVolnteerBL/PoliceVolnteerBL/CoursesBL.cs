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

        //create CourseList and add all the CourseBL objects
        public CoursesBL()
        {
            this.CourseList = new List<CourseBL>();
            DataRowCollection coursesRows = CourseDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < coursesRows.Count; i++)
            {
                CourseList.Add(new CourseBL((int)coursesRows[i]["CourseCode"]));
            }
        }

        //return the sum of courses in a peroid of time
        public int SumOfCoursesInPeriod(DateTime from, DateTime to)
        {
            int sum = 0;
            foreach (CourseBL index in CourseList)
                if(index.CourseDate >= from && index.CourseDate <= to)
                    sum++;
            return sum;
        }

        //return the sum of all the participants in all the courses in a period of time
        public int SumOfParticipantsInPeriod(DateTime from, DateTime to)
        {
            //add to list all the courses code of courses that were in a period of time
            List<int> courseCodeSetInPeriod = new List<int>();
            foreach (CourseBL index in CourseList)
                if(index.CourseDate >= from && index.CourseDate <= to)
                    courseCodeSetInPeriod.Add(index.CourseCode);

            DataTable coursesToVolunteer = CoursesToVolunteerDAL.GetTable().Tables[0];

            //add to hash set(filter) all the phone number of the volunteer that were in those courses
            HashSet<string> phoneNumberSet = new HashSet<string>();
            for (int i = 0; i < coursesToVolunteer.Rows.Count; i++)
                if (courseCodeSetInPeriod.Contains((int)coursesToVolunteer.Rows[i]["CourseCode"]))
                    phoneNumberSet.Add((string)coursesToVolunteer.Rows[i]["PhoneNumber"]);
            
            //return sum of all the different phone numbers
            return phoneNumberSet.Count;
        }

    }
}

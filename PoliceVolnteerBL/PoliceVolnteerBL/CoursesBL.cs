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
        public DataSet Courses { get; set; }

        /// <summary>
        /// create CourseList and add all the CourseBL objects
        /// </summary>
        public CoursesBL()
        {
            this.Courses = CourseDAL.GetTable();
        }

        /// <summary>
        /// creates an object with all courses from a cetain date
        /// </summary>
        public CoursesBL(DateTime from)
        {
            this.Courses = CourseDAL.GetTable(new FieldValue<CourseField>(CourseField.CourseDate, from, Table.Course, FieldType.Date, OperatorType.Greater));
        }

        /// <summary>
        /// creates an object with all courses from a cetain date to a certain date
        /// </summary>
        public CoursesBL(DateTime from, DateTime to)
        {
            Queue<FieldValue<CourseField>> parameters = new Queue<FieldValue<CourseField>>();
            parameters.Enqueue(new FieldValue<CourseField>(CourseField.CourseDate, from, Table.Course, FieldType.Date, OperatorType.GreaterAndEquals));
            parameters.Enqueue(new FieldValue<CourseField>(CourseField.CourseDate, to, Table.Course, FieldType.Date, OperatorType.Lower));
            this.Courses = CourseDAL.GetTable(parameters, true);
        }

        /// <summary>
        /// creates an object with all courses which correspond to specific parameters
        /// </summary>
        public CoursesBL(Queue<FieldValue<CourseField>> parameters, bool operation)
        {
            this.Courses = CourseDAL.GetTable(parameters, operation);
        }

        /// <summary>
        /// return the sum of courses in a peroid of time
        /// </summary>
        public int SumOfCoursesInPeriod(DateTime from, DateTime to)
        {
            return this.Courses.Tables[0].Rows.Count;
        }

        /// <summary>
        /// return the sum of all the participants in all the courses in a period of time
        /// </summary>
        public int SumOfParticipants(DateTime from, DateTime to)
        {
            //add to list all the courses code of courses that were in a period of time
            List<int> courseCodeSetInPeriod = new List<int>();
            foreach (DataRow row in this.Courses.Tables[0].Rows)
            {
                CourseBL index = new CourseBL(int.Parse(row["CourseCode"].ToString()));
                if (index.CourseDate >= from && index.CourseDate <= to)
                    courseCodeSetInPeriod.Add(index.CourseCode);
            }

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

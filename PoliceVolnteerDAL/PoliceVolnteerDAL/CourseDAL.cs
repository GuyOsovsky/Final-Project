using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum CourseField { CourseCode, CourseName, CourseDate, StartTime, FinishTime, NameOfInstructor, IsRequeired, Place, Description, ValidityCode };

    public class CourseDAL
    {
        public static bool AddCourse(string cCourseName, DateTime cCourseDate, DateTime cStartTime, DateTime cFinishTime, string cNameOfInstructor, string cIsRequeired, string cPlace, string cDescription, int cValidityCode)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Course ([CourseName], [CourseDate], [StartTime], [FinishTime], [NameOfInstructor], [IsRequeired], [Place], [Description], [ValidityCode]) VALUES ('" + cCourseName + "','" + cCourseDate.ToShortDateString() + "','" + cStartTime.ToShortTimeString() + "','" + cFinishTime.ToShortTimeString() + "','" + cNameOfInstructor + "','" + cIsRequeired + "','" + cPlace + "','" + cDescription + "','" + cValidityCode + "')");
                return true;
            }
            catch(Exception e)
            {
                //throw e;
                return false;
            }
        }
        
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Course", "Course");
        }
        
        public static DataSet GetTable(FieldValue<CourseField> fv)
        {
            string SQL = "SELECT * FROM Course WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "Course");
        }

        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<CourseField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM Course WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "Course");
        }
        
        public static bool DelCourse(int courseCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Course WHERE CourseCode=" + courseCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{

    public enum CoursesToVolunteerField { PhoneNumber, CourseCode, status };

    public class CoursesToVolunteerDAL
    {       

        public static bool AddCoursesToVolunteer(string cPhoneNumber, int cCourseCode)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO CoursesToVolunteer ([PhoneNumber], [CourseCode], [status]) VALUES ('" + cPhoneNumber + "','" + cCourseCode + "','0')");
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from CoursesToVolunteer", "CoursesToVolunteer");
        }

        public static DataSet GetTable(FieldValue<CoursesToVolunteerField> fv)
        {
            string SQL = "SELECT * FROM CoursesToVolunteer WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "CoursesToVolunteer");
        }

        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<CoursesToVolunteerField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM CoursesToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "CoursesToVolunteer");
        }
        
        public static bool DelCourse(string phoneNumber, int courseCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM CoursesToVolunteer WHERE CourseCode=" + courseCode + " AND PhoneNumber='" + phoneNumber + "'";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        //change status to true
        public static bool Participated(string phoneNumber, int courseCode)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill("SELECT * FROM CoursesToVolunteer WHERE CourseCode=" + courseCode + " AND PhoneNumber='" + phoneNumber + "'", "CoursesToVolunteer");
                if (ds.Tables["CoursesToVolunteer"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["CoursesToVolunteer"].Rows[0];
                    dr["status"] = "true";
                    OleDbHelper2.update(ds, "SELECT * FROM CoursesToVolunteer", "CoursesToVolunteer");
                }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum ActivityField { ActivityCode, ActivityName, ActivityDate, StartTime, FinishTime, ActivityManager, TypeCode, Place, MinNumberOfVolunteer };

    public class ActivityDAL
    {
        public static bool AddActivity(string aActivityName, DateTime aActivityDate, DateTime aStartTime, DateTime aFinishTime, string aActivityManager, int aTypeCode, string aPlace, int aMinNumberOfVolunteer)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Activity ([ActivityName], [ActivityDate], [StartTime], [FinishTime], [ActivityManager], [TypeCode], [Place], [MinNumberOfVolunteer]) VALUES ('" + aActivityName + "','" + aActivityDate.ToShortDateString() + "','" + aStartTime.ToShortTimeString() + "','" + aFinishTime.ToShortTimeString() + "','" + aActivityManager + "','" + aTypeCode + "','" + aPlace + "','" + aMinNumberOfVolunteer + "')");
                return true;
            }
            catch (Exception e)
            {
                throw e;
                //return false;
            }
        }

        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Activity", "Activity");
        }

        /**/
        public static DataSet GetTable(FieldValue<ActivityField> fv)
        {
            string SQL = "SELECT * FROM Activity WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + Field.ToString() + "]=";
            SQL += "'" + Select + "'";*/
            return OleDbHelper2.Fill(SQL, "Activity");
        }

        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ActivityField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM Activity WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "Activity");
        }

        public static bool DelActivity(int activityCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Activity WHERE ActivityCode=" + activityCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static bool DelAllActivitys()
        {
            try
            {
                string deleteSQL;
                deleteSQL = "DELETE * FROM Activity";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

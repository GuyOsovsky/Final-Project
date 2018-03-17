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
        //Add new activity row to activity table and return state boolean
        public static void AddActivity(string aActivityName, DateTime aActivityDate, DateTime aStartTime, DateTime aFinishTime, string aActivityManager, int aTypeCode, string aPlace, int aMinNumberOfVolunteer)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Activity ([ActivityName], [ActivityDate], [StartTime], [FinishTime], [ActivityManager], [TypeCode], [Place], [MinNumberOfVolunteer]) VALUES ('" + aActivityName + "','" + aActivityDate.ToShortDateString() + "','" + aStartTime.ToShortTimeString() + "','" + aFinishTime.ToShortTimeString() + "','" + aActivityManager + "','" + aTypeCode + "','" + aPlace + "','" + aMinNumberOfVolunteer + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all activity table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select Activity.*,typeName from (Activity INNER JOIN TypeToActivity ON Activity.TypeCode = TypeToActivity.typeCode)", "Activity");
        }

        //get activity table by field and value
        public static DataSet GetTable(FieldValue<ActivityField> fv)
        {
            string SQL = "SELECT Activity.*,typeName FROM (Activity INNER JOIN TypeToActivity ON Activity.TypeCode = TypeToActivity.typeCode) WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "Activity");
        }

        ////get activity table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ActivityField>> qfv, bool Operation)
        {
            string SQL = "SELECT Activity.*,typeName FROM (Activity INNER JOIN TypeToActivity ON Activity.TypeCode = TypeToActivity.typeCode) WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "Activity");
        }

        //delete activity row by activity code(by key) from activity table
        public static void DelActivity(int activityCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Activity WHERE ActivityCode=" + activityCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete all activity rows from activity table
        public static void DelAllActivitys()
        {
            try
            {
                string deleteSQL;
                deleteSQL = "DELETE * FROM Activity";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch(Exception e)
            {
                throw e;
            }

        }
    }
}

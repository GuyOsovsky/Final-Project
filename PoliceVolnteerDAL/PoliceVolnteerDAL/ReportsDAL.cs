using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum ReportsField { PhoneNumber, ReportDate, ActivityCode, Description }

    public class ReportsDAL
    {
        //Add new report row to Reports table and return state boolean
        public static void AddReport(string rPhoneNumber, DateTime rReportDate, int rActivityCode, string rDescription)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Reports ([PhoneNumber], [ReportDate], [ActivityCode], [Description]) VALUES ('" + rPhoneNumber + "','" + rReportDate.ToShortDateString() + "','" + rActivityCode + "','" + rDescription + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all Reports table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select Reports.*, ActivityName, ActivityDate, StartTime, FinishTime, ActivityManager, TypeCode, Place, MinNumberOfVolunteer, EmergencyNumber, FName, LName, BirthDate, UserName, Password, HomeAddress, HomeCity, EmailAddress, ID, status FROM ((Reports INNER JOIN Activity ON Reports.ActivityCode = Activity.ActivityCode) INNER JOIN VolunteerInfo ON Reports.PhoneNumber = VolunteerInfo.PhoneNumber)", "Reports");
        }

        //get Reports table by field and value
        public static DataSet GetTable(FieldValue<ReportsField> fv)
        {
            string SQL = "SELECT Reports.*, ActivityName, ActivityDate, StartTime, FinishTime, ActivityManager, TypeCode, Place, MinNumberOfVolunteer, EmergencyNumber, FName, LName, BirthDate, UserName, Password, HomeAddress, HomeCity, EmailAddress, ID, status FROM ((Reports INNER JOIN Activity ON Reports.ActivityCode = Activity.ActivityCode) INNER JOIN VolunteerInfo ON Reports.PhoneNumber = VolunteerInfo.PhoneNumber) WHERE ";
            bool isDetailed = false;
            for (int i = 0; i < Enum.GetNames(typeof(ActivityField)).Length; i++)
            {
                for(int j = 0; j < Enum.GetNames(typeof(VolunteerInfoDALField)).Length; j++)
                {
                    if (fv.Field.ToString() == ((ActivityField)i).ToString() || fv.Field.ToString() == ((VolunteerInfoDALField)j).ToString())
                    {
                        isDetailed = true;
                        break;
                    }
                }
                if (isDetailed)
                    break;
            }
            if(!isDetailed)
                SQL += fv.ToSql();
            else
            {
                SQL += fv.ToDetailedSql();
            }
            return OleDbHelper2.Fill(SQL, "Reports");
        }

        ////get Reports table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ReportsField>> qfv, bool Operation)
        {
            FieldValue<ReportsField> fv;
            if (qfv.Count == 0)
                return new DataSet();
            bool isDetailed;
            string SQL = "SELECT Reports.*, ActivityName, ActivityDate, StartTime, FinishTime, ActivityManager, TypeCode, Place, MinNumberOfVolunteer, EmergencyNumber, FName, LName, BirthDate, UserName, Password, HomeAddress, HomeCity, EmailAddress, ID, status FROM ((Reports INNER JOIN Activity ON Reports.ActivityCode = Activity.ActivityCode) INNER JOIN VolunteerInfo ON Reports.PhoneNumber = VolunteerInfo.PhoneNumber) WHERE ";
            while (qfv.Count > 1)
            {
                fv = qfv.Dequeue();
                isDetailed = false;
                for (int i = 0; i < Enum.GetNames(typeof(ActivityField)).Length; i++)
                {
                    for (int j = 0; j < Enum.GetNames(typeof(VolunteerInfoDALField)).Length; j++)
                    {
                        if (fv.Field.ToString() == ((ActivityField)i).ToString() || fv.Field.ToString() == ((VolunteerInfoDALField)j).ToString())
                        {
                            isDetailed = true;
                            break;
                        }
                    }
                    if (isDetailed)
                        break;
                }
                if (!isDetailed)
                    SQL += fv.ToSql();
                else
                {
                    SQL += fv.ToDetailedSql();
                }
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            fv = qfv.Dequeue();
            isDetailed = false;
            for (int i = 0; i < Enum.GetNames(typeof(ActivityField)).Length; i++)
            {
                //finish this!!!!!!!!!!!! fucking stupid
                for (int j = 0; j < Enum.GetNames(typeof(VolunteerInfoDALField)).Length; j++)
                {
                    if (fv.Field.ToString() == ((ActivityField)i).ToString() || fv.Field.ToString() == ((VolunteerInfoDALField)j).ToString())
                    {
                        isDetailed = true;
                        break;
                    }
                }
                if (isDetailed)
                    break;
            }
            if (!isDetailed)
                SQL += fv.ToSql();
            else
            {
                SQL += fv.ToDetailedSql();
            }
            return OleDbHelper2.Fill(SQL, "Reports");
        }

        //change field value of report row by phoneNumber and activityCode in Reports table
        public static void UpdateFrom(string phoneNumber, int activityCode, FieldValue<ReportsField> change)
        {
            if (change.Field == ReportsField.ActivityCode || change.Field == ReportsField.PhoneNumber)
                throw new Exception("ActivityCode or PhoneNumber cannot be changed");
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM Reports WHERE PhoneNumber='{0}' AND ActivityCode='{1}'", phoneNumber, activityCode), "Reports");
                if (ds.Tables["Reports"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["Reports"].Rows[0];
                    dr[change.Field.ToString()] = change.Value.ToString();
                    OleDbHelper2.update(ds, "SELECT * FROM Reports", "Reports");
                }
                else
                {
                    throw new ArgumentException("PhoneNumber or ActivityCode not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete report row by phoneNumber code(by key) from Reports table
        public static void DelReport(string phoneNumber, int activityCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Reports WHERE PhoneNumber='" + phoneNumber + "' AND ActivityCode=" + activityCode;
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

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
        public static bool AddReport(string rPhoneNumber, DateTime rReportDate, int rActivityCode, string rDescription)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Reports ([PhoneNumber], [ReportDate], [ActivityCode], [Description]) VALUES ('" + rPhoneNumber + "','" + rReportDate.ToShortDateString() + "','"  + rActivityCode + "','" + rDescription + "')");
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
        }

        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Reports", "Reports");
        }

        public static DataSet GetTable(FieldValue<ReportsField> fv)
        {
            string SQL = "SELECT * FROM Reports WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "Reports");
        }

        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ReportsField>> qfv, bool Operation)
        {
            if (qfv.Count == 0)
                return new DataSet();
            string SQL = "SELECT * FROM Reports WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "Reports");
        }

        public static bool UpdateFrom(string phoneNumber, int activityCode, FieldValue<ReportsField> change)
        {
            if(change.Field == ReportsField.ActivityCode || change.Field == ReportsField.PhoneNumber)
                return false;
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM Reports WHERE PhoneNumber='{0}' AND ActivityCode='{1}'", phoneNumber, activityCode), "Reports");
                if (ds.Tables["Reports"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["Reports"].Rows[0];
                    dr[change.Field.ToString()] = change.Value.ToString();
                    OleDbHelper2.update(ds, "SELECT * FROM Reports", "Reports");
                }
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static bool DelReport(string phoneNumber)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Reports WHERE PhoneNumber='" + phoneNumber + "'";
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

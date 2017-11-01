using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum VolunteerToValidityField { PhoneNumber, ValidityCode, Status, EndDate };

    public class VolunteerToValidityDAL
    {
        public static bool Add(string PhoneNumber, int ValidityCode, DateTime EndDate)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerToValidity ([PhoneNumber], [ValidityCode], [Status], [EndDate]) VALUES ('" + PhoneNumber + "','" + ValidityCode + "','" + "1" + "'," + EndDate.ToOADate() + ")");
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
            return OleDbHelper2.Fill("select * from VolunteerToValidity", "VolunteerToValidity");
        }

        public static DataSet GetTable(FieldValue<VolunteerToValidityField> fv)
        {
            string SQL = "SELECT * FROM VolunteerToValidity WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "VolunteerToValidity");
        }

        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerToValidityField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM VolunteerToValidity WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "VolunteerToValidity");
        }

        //update status to true or false
        public static bool UpdateStatus(bool Status)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerToValidity WHERE Status='{0}'", Status), "VolunteerToValidity");
                if (ds.Tables["VolunteerInfo"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerInfo"].Rows[0];
                    dr["status"] = Status;
                    OleDbHelper2.update(ds, "SELECT * FROM VolunteerToValidity", "VolunteerToValidity");
                }
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        //Dels
    }
}

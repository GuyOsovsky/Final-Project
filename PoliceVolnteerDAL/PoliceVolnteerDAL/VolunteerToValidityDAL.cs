using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum VolunteerToValidityField { PhoneNumber, ValidityCode, EndDate};

    public class VolunteerToValidityDAL
    {
        //Add new volunteer validity row to VolunteerToValidity table and return state boolean
        public static void Add(string PhoneNumber, int ValidityCode, DateTime EndDate)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerToValidity ([PhoneNumber], [ValidityCode], [EndDate]) VALUES ('" + PhoneNumber + "','" + ValidityCode + "'," + EndDate.ToOADate() + ")");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all VolunteerToValidity table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from (VolunteerToValidity INNER JOIN ValidityTypes ON VolunteerToValidity.ValidityCode = ValidityTypes.ValidityCode)", "VolunteerToValidity");
        }

        //get VolunteerToValidity table by field and value
        public static DataSet GetTable(FieldValue<VolunteerToValidityField> fv)
        {
            string SQL = "SELECT * FROM (VolunteerToValidity INNER JOIN ValidityTypes ON VolunteerToValidity.ValidityCode = ValidityTypes.ValidityCode) WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "VolunteerToValidity");
        }

        ////get VolunteerToValidity table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerToValidityField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM (VolunteerToValidity INNER JOIN ValidityTypes ON VolunteerToValidity.ValidityCode = ValidityTypes.ValidityCode) WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "VolunteerToValidity");
        }

        //delete all volunteer validity rows from VolunteerToValidity table
        public static void DelAllValiditys()
        {
            try
            {
                string deleteSQL;
                deleteSQL = "DELETE * FROM VolunteerToValidity";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch(Exception e)
            {
                throw e;
            }

        }
    }
}

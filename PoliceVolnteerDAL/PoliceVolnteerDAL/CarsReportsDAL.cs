using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum CarsReportsField { ShiftCode, CarID, Distance }

    public class CarsReportsDAL
    {        
        public static bool AddCarReport(int cShiftCode, string cCarID, int Distance)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO CarsReports ([ShiftCode], [CarID], [Distance]) VALUES ('" + cShiftCode + "','" + cCarID + "','" + Distance + "')");
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
            return OleDbHelper2.Fill("select * from CarsReports", "CarsReports");
        }

        /**/
        public static DataSet GetTable(FieldValue<CarsReportsField> fv)
        {
            string SQL = "SELECT * FROM CarsReports WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + Field.ToString() + "]=";
            SQL += "'" + Select + "'";*/
            return OleDbHelper2.Fill(SQL, "CarsReports");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<CarsReportsField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM CarsReports WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "CarsReports");
        }

        public static bool DelCarReport(int shiftCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM CarsReports WHERE ShiftCode=" + shiftCode + "";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum ValidityTypesDALField { ValidityCode, ValidityName };

    public class ValidityTypesDAL
    {
        //Add new validity types row to ValidityTypes table and return state boolean
        public static bool AddNewValidity(string ValidityName)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO ValidityTypes ([ValidityName]) VALUES ('" + ValidityName + "')");

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
            return OleDbHelper2.Fill("select * from ValidityTypes", "ValidityTypes");
        }


        public static DataSet GetTable(FieldValue<ValidityTypesDALField> fv)
        {
            string SQL = "SELECT * FROM ValidityTypes WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "ValidityTypes");
        }


        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ValidityTypesDALField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM ValidityTypes WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "ValidityTypes");
        }


        public static bool DelCourse(int validityCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM ValidityTypes WHERE ValidityCode=" + validityCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }


        public static bool DelAll()
        {
            try
            {
                string deleteSQL;
                deleteSQL = "DELETE * FROM ValidityTypes";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public class ValidityTypesDAL
    {
        public enum ValidityTypesDALEnum { ValidityCode, ValidityName};

        public static bool AddNewValidity(string ValidityName)
        {
            try
            {
                //VolunteerInfo table
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

        /**/
        public static DataSet GetTable(FieldValue<ValidityTypesDALEnum> fv)
        {
            string SQL = "SELECT * FROM ValidityTypes WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + field.ToString() + "]=";
            SQL += "'" + select + "'";*/
            return OleDbHelper2.Fill(SQL, "ValidityTypes");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ValidityTypesDALEnum>> qfv, bool Operation)
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

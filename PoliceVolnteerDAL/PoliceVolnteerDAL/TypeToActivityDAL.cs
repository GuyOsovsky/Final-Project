using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum TypeToActivityField { typeCode, typeName }

    public class TypeToActivityDAL
    {
        //Add new type to activity row to TypeToActivity table and return state boolean
        public static void AddTypeToActivity(string tTypeName)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO TypeToActivity ([typeName]) VALUES ('" + tTypeName + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all TypeToActivity table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from TypeToActivity", "TypeToActivity");
        }

        //get TypeToActivity table by field and value
        public static DataSet GetTable(FieldValue<TypeToActivityField> fv)
        {
            string SQL = "SELECT * FROM TypeToActivity WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "TypeToActivity");
        }

        ////get TypeToActivity table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<TypeToActivityField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM TypeToActivity WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "TypeToActivity");
        }

        //delete type to activity row by type code(by key) from TypeToActivity table
        public static void DelTypeToActivity(int typeCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM TypeToActivity WHERE typeCode=" + typeCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

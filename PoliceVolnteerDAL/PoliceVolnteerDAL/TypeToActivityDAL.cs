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
        public static bool AddTypeToActivity(string tTypeName)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO TypeToActivity ([typeName]) VALUES ('" + tTypeName + "')");
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
            return OleDbHelper2.Fill("select * from TypeToActivity", "TypeToActivity");
        }

        public static DataSet GetTable(FieldValue<TypeToActivityField> fv)
        {
            string SQL = "SELECT * FROM TypeToActivity WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "TypeToActivity");
        }

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

        public static bool DelTypeToActivity(int typeCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM TypeToActivity WHERE typeCode=" + typeCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch (Exception e)
            {
                //אין אפשרות למחוק או לשנות את הרשומה מכיוון שהטבלה 'Activity' כוללת רשומות קשורות.
                Console.WriteLine("You can not delete or change the entry from 'Activity' because the table contains related entries.");
                //throw e;
                return false;
            }
        }
    }
}

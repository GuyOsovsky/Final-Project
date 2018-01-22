using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;


namespace PoliceVolnteerDAL
{
    public enum ShiftsTypeField { typeCode, TypeName }
    
    public class ShiftsTypesDAL
    {
        //Add new shift type row to ShiftsType table and return state boolean
        public static void AddShift(string TypeName)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO ShiftsType ([TypeName]) VALUES ('" + TypeName + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all ShiftsType table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from ShiftsType", "ShiftsType");
        }

        //get ShiftsType table by field and value
        public static DataSet GetTable(FieldValue<ShiftsTypeField> fv)
        {
            string SQL = "SELECT * FROM ShiftsType WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "ShiftsType");
        }

        ////get ShiftsType table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ShiftsTypeField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM ShiftsType WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "ShiftsType");
        }

        //delete shift type row by ShiftsType code(by key) from ShiftsType table
        public static void DelShift(int typeCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM ShiftsType WHERE typeCode=" + typeCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete all shift type rows from ShiftsType table
        public static void DelAll()
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM ShiftsType");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

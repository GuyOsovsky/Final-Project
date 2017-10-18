using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;


namespace PoliceVolnteerDAL
{
    public class ShiftsTypesDAL
    {
        public enum ShiftsTypeEnum {typeCode, TypeName}

        public static bool AddShift(string TypeName)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO ShiftsType ([TypeName]) VALUES ('" + TypeName + "')");
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
            return OleDbHelper2.Fill("select * from ShiftsType", "ShiftsType");
        }

        /**/
        public static DataSet GetTable(FieldValue<ShiftsTypeEnum> fv)
        {
            string SQL = "SELECT * FROM ShiftsType WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + Field.ToString() + "]=";
            SQL += "'" + Select + "'";*/
            return OleDbHelper2.Fill(SQL, "ShiftsType");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ShiftsTypeEnum>> qfv, bool Operation)
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

        public static bool DelShift(int typeCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM ShiftsType WHERE typeCode=" + typeCode + "";
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
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM ShiftsType");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum ShiftsToVolunteerField { ShiftCode, PhoneNumber, Comments }
    
    public class ShiftsToVolunteerDAL
    {
        //Add new shifts to volunteer row to ShiftsToVolunteer table and return state boolean
        public static void AddShift(int shiftCode, string PhoneNumber, string comments)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO ShiftsToVolunteer ([shiftCode], [PhoneNumber], [comments]) VALUES ('" + shiftCode + "','" + PhoneNumber + "','" + comments + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all ShiftsToVolunteer table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from ShiftsToVolunteer", "ShiftsToVolunteer");
        }

        //get ShiftsToVolunteer table by field and value
        public static DataSet GetTable(FieldValue<ShiftsToVolunteerField> fv)
        {
            string SQL = "SELECT * FROM ShiftsToVolunteer WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "ShiftsToVolunteer");
        }

        ////get ShiftsToVolunteer table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ShiftsToVolunteerField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM ShiftsToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "ShiftsToVolunteer");
        }

        //change comment in shifts to volunteer row by phoneNumber and shift code(by 2 keys/complex key) from ShiftsToVolunteer table
        public static void UpdateComment(string PhoneNumber, int ShiftCode, string comment)
        {
            try
            {
                string sql = "UPDATE ShiftsToVolunteer SET comments='" + comment + "' WHERE [PhoneNumber]='" + PhoneNumber + "' AND [shiftCode]=" + ShiftCode.ToString();
                OleDbHelper2.DoQuery(sql);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete shift row by shift code(by key) from shift table
        public static void DelShiftToVolunteer(string phoneNumber, int shiftCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM ShiftsToVolunteer WHERE shiftCode=" + shiftCode + " AND PhoneNumber='" + phoneNumber + "'";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete all shifts to volunteer rows from ShiftsToVolunteer table
        public static void DelAll()
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM ShiftsToVolunteer");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

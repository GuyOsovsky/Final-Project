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

        public static bool AddShift(int shiftCode, string PhoneNumber, string comments)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO ShiftsToVolunteer ([shiftCode], [PhoneNumber], [comments]) VALUES ('" + shiftCode + "','" + PhoneNumber + "','" + comments + "')");
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
            return OleDbHelper2.Fill("select * from ShiftsToVolunteer", "ShiftsToVolunteer");
        }

        /**/
        public static DataSet GetTable(FieldValue<ShiftsToVolunteerField> fv)
        {
            string SQL = "SELECT * FROM ShiftsToVolunteer WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + Field.ToString() + "]=";
            SQL += "'" + Select + "'";*/
            return OleDbHelper2.Fill(SQL, "ShiftsToVolunteer");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ShiftsToVolunteerField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM ShiftsToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "ShiftsToVolunteer");
        }

        public static bool DelAll()
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM ShiftsToVolunteer");
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

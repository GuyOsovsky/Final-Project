using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public class StockToVolunteerDAL
    {
        public enum StockToVolunteerEnum { PhoneVolunteer, ItemID, Amount, BorrowDate, ReturnDate };

        public static bool AddTransference(string phoneNumber, int itemID, int Amount, DateTime borrowDate)
        {
            try
            {
                if (StockDAL.AddToStock(itemID, -Amount))
                {
                    OleDbHelper2.ExecuteNonQuery("INSERT INTO StockToVolunteer ([PhoneVolunteer],[ItemID],[Amount], [BorrowDate], [ReturnDate]) VALUES ('" + phoneNumber + "','" + itemID + "','" + Amount + "','" + borrowDate.ToShortDateString() + "','" + new DateTime(1999, 1, 1).ToShortDateString() + "')");
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from StockToVolunteer", "StockToVolunteer");
        }

        public static DataSet GetTable(FieldValue<StockToVolunteerEnum> fv)//string select, StockEnum field)
        {
            string SQL = "SELECT * FROM StockToVolunteer WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "StockToVolunteer");
        }

        public static DataSet GetTable(Queue<FieldValue<StockToVolunteerEnum>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM StockToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "StockToVolunteer");
        }

        public static bool ReturnItem(int TrasferCode)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("UPDATE StockToVolunteer SET ReturnDate=#" + DateTime.Now.ToShortDateString() + "# WHERE TrasferCode=" + TrasferCode + "");
                return true;
            }
            catch(Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static bool DelAll()
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM Stock");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum StockField { ItemID, ItemName, AmountInStock, Recyclable };
    public class StockDAL
    {
        //Add new stock row to Stock table and return state boolean
        public static void AddItemToStock(string iName, int startingNumber, bool Recycable)
        {
            try
            {
                int rec = 0;
                if (Recycable)
                    rec++;
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Stock ([ItemName],[AmountInStock],[Recyclable]) VALUES ('" + iName + "','" + startingNumber + "','" + rec + "')");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //get all Stock table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Stock", "Stock");
        }

        //get Stock table by field and value
        public static DataSet GetTable(FieldValue<StockField> fv)
        {
            string SQL = "SELECT * FROM Stock WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "Stock");
        }

        ////get Stock table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<StockField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM Stock WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "Stock");
        }

        //update number of items in stock row in Stock table
        public static void UpdateStock(int itemID, int amount)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM Stock WHERE ItemID={0}", itemID), "Stock");
                if (ds.Tables["Stock"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["Stock"].Rows[0];
                    if (int.Parse(dr["AmountInStock"].ToString()) + amount >= 0)
                    {
                        dr["AmountInStock"] = int.Parse(dr["AmountInStock"].ToString()) + amount;
                        OleDbHelper2.update(ds, "SELECT * FROM Stock", "Stock");
                    }
                    else
                    {
                        throw new Exception("New amount not valid");
                    }
                }
                else
                {
                    throw new ArgumentException("ItemID not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete stock row by item code(by key) from Stock table
        public static void DelItem(int itemID)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM Stock WHERE ItemID=" + itemID + "");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete all stock rows from Stock table
        public static void DelAll()
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM Stock");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}

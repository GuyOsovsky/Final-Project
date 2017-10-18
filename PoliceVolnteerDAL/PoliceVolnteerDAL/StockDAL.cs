using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public class StockDAL
    {
        public enum StockEnum { ItemID, ItemName, AmountInStock, Recyclable};

        public static bool AddItemToStock(string iName, int startingNumber, bool Recycable)
        {
            try
            {
                int rec = 0;
                if (Recycable)
                    rec++;
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Stock ([ItemName],[AmountInStock],[Recyclable]) VALUES ('" + iName + "','" + startingNumber + "','" + rec + "')");

                return true;
            }
            catch(Exception e)
            {
                //throw e
                return false;
            }
        }

        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Stock", "Stock");
        }

        public static DataSet GetTable(FieldValue<StockEnum> fv)//string select, StockEnum field)
        {
            string SQL = "SELECT * FROM Stock WHERE ";
            SQL += fv.ToString();
            //SQL += "[" + field.ToString() + "]=";
            //if (field == StockEnum.ItemName)
            //    SQL += "'";
            //SQL += select;
            //if (field == StockEnum.ItemName)
            //    SQL += "'";
            return OleDbHelper2.Fill(SQL, "Stock");
        }

        public static DataSet GetTable(Queue<FieldValue<StockEnum>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM Stock WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "Stock");
        }


        public static bool AddToStock(int itemID, int numToAdd)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM Stock WHERE ItemID={0}", itemID), "Stock");
                if (ds.Tables["Stock"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["Stock"].Rows[0];
                    if (int.Parse(dr["AmountInStock"].ToString()) + numToAdd > 0)
                    {
                        dr["AmountInStock"] = int.Parse(dr["AmountInStock"].ToString()) + numToAdd;
                        OleDbHelper2.update(ds, "SELECT * FROM Stock", "Stock");
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }

            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static bool DelItem(int itemID)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM Stock WHERE ItemID=" + itemID + "");
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum StockToVolunteerField { TransferCode, PhoneVolunteer, ItemID, Amount, BorrowDate, ReturnDate };

    public class StockToVolunteerDAL
    {
        //Add new stock to volunteer row to StockToVolunteer table and return state boolean
        public static void AddTransference(string phoneNumber, int itemID, int amount, DateTime borrowDate)
        {
            try
            {
                StockDAL.UpdateStock(itemID, -amount);
                OleDbHelper2.ExecuteNonQuery("INSERT INTO StockToVolunteer ([PhoneVolunteer],[ItemID],[Amount], [BorrowDate], [ReturnDate]) VALUES ('" + phoneNumber + "','" + itemID + "','" + amount + "','" + borrowDate.ToShortDateString() + "','" + new DateTime(1999, 1, 1).ToShortDateString() + "')");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //get all StockToVolunteer table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from StockToVolunteer", "StockToVolunteer");
        }

        //get StockToVolunteer table by field and value
        public static DataSet GetTable(FieldValue<StockToVolunteerField> fv)//string select, StockEnum field)
        {
            string SQL = "SELECT * FROM StockToVolunteer WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "StockToVolunteer");
        }

        ////get StockToVolunteer table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<StockToVolunteerField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM StockToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "StockToVolunteer");
        }

        //change return date value from defult date to today by transfer code(by key) in StockToVolunteer table
        public static void ReturnItem(int TransferCode)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM StockToVolunteer WHERE TransferCode={0}", TransferCode), "StockToVolunteer");
                if (ds.Tables["StockToVolunteer"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["StockToVolunteer"].Rows[0];
                    dr["ReturnDate"] = DateTime.Now.ToShortDateString();
                    OleDbHelper2.update(ds, "SELECT * FROM StockToVolunteer", "StockToVolunteer");
                }
                else
                {
                    throw new ArgumentException("TransferCode not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete all stock to volunteer rows from StockToVolunteer table
        public static void DelAll()
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM StockToVolunteer");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

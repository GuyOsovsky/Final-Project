﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum CarsReportsField { ShiftCode, CarID, Distance }

    public class CarsReportsDAL
    {
        //add new car report row to cars reports table and return state boolean
        public static void AddCarReport(int cShiftCode, string cCarID, double Distance)
        {
            try
            {
                if (Distance < 1 && Distance != 0)
                    Distance *= -1;
                OleDbHelper2.ExecuteNonQuery("INSERT INTO CarsReports ([ShiftCode], [CarID], [Distance]) VALUES ('" + cShiftCode + "','" + cCarID + "','" + Distance + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all cars reports table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from CarsReports", "CarsReports");
        }

        //get cars reports table by field and value
        public static DataSet GetTable(FieldValue<CarsReportsField> fv)
        {
            string SQL = "SELECT * FROM CarsReports WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "CarsReports");
        }

        ////get cars reports table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<CarsReportsField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM CarsReports WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "CarsReports");
        }

        //delete car report row by shift code(by key) from cars reports table
        public static void DelCarReport(int shiftCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM CarsReports WHERE ShiftCode=" + shiftCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

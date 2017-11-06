﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{

    public enum ShiftsField { ShiftCode, TypeCode, DateOfShift, StartTime, FinishTime, Place }

    public class ShiftsDAL
    {
        public static bool AddShift(int typeCode, DateTime dateshift, DateTime startTime, DateTime finishTime, string place)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Shifts ([TypeCode], [DateOfShift], [StartTime], [FinishTime], [Place]) VALUES ('" + typeCode + "'," + dateshift.ToOADate() + "," + startTime.ToOADate() + "," + finishTime.ToOADate() + ",'" + place + "')");
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
        }
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Shifts", "Shifts");
        }

        public static DataSet GetTable(FieldValue<ShiftsField> fv)
        {
            string SQL = "SELECT * FROM Shifts WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "Shifts");
        }

        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ShiftsField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM Shifts WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "Shifts");
        }

        public static bool DelShift(int shiftCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Shifts WHERE ShiftCode=" + shiftCode + "";
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
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM Shifts");
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

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
        //Add new shift row to Shifts table and return state boolean
        public static void AddShift(int typeCode, DateTime dateshift, DateTime startTime, DateTime finishTime, string place)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Shifts ([TypeCode], [DateOfShift], [StartTime], [FinishTime], [Place]) VALUES ('" + typeCode + "'," + dateshift.ToOADate() + "," + startTime.ToOADate() + "," + finishTime.ToOADate() + ",'" + place + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all Shifts table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Shifts", "Shifts");
        }

        //get Shifts table by field and value
        public static DataSet GetTable(FieldValue<ShiftsField> fv)
        {
            string SQL = "SELECT * FROM Shifts WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "Shifts");
        }

        ////get Shifts table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<ShiftsField>> qfv, bool Operation)
        {
            if (qfv.Count == 0)
                return GetTable(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, -1, Table.Shifts, FieldType.Number, OperatorType.Equals));
            string SQL = "SELECT * FROM Shifts WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "Shifts");
        }

        //delete shift row by shift code(by key) from Shifts table
        public static void DelShift(int shiftCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Shifts WHERE ShiftCode=" + shiftCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete all shift rows from Shifts table
        public static void DelAll()
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("DELETE * FROM Shifts");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

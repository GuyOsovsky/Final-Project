﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum VolunteerPoliceInfoDALField { PhoneNumber, PoliceID, ServeCity, StartDate, Type };

    public class VolunteerPoliceInfoDAL
    {
        //Add new volunteer police row to VolunteerPoliceInfo table and return state boolean
        public static void AddVolunteer(string phoneNumber, string policeID, string serveCity, DateTime startDate, int type)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerPoliceInfo ([PhoneNumber],[PoliceID],[ServeCity],[StartDate],[Type]) VALUES ('" + phoneNumber + "','" + policeID + "','" + serveCity + "','" + startDate.ToShortDateString() + "','" + type + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all VolunteerPoliceInfo table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from VolunteerPoliceInfo", "VolunteerPoliceInfo");
        }

        //get VolunteerPoliceInfo table by field and value
        public static DataSet GetTable(FieldValue<VolunteerPoliceInfoDALField> fv)
        {
            string SQL = "SELECT * FROM VolunteerPoliceInfo WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "VolunteerPoliceInfo");
        }

        ////get VolunteerPoliceInfo table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerPoliceInfoDALField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM VolunteerPoliceInfo WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "VolunteerPoliceInfo");
        }

        //change value of volunteer police row field in VolunteerPoliceInfo table
        public static void UpdateFrom(string UPhoneNumber, FieldValue<VolunteerPoliceInfoDALField> change)
        {
            if (change.Field == VolunteerPoliceInfoDALField.PhoneNumber)
                throw new Exception("PhoneNumber cannot be changed");
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerPoliceInfo WHERE PhoneNumber='{0}'", UPhoneNumber), "VolunteerPoliceInfo");
                if (ds.Tables["VolunteerPoliceInfo"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerPoliceInfo"].Rows[0];
                    dr[change.Field.ToString()] = change.Value.ToString();
                    OleDbHelper2.update(ds, "SELECT * FROM VolunteerPoliceInfo", "VolunteerPoliceInfo");
                }
                else
                {
                    throw new ArgumentException("PhoneNumber not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete volunteer police row by phone number(by key) from VolunteerPoliceInfo table
        public static void DelUser(string vPhoneNumber)
        {
            try
            {
                string deleteSQL = "DELETE * FROM VolunteerPoliceInfo WHERE PhoneNumber='" + vPhoneNumber + "'";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //delete all volunteer police rows from VolunteerPoliceInfo table
        public static void DelAllUsers()
        {
            try
            {
                string deleteSQL = "DELETE * FROM VolunteerPoliceInfo";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

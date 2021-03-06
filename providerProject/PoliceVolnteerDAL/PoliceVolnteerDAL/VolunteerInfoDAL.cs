﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum VolunteerInfoDALField { PhoneNumber, EmergencyNumber, FName, LName, BirthDate, UserName, Password, HomeAddress, HomeCity, EmailAddress, ID, status };

    public static class VolunteerInfoDAL
    {
        //a function that adds a new volnteer to the system. the function creates the volunteer in the VolunteerInfo table, VolnteerPoliceInfo table and CarToVolnteer table if nececery.
        public static void AddVolunteer(string vPhoneNumber, string vEmergencyNumber, string vFName, string vLName, DateTime vBirthDate, string vUserName, string vPassword, string vHomeAddress, string vHomeCity, string vEmailAddress, string vID, string PoliceID, string ServeCity, DateTime startDate, int type, string CarID = "")
        {
            bool Info = false, PoliceInfo = false, CarInfo = false;
            try
            {
                //VolunteerInfo table
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerInfo ([PhoneNumber],[EmergencyNumber],[FName],[LName],[BirthDate],[UserName],[Password],[HomeAddress],[HomeCity],[EmailAddress],[ID], [status]) VALUES ('" + vPhoneNumber + "','" + vEmergencyNumber + "','" + vFName + "','" + vLName + "','" + vBirthDate.ToShortDateString() + "','" + vUserName + "','" + vPassword + "','" + vHomeAddress + "','" + vHomeCity + "','" + vEmailAddress + "','" + vID + "','" + "1" + "')");
                Info = true;
                //VolnteerPoliceInfo table
                VolunteerPoliceInfoDAL.AddVolunteer(vPhoneNumber, PoliceID, ServeCity, startDate, type);
                PoliceInfo = true;
                if (CarID != "")
                {
                    //CarToVolnteer table, if nececery
                    CarToVolunteerDAL.AddCar(vPhoneNumber, CarID);
                    CarInfo = true;
                }
            }
            catch(Exception e)
            {
                if (CarInfo)
                {
                    CarToVolunteerDAL.DelCar(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, vPhoneNumber,Table.CarToVolunteer, FieldType.String, OperatorType.Equals));
                }
                if (PoliceInfo)
                {
                    VolunteerPoliceInfoDAL.DelUser(vPhoneNumber);
                }
                if (Info)
                {
                    DelUser(vPhoneNumber);
                }
                throw e;
            }
        }

        //get all VolunteerInfo table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from VolunteerInfo", "VolunteerInfo");
        }

        //get VolunteerInfo table by field and value
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="PoliceInfo">whether or not to join the police info</param>
        /// <returns></returns>
        public static DataSet GetTable(FieldValue<VolunteerInfoDALField> fv, bool PoliceInfo)
        {
            string SQL = "SELECT * FROM VolunteerInfo "; // fix this shit
            //if (PoliceInfo)
            //{
            //    SQL += "FULL OUTER JOIN VolunteerPoliceInfo ON VolunteerInfo.PhoneNumber=VolunteerPoliceInfo.PhoneNumber ";
            //}
            SQL += "WHERE " + fv.ToSql();
            return OleDbHelper2.Fill(SQL, "VolunteerInfo");
        }

        ////get VolunteerInfo table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerInfoDALField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM VolunteerInfo WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if(Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "VolunteerInfo");
        }

        //change value of volunteer row field in VolunteerInfo table
        public static void UpdateFrom(string UPhoneNumber, FieldValue<VolunteerInfoDALField> change)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerInfo WHERE PhoneNumber='{0}'", UPhoneNumber), "VolunteerInfo");
                if (ds.Tables["VolunteerInfo"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerInfo"].Rows[0];
                    dr[change.Field.ToString()] = change.Value.ToString();
                    OleDbHelper2.update(ds, "SELECT * FROM VolunteerInfo", "VolunteerInfo");
                }
                else
                {
                    throw new ArgumentException("PhoneNumber not valid");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //delete volunteer row by phone number(by key) from VolunteerInfo table, VolnteerPoliceInfo table and CarToVolnteer table
        public static void DelUser(string UPhoneNumber)
        {
            try
            {
                string deleteSQL;
                VolunteerPoliceInfoDAL.DelUser(UPhoneNumber);
                CarToVolunteerDAL.DelCar(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, UPhoneNumber, Table.CarToVolunteer, FieldType.String, OperatorType.Equals));
                deleteSQL = "DELETE * FROM VolunteerInfo WHERE PhoneNumber='" + UPhoneNumber + "'";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //delete all volunteer rows from VolunteerInfo table, VolnteerPoliceInfo table and CarToVolnteer table
        public static void DelAllUsers()
        {
            try
            {
                string deleteSQL;
                VolunteerPoliceInfoDAL.DelAllUsers();
                CarToVolunteerDAL.DelAllCars();
                deleteSQL = "DELETE * FROM VolunteerInfo";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}

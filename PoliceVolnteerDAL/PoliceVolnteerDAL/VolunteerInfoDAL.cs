using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public static class VolunteerInfoDAL
    {
        //public enum FormEnum { ePhoneNumber, eEmergencyNumber, eFName, eLName, eBirthDate, eUserName, ePassword, eHomeAddress, eHomeCity, eEmailAddress, eID };
        public enum VolunteerInfoDALEnum { PhoneNumber, EmergencyNumber, FName, LName, BirthDate, UserName, Password, HomeAddress, HomeCity, EmailAddress, ID, status };

        //a function that adds a new volnteer to the system. the function creates the volnteer in the VolunteerInfo table, VolnteerPoliceInfo table and CarToVolnteer table.
        public static bool AddVolunteer(string vPhoneNumber, string vEmergencyNumber, string vFName, string vLName, DateTime vBirthDate, string vUserName, string vPassword, string vHomeAddress, string vHomeCity, string vEmailAddress, string vID, string PoliceID, string ServeCity, DateTime startDate, int type, string CarID = "")
        {
            try
            {
                //VolunteerInfo table
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerInfo ([PhoneNumber],[EmergencyNumber],[FName],[LName],[BirthDate],[UserName],[Password],[HomeAddress],[HomeCity],[EmailAddress],[ID], [status]) VALUES ('" + vPhoneNumber + "','" + vEmergencyNumber + "','" + vFName + "','" + vLName + "','" + vBirthDate.ToShortDateString() + "','" + vUserName + "','" + vPassword + "','" + vHomeAddress + "','" + vHomeCity + "','" + vEmailAddress + "','" + vID + "','" + "1" + "')");

                //VolnteerPoliceInfo table
                VolunteerPoliceInfoDAL.AddVolnteer(vPhoneNumber, PoliceID, ServeCity, startDate, type);
                if (CarID != "")
                {
                    //CarToVolnteer table, if nececery
                    CarToVolunteerDAL.AddCar(vPhoneNumber, CarID);
                }
                return true;
            }
            catch(Exception e)
            {
                //למחוק את המתנדב אם לא עבד במשטרה!
                throw e;
                return false;
            }
        }

        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from VolunteerInfo", "VolunteerInfo");
        }

        /**/
        public static DataSet GetTable(FieldValue<VolunteerInfoDALEnum> fv)
        {
            string SQL = "SELECT * FROM VolunteerInfo WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + field.ToString() + "]=";
            SQL += "'" + select + "'";*/
            return OleDbHelper2.Fill(SQL, "VolunteerInfo");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerInfoDALEnum>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM VolunteerInfo WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if(Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "VolunteerInfo");
        }



        public static bool UpdateFrom(string UPhoneNumber, VolunteerInfoDALEnum eFrom, string updateStr)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerInfo WHERE PhoneNumber='{0}'", UPhoneNumber), "VolunteerInfo");
                if (ds.Tables["VolunteerInfo"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerInfo"].Rows[0];
                    dr[eFrom.ToString()] = updateStr;
                    OleDbHelper2.update(ds, "SELECT * FROM VolunteerInfo", "VolunteerInfo");
                }
                return true;
            }
            catch(Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static bool DelUser(string UPhoneNumber)
        {
            try
            {
                string deleteSQL;
                VolunteerPoliceInfoDAL.DelUser(UPhoneNumber);
                CarToVolunteerDAL.DelUser(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, UPhoneNumber, 2));
                //CarVolnteerDAL.DelUser(new FieldValue<CarVolnteerDAL.CarVolunteerField>(CarVolnteerDAL.CarVolunteerField.PhoneNumber, UPhoneNumber, 2));
                deleteSQL = "DELETE * FROM VolunteerInfo WHERE PhoneNumber='" + UPhoneNumber + "'";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DelAllUsers()
        {
            try
            {
                string deleteSQL;
                VolunteerPoliceInfoDAL.DelAllUsers();
                CarToVolunteerDAL.DelAllCars();
                deleteSQL = "DELETE * FROM VolunteerInfo";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch(Exception e)
            {
                //throw e;
                return false;
            }
        }
    }
}

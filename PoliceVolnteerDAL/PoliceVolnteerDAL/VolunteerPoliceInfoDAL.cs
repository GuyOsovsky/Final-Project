using System;
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
        public static bool AddVolunteer(string phoneNumber, string policeID, string serveCity, DateTime startDate, int type)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerPoliceInfo ([PhoneNumber],[PoliceID],[ServeCity],[StartDate],[Type]) VALUES ('" + phoneNumber + "','" + policeID + "','" + serveCity + "','" + startDate.ToShortDateString() + "','" + type + "')");
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
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
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "VolunteerPoliceInfo");
        }

        ////get VolunteerPoliceInfo table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerPoliceInfoDALField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM VolunteerPoliceInfo WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "VolunteerPoliceInfo");
        }

        //change value of volunteer police row field in VolunteerPoliceInfo table
        public static bool UpdateFrom(string UPhoneNumber, FieldValue<VolunteerPoliceInfoDALField> change)
        {
            if (change.Field == VolunteerPoliceInfoDALField.PhoneNumber)
                return false;
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerPoliceInfo WHERE PhoneNumber='{0}'", UPhoneNumber), "VolunteerPoliceInfo");
                if (ds.Tables["VolunteerPoliceInfo"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerPoliceInfo"].Rows[0];
                    dr[change.Field.ToString()] = change.Value.ToString();
                    OleDbHelper2.update(ds, "SELECT * FROM VolunteerPoliceInfo", "VolunteerPoliceInfo");
                }
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        //delete volunteer police row by phone number(by key) from VolunteerPoliceInfo table
        public static bool DelUser(string vPhoneNumber)
        {
            try
            {
                string deleteSQL = "DELETE * FROM VolunteerPoliceInfo WHERE PhoneNumber='" + vPhoneNumber + "'";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //delete all volunteer police rows from VolunteerPoliceInfo table
        public static bool DelAllUsers()
        {
            try
            {
                string deleteSQL = "DELETE * FROM VolunteerPoliceInfo";
                OleDbHelper2.DoQuery(deleteSQL);
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

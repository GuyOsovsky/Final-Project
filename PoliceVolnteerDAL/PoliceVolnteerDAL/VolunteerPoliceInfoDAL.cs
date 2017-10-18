using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public class VolunteerPoliceInfoDAL
    {
        public enum VolunteerPoliceInfoDALEnum { PhoneNumber, PoliceID, ServeCity, StartDate, Type};

        public static bool AddVolnteer(string phoneNumber, string policeID, string serveCity, DateTime startDate, int type)
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

        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from VolunteerPoliceInfo", "VolunteerPoliceInfo");
        }

        /**/
        public static DataSet GetTable(FieldValue<VolunteerPoliceInfoDALEnum> fv)
        {
            string SQL = "SELECT * FROM VolunteerPoliceInfo WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + field.ToString() + "]=";
            SQL += "'" + select + "'";*/
            return OleDbHelper2.Fill(SQL, "VolunteerPoliceInfo");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerPoliceInfoDALEnum>> qfv, bool Operation)
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

        public static bool UpdateFrom(string UPhoneNumber, VolunteerPoliceInfoDALEnum eFrom, string updateStr)
        {
            if (eFrom == VolunteerPoliceInfoDALEnum.PhoneNumber)
                return false;
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerPoliceInfo WHERE PhoneNumber='{0}'", UPhoneNumber), "VolunteerPoliceInfo");
                if (ds.Tables["VolunteerPoliceInfo"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerPoliceInfo"].Rows[0];
                    dr[eFrom.ToString()] = updateStr;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum CarVolunteerField { PhoneNumber, CarID };
    
    public class CarToVolunteerDAL
    {
        public static bool AddCar(string vPhoneNumber, string CarID)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO CarToVolunteer ([PhoneNumber],[CarID]) VALUES ('" + vPhoneNumber + "','" + CarID + "')");
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
            return OleDbHelper2.Fill("select * from CarToVolunteer", "CarToVolnteer");
        }

        /**/
        public static DataSet GetTable(FieldValue<CarVolunteerField> fv)
        {
            string SQL = "SELECT * FROM CarToVolunteer WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + field.ToString() + "]=";
            SQL += "'" + select + "'";*/
            return OleDbHelper2.Fill(SQL, "CarToVolunteer");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<CarVolunteerField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM CarToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "CarToVolunteer");
        }

        public static bool DelUser(FieldValue<CarVolunteerField> change)
        {
            try
            {
                string deleteSQL = "DELETE * FROM CarToVolunteer WHERE " + change.ToString() + "'";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DelAllCars()
        {
            try
            {
                string deleteSQL = "DELETE * FROM CarToVolunteer";
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

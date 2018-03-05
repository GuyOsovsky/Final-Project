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
        //Add new car row to CarToVolunteer table and return state boolean
        public static void AddCar(string vPhoneNumber, string CarID)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO CarToVolunteer ([PhoneNumber],[CarID]) VALUES ('" + vPhoneNumber + "','" + CarID + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all CarToVolunteer table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from CarToVolunteer", "CarToVolnteer");
        }

        //get CarToVolunteer table by field and value
        public static DataSet GetTable(FieldValue<CarVolunteerField> fv)
        {
            string SQL = "SELECT * FROM CarToVolunteer WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "CarToVolunteer");
        }

        ////get CarToVolunteer table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<CarVolunteerField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM CarToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "CarToVolunteer");
        }

        //delete car row by field and value
        public static void DelCar(FieldValue<CarVolunteerField> del)
        {
            try
            {
                string deleteSQL = "DELETE * FROM CarToVolunteer WHERE " + del.ToSql();
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //delete all car rows from CarToVolunteer table
        public static void DelAllCars()
        {
            try
            {
                string deleteSQL = "DELETE * FROM CarToVolunteer";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

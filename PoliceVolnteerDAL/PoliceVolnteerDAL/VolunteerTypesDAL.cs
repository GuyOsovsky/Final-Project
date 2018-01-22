using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum VolunteerTypesField { TypeCode, TypeName, PermmisionShifts, PermmisionActivity, PermmisionStock, Independent }

    public class VolunteerTypesDAL
    {
        //Add new volunteer type row to VolunteerTypes table and return state boolean
        public static void AddVolunteerType(string vTypeName, bool vPermmisionShifts, bool vPermmisionActivity, bool vPermmisionStock, bool vIndependent)
        {
            string sPermmisionShifts, sPermmisionActivity, sPermmisionStock, sIndependent;
            sPermmisionShifts = (vPermmisionShifts) ? "1" : "0";
            sPermmisionActivity = (vPermmisionActivity) ? "1" : "0";
            sPermmisionStock = (vPermmisionStock) ? "1" : "0";
            sIndependent = (vIndependent) ? "1" : "0";
            
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerTypes ([TypeName], [PermmisionShifts], [PermmisionActivity], [PermmisionStock], [Independent]) VALUES ('" + vTypeName + "','" + sPermmisionShifts + "','" + sPermmisionActivity + "','" + sPermmisionStock + "','" + sIndependent + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all VolunteerTypes table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from VolunteerTypes", "VolunteerTypes");
        }

        //get VolunteerTypes table by field and value
        public static DataSet GetTable(FieldValue<VolunteerTypesField> fv)
        {
            string SQL = "SELECT * FROM VolunteerTypes WHERE ";
            SQL += fv.ToString();
            return OleDbHelper2.Fill(SQL, "VolunteerTypes");
        }

        ////get VolunteerTypes table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerTypesField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM VolunteerTypes WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "VolunteerTypes");
        }

        //delete volunteer type row by type code(by key) from VolunteerTypes table
        public static void DelVolunteerType(int TypeCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM VolunteerTypes WHERE TypeCode=" + TypeCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //change value of volunteer type row field in VolunteerTypes table
        public static void UpdateFrom(int TypeCode, VolunteerTypesField eFrom, string updateStr)
        {
            if (eFrom == VolunteerTypesField.TypeCode)
                throw new Exception("TypeCode cannot be changed");
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerTypes WHERE TypeCode={0}", TypeCode), "VolunteerTypes");
                if (ds.Tables["VolunteerTypes"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerTypes"].Rows[0];
                    dr[eFrom.ToString()] = updateStr;
                    OleDbHelper2.update(ds, "SELECT * FROM VolunteerTypes", "VolunteerTypes");
                }
                else
                {
                    throw new ArgumentException("TypeCode not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

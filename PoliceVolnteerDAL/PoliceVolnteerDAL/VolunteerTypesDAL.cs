using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public class VolunteerTypesDAL
    {
        public enum VolunteerTypesEnum { TypeCode, TypeName, PermmisionShifts, PermmisionActivity, PermmisionStock, Independent }
        
        public static bool AddVolunteerType(string vTypeName, bool vPermmisionShifts, bool vPermmisionActivity, bool vPermmisionStock, bool vIndependent)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO VolunteerTypes ([TypeName], [PermmisionShifts], [PermmisionActivity], [PermmisionStock], [Independent]) VALUES ('" + vTypeName + "','" + vPermmisionShifts + "','" + vPermmisionActivity + "','" + vPermmisionStock + "','" + vIndependent + "')");
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }
        
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from VolunteerTypes", "VolunteerTypes");
        }

        /**/
        public static DataSet GetTable(FieldValue<VolunteerTypesEnum> fv)
        {
            string SQL = "SELECT * FROM VolunteerTypes WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + Field.ToString() + "]=";
            SQL += "'" + Select + "'";*/
            return OleDbHelper2.Fill(SQL, "VolunteerTypes");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<VolunteerTypesEnum>> qfv, bool Operation)
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

        public static bool DelVolunteerType(int TypeCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM VolunteerTypes WHERE TypeCode=" + TypeCode + "";
                OleDbHelper2.DoQuery(deleteSQL);
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static bool UpdateFrom(int TypeCode, VolunteerTypesEnum eFrom, string updateStr)
        {
            if (eFrom == VolunteerTypesEnum.TypeCode)
                return false;
            try
            {
                DataSet ds = OleDbHelper2.Fill(string.Format("SELECT * FROM VolunteerTypes WHERE TypeCode={0}", TypeCode), "VolunteerTypes");
                if (ds.Tables["VolunteerTypes"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["VolunteerTypes"].Rows[0];
                    dr[eFrom.ToString()] = updateStr;
                    OleDbHelper2.update(ds, "SELECT * FROM VolunteerTypes", "VolunteerTypes");
                }
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

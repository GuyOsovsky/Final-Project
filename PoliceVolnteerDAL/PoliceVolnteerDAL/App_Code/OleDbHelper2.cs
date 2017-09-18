 using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
//////////
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    class OleDbHelper2
    {
            // Disconnected 
            public static DataSet GetDataSet(string strSql)
            {
                OleDbConnection connection = new OleDbConnection(Connect.GetConnectionString());
                OleDbCommand command = new OleDbCommand(strSql, connection);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            // connected 
            public static object ExecuteScalar(string strSql)// מיועד לפעולות שמחזירות נתון בודד
            {
                OleDbConnection connection = new OleDbConnection(Connect.GetConnectionString());
                OleDbCommand command = new OleDbCommand(strSql, connection);
                connection.Open();
                object obj = command.ExecuteScalar();
                connection.Close();
                return obj;
            }
            // connected 
            static public int ExecuteNonQuery(string strSql)// INSERT UPDATE DELETE מחזיר את מס השורות שהושפעו ע"י הפעולה
            {
                OleDbConnection connection = new OleDbConnection(Connect.GetConnectionString());
                OleDbCommand command = new OleDbCommand(strSql, connection);
                //try
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected; 
                //catch  אם יש EXP אין RETURN
            }
            // Disconnected     
            static public DataSet Fill(string com, string tableName)// שם לוגי לטבלה שתווצר בתוך הDS tableName
        {
            OleDbConnection cn = new OleDbConnection(Connect.GetConnectionString());
            OleDbCommand command = new OleDbCommand();
            command.Connection = cn;
            command.CommandText = com;
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            try
            {
                adapter.Fill(ds, tableName);
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
            }
            return ds;

        }
       //פעולה המעדכנת את הדטהבייס בהתאם לדטהסט
        public static void update(DataSet ds, string com, string name)
        {
            OleDbConnection cn = new OleDbConnection(Connect.GetConnectionString());
            OleDbCommand command = new OleDbCommand();
            command.Connection = cn;
            command.CommandText = com;

            OleDbDataAdapter adapter = new OleDbDataAdapter(command);

            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            try
            {
                cn.Open();
                adapter.Update(ds, name);
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }
        }
        //Connected
        public static int DoQuery(string sql)
        //הפעולה מקבלת שם מסד נתונים ומחרוזת מחיקה/ הוספה/ עדכון
        //ומבצעת את הפקודה על המסד הפיזי
        {
            OleDbConnection conn =new OleDbConnection(Connect.GetConnectionString());
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            int res = com.ExecuteNonQuery();
            conn.Close();
            return res; //מספר השורות שהושפעו
        }

/// <summary>
///  FULLCONNECTION ב READER עבודה עם  
///  מחזיר הפניה לטבלה הפיסית 
/// </summary>
/// <returns></returns>
        public static OleDbDataReader getReader()
        {
            OleDbConnection conn = new OleDbConnection(Connect.GetConnectionString());
            OleDbCommand command = new OleDbCommand("select * from TeachersTbl", conn);

            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            return reader;
            //  אלה שישתמשו יעבדו כך בקוד
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader[0].ToString());
            //}
            //reader.Close();
        }
    
      
    }
}

      



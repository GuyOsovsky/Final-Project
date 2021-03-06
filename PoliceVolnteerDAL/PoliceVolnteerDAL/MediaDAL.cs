﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public enum MediaField { FileName, ActivityCode, FileType }

    public class MediaDAL
    {
        //Add new media row to Media table and return state boolean
        public static void AddMedia(string mFileName, int mActivityCode, int mFileType)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Media ([FileName], [ActivityCode], [FileType]) VALUES ('" + mFileName + "','" + mActivityCode + "','" + mFileType + "')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all Media table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from Media", "Media");
        }

        //get Media table by field and value
        public static DataSet GetTable(FieldValue<MediaField> fv)
        {
            string SQL = "SELECT * FROM Media WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "Media");
        }

        ////get Media table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<MediaField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM Media WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "Media");
        }

        //לשנות בהזדמנות למפתח מורכב
        public static void DelMedia(string fileName)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Media WHERE FileName='" + fileName + "'";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

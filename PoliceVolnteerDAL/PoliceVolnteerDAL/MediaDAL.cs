﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public class MediaDAL
    {
        public enum MediaEnum { FileName, ActivityCode, filePath, FileType }

        public static bool AddMedia(string mFileName, int mActivityCode, string mFilePath, int mFileType)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO Media ([FileName], [ActivityCode], [filePath], [FileType]) VALUES ('" + mFileName + "','" + mActivityCode + "','" + mFilePath + "','" + mFileType + "')");
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
            return OleDbHelper2.Fill("select * from Media", "Media");
        }

        /**/
        public static DataSet GetTable(FieldValue<MediaEnum> fv)
        {
            string SQL = "SELECT * FROM Media WHERE ";
            SQL += fv.ToString();
            /*SQL += "[" + Field.ToString() + "]=";
            SQL += "'" + Select + "'";*/
            return OleDbHelper2.Fill(SQL, "Media");
        }

        /// <summary>
        /// the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<MediaEnum>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM Media WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToString();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToString();
            return OleDbHelper2.Fill(SQL, "Media");
        }

        public static bool DelMedia(string fileName)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM Media WHERE FileName='" + fileName + "'";
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{

    public enum CoursesToVolunteerField { PhoneNumber, CourseCode, status };

    public class CoursesToVolunteerDAL
    {
        //Add new course to volunteer row to CoursesToVolunteer table and return state boolean
        public static void AddCoursesToVolunteer(string cPhoneNumber, int cCourseCode)
        {
            try
            {
                OleDbHelper2.ExecuteNonQuery("INSERT INTO CoursesToVolunteer ([PhoneNumber], [CourseCode], [status]) VALUES ('" + cPhoneNumber + "','" + cCourseCode + "','0')");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //get all CoursesToVolunteer table
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from CoursesToVolunteer", "CoursesToVolunteer");
        }

        //get CoursesToVolunteer table by field and value
        public static DataSet GetTable(FieldValue<CoursesToVolunteerField> fv)
        {
            string SQL = "SELECT * FROM CoursesToVolunteer WHERE ";
            SQL += fv.ToSql();
            return OleDbHelper2.Fill(SQL, "CoursesToVolunteer");
        }

        ////get CoursesToVolunteer table by queue of fields and values
        /// <summary>the operation parameter True is for and, False is for or</summary>
        public static DataSet GetTable(Queue<FieldValue<CoursesToVolunteerField>> qfv, bool Operation)
        {
            string SQL = "SELECT * FROM CoursesToVolunteer WHERE ";
            while (qfv.Count > 1)
            {
                SQL += qfv.Dequeue().ToSql();
                if (Operation)
                    SQL += " AND ";
                else
                    SQL += " OR ";
            }
            SQL += qfv.Dequeue().ToSql();
            return OleDbHelper2.Fill(SQL, "CoursesToVolunteer");
        }

        //delete course to volunteer row by phoneNumber and course code(by 2 keys/complex key) from CoursesToVolunteer table
        public static void DelCourse(string phoneNumber, int courseCode)
        {
            string deleteSQL;
            try
            {
                deleteSQL = "DELETE * FROM CoursesToVolunteer WHERE CourseCode=" + courseCode + " AND PhoneNumber='" + phoneNumber + "'";
                OleDbHelper2.DoQuery(deleteSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //change status of course to volunteer row to true in CoursesToVolunteer table by phoneNumber and course code(by 2 keys/complex key)
        public static void Participated(string phoneNumber, int courseCode)
        {
            try
            {
                DataSet ds = OleDbHelper2.Fill("SELECT * FROM CoursesToVolunteer WHERE CourseCode=" + courseCode + " AND PhoneNumber='" + phoneNumber + "'", "CoursesToVolunteer");
                if (ds.Tables["CoursesToVolunteer"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["CoursesToVolunteer"].Rows[0];
                    dr["status"] = "true";
                    OleDbHelper2.update(ds, "SELECT * FROM CoursesToVolunteer", "CoursesToVolunteer");
                }
                else
                {
                    throw new ArgumentException("CourseCode and PhoneNumber not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

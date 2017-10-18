using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceVolnteerDAL;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerBL
{
    public class CourseBL
    {
        public int CourseCode { get; set; }
        public string CourseName { get; set; }
        public DateTime CourseDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string NameOfInstructor { get; set; }
        public bool IsRequeired { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public int ValidityCode { get; set; }

        public CourseBL(string CourseName, DateTime CourseDate, DateTime StartTime, DateTime FinishTime, string NameOfInstruc, bool IsRequeired, string Place, string Description, int ValidityCode)
        {
            this.CourseName = CourseName;
            this.CourseDate = CourseDate;
            this.StartTime = StartTime;
            this.FinishTime = FinishTime;
            this.NameOfInstructor = NameOfInstruc;
            this.IsRequeired = IsRequeired;
            string isRequeired;
            if(IsRequeired)
                isRequeired = "1";
            else
                isRequeired = "0";
            this.Place = Place;
            this.Description = Description;
            CourseDAL.AddCourse(CourseName, CourseDate, StartTime, FinishTime, NameOfInstruc, isRequeired, Place, Description, ValidityCode);
            this.CourseCode = (int)CourseDAL.GetTable().Tables[0].Rows[CourseDAL.GetTable().Tables[0].Rows.Count - 1]["CourseCode"];
            this.ValidityCode = ValidityCode;
        }

        public CourseBL(int CourseCode)
        {
            DataSet ds = CourseDAL.GetTable(new FieldValue<CourseFiled>(CourseFiled.CourseCode, CourseCode.ToString(), 1));
            this.CourseCode = CourseCode;
            this.CourseName = (string)ds.Tables[0].Rows[0]["CourseName"];
            this.CourseDate = (DateTime)ds.Tables[0].Rows[0]["CourseDate"];
            this.StartTime = (DateTime)ds.Tables[0].Rows[0]["StartTime"];
            this.FinishTime = (DateTime)ds.Tables[0].Rows[0]["FinishTime"];
            this.NameOfInstructor = (string)ds.Tables[0].Rows[0]["NameOfInstructor"];
            this.IsRequeired = (bool)ds.Tables[0].Rows[0]["IsRequeired"];
            this.Place = (string)ds.Tables[0].Rows[0]["Place"];
            this.Description = (string)ds.Tables[0].Rows[0]["Description"];
            this.ValidityCode = (int)ds.Tables[0].Rows[0]["ValidityCode"];
        }
    }
}
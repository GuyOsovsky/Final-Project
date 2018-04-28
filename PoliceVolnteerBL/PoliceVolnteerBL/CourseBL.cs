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
        public ValidityTypeBL ValidityCode { get; set; }

        /// <summary>
        /// build and adding to database
        /// </summary>
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
            this.ValidityCode = new ValidityTypeBL(ValidityCode);
        }

        /// <summary>
        /// build from the database
        /// </summary>
        public CourseBL(int CourseCode)
        {
            DataSet courseDataSet = CourseDAL.GetTable(new FieldValue<CourseField>(CourseField.CourseCode, CourseCode, Table.Course, FieldType.Number, OperatorType.Equals));
            this.CourseCode = CourseCode;
            this.CourseName = (string)courseDataSet.Tables[0].Rows[0]["CourseName"];
            this.CourseDate = (DateTime)courseDataSet.Tables[0].Rows[0]["CourseDate"];
            this.StartTime = (DateTime)courseDataSet.Tables[0].Rows[0]["StartTime"];
            this.FinishTime = (DateTime)courseDataSet.Tables[0].Rows[0]["FinishTime"];
            this.NameOfInstructor = (string)courseDataSet.Tables[0].Rows[0]["NameOfInstructor"];
            this.IsRequeired = (bool)courseDataSet.Tables[0].Rows[0]["IsRequeired"];
            this.Place = (string)courseDataSet.Tables[0].Rows[0]["Place"];
            this.Description = (string)courseDataSet.Tables[0].Rows[0]["Description"];
            this.ValidityCode = new ValidityTypeBL((int)courseDataSet.Tables[0].Rows[0]["ValidityCode"]);
        }

        /// <summary>
        /// return all the important fields(all fields without CourseCode and ValidityCode)
        /// </summary>
        /// <returns></returns>
        public DataTable GetDetails()
        {
            DataTable detailsTable = CourseDAL.GetTable(new FieldValue<CourseField>(CourseField.CourseCode, CourseCode, Table.Course, FieldType.Number, OperatorType.Equals)).Tables[0];
            //remove minor fields
            detailsTable.Columns.Remove("CourseCode");
            detailsTable.Columns.Remove("ValidityCode");
            return detailsTable;
        }

        /// <summary>
        /// return sum of the participants in this specific course
        /// </summary>
        /// <returns></returns>
        public int SumOfParticipants()
        {
            int sum = 0;

            DataTable coursesToVoluteer = CoursesToVolunteerDAL.GetTable().Tables[0];
            for (int i = 0; i < coursesToVoluteer.Rows.Count; i++)
                if((int)coursesToVoluteer.Rows[i]["CourseCode"] == CourseCode)
                    sum++;
            return sum;
        }
    }
}
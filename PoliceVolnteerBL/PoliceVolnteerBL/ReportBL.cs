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
    public class ReportBL
    {
        public string PhoneNumber { get; set; }
        public DateTime ReportDate { get; set; }
        public int ActivityCode { get; set; }
        public string Description { get; set; }

        //build and adding to database
        public ReportBL(string PhoneNumber, DateTime ReportDate, int ActivityCode, string Description)
        {
            this.PhoneNumber = PhoneNumber;
            this.ReportDate = ReportDate;
            this.ActivityCode = ActivityCode;
            this.Description = Description;
            ReportsDAL.AddReport(PhoneNumber,ReportDate, ActivityCode, Description);
        }

        //build from the database
        public ReportBL(string PhoneNumber, int activityCode)
        {
            this.PhoneNumber = PhoneNumber;
            Queue<FieldValue<ReportsField>> parameters = new Queue<FieldValue<ReportsField>>();
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.PhoneNumber, PhoneNumber, FieldType.String, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.ActivityCode, activityCode, FieldType.Number, OperatorType.Equals));
            DataSet ds = ReportsDAL.GetTable(parameters, true);
            this.ActivityCode = activityCode;
            this.PhoneNumber = PhoneNumber;
            this.ReportDate = (DateTime)ds.Tables[0].Rows[0]["ReportDate"];
            this.Description = (string)ds.Tables[0].Rows[0]["Description"];
        }

        //return textual report of a specific activiy + the report description
        public string getReport()
        {
            return (new ActivityBL(this.ActivityCode)).GetActivityReport() + this.Description + "\n";
        }
    }
}

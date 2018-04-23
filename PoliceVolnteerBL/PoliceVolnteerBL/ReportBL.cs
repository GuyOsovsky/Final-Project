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
        public string PhoneNumber { get; private set; }
        public DateTime ReportDate { get; private set; }
        public int ActivityCode { get; private set; }
        public string Description { get; private set; }

        /// <summary>
        /// build and adding to database
        /// </summary>
        public ReportBL(string PhoneNumber, DateTime ReportDate, int ActivityCode, string Description)
        {
            this.PhoneNumber = PhoneNumber;
            this.ReportDate = ReportDate;
            this.ActivityCode = ActivityCode;
            this.Description = Description;
            ReportsDAL.AddReport(PhoneNumber, ReportDate, ActivityCode, Description);
        }

        /// <summary>
        /// build from the database
        /// </summary>
        public ReportBL(string PhoneNumber, int activityCode)
        {
            this.PhoneNumber = PhoneNumber;
            Queue<FieldValue<ReportsField>> parameters = new Queue<FieldValue<ReportsField>>();
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.PhoneNumber, PhoneNumber, Table.Reports, FieldType.String, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.ActivityCode, activityCode, Table.Reports, FieldType.Number, OperatorType.Equals));
            DataSet ReportsDataSet = ReportsDAL.GetTable(parameters, true);
            this.ActivityCode = activityCode;
            this.PhoneNumber = PhoneNumber;
            this.ReportDate = (DateTime)ReportsDataSet.Tables[0].Rows[0]["ReportDate"];
            this.Description = (string)ReportsDataSet.Tables[0].Rows[0]["Description"];
        }

        /// <summary>
        /// return textual report of a specific activiy + the report description
        /// </summary>
        public string getReport()
        {
            return (new ActivityBL(this.ActivityCode)).GetActivityReport() + this.Description + "\n";
        }

        /// <summary>
        /// adds a description
        /// </summary>
        public void UpdateDescription(string descriptionContent, DateTime updateDate)
        {
            ReportsDAL.UpdateFrom(this.PhoneNumber, this.ActivityCode, new FieldValue<ReportsField>(ReportsField.Description, descriptionContent, Table.Reports, FieldType.String));
            ReportsDAL.UpdateFrom(this.PhoneNumber, this.ActivityCode, new FieldValue<ReportsField>(ReportsField.ReportDate, updateDate, Table.Reports, FieldType.Date));
        }

        /// <summary>
        /// creates a datarow according to the property
        /// </summary>
        public DataRow ToDataRow()
        {
            Queue<FieldValue<ReportsField>> parameters = new Queue<FieldValue<ReportsField>>();
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, Table.Reports, FieldType.String, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.ActivityCode, this.ActivityCode, Table.Reports, FieldType.Number, OperatorType.Equals));
            return ReportsDAL.GetTable(parameters, true).Tables[0].Rows[0];
        }
    }
}

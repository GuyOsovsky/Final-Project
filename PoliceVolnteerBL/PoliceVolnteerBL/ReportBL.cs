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

        public ReportBL(string PhoneNumber, DateTime ReportDate, int ActivityCode, string Description)
        {
            this.PhoneNumber = PhoneNumber;
            this.ReportDate = ReportDate;
            this.ActivityCode = ActivityCode;
            this.Description = Description;
            ReportsDAL.AddReport(PhoneNumber, ReportDate, ActivityCode, Description);
        }

        public ReportBL(string PhoneNumber)
        {
            this.PhoneNumber = PhoneNumber;
            DataSet ds = ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, PhoneNumber, 2));
            this.ActivityCode = (int)ds.Tables[0].Rows[0]["ActivityCode"];
            this.ReportDate = (DateTime)ds.Tables[0].Rows[0]["ReportDate"];
            this.Description = (string)ds.Tables[0].Rows[0]["Description"];
        }
    }
}

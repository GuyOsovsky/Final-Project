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
    public class ReportsBL
    {
        public DataSet Reports { get;private set; }

        //create ReportList and add ReportBL objects by phone number and activity code
        public ReportsBL(string phoneNumber, int activityCode)
        {
            Queue<FieldValue<ReportsField>> parameters = new Queue<FieldValue<ReportsField>>();
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.PhoneNumber, phoneNumber, Table.Reports, FieldType.String, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<ReportsField>(ReportsField.ActivityCode, activityCode, Table.Reports, FieldType.Number, OperatorType.Equals));
            this.Reports = ReportsDAL.GetTable(parameters, true);
        }

        public ReportsBL(string phoneNumber)
        {
            this.Reports = ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, phoneNumber, Table.Reports, FieldType.String, OperatorType.Equals));
        }

        public ReportsBL(int activityCode)
        {
            this.Reports = ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.ActivityCode, activityCode, Table.Reports, FieldType.String, OperatorType.Equals));
        }

        public ReportsBL()
        {
            this.Reports = ReportsDAL.GetTable();
        }
    }
}

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
        public List<ReportBL> ReportList { get;private set; }

        //create ReportList and add ReportBL objects by phone number and activity code
        public ReportsBL(string phoneNumber = "", int activityCode = 0)
        {
            this.ReportList = new List<ReportBL>();
            DataRowCollection reportRows = ReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < reportRows.Count; i++)
            {
                if ((phoneNumber == "" || (string)reportRows[i]["PhoneNumber"] == phoneNumber) && (activityCode == 0 || (int)reportRows[i]["ActivityCode"] == activityCode))
                    ReportList.Add(new ReportBL((string)reportRows[i]["PhoneNumber"], (int)reportRows[i]["ActivityCode"]));
            }
        }
    }
}

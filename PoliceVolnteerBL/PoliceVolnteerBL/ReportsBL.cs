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
        public ReportsBL(string phoneNumber, int activityCode)
        {
            this.ReportList = new List<ReportBL>();
            DataRowCollection reportRows = ReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < reportRows.Count; i++)
            {
                if (((string)reportRows[i]["PhoneNumber"] == phoneNumber) && ((int)reportRows[i]["ActivityCode"] == activityCode))
                    ReportList.Add(new ReportBL((string)reportRows[i]["PhoneNumber"], (int)reportRows[i]["ActivityCode"]));
            }
        }

        public ReportsBL(string phoneNumber)
        {
            this.ReportList = new List<ReportBL>();
            DataRowCollection reportRows = ReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < reportRows.Count; i++)
            {
                if ((string)reportRows[i]["PhoneNumber"] == phoneNumber)
                    ReportList.Add(new ReportBL((string)reportRows[i]["PhoneNumber"], (int)reportRows[i]["ActivityCode"]));
            }
        }

        public ReportsBL(int activityCode)
        {
            this.ReportList = new List<ReportBL>();
            DataRowCollection reportRows = ReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < reportRows.Count; i++)
            {
                if ((int)reportRows[i]["ActivityCode"] == activityCode)
                    ReportList.Add(new ReportBL((string)reportRows[i]["PhoneNumber"], (int)reportRows[i]["ActivityCode"]));
            }
        }

        public ReportsBL()
        {
            this.ReportList = new List<ReportBL>();
            DataRowCollection reportRows = ReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < reportRows.Count; i++)
            {
                ReportList.Add(new ReportBL((string)reportRows[i]["PhoneNumber"], (int)reportRows[i]["ActivityCode"]));
            }
        }
    }
}

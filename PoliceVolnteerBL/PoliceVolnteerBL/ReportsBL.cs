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
            DataRowCollection drc = ReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                if ((phoneNumber == "" || (string)drc[i]["PhoneNumber"] == phoneNumber) && (activityCode == 0 || (int)drc[i]["ActivityCode"] == activityCode))
                    ReportList.Add(new ReportBL((string)drc[i]["PhoneNumber"], (int)drc[i]["ActivityCode"]));
            }
        }
    }
}

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
        public List<ReportBL> ReportList { get; set; }

        public ReportsBL()
        {
            this.ReportList = new List<ReportBL>();
            DataRowCollection drc = ReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ReportList.Add(new ReportBL((string)drc[i]["PhoneNumber"]));
            }
        }
    }
}

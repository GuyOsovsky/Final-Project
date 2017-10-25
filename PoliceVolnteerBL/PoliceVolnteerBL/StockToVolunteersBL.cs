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
    public class StockToVolunteersBL
    {
        public List<StockToVolunteerBL> StockToVolunteerList { get; set; }

        public StockToVolunteersBL()
        {
            this.StockToVolunteerList = new List<StockToVolunteerBL>();
            DataRowCollection drc = StockToVolunteerDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                StockToVolunteerList.Add(new StockToVolunteerBL((int)drc[i]["TransferCode"]));
            }
        }
    }
}

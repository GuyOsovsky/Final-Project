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
    public class StocksBL
    {
        public List<StockBL> StockList { get; set; }

        public StocksBL()
        {
            this.StockList = new List<StockBL>();
            DataRowCollection drc = StockDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                StockList.Add(new StockBL((int)drc[i]["ItemID"]));
            }
        }
    }
}

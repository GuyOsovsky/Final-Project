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
    public class StockBL
    {
        public List<ItemBL> StockList { get; set; }

        public StockBL()
        {
            this.StockList = new List<ItemBL>();
            DataRowCollection drc = StockDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                StockList.Add(new ItemBL((int)drc[i]["ItemID"]));
            }
        }
        public DataTable GetAllItems()
        {
            return StockDAL.GetTable().Tables[0];
        }

        public DataTable GetAllUnreturnedItems()
        {
            
            VolunteersBL volunteers = new VolunteersBL();
            DataTable ret = volunteers.VolunteerList[0].GetItemsInPossession();
            volunteers.VolunteerList.RemoveAt(0);
            foreach (VolunteerBL volunteer in volunteers.VolunteerList)
            {
                ret.Merge(volunteer.GetItemsInPossession());
            }
            return ret;
        }

        public DataTable GetAllTransference()
        {
            return StockToVolunteerDAL.GetTable().Tables[0];
        }
    }
}

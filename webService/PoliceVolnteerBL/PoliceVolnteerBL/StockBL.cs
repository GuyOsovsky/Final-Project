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

        //create StockList and add all the ItemBL objects
        public StockBL()
        {
            this.StockList = new List<ItemBL>();
            DataRowCollection stockRows = StockDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < stockRows.Count; i++)
            {
                StockList.Add(new ItemBL((int)stockRows[i]["ItemID"]));
            }
        }

        //return all items from stock
        public DataSet GetAllItems()
        {
            return StockDAL.GetTable();
        }

        //return all existing transferences
        public DataTable GetAllTransference()
        {
            return StockToVolunteerDAL.GetTable().Tables[0];
        }
    }
}

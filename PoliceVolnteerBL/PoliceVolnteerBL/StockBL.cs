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
        public DataSet Stock { get; set; }

        /// <summary>
        /// creates an object with all activitys
        /// </summary>
        public StockBL()
        {
            this.Stock = StockDAL.GetTable();
        }

        //return all existing transferences
        public static DataSet GetAllTransference()
        {
            return StockToVolunteerDAL.GetTable();
        }
    }
}

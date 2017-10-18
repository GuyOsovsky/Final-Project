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
    public class CarsReportsBL
    {
        public List<CarReportsBL> CarReportsList { get; set; }

        public CarsReportsBL()
        {
            this.CarReportsList = new List<CarReportsBL>();
            DataRowCollection drc = CarsReportsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                CarReportsList.Add(new CarReportsBL((int)drc[i]["ShiftCode"]));
            }
        }
    }
}

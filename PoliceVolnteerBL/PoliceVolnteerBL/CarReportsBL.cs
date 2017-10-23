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
    public class CarReportsBL
    {
        public int ShiftCode { get; set; }
        public string CarID { get; set; }
        public int Distance { get; set; }

        public CarReportsBL(int ShiftCode, string CarID, int Distance)
        {
            this.CarID = CarID;
            this.Distance = Distance;
            this.ShiftCode = ShiftCode;
            CarsReportsDAL.AddCarReport(ShiftCode, CarID, Distance);
        }

        public CarReportsBL(int ShiftCode)
        {
            DataSet ds = CarsReportsDAL.GetTable(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, ShiftCode.ToString(), FieldType.Number));
            this.CarID = (string)ds.Tables[0].Rows[0]["CarID"];
            this.Distance = (int)ds.Tables[0].Rows[0]["Distance"];
            this.ShiftCode = (int)ds.Tables[0].Rows[0]["ShiftCode"];
        }

    }
}

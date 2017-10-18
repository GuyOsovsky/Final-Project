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
    public class ShiftsToVolunteerBL
    {
        //לבדוק
        public List<ShiftToVolunteerBL> ShiftToVolunteerList { get; set; }

        public ShiftsToVolunteerBL()
        {
            this.ShiftToVolunteerList = new List<ShiftToVolunteerBL>();
            DataRowCollection drc = ShiftsToVolunteerDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ShiftToVolunteerList.Add(new ShiftToVolunteerBL((int)drc[i]["ShiftCode"], (string)drc[i]["PhoneNumber"]));
            }
        }
    }
}

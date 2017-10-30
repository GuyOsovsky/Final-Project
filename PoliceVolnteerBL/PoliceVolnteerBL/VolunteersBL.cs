using PoliceVolnteerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PoliceVolnteerBL
{
    public class VolunteersBL
    {
        public List<VolunteerBL> VolunteerList { get; set; }
        public VolunteersBL()
        {
            this.VolunteerList = new List<VolunteerBL>();

            foreach (DataRow row in VolunteerInfoDAL.GetTable().Tables[0].Rows)
                VolunteerList.Add(new VolunteerBL(row["PhoneNumber"].ToString()));
        }

    }
}

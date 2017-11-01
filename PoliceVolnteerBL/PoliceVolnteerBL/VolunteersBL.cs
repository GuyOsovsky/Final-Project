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

        public int SumOfActivesVolunteers()
        {
            int sum = 0;
            for (int i = 0; i < VolunteerList.Count; i++)
                if (VolunteerList[i].Status)
                    sum++;
            return sum;
        }
    }
}

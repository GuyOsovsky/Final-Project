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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isEmpty">tells the program if the list should be empty or not</param>
        public VolunteersBL(bool isEmpty)
        {
            this.VolunteerList = new List<VolunteerBL>();
            if (!isEmpty)
            {
                foreach (DataRow row in VolunteerInfoDAL.GetTable().Tables[0].Rows)
                    VolunteerList.Add(new VolunteerBL(row["PhoneNumber"].ToString()));
            }
        }


        public void AddVolunteer(VolunteerBL volunteer)
        {
            this.VolunteerList.Add(volunteer);
        }

        public void DelVolunteer(VolunteerBL volunteer)
        {
            this.VolunteerList.Remove(this.VolunteerList.Find(x => x.PhoneNumber.Contains(volunteer.PhoneNumber)));
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

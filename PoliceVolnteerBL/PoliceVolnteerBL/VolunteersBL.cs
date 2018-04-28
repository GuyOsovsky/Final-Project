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
        /// create VolunteerList of VolunteerBL object, empty or full(with all the volunteers that exists)
        /// </summary>
        /// <param name="isEmpty">tells the program if the list should be empty or not</param>
        public VolunteersBL(bool isEmpty)
        {
            this.VolunteerList = new List<VolunteerBL>();
            if (!isEmpty)
            {
                foreach (DataRow row in VolunteerInfoDAL.GetTable().Tables[0].Rows)
                    if(new VolunteerBL(row["PhoneNumber"].ToString()).Status)
                        VolunteerList.Add(new VolunteerBL(row["PhoneNumber"].ToString()));
            }
        }

        /// <summary>
        /// add new volunteer to list
        /// </summary>
        public void AddVolunteer(VolunteerBL volunteer)
        {
            this.VolunteerList.Add(volunteer);
        }

        /// <summary>
        /// delete exist volunteer from list
        /// </summary>
        public void DelVolunteer(VolunteerBL volunteer)
        {
            this.VolunteerList.Remove(this.VolunteerList.Find(x => x.PhoneNumber.Contains(volunteer.PhoneNumber)));
        }

        /// <summary>
        /// return sum of volunteers that active
        /// </summary>
        public int SumOfActivesVolunteers()
        {
            int sum = 0;
            for (int i = 0; i < VolunteerList.Count; i++)
                if (VolunteerList[i].Status)
                    sum++;
            return sum;
        }

        /// <summary>
        /// return table of all the Unreturned items
        /// </summary>
        public DataTable GetTrasfersNotReturned()
        {
            DataTable NotReturnedItems;
            if (VolunteerList.Count > 0)
            {

                NotReturnedItems = VolunteerList[0].GetItemsInPossession().Tables[0];
                for (int i = 1; i < VolunteerList.Count; i++)
                {
                    NotReturnedItems.Merge(VolunteerList[i].GetItemsInPossession().Tables[0]);
                }
                return NotReturnedItems;
            }

            DataTable emptyTable = StockToVolunteerDAL.GetTable().Tables[0];
            emptyTable.Clear();
            return emptyTable;
        }
    }
}

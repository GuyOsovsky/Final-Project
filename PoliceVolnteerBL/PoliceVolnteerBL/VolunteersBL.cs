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

        //create VolunteerList of VolunteerBL object, empty or full(with all the volunteers that exists)
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

        //add new volunteer to list
        public void AddVolunteer(VolunteerBL volunteer)
        {
            this.VolunteerList.Add(volunteer);
        }

        //delete exist volunteer from list
        public void DelVolunteer(VolunteerBL volunteer)
        {
            this.VolunteerList.Remove(this.VolunteerList.Find(x => x.PhoneNumber.Contains(volunteer.PhoneNumber)));
        }

        //return sum of volunteers that active
        public int SumOfActivesVolunteers()
        {
            int sum = 0;
            for (int i = 0; i < VolunteerList.Count; i++)
                if (VolunteerList[i].Status)
                    sum++;
            return sum;
        }

        //לבדוק
        //return table of all the Unreturned items
        public DataTable GetTrasfersNotReturned()
        {
            DataTable NotReturnedItems;
            if (VolunteerList.Count > 0)
            {
                NotReturnedItems = VolunteerList[0].GetItemsInPossession();
                for (int i = 1; i < VolunteerList.Count; i++)
                {
                    NotReturnedItems.Merge(VolunteerList[i].GetItemsInPossession());
                }
                return NotReturnedItems;
            }

            DataTable emptyTable = StockToVolunteerDAL.GetTable().Tables[0];
            emptyTable.Clear();
            return emptyTable;
        }
    }
}

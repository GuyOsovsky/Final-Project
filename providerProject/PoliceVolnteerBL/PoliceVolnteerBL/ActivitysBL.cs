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
    public class ActivitysBL
    {
        public DataSet Activitys { get; set; }

        /// <summary>
        /// creates an object with all activitys
        /// </summary>
        public ActivitysBL()
        {
            this.Activitys = ActivityDAL.GetTable();
        }

        /// <summary>
        /// creates an object with activitys which their date is after the from parameter
        /// </summary>
        /// <param name="from">from which date to start adding activitys to the object</param>
        public ActivitysBL(DateTime from)
        {
            this.Activitys = ActivityDAL.GetTable(new FieldValue<ActivityField>(ActivityField.ActivityDate, from, Table.Activity, FieldType.Date, OperatorType.GreaterAndEquals));
        }
        
        /// <summary>
        /// creates an object with activitys ranging from a date to another date
        /// </summary>
        /// <param name="from">from which date to start adding activitys to the object</param>
        /// <param name="to">to which date to add activitys to the object</param>
        public ActivitysBL(DateTime from, DateTime to)
        {
            Queue<FieldValue<ActivityField>> parameters = new Queue<FieldValue<ActivityField>>();
            parameters.Enqueue(new FieldValue<ActivityField>(ActivityField.ActivityDate, from, Table.Activity, FieldType.Date, OperatorType.GreaterAndEquals));
            parameters.Enqueue(new FieldValue<ActivityField>(ActivityField.ActivityDate, to, Table.Activity, FieldType.Date, OperatorType.Lower));
            this.Activitys = ActivityDAL.GetTable(parameters, true);
        }
        
        /// <summary>
        /// retrun sum of activities that were in a period of time
        /// </summary>
        public int SumOfActivities()
        {
            return this.Activitys.Tables[0].Rows.Count;
        }


    }
}

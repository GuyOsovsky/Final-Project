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
    public class VolunteerTypeBL
    {
        public int TypeCode { get; set; }
        public string TypeName { get; set; }

        //whether or not a volunteer can create new shifts and edit shifts
        public bool PermmisionShifts { get; set; }

        //whether or not a volunteer can create new activities and edit activities
        public bool PermmisionActivity { get; set; }

        //whether or not a volunteer can edit the stock
        public bool PermmisionStock { get; set; }

        //Determines whether this volunteer is able to go alone on car shifts
        public bool Independent { get; set; }

        //whether or not a volunteer can create new courses and edit courses
        public bool PermmisionCourse { get; set; }

        //whether or not a volunteer can edit a volunteer and create a new volunteer
        public bool PermmisionVolunteer { get; set; }

        /// <summary>
        /// build and adding to database
        /// </summary>
        public VolunteerTypeBL(string TypeName, bool PermmisionShifts, bool PermmisionActivity, bool PermmisionStock, bool Independent, bool PermmisionCourse, bool PermmisionVolunteer)
        {
            this.TypeName = TypeName;
            this.PermmisionShifts = PermmisionShifts;
            this.PermmisionActivity = PermmisionActivity;
            this.PermmisionStock = PermmisionStock;
            this.Independent = Independent;
            this.PermmisionCourse = PermmisionCourse;
            this.PermmisionVolunteer = PermmisionVolunteer;
            VolunteerTypesDAL.AddVolunteerType(TypeName, PermmisionShifts, PermmisionActivity, PermmisionStock, Independent, PermmisionCourse, PermmisionVolunteer);
            this.TypeCode = (int)VolunteerTypesDAL.GetTable().Tables[0].Rows[VolunteerTypesDAL.GetTable().Tables[0].Rows.Count - 1]["TypeCode"];
        }

        /// <summary>
        /// build from the database
        /// </summary>
        public VolunteerTypeBL(int TypeCode)
        {
            this.TypeCode = TypeCode;
            DataRow volunteerTypesRow = VolunteerTypesDAL.GetTable(new FieldValue<VolunteerTypesField>(VolunteerTypesField.TypeCode, TypeCode, Table.VolunteerTypes, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.TypeName = (string)volunteerTypesRow["TypeName"];
            this.PermmisionShifts = (bool)volunteerTypesRow["PermmisionShifts"];
            this.PermmisionActivity = (bool)volunteerTypesRow["PermmisionActivity"];
            this.PermmisionStock = (bool)volunteerTypesRow["PermmisionStock"];
            this.Independent = (bool)volunteerTypesRow["Independent"];
            this.PermmisionCourse = (bool)volunteerTypesRow["PermmisionCourse"];
            this.PermmisionVolunteer = (bool)volunteerTypesRow["PermmisionVolunteer"];
        }

        /// <summary>
        /// build from the database
        /// </summary>
        public VolunteerTypeBL(string TypeName)
        {
            this.TypeName = TypeName;
            DataRow volunteerTypesRow = VolunteerTypesDAL.GetTable(new FieldValue<VolunteerTypesField>(VolunteerTypesField.TypeName, TypeName, Table.VolunteerTypes, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            this.TypeName = (string)volunteerTypesRow["TypeName"];
            this.PermmisionShifts = (bool)volunteerTypesRow["PermmisionShifts"];
            this.PermmisionActivity = (bool)volunteerTypesRow["PermmisionActivity"];
            this.PermmisionStock = (bool)volunteerTypesRow["PermmisionStock"];
            this.Independent = (bool)volunteerTypesRow["Independent"];
            this.PermmisionCourse = (bool)volunteerTypesRow["PermmisionCourse"];
            this.PermmisionVolunteer = (bool)volunteerTypesRow["PermmisionVolunteer"];
        }

        /// <summary>
        /// return all the permmisions of this specific volunteer
        /// </summary>
        public DataTable GetPermmisions()
        {
            DataTable allPermmisions = VolunteerTypesDAL.GetTable(new FieldValue<VolunteerTypesField>(VolunteerTypesField.TypeCode, TypeCode, Table.VolunteerTypes, FieldType.Number, OperatorType.Equals)).Tables[0];
            allPermmisions.Columns.Remove("TypeCode");
            return allPermmisions;
        }

    }
}

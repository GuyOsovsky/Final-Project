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
        public bool PermmisionShifts { get; set; }
        public bool PermmisionActivity { get; set; }
        public bool PermmisionStock { get; set; }
        public bool Independent { get; set; }

        public VolunteerTypeBL(string TypeName, bool PermmisionShifts, bool PermmisionActivity, bool PermmisionStock, bool Independent)
        {
            this.TypeName = TypeName;
            this.PermmisionShifts = PermmisionShifts;
            this.PermmisionActivity = PermmisionActivity;
            this.PermmisionStock = PermmisionStock;
            this.Independent = Independent;
            VolunteerTypesDAL.AddVolunteerType(TypeName, PermmisionShifts, PermmisionActivity, PermmisionStock, Independent);
            this.TypeCode = (int)VolunteerTypesDAL.GetTable().Tables[0].Rows[VolunteerTypesDAL.GetTable().Tables[0].Rows.Count - 1]["TypeCode"];
        }

        public VolunteerTypeBL(int TypeCode)
        {
            this.TypeCode = TypeCode;
            DataRow dr = VolunteerTypesDAL.GetTable(new FieldValue<VolunteerTypesField>(VolunteerTypesField.TypeCode, TypeCode.ToString(), FieldType.Number)).Tables[0].Rows[0];
            this.TypeName = (string)dr["TypeName"];
            this.PermmisionShifts = (bool)dr["PermmisionShifts"];
            this.PermmisionActivity = (bool)dr["PermmisionActivity"];
            this.PermmisionStock = (bool)dr["PermmisionStock"];
            this.Independent = (bool)dr["Independent"];
        }
    }
}

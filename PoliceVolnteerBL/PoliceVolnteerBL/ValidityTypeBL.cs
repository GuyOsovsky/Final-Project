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
    public class ValidityTypeBL
    {
        public int ValidityCode { get; set; }
        public string ValidityName { get; set; }
        
        /// <summary>
        /// build and adding to database
        /// </summary>
        public ValidityTypeBL(string ValidityName)
        {
            this.ValidityName = ValidityName;
            ValidityTypesDAL.AddNewValidity(ValidityName);
            this.ValidityCode = (int)ValidityTypesDAL.GetTable().Tables[0].Rows[ValidityTypesDAL.GetTable().Tables[0].Rows.Count - 1]["ValidityCode"]; //(int)ActivityDAL.GetTable().Tables[0].Rows[ActivityDAL.GetTable().Tables[0].Rows.Count - 1]["ActivityCode"];
        }
        
        /// <summary>
        /// build from the database
        /// </summary>
        public ValidityTypeBL(int ValidityCode)
        {
            this.ValidityCode = ValidityCode;
            DataRow validityTypesRow = ValidityTypesDAL.GetTable(new FieldValue<ValidityTypesDALField>(ValidityTypesDALField.ValidityCode, ValidityCode, Table.ValidityTypes, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.ValidityName = (string)validityTypesRow["ValidityName"];
        }

    }
}

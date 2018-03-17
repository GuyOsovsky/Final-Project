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
    public class StockBL
    {
        public DataSet Stock { get; set; }

        /// <summary>
        /// creates an object with all activitys
        /// </summary>
        public StockBL()
        {
            this.Stock = StockDAL.GetTable();
        }

        public int ExistingItemID(string itemName, bool isRecyclable)
        {
            Queue<FieldValue<StockField>> q = new Queue<FieldValue<StockField>>();
            q.Enqueue(new FieldValue<StockField>(StockField.ItemName, itemName, Table.StockToVolunteer, FieldType.String, OperatorType.Equals));
            q.Enqueue(new FieldValue<StockField>(StockField.Recyclable, isRecyclable, Table.StockToVolunteer, FieldType.Boolean, OperatorType.Equals));
            DataSet check = StockDAL.GetTable(q, true);
            if (check.Tables[0].Rows.Count <= 0)
                return -1;
            return (int)check.Tables[0].Rows[0]["ItemID"];
        }

        //return all existing transferences
        public static DataSet GetAllTransference()
        {
            return StockToVolunteerDAL.GetTable();
        }

        public static void AddExistsItems(int itemID, int amount)
        {
            StockDAL.UpdateStock(itemID, amount);
        }
    }
}

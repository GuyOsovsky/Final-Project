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
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public int AmountInStock { get; set; }
        public bool Recycable { get; set; }

        public StockBL(string itemName, int amountInStock, bool recycable)
        {
            StockDAL.AddItemToStock(itemName, amountInStock, recycable);
            this.ItemName = itemName;
            this.AmountInStock = amountInStock;
            this.Recycable = recycable;
            this.ItemID = (int)StockDAL.GetTable(new FieldValue<StockField>(StockField.ItemName, itemName, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["ItemID"];
        }

        public StockBL(int itemID)
        {
            DataRow obj = StockDAL.GetTable(new FieldValue<StockField>(StockField.ItemID, itemID.ToString(), FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.ItemID = itemID;
            this.ItemName = (string)obj["ItemName"];
            this.AmountInStock = (int)obj["AmountInStock"];
            this.Recycable = (bool)obj["Recyclable"];
        }
    }
}

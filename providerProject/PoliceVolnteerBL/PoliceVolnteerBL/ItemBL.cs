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
    public class ItemBL
    {
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public int AmountInStock { get; set; }
        public bool Recycable { get; set; }

        //build and adding to database
        public ItemBL(string itemName, int amountInStock, bool recycable)
        {
            StockDAL.AddItemToStock(itemName, amountInStock, recycable);
            this.ItemName = itemName;
            this.AmountInStock = amountInStock;
            this.Recycable = recycable;
            this.ItemID = (int)StockDAL.GetTable(new FieldValue<StockField>(StockField.ItemName, itemName, Table.Stock, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["ItemID"];
        }

        //build from the database
        public ItemBL(int itemID)
        {
            DataRow stockRow = StockDAL.GetTable(new FieldValue<StockField>(StockField.ItemID, itemID, Table.Stock, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.ItemID = itemID;
            this.ItemName = (string)stockRow["ItemName"];
            this.AmountInStock = (int)stockRow["AmountInStock"];
            this.Recycable = (bool)stockRow["Recyclable"];
        }

    }
}

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
        public string itemName { get; set; }
        public int itemID { get; set; }
        public int amountInStock { get; set; }
        public bool recycable { get; set; }

        //build and adding to database
        public ItemBL(string itemName, int amountInStock, bool recycable)
        {
            StockDAL.AddItemToStock(itemName, amountInStock, recycable);
            this.itemName = itemName;
            this.amountInStock = amountInStock;
            this.recycable = recycable;
            this.itemID = (int)StockDAL.GetTable(new FieldValue<StockField>(StockField.ItemName, itemName, Table.Stock, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["ItemID"];
        }

        //build from the database
        public ItemBL(int itemID)
        {
            DataRow stockRow = StockDAL.GetTable(new FieldValue<StockField>(StockField.ItemID, itemID, Table.Stock, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.itemID = itemID;
            this.itemName = (string)stockRow["ItemName"];
            this.amountInStock = (int)stockRow["AmountInStock"];
            this.recycable = (bool)stockRow["Recyclable"];
        }

        public void BorrowItemFromStock(string phoneNumber, int amount)
        {
            StockToVolunteerDAL.AddTransference(phoneNumber, itemID, amount, DateTime.Now);
        }

        public void ReturnItemToStock(int transferCode, int itemID, int amount)
        {
            StockToVolunteerDAL.ReturnItem(transferCode);
            StockDAL.UpdateStock(itemID, amount);
        }

        static public void DelItem(int itemID)
        {
            StockDAL.DelItem(itemID);
        }
    }
}

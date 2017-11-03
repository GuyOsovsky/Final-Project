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
    public class StockToVolunteerBL
    {
        public int TransferCode { get; set; }
        public string PhoneNumber { get; set; }
        public int ItemID { get; set; }
        public int Amount { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public StockToVolunteerBL(string phoneNumber, int itemID, int amount, DateTime borrowDate)
        {
            borrowDate = DateTime.Parse(borrowDate.ToShortDateString());
            StockToVolunteerDAL.AddTransference(phoneNumber, itemID, amount, borrowDate);
            this.PhoneNumber = phoneNumber;
            this.ItemID = itemID;
            this.Amount = amount;
            this.BorrowDate = borrowDate;
            Queue<FieldValue<StockToVolunteerField>> parameters = new Queue<FieldValue<StockToVolunteerField>>();
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.PhoneVolunteer, phoneNumber, FieldType.String, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.ItemID, itemID.ToString(), FieldType.Number, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.BorrowDate, borrowDate.ToString(), FieldType.DateTime, OperatorType.Equals));
            DataRow obj = StockToVolunteerDAL.GetTable(parameters, true).Tables[0].Rows[0];
            this.TransferCode = (int)obj["TransferCode"];
            this.ReturnDate = DateTime.Parse(obj["BorrowDate"].ToString());
        }

        public StockToVolunteerBL(int transferCode)
        {
            
            DataRow dr = StockToVolunteerDAL.GetTable(new FieldValue<StockToVolunteerField>(StockToVolunteerField.TransferCode, transferCode.ToString(), FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.TransferCode = transferCode;
            this.PhoneNumber = dr["PhoneVolunteer"].ToString();
            this.ItemID = (int)dr["ItemID"];
            this.Amount = (int)dr["Amount"];
            this.BorrowDate = DateTime.Parse(dr["BorrowDate"].ToString());
            this.ReturnDate = DateTime.Parse(dr["ReturnDate"].ToString());
        }
    }
}

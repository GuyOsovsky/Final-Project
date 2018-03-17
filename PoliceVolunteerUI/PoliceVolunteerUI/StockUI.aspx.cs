using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceVolnteerBL;

namespace PoliceVolunteerUI
{
    public partial class StockUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }

            FillItemsToBorrow();
            FillBorrowedItems();

            if ((new VolunteerBL(Session["User"].ToString()).Type.PermmisionStock))
            {
                FillTransferTable();
            }

            if (!IsPostBack)
            {
                FillVolunteerList();
            }
        }

        protected void FillItemsToBorrow()
        {
            StockBL stock = new StockBL();
            DataTable items = (new StockBL()).Stock.Tables[0];
            ItemsToBorrow.DataSource = items.AsEnumerable().Where(row => (int)row["AmountInStock"] > 0).CopyToDataTable();
            ItemsToBorrow.DataBind();
        }

        protected void FillBorrowedItems()
        {
            VolunteerBL user = new VolunteerBL(Session["User"].ToString());
            DataSet items = user.GetItemsInPossession();
            items.Tables[0].Columns.Add("ItemName", typeof(string));
            for (int i = 0; i < items.Tables[0].Rows.Count; i++)
            {
                items.Tables[0].Rows[i]["ItemName"] = (new ItemBL((int)items.Tables[0].Rows[i]["ItemID"])).itemName;
            }
            BorrowedItems.DataSource = items;
            BorrowedItems.DataBind();
        }

        protected void FillTransferTable()
        {
            VolunteerBL volunteer;
            if (VolunteerChooseStock.Text != "")
                volunteer = new VolunteerBL(VolunteerChooseStock.SelectedValue.ToString());
            else
                volunteer = new VolunteerBL("");

            DataSet items = volunteer.GetItems();
            
            //add itemName to table
            items.Tables[0].Columns.Add("ItemName", typeof(string));
            for (int i = 0; i < items.Tables[0].Rows.Count; i++)
            {
                items.Tables[0].Rows[i]["ItemName"] = (new ItemBL((int)items.Tables[0].Rows[i]["itemID"])).itemName;
            }

            items.Tables[0].Columns.Add("ReturnString", typeof(string));
            for (int i = 0; i < items.Tables[0].Rows.Count; i++)
            {
                string date = ((DateTime)items.Tables[0].Rows[i]["ReturnDate"]).ToShortDateString();
                items.Tables[0].Rows[i]["ReturnString"] = (date != "01/01/1999") ? date : "-";
            }

            Transfers.DataSource = new DataView(items.Tables[0]);
            Transfers.DataBind();
        }

        protected void BorrowItem(object sender, EventArgs e)
        {
            GridViewRow row = ItemsToBorrow.Rows[int.Parse(((Button)sender).CommandArgument)];

            int amount = int.Parse((row.FindControl("AmountToBorrow") as TextBox).Text);

            if (amount == 0 || int.Parse(ItemsToBorrow.Rows[row.RowIndex].Cells[2].Text) - amount < 0)
                return;

            ItemBL item = new ItemBL(int.Parse(((Label)row.FindControl("HiddenItemIDColumn")).Text));

            item.BorrowItemFromStock(Session["User"].ToString(), amount);
        }

        protected void DeleteItem(object sender, EventArgs e)
        {
            GridViewRow row = ItemsToBorrow.Rows[int.Parse(((Button)sender).CommandArgument)];
            ItemBL.DelItem(int.Parse(((Label)row.FindControl("HiddenItemIDColumn")).Text));
        }

        protected void ReturnItem(object sender, EventArgs e)
        {
            GridViewRow row = BorrowedItems.Rows[int.Parse(((Button)sender).CommandArgument)];

            int transferCode = int.Parse(((Label)row.FindControl("HiddenTransferCodeColumn")).Text);
            int itemID = int.Parse(((Label)row.FindControl("HiddenItemIDColumn")).Text);

            ItemBL item = new ItemBL(itemID);
            item.ReturnItemToStock(transferCode, itemID, int.Parse(BorrowedItems.Rows[row.RowIndex].Cells[2].Text));
            
        }

        protected bool isAbleStock()
        {
            if (Session["User"].ToString() == "") return false;
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            return volunteer.Type.PermmisionStock;
        }
        
        protected void AddItem(object sender, EventArgs e)
        {
            ItemBL item;

            string itemName = itemNameTextBox.Text;

            int itemID = (new StockBL()).ExistingItemID(itemName, isRecyclableCheckBox.Checked);

            if (Page.IsValid)
            {
                if (itemID != -1)
                {
                    StockBL.AddExistsItems(itemID, int.Parse(itemsAmountTextBox.Text));
                }
                else
                {
                    item = new ItemBL(itemName, int.Parse(itemsAmountTextBox.Text), isRecyclableCheckBox.Checked);
                }
            }
        }

        protected void FillVolunteerList()
        {
            //clear list
            VolunteerChooseStock.Items.Clear();
            //load volunteers
            VolunteersBL volunteers = new VolunteersBL(false);
            //add a blank space
            VolunteerChooseStock.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            //add all volunteers to list
            foreach (VolunteerBL volunteer in volunteers.VolunteerList)
            {
                VolunteerChooseStock.Items.Add(new ListItem(volunteer.FName + " " + volunteer.LName, volunteer.PhoneNumber));
            }
            VolunteerChooseStock.DataBind();
        }

        protected void SetTransferTable(object sender, EventArgs e)
        {
            FillTransferTable();
        }
    }
}
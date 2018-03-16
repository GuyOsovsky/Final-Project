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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            if (!IsPostBack)
            {
                FillItemsToBorrow();
                FillBorrowedItems();
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

        protected void BorrowItem(object sender, EventArgs e)
        {
            GridViewRow row = ItemsToBorrow.Rows[int.Parse(((Button)sender).CommandArgument)];
            
            TextBox AmountText = row.FindControl("AmountToBorrow") as TextBox;
            
            int amount;
            if (int.TryParse(AmountText.Text, out amount) && amount > 0)
            {
                if (int.Parse(ItemsToBorrow.Rows[row.RowIndex].Cells[2].Text) - amount > 0)
                {
                    Label itemIDLable = (Label)row.FindControl("HiddenItemIDColumn");

                    ItemBL item = new ItemBL(int.Parse(itemIDLable.Text));

                    item.BorrowItemFromStock(Session["User"].ToString(), amount);

                    Response.Redirect("StockUI.aspx");
                }
                else
                {
                    AmountText.Text = "כמות מופרזת";
                }
            }
            else
            {
                AmountText.Text = "מספר לא חוקי";
            }
        }

        protected void ReturnItem(object sender, EventArgs e)
        {
            GridViewRow row = BorrowedItems.Rows[int.Parse(((Button)sender).CommandArgument)];

            int transferCode = int.Parse(((Label)row.FindControl("HiddenTransferCodeColumn")).Text);
            int itemID = int.Parse(((Label)row.FindControl("HiddenItemIDColumn")).Text);

            ItemBL item = new ItemBL(itemID);
            item.ReturnItemToStock(transferCode, itemID, int.Parse(BorrowedItems.Rows[row.RowIndex].Cells[3/*2*/].Text));

            Response.Redirect("StockUI.aspx");
        }

        protected bool isAbleStock()
        {
            if (Session["User"].ToString() == "") return false;
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            return volunteer.Type.PermmisionStock;
        }

        //change to validator!
        protected void AddItem(object sender, EventArgs e)
        {
            string itemName = itemNameTextBox.Text;
            int amount = int.Parse(itemsAmountTextBox.Text);
            //ItemBL item = new ItemBL()
        }

    }
}
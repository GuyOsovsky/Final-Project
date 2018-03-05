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
            DataTable items = stock.GetAllItems().Tables[0];
            for (int i = 0; i < items.Rows.Count; i++)
            {
                if((int)items.Rows[i]["AmountInStock"]==0)
                {
                    items.Rows[i].Delete();
                    //i--; //fixed
                }
            }
            DataView dataView = new DataView(items);
            ItemsToBorrow.DataSource = dataView;
            ItemsToBorrow.DataBind();
        }

        protected void FillBorrowedItems()
        {
            VolunteerBL user = new VolunteerBL(Session["User"].ToString());
            DataSet items = user.GetItemsInPossession();
            items.Tables[0].Columns.Add("ItemName", typeof(string));
            for (int i = 0; i < items.Tables[0].Rows.Count; i++)
            {
                items.Tables[0].Rows[i]["ItemName"] = (new ItemBL((int)items.Tables[0].Rows[i]["ItemID"])).ItemName;
            }
            BorrowedItems.DataSource = items;
            BorrowedItems.DataBind();
        }

        //לא להראות למתמש מה הוא לקח אם לא ניתן להחזיר את החפץ

        /*protected void LoadItemsInPossession()
        {
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            DataSet items = volunteer.GetItemsInPossession();
            ItemsInPossession.DataSource = items;
            ItemsInPossession.DataBind();
        }

        protected void LoadStock()
        {
            StockBL stock = new StockBL();
            DataSet items = stock.GetAllItems();
            AllStockItems.DataSource = items;
            AllStockItems.DataBind();
        }*/

    }
}
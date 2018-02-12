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
            if (!IsPostBack)
            {
                Load_ItemsInPossession();
                Load_Stock();
            }
        }

        protected void Load_ItemsInPossession()
        {
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            DataSet items = volunteer.GetItemsInPossession();
            ItemsInPossession.DataSource = items;
            ItemsInPossession.DataBind();
        }

        protected void Load_Stock()
        {
            StockBL stock = new StockBL();
            DataSet items = stock.GetAllItems();
            AllStockItems.DataSource = items;
            AllStockItems.DataBind();
        }

    }
}
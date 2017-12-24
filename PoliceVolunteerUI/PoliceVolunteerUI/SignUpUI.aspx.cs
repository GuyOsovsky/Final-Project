using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace PoliceVolunteerUI
{
    public partial class SignUpUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCities();
        }

        private void LoadCities()
        {
            string lang = Request.UserLanguages.Contains("he-IL") ? "Heb" : "En";
            XmlDocument doc = new XmlDocument();
            doc.Load("http://img2.timg.co.il/forums/1_102884894.xml");
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            HomeCityIN.DataSource = cities.Cast<XmlNode>().Select(node => node.Attributes[lang].Value).ToArray();
            HomeCityIN.DataBind();
        }
    }
}
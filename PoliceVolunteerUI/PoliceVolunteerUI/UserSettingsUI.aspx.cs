using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceVolnteerBL;
using PoliceVolnteerDAL;
using System.Xml;

namespace PoliceVolunteerUI
{
    public partial class UserSettingsUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if(Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            //fill gridview
            //FillUserSettings();
            LoadCitiesHome();
            loadUserSettings();
        }

        //protected void FillUserSettings()
        //{
        //    //create datatable
        //    VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
        //    DataTable SettingsTable = new DataTable();
        //    //fill datatable
        //    SettingsTable.Columns.Add("FieldName", typeof(String));
        //    SettingsTable.Columns.Add("FieldValue", typeof(String));
        //    DataTable volunteerTable = volunteer.VolunteerToDataSet().Tables[0];
        //    for (int i = 0; i < volunteerTable.Columns.Count; i++)
        //    {
        //        SettingsTable.Rows.Add();
        //        SettingsTable.Rows[i][0] = volunteerTable.Columns[i];
        //        SettingsTable.Rows[i][1] = volunteerTable.Rows[0][i];
        //    }
        //    //bind data to gridview
        //    DataView dataView = new DataView(SettingsTable);
        //    UserInformation.DataSource = dataView;
        //    UserInformation.DataBind();
        //}

        //protected void UserInformationRowEditingRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    //deselect row
        //    UserInformation.EditIndex = -1;
        //}

        //protected void UserInformationRowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    //select row
        //    UserInformation.EditIndex = e.NewEditIndex;
        //}

        //protected void UserInformationRowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    //get selected row
        //    GridViewRow row = UserInformation.Rows[e.RowIndex];
        //    //update in database
        //    string UpdatedValue = ((TextBox)row.Cells[1].FindControl("txt_FieldValue")).Text;
        //    object field = null;
        //    switch (e.RowIndex)
        //    {
        //        case 0:
        //            field = VolunteerInfoDALField.PhoneNumber;
        //            break;
        //        case 1:
        //            field = VolunteerInfoDALField.EmergencyNumber;
        //            break;
        //        case 2:
        //            field = VolunteerInfoDALField.FName;
        //            break;
        //        case 3:
        //            field = VolunteerInfoDALField.LName;
        //            break;
        //        case 4:
        //            field = VolunteerInfoDALField.BirthDate;
        //            break;
        //        case 5:
        //            field = VolunteerInfoDALField.HomeAddress;
        //            break;
        //        case 6:
        //            field = VolunteerInfoDALField.EmailAddress;
        //            break;
        //        case 7:
        //            field = VolunteerInfoDALField.ID;
        //            break;
        //        case 8:
        //            field = VolunteerPoliceInfoDALField.PoliceID;
        //            break;
        //        case 9:
        //            field = VolunteerPoliceInfoDALField.ServeCity;
        //            break;
        //        case 10:
        //            field = VolunteerPoliceInfoDALField.StartDate;
        //            break;
        //    }
        //    (new VolunteerBL(Session["User"].ToString())).UpdateVolunteer(field, UpdatedValue);
        //    if(e.RowIndex == 0)
        //    {
        //        Session["User"] = UpdatedValue;
        //    }
        //    //diselect row
        //    UserInformation.EditIndex = -1;
        //}

        protected void DeleteUser(object sender, EventArgs e)
        {
            (new VolunteerBL(Session["User"].ToString())).ChangeStatus(false);
            Session["User"] = "";
        }
        private void LoadCitiesHome()
        {
            //load home city input xml
            //string lang = Request.UserLanguages.Contains("he-IL") ? "Heb" : "En";
            string lang = "Heb";
            XmlDocument doc = new XmlDocument();
            doc.Load("http://img2.timg.co.il/forums/1_102884894.xml");
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            HomeCity.DataSource = cities.Cast<XmlNode>().Select(node => node.Attributes[lang].Value).ToArray();
            HomeCity.DataBind();
        }

        private void loadUserSettings()
        {
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            PhoneNumber.Text = volunteer.PhoneNumber;
            EmergencyPhoneNumber.Text = volunteer.EmergencyNumber;
            FName.Text = volunteer.FName;
            LName.Text = volunteer.LName;
            HomeAdress.Text = volunteer.HomeAddress;
            Email.Text = volunteer.EmailAddress;
            PoliceID.Text = volunteer.PoliceID;
            ServeCity.Text = volunteer.ServeCity;
            int homeCityIndex = -1;
            string[] citys = (string[])HomeCity.DataSource;
            for (int i = 0; i < citys.Length; i++)
            {
                if (volunteer.HomeCity.Equals(citys[i]))
                {
                    homeCityIndex = i;
                    break;
                }
            }
            HomeCity.SelectedIndex = homeCityIndex;
        }
    }
}
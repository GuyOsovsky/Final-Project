﻿using System;
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

        protected void updateVolunteer(object sender, EventArgs e)
        {
            VolunteerInfoDALField field = (VolunteerInfoDALField)(10);
            string value = "";
            switch (((WebControl)sender).ID.ToString())
            {
                case "PhoneNumber":
                    field = VolunteerInfoDALField.PhoneNumber;
                    break;
                case "EmergencyPhoneNumber":
                    field = VolunteerInfoDALField.EmergencyNumber;
                    break;
                case "FName":
                    field = VolunteerInfoDALField.FName;
                    break;
                case "LName":
                    field = VolunteerInfoDALField.LName;
                    break;
                case "HomeAdress":
                    field = VolunteerInfoDALField.HomeAddress;
                    break;
                case "HomeCity":
                    field = VolunteerInfoDALField.HomeCity;
                    break;
                case "Email":
                    field = VolunteerInfoDALField.EmailAddress;
                    break;
                default:
                    break;
            }
            if (sender is TextBox)
            {
                value = ((TextBox)sender).Text;
            }
            else if (sender is DropDownList)
            {
                value = ((DropDownList)sender).SelectedValue;
            }
            (new VolunteerBL(Session["User"].ToString())).UpdateVolunteer(field, value);
        }

    }
}
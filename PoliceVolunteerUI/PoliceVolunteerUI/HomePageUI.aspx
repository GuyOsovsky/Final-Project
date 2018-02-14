<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="HomePageUI.aspx.cs" Inherits="PoliceVolunteerUI.HomePageUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>
            :מפת ישראל - הוד השרון, כפר סבא
        </h1>
    </div>
    למפה מוגדלת -
    <a href="https://www.govmap.gov.il/?c=191000,674516&amp;z=2&amp;b=0&amp;lay=SPEEDCAMERA,POLICE_YEHIDA_LOCATION,RED_ROADS"
        id="ContentPlaceMain_lnkMap" target="_blank" class=" wcag-underline" data-wcag="open-in-new-window" rel="external"
        data-wcag-dynamic-font="true" data-wcag-org-font-size="10" title="GetMapPage">לחץ כאן</a>
    <br />
    <iframe id="ContentPlaceMain_ifrMap" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" width="550" height="700" data-wcag-title="map" data-wcag="empty-iframe" title="map"
        src="https://www.govmap.gov.il/map.html?showBackBtn=1&amp;showNavBtn=1&amp;AllowDrag=1&amp;in=1&amp;c=191000,674516&amp;z=5&amp;b=0&amp;lay=SPEEDCAMERA,POLICE_YEHIDA_LOCATION,RED_ROADS,NEIGHBORHOODS_AREA">
    </iframe>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BigMapUI.aspx.cs" Inherits="PoliceVolunteerUI.BigMapUI" %>

<!DOCTYPE html>

<html style="height: 100%;">
<head runat="server">
    <title>Israel Police Map</title>
</head>
<body style="width:100%; height:100%; background-size:100%;">
    <iframe id="ContentPlaceMain_ifrMap" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" data-wcag-title="bigMap" data-wcag="empty-iframe" title="bigMap" width="100%" height="100%"
        src="https://www.govmap.gov.il/map.html?showBackBtn=1&amp;showNavBtn=1&amp;AllowDrag=1&amp;in=1&amp;c=191000,674516&amp;z=5&amp;b=0&amp;lay=SPEEDCAMERA,POLICE_YEHIDA_LOCATION,RED_ROADS,NEIGHBORHOODS_AREA"></iframe>
</body>
</html>

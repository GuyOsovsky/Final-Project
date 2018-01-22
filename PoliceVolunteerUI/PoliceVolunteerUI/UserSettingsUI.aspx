<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="UserSettingsUI.aspx.cs" Inherits="PoliceVolunteerUI.UserSettingsUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <center>
            <h1>
                פרטים אישיים
            </h1>
        </center>
    </div>
    <center>
    <asp:GridView ID="UserInformationGV" runat="server"></asp:GridView>
    <asp:DataList ID="DataList1" runat="server"></asp:DataList>
    </center>
</asp:Content>

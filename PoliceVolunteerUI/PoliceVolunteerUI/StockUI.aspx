<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="StockUI.aspx.cs" Inherits="PoliceVolunteerUI.StockUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <center>
            <h1>
                ניהול פריטים
            </h1>
        </center>
    </div>
    <asp:GridView ID="ItemsInPossession" runat="server" AutoGenerateColumns="true" >

    </asp:GridView>

    <asp:GridView ID="AllStockItems" runat="server" AutoGenerateColumns="true" >

    </asp:GridView>
</asp:Content>

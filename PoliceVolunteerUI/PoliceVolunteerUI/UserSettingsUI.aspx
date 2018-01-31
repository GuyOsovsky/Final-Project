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
    <<%--asp:GridView ID="UserInformation" runat="server"></asp:GridView>--%>
        <asp:GridView ID="UserInformation" runat="server" AutoGenerateColumns="False" OnRowEditing="UserInformation_RowEditing"
            Style="z-index: 101; left: 18px; position: relative; top: 9px" 
        OnRowCancelingEdit="UserInformation_RowEditing_RowCancelingEdit" 
        OnRowUpdating="UserInformation_RowEditing_RowUpdating" OnRowDataBound="GridView2_RowDataBound" 
        OnRowDeleting="UserInformation_RowEditing_RowDeleting" BorderColor="#000099" CellPadding="4" ShowFooter="True" 
                    ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="ProductID">
                   <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ProdName" HeaderText="שם מוצר" ReadOnly="True" SortExpression="ProdName" />
                <asp:BoundField DataField="Quantity" HeaderText="כמות" />
                <asp:BoundField DataField="Price" HeaderText="מחיר" ReadOnly="True" />
                <asp:TemplateField HeaderText="מחיר כולל">
                    <FooterTemplate>
                        <asp:Label ID="LabelFooter" runat="server" Style=" left: 15px; top: 1px" Text="Label"></asp:Label>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelSum" runat="server" Style="left: 36px; top: 1px" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

    <asp:Button ID="TableButton" runat="server" Text="edit" OnClick="Edit_Settings"></asp:Button>

    <%--<asp:DataList ID="UserInformation" runat="server">--%>
   <%-- </asp:DataList>--%>
    </center>
</asp:Content>

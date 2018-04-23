<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="EditUserSettingsUI.aspx.cs" Inherits="PoliceVolunteerUI.EditUserSettingsUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    
        <asp:DropDownList ID="SearchUser" runat="server"></asp:DropDownList>
    
        <asp:Button ID="UpdateGridView" runat="server" Text="חפש" OnClick="Page_LoadComplete"></asp:Button>
        <asp:GridView ID="UserInformation" runat="server" AutoGenerateColumns="False" OnRowEditing="UserInformationRowEditing"
            Style="z-index: 101; left: 18px; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true"
            OnRowCancelingEdit="UserInformationRowEditingRowCancelingEdit" 
            OnRowUpdating="UserInformationRowUpdating"
            >
            <AlternatingRowStyle BackColor="#dbffe5" />
            <Columns>
                <asp:BoundField DataField="FieldName" ReadOnly="True" />
                <asp:TemplateField>   
                    <ItemTemplate>   
                        <asp:Label ID="lbl_FieldValue" runat="server" Text='<%#Eval("FieldValue") %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:TextBox ID="txt_FieldValue" runat="server" Text='<%#Eval("FieldValue") %>'></asp:TextBox>   
                    </EditItemTemplate>  
                </asp:TemplateField>   
                <asp:CommandField ShowEditButton="True" />
            </Columns>
            <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="DeleteSelectedUserButton" runat="server" Text="מחיקת המשתמש" OnClick="DeleteSelectedUser" OnClientClick="return confirm('מאם אתה בטוח שאתה רוצה למחוק משתמש זה?');" ValidationGroup="deleteUser" />
    </center>
</asp:Content>

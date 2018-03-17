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
        <asp:GridView ID="UserInformation" runat="server" AutoGenerateColumns="False" OnRowEditing="UserInformationRowEditing"
            Style="z-index: 101; left: 18px; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
            GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true"
            OnRowCancelingEdit="UserInformationRowEditingRowCancelingEdit" 
            OnRowUpdating="UserInformationRowUpdating">
            <AlternatingRowStyle BackColor="#dbffe5" />
            <Columns>
                <asp:BoundField DataField="FieldName" HeaderText="קטגוריה" ReadOnly="True" />
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
    </center>
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Button ID="DeleteUserBtn" runat="server" Text="מחיקת המשתמש" OnClick="DeleteUser" ValidationGroup="deleteUser" />
    <asp:CheckBox ID="DeleteValidation" runat="server" ValidationGroup="deleteUser" />
    <asp:CustomValidator ID="DeleteUseValidator" runat="server" ErrorMessage="לא אישרת את במחיקה" ValidationGroup="deleteUser" ClientValidationFunction="deleteUserValidation"></asp:CustomValidator>
    <script type="text/javascript">
        function deleteUserValidation(oSrc, args) {
            var checkBox = document.getElementById("DeleteValidation");
            args.isValid = checkBox.checked;
        }
    </script>
</asp:Content>

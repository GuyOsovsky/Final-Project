<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="StockUI.aspx.cs" Inherits="PoliceVolunteerUI.StockUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript" src="js/ValidationsLab.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <center>
            <h1>
                פריטים במחסן
            </h1>
        </center>
    </div>
    <br />

    <asp:GridView ID="ItemsToBorrow" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="HiddenItemIDColumn" runat="server" Text='<%# Eval("ItemID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemName" HeaderText="שם הפריט" ItemStyle-Width="28%" ReadOnly="True" />
            <asp:BoundField DataField="AmountInStock" HeaderText="כמות הפריטים" ItemStyle-Width="28%" ReadOnly="True" />
            <asp:CheckBoxField DataField="Recyclable" HeaderText="לא צריך להחזיר" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    
                    <asp:TextBox ID="AmountToBorrow" Text="0" runat="server"></asp:TextBox>
                    <asp:Button ID="BorrowButton1" Text="השאל פריט" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="BorrowItem" ValidationGroup="TakeItems"/>
                    <asp:RequiredFieldValidator ID="BorrowAmountRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="AmountToBorrow" ValidationGroup="TakeItems"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="BorrowAmountCustomV" runat="server" ErrorMessage="מספר זה לא תקין" ControlToValidate="AmountToBorrow" ClientValidationFunction="NumberValidation" ValidationGroup="TakeItems"></asp:CustomValidator>
                    <%
                        if (isAbleStock())
                        {
                    %>
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="DeleteItem" ForeColor="Red" Text="תוציא מהמחסן" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="DeleteItem" />
                    <%
                        }
                    %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br />
    <br />
    <br />

    <div class="page-header">
        <center>
            <h1>
                פריטים מושאלים
            </h1>
        </center>
    </div>
    <br />

    <asp:GridView ID="BorrowedItems" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="HiddenTransferCodeColumn" runat="server" Text='<%# Eval("TransferCode")%>' />
                    <asp:Label ID="HiddenItemIDColumn" runat="server" Text='<%# Eval("ItemID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemName" HeaderText="שם הפריט" ItemStyle-Width="25%" ReadOnly="True" />
            <asp:BoundField DataField="Amount" HeaderText="כמות הפריטים" ItemStyle-Width="25%" ReadOnly="True" />
            <asp:BoundField DataField="BorrowDate" DataFormatString="{0:d}" ItemStyle-Width="25%" HeaderText="תאריך השאלה" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="ReturnButton1" Text="החזר פריט" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ReturnItem" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <%
        if (isAbleStock())
        {
    %>

    <br />
    <br />
    <br />

    <div class="page-header">
        <center>
            <h1>
                הוספת פריטים
            </h1>
        </center>
    </div>
    <br />

    <asp:Table ID="AddTable" runat="server" Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>שם הפריט</asp:TableHeaderCell>
            <asp:TableHeaderCell>כמות הפריטים</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="25%">לא צריך להחזיר</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="10%"> </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow ID="TableRow1" runat="server" BackColor="#f4fbff">
            <asp:TableCell>
                <asp:TextBox ID="itemNameTextBox" Text="" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="itemNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="itemNameTextBox" ValidationGroup="AddItems"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="itemNameCustomV" runat="server" ErrorMessage="שם זה לא תקין" ControlToValidate="itemNameTextBox" ClientValidationFunction="NameValidation" ValidationGroup="AddItems"></asp:CustomValidator>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="itemsAmountTextBox" Text="0" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="amountRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="itemsAmountTextBox" ValidationGroup="AddItems"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="amountCustomV" runat="server" ErrorMessage="מספר זה לא תקין" ControlToValidate="itemsAmountTextBox" ClientValidationFunction="NumberValidation" ValidationGroup="AddItems"></asp:CustomValidator>
            </asp:TableCell>
            <asp:TableCell>
                <asp:CheckBox ID="isRecyclableCheckBox" runat="server" />
                <asp:HiddenField runat="server" />
                <asp:HiddenField runat="server" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="AddButton1" Text="הוסף פריט" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="AddItem" ValidationGroup="AddItems" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <br />
    <br />
    <br />

    <div class="page-header">
        <center>
            <h1>
                פריטים שמתנדבים שאלו/החזירו
            </h1>
            <br />
            בחר מתנדב :&nbsp;&nbsp; <asp:DropDownList ID="VolunteerChooseStock" runat="server"></asp:DropDownList>
            <asp:Button ID="SetItemsTable" runat="server" Text="הצג" OnClick="SetTransferTable" CausesValidation="False" />
        </center>
    </div>
    <br />


    <asp:GridView ID="Transfers" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="HiddenTransferCodeColumn" runat="server" Text='<%# Eval("TransferCode")%>' />
                    <asp:Label ID="HiddenItemIDColumn" runat="server" Text='<%# Eval("ItemID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemName" HeaderText="שם הפריט" ItemStyle-Width="20%" ReadOnly="True" />
            <asp:BoundField DataField="Amount" HeaderText="כמות הפריטים" ItemStyle-Width="20%" ReadOnly="True" />
            <asp:BoundField DataField="BorrowDate" DataFormatString="{0:d}" ItemStyle-Width="20%" HeaderText="תאריך השאלה" ReadOnly="True" />
            <asp:BoundField DataField="ReturnString" ItemStyle-Width="20%" HeaderText="תאריך החזרה" ReadOnly="True" />
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <%
        }
    %>
    <br />
    <br />
    <br />
</asp:Content>

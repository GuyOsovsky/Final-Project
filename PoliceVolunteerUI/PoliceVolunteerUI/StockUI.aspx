<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="StockUI.aspx.cs" Inherits="PoliceVolunteerUI.StockUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <center>
            <h1>
                פריטים במחסן
            </h1>
        </center>
    </div>

    <asp:GridView ID="ItemsToBorrow" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; left: 18px; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Snow" BackColor="OliveDrab" Font-Bold="true">
        <AlternatingRowStyle BackColor="OrangeRed" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="HiddenItemIDColumn" runat="server" Text='<%# Eval("ItemID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemName" HeaderText="שם הפריט" ItemStyle-Width="33%" ReadOnly="True" />
            <asp:BoundField DataField="AmountInStock" HeaderText="כמות הפריטים" ItemStyle-Width="33%" ReadOnly="True" />
            <%--<asp:BoundField DataField="Recyclable" HeaderText="לא להחזיר" ReadOnly="True" />--%>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="AmountToBorrow" Text="0" runat="server"></asp:TextBox>
                    <asp:Button ID="BorrowButton1" Text="השאל פריט" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="BorrowItem" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="Orange" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
    
    <br /><br /><br />
    
    <div class="page-header">
        <center>
            <h1>
                פריטים מושאלים
            </h1>
        </center>
    </div>

    <asp:GridView ID="BorrowedItems" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; left: 18px; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Snow" BackColor="OliveDrab" Font-Bold="true">
        <AlternatingRowStyle BackColor="OrangeRed" />
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
        <RowStyle BackColor="Orange" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
    
    <%
        if (isAbleStock())
        {
    %>
    
    <br /><br /><br />
    
    <div class="page-header">
        <center>
            <h1>
                הוספת פריטים
            </h1>
        </center>
    </div>

    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableHeaderRow Style="z-index: 101; left: 18px; position: relative; top: 9px" BorderColor="#000099" Width="100%" ForeColor="Snow" BackColor="OliveDrab" Font-Bold="true">
            <asp:TableHeaderCell>שם הפריט</asp:TableHeaderCell>
            <asp:TableHeaderCell>כמות הפריטים</asp:TableHeaderCell>
            <asp:TableHeaderCell>האם צריך להחזיר</asp:TableHeaderCell>
            <asp:TableHeaderCell> </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow ID="TableRow1" runat="server" BackColor="OrangeRed">
            <asp:TableCell>
                <asp:TextBox ID="itemNameTextBox" Text="שם" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="itemsAmountTextBox" Text="0" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:CheckBox ID="isRecyclableCheckBox" runat="server" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="AddButton1" Text="הוסף פריט" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="AddItem" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <%
        }
    %>
</asp:Content>

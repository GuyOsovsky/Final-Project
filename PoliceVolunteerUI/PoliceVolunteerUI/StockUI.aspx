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
        Style="z-index: 101; left: 18px; position: relative; top: 9px"
        BorderColor="#000099" CellPadding="4" ShowFooter="True"
        ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="HiddenItemIDColumn" runat="server" Text='<%# Eval("ItemID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemName" HeaderText="שם הפריט" ReadOnly="True" />
            <asp:BoundField DataField="AmountInStock" HeaderText="כמות הפריטים" ReadOnly="True" />
            <asp:BoundField DataField="Recyclable" HeaderText="האם ניתן להחזיר" ReadOnly="True" />
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="הירשם לפעילות" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ActivitySignUp" />
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <asp:GridView ID="BorrowedItems" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; left: 18px; position: relative; top: 9px"
        BorderColor="#000099" CellPadding="4" ShowFooter="True"
        ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="HiddenTransferCodeColumn" runat="server" Text='<%# Eval("TransferCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemName" HeaderText="שם הפריט" ReadOnly="True" />
            <asp:BoundField DataField="Amount" HeaderText="כמות הפריטים" ReadOnly="True" />
            <asp:BoundField DataField="BorrowDate" DataFormatString="{0:d}" HeaderText="תאריך השאלה" ReadOnly="True" />
            
            <%--<asp:BoundField DataField="ReturnDate" DataFormatString="{0:d}" HeaderText="תאריך החזרה" ReadOnly="True" />--%>
            
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="הירשם לפעילות" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ActivitySignUp" />
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
    

</asp:Content>

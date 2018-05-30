<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="ShiftsAdminUI.aspx.cs" Inherits="PoliceVolunteerUI.ShiftsAdminUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h1>משמרות עתידיות</h1>
    </center>
    <asp:GridView ID="ShiftsInformation" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblShiftCode" runat="server" Text='<%# Eval("ShiftCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="סוג המשמרת">
                <ItemTemplate>
                    <asp:Label ID="lblShiftType" runat="server" Text='<%# Eval("ShiftType")%>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="InputShiftType" runat="server" OnLoad="FillShiftTypesList"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="תאריך">
                <ItemTemplate>
                    <asp:Label ID="lblDateOfShift" runat="server" Text='<% #Eval("DateOfShift").ToString() != "" ? ((DateTime)Eval("DateOfShift")).ToShortDateString() : "" %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputDateOfShift" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="DateOfShiftRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newShift" ControlToValidate="InputDateOfShift"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="שעת התחלה">
                <ItemTemplate>
                    <asp:Label ID="lblShiftStartTime" runat="server" Text='<%# Eval("StartTime").ToString() != "" ? ((DateTime)Eval("StartTime")).ToShortTimeString() : "" %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputShiftStartTime" runat="server" TextMode="Time"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ShiftStartTimeRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newShift" ControlToValidate="InputShiftStartTime"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="שעת סיום">
                <ItemTemplate>
                    <asp:Label ID="lblShiftFinishTime" runat="server" Text='<%# Eval("FinishTime").ToString() != "" ? ((DateTime)Eval("FinishTime")).ToShortTimeString() : "" %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputShiftFinishTime" runat="server" TextMode="Time"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ShiftFinishTimeRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newShift" ControlToValidate="InputShiftFinishTime"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="מקום">
                <ItemTemplate>
                    <asp:Label ID="lblShiftPlace" runat="server" Text='<%#Eval("Place") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputShiftPlace" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ShiftPlaceRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newShift" ControlToValidate="InputShiftPlace"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:Button ID="AddShiftBtn" runat="server" ValidationGroup="newShift" Text="הוסף" OnClick="AddNewShift" />
                </EditItemTemplate>
            </asp:TemplateField>

        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br /><br /><br /><br /><br />

    <center>
        <h1>דיווחי משמרות שמתנדבים סיימו למלא</h1>
    </center>
    <asp:GridView ID="CompleteShiftComments" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblShiftCode" runat="server" Text='<%# Eval("ShiftCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="ShiftType" HeaderText="סוג המשמרת" ReadOnly="True" />
            
            <asp:BoundField DataField="DateOfShift" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />

            <asp:BoundField DataField="comments" HeaderText="תיאור משמרת / דיווח" ReadOnly="true" />

            <%--<asp:TemplateField HeaderText="תיאור משמרת / דיווח">
                <ItemTemplate>
                    <div dir="rtl">
                        <asp:TextBox ID="CommentText" Text="" runat="server"  Width="80%" TextMode="MultiLine"></asp:TextBox>
                    </div>                    
                    <asp:Button ID="btnUpdateComment" Text="הוסף" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="UpdateComment" ValidationGroup="newComment"/>
                    <asp:RequiredFieldValidator ID="CommentTextRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="CommentText" ValidationGroup="newComment"></asp:RequiredFieldValidator>
                </ItemTemplate>
            </asp:TemplateField>--%>

        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
    <br /><br /><br /><br /><br />

    <center>
        <h1>דיווחי רכב שהושלמו</h1>
    </center>

    <asp:GridView ID="CompleteCarReport" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblShiftCode" runat="server" Text='<%# Eval("ShiftCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="ShiftType" HeaderText="סוג המשמרת" ReadOnly="True" />
            
            <asp:BoundField DataField="DateOfShift" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />

            <asp:BoundField DataField="CarID" HeaderText="מספר רכב" ReadOnly="True" />
            
            <asp:BoundField DataField="Distance" HeaderText="דרך בקילומטרים" ReadOnly="True" />

        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br /><br /><br /><br /><br />

    <center>
        <h1>דיווחי רכב שלא הושלמו</h1>
    </center>

    <asp:GridView ID="CarReport" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblShiftCode" runat="server" Text='<%# Eval("ShiftCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="ShiftType" HeaderText="סוג המשמרת" ReadOnly="True" />
            
            <asp:BoundField DataField="DateOfShift" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />

            <asp:TemplateField HeaderText="מספר רכב">
                <ItemTemplate>
                    <asp:DropDownList id="ChooseCarID" runat="server"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="דרך בקילומטרים">
                <ItemTemplate>
                    <asp:TextBox ID="DistanceTbx" Text="" runat="server" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="DistanceRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="DistanceTbx" ValidationGroup="newCarReport"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="DistanceCustomV" runat="server" ErrorMessage="מספר זה לא תקין" ControlToValidate="DistanceTbx" ClientValidationFunction="NumberValidation" ValidationGroup="newCarReport"></asp:CustomValidator>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnUpdateCarReport" Text="הוסף" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="UpdateCarReport" ValidationGroup="newCarReport" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br /><br /><br /><br /><br />

</asp:Content>

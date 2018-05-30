<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="CoursesAdminUI.aspx.cs" Inherits="PoliceVolunteerUI.CoursesAdminUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h1>קורסים עתידיים</h1>
    </center>
    <asp:GridView ID="CoursesInformation" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("CourseCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="שם הקורס">
                <ItemTemplate>
                    <asp:Label ID="lblCourseName" runat="server" Text='<%#Eval("CourseName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="inputCourseName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CourseNameReqFieldVal" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newCourse" ControlToValidate="inputCourseName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="תאריך">
                <ItemTemplate>
                    <asp:Label ID="lblCourseDate" runat="server" Text='<%#Eval("CourseDate").ToString() != "" ? ((DateTime)Eval("CourseDate")).ToShortDateString() : ""  %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="inputCourseDate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CourseDateReqFieldVal" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newCourse" ControlToValidate="inputCourseDate"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="שעת התחלה">
                <ItemTemplate>
                    <asp:Label ID="lblCourseStartTime" runat="server" Text='<%#Eval("StartTime").ToString() != "" ? ((DateTime)Eval("StartTime")).ToShortTimeString() : "" %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="inputCourseStartTime" runat="server" TextMode="Time"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CourseStartTimeReqFieldVal" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newCourse" ControlToValidate="inputCourseStartTime"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="שעת סיום">
                <ItemTemplate>
                    <asp:Label ID="lblCourseFinishTime" runat="server" Text='<%#Eval("FinishTime").ToString() != "" ? ((DateTime)Eval("FinishTime")).ToShortTimeString() : "" %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="inputCourseFinishTime" runat="server" TextMode="Time"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CourseFinishTimeReqFieldVal" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newCourse" ControlToValidate="inputCourseFinishTime"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="מקום">
                <ItemTemplate>
                    <asp:Label ID="lblCoursePlace" runat="server" Text='<%#Eval("Place") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="inputCoursePlace" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CoursePlaceReqFieldVal" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newCourse" ControlToValidate="inputCoursePlace"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="תיאור">
                <ItemTemplate>
                    <asp:Label ID="lblCourseDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="inputCourseDescription" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CourseDescriptionReqFieldVal" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newCourse" ControlToValidate="inputCourseDescription"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="רישיון">
                <ItemTemplate>
                    <asp:Label ID="lblValidityType" runat="server" Text='<%#Eval("Validity") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="InputValidityType" runat="server" OnLoad="FillValidityTypesList"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:Button ID="UpdateBtn" runat="server" ValidationGroup="newCourse" Text="הוסף" OnClick="AddNewCourse" />
                </EditItemTemplate>
            </asp:TemplateField>

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

    <center>
        <h1>קורסים מתוכננים של יחידות אחרות</h1>
    </center>

    <asp:GridView ID="OtherCourses" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("CourseCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CourseName" HeaderText="שם הקורס" ReadOnly="True" />
            <asp:BoundField DataField="CourseDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="תיאור" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="AddCourse" Text="הוסף קורס" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="AddOtherCourses" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

</asp:Content>

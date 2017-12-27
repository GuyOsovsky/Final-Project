<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" Inherits="PoliceVolunteerUI.SignUpUI" Codebehind="SignUpUI.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center" class="">
        <asp:DropDownList ID="HomeCityIN" runat="server"></asp:DropDownList>
        <form class="form-inline">
            <div class="form-group" >
                <label for="PhoneNumberIN">מספר טלפון:</label>
                <br />
                <asp:TextBox ID="PhoneNumberIN" runat="server" TextMode="Phone" CssClass="form-control" Width ="300"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="EmergencyNumberIN">מספר חירום:</label>
                <br />
                <asp:TextBox ID="EmergencyNumberIN" runat="server" TextMode="Phone" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="FNameIN">שם פרטי:</label>
                <br />
                <asp:TextBox ID="FNameIN" runat="server" TextMode="Phone" CssClass="form-control"></asp:TextBox>
            </div>


            <div class="checkbox">
                <label>
                    <input type="checkbox">
                    Remember me
                </label>
            </div>
            <button type="submit" class="btn btn-default">Submit</button>
        </form>
    </div>
</asp:Content>
<%--    public string PhoneNumber { get; set; }
        public string EmergencyNumber { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string EmailAddress { get; set; }
        public string ID { get; set; }
        public string PoliceID { get; set; }
        public string ServeCity { get; set; }
        public DateTime StartDate { get; set; }
        public VolunteerTypeBL Type { get; set; }
        public bool Status { get; set; }--%>


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newShiftMaster.aspx.cs" Inherits="ERP_Demo.newShiftMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validatePage() {
            if (Page_ClientValidate()) {
                return confirm('Do you want to save?');
            }
            else {
                return false;
            }
        }
    </script>
    <asp:Table ID="Table1" runat="server" Height="30%" Width="45%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="3"><h3>SHIFT MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">SHIFT NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">WORKING HOURS</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="shiftNameTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="shiftNameReq" CssClass="required" runat="server" ControlToValidate="shiftNameTextBox" ErrorMessage="please enter shift name"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="workingHoursTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="workingHoursReq" CssClass="required" runat="server" ControlToValidate="workingHoursTextBox" ErrorMessage="please enter working hours"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="return validatePage()"/>
                &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

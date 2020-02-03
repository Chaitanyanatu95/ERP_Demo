<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newUnitMeasurementMaster.aspx.cs" Inherits="ERP_Demo.newUnitMeasurementMaster" %>
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
    <asp:Table ID="Table1" runat="server" Height="30%" Width="40%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="3"><h3>UOM DETAILS</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" VerticalAlign="Bottom">
            <asp:TableCell runat="server" CssClass="margin">UNIT OF MEASUREMENT</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">ABBREVIATION</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" >
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="unitofmeasurementTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="umoReq" ControlToValidate="unitofmeasurementTextBox" CssClass="required" runat="server" ErrorMessage="please enter unit"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:8em">*</div><asp:TextBox ID="abbreviationTextBox" runat="server" CssClass="sizeAbbreviationTextbox"></asp:TextBox><br /><asp:RequiredFieldValidator ID="abrReq" ControlToValidate="abbreviationTextBox" runat="server" CssClass="required" ErrorMessage="please enter abbreviation"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="5"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="return validatePage()"/>
            <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newRejection.aspx.cs" Inherits="ERP_Demo.newRejection" %>
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
    <asp:Table ID="Table1" runat="server" Height="20%" Width="60%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="5"><h3>REJECTION DETAILS</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">Rejection type</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">Code</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">Description</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" ><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="rejectionTypeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rejTypeReq" CssClass="required" runat="server" ControlToValidate="rejectionTypeTextBox" ErrorMessage="please enter rejection type"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" ><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="codeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="codeReq" CssClass="required" runat="server" ControlToValidate="codeTextBox" ErrorMessage="please enter rejection code"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" ><asp:TextBox ID="descTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" RowSpan="2">
                <asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="return validatePage()"/>
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
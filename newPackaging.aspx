<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newPackaging.aspx.cs" Inherits="ERP_Demo.newPackaging" %>
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
            <asp:TableCell runat="server" ColumnSpan="4"><h3>PACKAGING DETAILS</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">PACKAGING TYPE <div class="required" style="display:inline">*</div><br />
                <asp:TextBox ID="packagingTypeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="packagingReq" CssClass="required" runat="server" ControlToValidate="packagingTypeTextBox" ErrorMessage="please enter packaging type"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">SIZE <div class="required" style="display:inline">*</div><br />
                <asp:TextBox ID="sizeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="sizeReq" CssClass="required" runat="server" ControlToValidate="sizeTextBox" ErrorMessage="please enter packaging size"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">
                <asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="return validatePage()"/>
                <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newPostOperationMaster.aspx.cs" Inherits="ERP_Demo.newPostOperationMaster" %>
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
    <asp:Table ID="Table1" runat="server" Height="25%" Width="30%" CssClass="tableClass" HorizontalAlign="Center">
        <asp:TableRow runat="server" TableSection="TableHeader" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="2" HorizontalAlign="Center"><h3>POST OPERATION DETAILS</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">POST OPERATION TYPE</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">
                <div class="required" style="padding-left:12em">*</div>
                <asp:TextBox ID="postOperationTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="postOpnReq" CssClass="required" runat="server" ControlToValidate="postOperationTextBox" ErrorMessage="please enter post operation type"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell  RowSpan="2" runat="server" HorizontalAlign="Center"  CssClass="margin">
                <asp:Button runat="server" CssClass="nextPage" Text="SAVE" OnClick="SaveBtn_Click" OnClientClick="return validatePage()"/>
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

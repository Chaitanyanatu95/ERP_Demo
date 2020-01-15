<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newFamilyMaster.aspx.cs" Inherits="ERP_Demo.newFamilyMaster" %>
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
    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Height="40%" Width="20%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="2"><h4>PRODUCT CATEGORY MASTER</h4></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" VerticalAlign="Bottom">
            <asp:TableCell runat="server" CssClass="margin">ADD NEW CATEGORY <div class="required" style="display:inline">*</div><br />
            <asp:TextBox ID="familyTextBox" runat="server"></asp:TextBox>
            <br /><asp:RequiredFieldValidator ID="famReq" runat="server" CssClass="required" ErrorMessage="please enter category" ControlToValidate="familyTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="custButtonCss" OnClientClick="return validatePage()"/>
            <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

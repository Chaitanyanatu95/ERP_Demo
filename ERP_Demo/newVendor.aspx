<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newVendor.aspx.cs" Inherits="ERP_Demo.newVendor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" CssClass="tableClass" Height="40%" Width="30%">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="3"><h3>VENDOR MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">VENDOR ID</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server"><asp:TextBox runat="server" ID="vendorIdTextBox" ReadOnly="true" BackColor="#f0f0f0" Font-Bold="true" CssClass="marginTextBox"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="vendorNameLabel" runat="server" CssClass="margin">VENDOR NAME</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"> <asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="vendorNameTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="vendorNameReq" CssClass="required" runat="server" ErrorMessage="Please enter vendor name" ControlToValidate="vendorNameTextBox"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="vendorAddressOneLabel" runat="server" CssClass="margin">ADDRESS ONE</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox TextMode="multiline" Columns="40" Rows="5" ID="vendorAddressOneTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="vendorAddressOneReq" CssClass="required" runat="server" ErrorMessage="Please enter address" ControlToValidate="vendorAddressOneTextBox"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="vendorAddressTwoLabel" runat="server" CssClass="margin">ADDRESS TWO</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox TextMode="multiline" Columns="40" Rows="5" ID="vendorAddressTwoTextBox" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="vendorContactLabel" runat="server" CssClass="margin">CONTACT NO</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="vendorContactNoTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="vendorContactReq" CssClass="required" runat="server" ErrorMessage="Please enter contact no" ControlToValidate="vendorContactNoTextBox"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="vendorEmailLabel" runat="server" CssClass="margin">EMAIL-ID</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="vendorEmailIdTextBox" runat="server"></asp:TextBox><br /><asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="vendorEmailIdTextBox" CssClass="required" ErrorMessage="Please enter valid Email-Id"></asp:RegularExpressionValidator>
            <br /><asp:RequiredFieldValidator ID="vendorEmailReq" CssClass="required" runat="server" ErrorMessage="Please enter Email-Id" ControlToValidate="vendorEmailIdTextBox"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="vendorContactPersonLabel" runat="server" CssClass="margin">CONTACT PERSON</asp:TableCell></asp:TableRow>  
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="vendorContactPersonTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="vendorContactPersonReq" CssClass="required" runat="server" ErrorMessage="Please enter contact person" ControlToValidate="vendorContactPersonTextBox"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="vendorGstNumberLabel" runat="server" CssClass="margin">GST NUMBER</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="vendorGstDetailsTextBox" runat="server"></asp:TextBox>
        <br /><asp:RequiredFieldValidator ID="vendorGstReq" CssClass="required" runat="server" ErrorMessage="Please enter GST NO." ControlToValidate="vendorGstDetailsTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator>
        <br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server"  
                    ControlToValidate="vendorGstDetailsTextBox" ErrorMessage="GST Number should be 15 in length..!"  
                    ValidationExpression="^[0-9,A-Z]{15}$" CssClass="required"></asp:RegularExpressionValidator></asp:TableCell></asp:TableRow>
           <asp:TableRow ID="saveRowLabel" runat="server"> <asp:TableCell ID="saveCellLabel" runat="server"><asp:Button ID="saveButtonLabel" runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="custButtonCss" OnClientClick="confirm('Do you want to save?');"/> 
            <asp:Button ID="cancelLabel" Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

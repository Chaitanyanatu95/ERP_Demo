<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newCustomer.aspx.cs" Inherits="ERP_Demo.newCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" CssClass="tableClass" Width="30%">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="3" Font-Bold="true" Font-Names="Helvetica"><h3>CUSTOMER MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerNameLabel" runat="server" CssClass="margin">CUSTOMER NAME</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="customerNameTextBox" runat="server"></asp:TextBox><br /><br /><asp:RequiredFieldValidator ID="custNameReq" CssClass="required" runat="server" ErrorMessage="Please enter name." ControlToValidate="customerNameTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerAddressOneLabel" runat="server" CssClass="margin">ADDRESS ONE</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox TextMode="multiline" Columns="40" Rows="5" ID="customerAddressOneTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="customerAddressReq" CssClass="required" runat="server" ErrorMessage="Please enter address." ControlToValidate="customerAddressOneTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerAddressTwoLabel" runat="server" CssClass="margin">ADDRESS TWO</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox TextMode="multiline" Columns="40" Rows="5" ID="customerAddressTwoTextBox" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerContactLabel" runat="server" CssClass="margin">CONTACT NO</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="customerContactNoTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="customerPhoneReq" CssClass="required" runat="server" ErrorMessage="Please enter contact no." ControlToValidate="customerContactNoTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerEmailLabel" runat="server" CssClass="margin">EMAIL-ID</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="customerEmailIdTextBox" runat="server"></asp:TextBox><br /><asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="customerEmailIdTextBox" CssClass="required" ErrorMessage="Please enter valid Email-Id."></asp:RegularExpressionValidator>
        <br /><asp:RequiredFieldValidator ID="customerEmailIdReq" CssClass="required" runat="server" ErrorMessage="Please enter email Id." ControlToValidate="customerEmailIdTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerContactPersonLabel" runat="server" CssClass="margin">CONTACT PERSON</asp:TableCell></asp:TableRow>  
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="customerContactPersonTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="customerContactPersonReq" CssClass="required" runat="server" ErrorMessage="Please enter contact person." ControlToValidate="customerContactPersonTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                     </asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerGstNumberLabel" runat="server" CssClass="margin">GST NUMBER</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="customerGstDetailsTextBox" runat="server"></asp:TextBox>
        <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="required" runat="server" ErrorMessage="Please enter GST NO." ControlToValidate="customerNameTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator>
        <br /><asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server"  
                    ControlToValidate="customerGstDetailsTextBox" ErrorMessage="GST Number should be 15 in length."  
                    ValidationExpression="^[0-9,A-Z]{15}$" CssClass="required"></asp:RegularExpressionValidator></asp:TableCell></asp:TableRow>
           <asp:TableRow ID="saveRowLabel" runat="server"> <asp:TableCell ID="saveCellLabel" runat="server"><asp:Button ID="saveButtonLabel" runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="custButtonCss" OnClientClick="confirm('Do you want to save?');"/> 
            <asp:Button ID="cancelLabel" Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

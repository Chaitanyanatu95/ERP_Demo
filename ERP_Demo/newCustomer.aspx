<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newCustomer.aspx.cs" Inherits="ERP_Demo.newCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" CssClass="tableClass" Width="30%">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="3" Font-Bold="true" Font-Names="Helvetica"><h3>CUSTOMER MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerNameLabel" runat="server" CssClass="margin">CUSTOMER NAME</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"> <asp:TableCell runat="server"><asp:TextBox ID="customerNameTextBox" runat="server" CssClass="marginTextBox"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerAddressOneLabel" runat="server" CssClass="margin">ADDRESS ONE</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox TextMode="multiline" Columns="40" Rows="5" CssClass="marginTextBox"  ID="customerAddressOneTextBox" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerAddressTwoLabel" runat="server" CssClass="margin">ADDRESS TWO</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox TextMode="multiline" Columns="40" Rows="5" CssClass="marginTextBox"  ID="customerAddressTwoTextBox" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerContactLabel" runat="server" CssClass="margin">CONTACT NO</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox ID="customerContactNoTextBox" CssClass="marginTextBox" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerEmailLabel" runat="server" CssClass="margin">EMAIL-ID</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox ID="customerEmailIdTextBox" CssClass="marginTextBox" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerContactPersonLabel" runat="server" CssClass="margin">CONTACT PERSON</asp:TableCell></asp:TableRow>  
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox ID="customerContactPersonTextBox" CssClass="marginTextBox" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell ID="customerGstNumberLabel" runat="server" CssClass="margin">GST NUMBER</asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox ID="customerGstDetailsTextBox" CssClass="marginTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server"  
                    ControlToValidate="customerGstDetailsTextBox" ErrorMessage="GST Number should be 15 in length..!"  
                    ValidationExpression="^[0-9,A-Z]{15}$" CssClass="required"></asp:RegularExpressionValidator></asp:TableCell></asp:TableRow>
           <asp:TableRow ID="saveRowLabel" runat="server"> <asp:TableCell ID="saveCellLabel" runat="server"><asp:Button ID="saveButtonLabel" runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="custButtonCss" /> 
            <asp:Button ID="cancelLabel" Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

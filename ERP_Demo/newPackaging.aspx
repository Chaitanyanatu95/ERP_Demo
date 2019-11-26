<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newPackaging.aspx.cs" Inherits="ERP_Demo.newPackaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Height="30%" Width="35%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="4"><h3>Packaging Master</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">PACKAGING TYPE</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">SIZE</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="packagingTypeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="packagingReq" CssClass="required" runat="server" ControlToValidate="packagingTypeTextBox" ErrorMessage="please enter packaging type"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="sizeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="sizeReq" CssClass="required" runat="server" ControlToValidate="sizeTextBox" ErrorMessage="please enter packaging size"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" ><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="confirm('Do you want to save?');"/></asp:TableCell>
            <asp:TableCell runat="server" ><asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
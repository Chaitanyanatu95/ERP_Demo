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
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="packagingTypeTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="sizeTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" ><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" /></asp:TableCell>
            <asp:TableCell runat="server" ><asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newPostOperationMaster.aspx.cs" Inherits="ERP_Demo.newPostOperationMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Height="25%" Width="30%" CssClass="tableClass" HorizontalAlign="Center">
        <asp:TableRow runat="server" TableSection="TableHeader" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="2" HorizontalAlign="Center"><h3>POST OPERATION MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">POST OPERATION TYPE</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin"><asp:TextBox ID="postOperationTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center"  CssClass="margin"><asp:Button runat="server" CssClass="nextPage" Text="SAVE" OnClick="SaveBtn_Click" />
            <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" />
            </asp:TableCell></asp:TableRow>
    </asp:Table>
</asp:Content>

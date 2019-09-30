<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newMasterBatch.aspx.cs" Inherits="ERP_Demo.newMasterBatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Table ID="Table1" runat="server" Height="40%" Width="70%" HorizontalAlign="Center" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="7"><h3>MASTERBATCH MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" VerticalAlign="Bottom">
            <asp:TableCell runat="server" CssClass="margin">MASTERBATCH NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">MASTERBATCH GRADE</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">MASTERBATCH MFG</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">COLOUR</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">COLOR CODE</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="mbnameTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="mbgradeTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="mbmfgTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="mbcolorTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="mbcolorcodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage"/></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

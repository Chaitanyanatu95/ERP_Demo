<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newDownTimeCode.aspx.cs" Inherits="ERP_Demo.newDownTimeCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Height="30%" Width="15%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="2"><h3>DOWN TIME CODE MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">DOWN TIME CODE</asp:TableCell>
            </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="downTimeCodeTextBox" runat="server"></asp:TextBox></asp:TableCell>

        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">DOWN TIME TYPE</asp:TableCell>

            </asp:TableRow>
            <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="downTimeTypeTextBox" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
        <asp:TableRow runat="server"> 
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="custButtonCss"/>
            <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

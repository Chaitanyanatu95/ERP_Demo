<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newUnitMeasurementMaster.aspx.cs" Inherits="ERP_Demo.newUnitMeasurementMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Height="30%" Width="40%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="3"><h3>UOM MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" VerticalAlign="Bottom">
            <asp:TableCell runat="server" CssClass="margin">UNIT OF MEASUREMENT</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">ABBREVIATION</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" >
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="unitofmeasurementTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="abbreviationTextBox" runat="server" CssClass="sizeAbbreviationTextbox"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="5"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage"/>
            <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

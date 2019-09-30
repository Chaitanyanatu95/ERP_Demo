<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newRawMaterialMaster.aspx.cs" Inherits="ERP_Demo.newRawMaterialMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Table ID="Table1" runat="server" Height="28%" Width="70%" HorizontalAlign="Center" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="5"><h3>RAW MATERIAL MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">MATERIAL NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">MATERIAL GRADE</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">MATERIAL COLOUR</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">MATERIAL MAKE</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" >
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="rmName" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="rmGrade" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="rmColor" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="rmMake" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage"/>
            <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

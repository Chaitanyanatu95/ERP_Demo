<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newMachineMaster.aspx.cs" Inherits="ERP_Demo.newMachineMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Height="20%" Width="65%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="5" CssClass="CustomerHeader"><h3>MACHINE MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" TableSection="TableBody">
            <asp:TableCell runat="server" CssClass="margin">M/C NO</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">M/C NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">M/C SPECS</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" TableSection="TableBody">
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="machineNoTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="machineNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:FileUpload runat="server" ID="machineFileUpload" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
    ControlToValidate="machineFileUpload" runat="server" ForeColor="Red" ErrorMessage="Please select a valid excel or .xlsx/.xls extension file."
    Display="Dynamic" />
            </asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage"  />
            <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow ID="rowAfterProcess" runat="server">
            <asp:TableCell ID="cellAfterProcess" runat="server"><asp:Label ID="lblAfterProcess" runat="server" Font-Bold="true"></asp:Label> </asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
</asp:Content>
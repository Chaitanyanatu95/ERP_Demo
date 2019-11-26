<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newMachineMaster.aspx.cs" Inherits="ERP_Demo.newMachineMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Height="20%" Width="65%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="5" CssClass="CustomerHeader"><h3>MACHINE MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" TableSection="TableBody">
            <asp:TableCell runat="server" CssClass="margin">M/C NO</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">M/C NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin" ColumnSpan="2">M/C SPECS</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" TableSection="TableBody">
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="machineNoTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="machineNoReq" CssClass="required" runat="server" ErrorMessage="please enter machine no" ControlToValidate="machineNoTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="machineNameTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="machineNameReq" CssClass="required" runat="server" ErrorMessage="please enter machine name" ControlToValidate="machineNameTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:FileUpload runat="server" ID="machineFileUpload" /><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx|.pdf)$"
    ControlToValidate="machineFileUpload" runat="server" ForeColor="Red" ErrorMessage="Please select a valid excel or .xlsx/.xls/.pdf extension file." SetFocusOnError="true"/>
            </asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="confirm('Do you want to save?');" />
            <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
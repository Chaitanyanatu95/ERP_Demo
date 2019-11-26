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
            <asp:TableCell runat="server"  CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="postOperationTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="postOpnReq" CssClass="required" runat="server" ControlToValidate="postOperationTextBox" ErrorMessage="please enter post operation type"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center"  CssClass="margin"><asp:Button runat="server" CssClass="nextPage" Text="SAVE" OnClick="SaveBtn_Click" OnClientClick="confirm('Do you want to save?');"/>
            <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');"/>
            </asp:TableCell></asp:TableRow>
    </asp:Table>
</asp:Content>

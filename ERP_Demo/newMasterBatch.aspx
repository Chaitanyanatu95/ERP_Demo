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
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="mbnameTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbNameReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch name" ControlToValidate="mbNameTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="mbgradeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbGradeReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch grade" ControlToValidate="mbGradeTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="mbmfgTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbMfgReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch mfg" ControlToValidate="mbMfgTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="mbcolorTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbColorReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch color" ControlToValidate="mbColorTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="mbcolorcodeTextBox" runat="server"></asp:TextBox><br /></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="confirm('Do you want to save?');"/></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

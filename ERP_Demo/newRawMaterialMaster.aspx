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
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="rmName" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rmNameReq" CssClass="required" runat="server" ControlToValidate="rmName" ErrorMessage="please enter raw material name"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="rmGrade" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rmGradeReq" CssClass="required" runat="server" ControlToValidate="rmGrade" ErrorMessage="please enter raw material grade"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="rmColor" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rmColorReq" CssClass="required" runat="server" ControlToValidate="rmColor" ErrorMessage="please enter raw material color"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="rmMake" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rmMakeReq" CssClass="required" runat="server" ControlToValidate="rmMake" ErrorMessage="please enter raw material make"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="confirm('Do you want to save?');"/>
            <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

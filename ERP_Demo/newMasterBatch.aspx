<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newMasterBatch.aspx.cs" Inherits="ERP_Demo.newMasterBatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validatePage() {
            if (Page_ClientValidate()) {
                return confirm('Do you want to save?');
            }
            else {
                return false;
            }
        }
    </script>
        <asp:Table ID="Table1" runat="server" Height="40%" Width="80%" HorizontalAlign="Center" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="7"><h3>MASTERBATCH MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" VerticalAlign="Bottom">
            <asp:TableCell runat="server" CssClass="margin">MASTERBATCH NAME <div class="required" style="display:inline">*</div><br />
                <asp:TextBox ID="mbnameTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbNameReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch name" ControlToValidate="mbNameTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">MASTERBATCH GRADE <div class="required" style="display:inline">*</div> <br /> <asp:TextBox ID="mbgradeTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbGradeReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch grade" ControlToValidate="mbGradeTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">MASTERBATCH MFG <div class="required" style="display:inline">*</div><br /> <asp:TextBox ID="mbmfgTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbMfgReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch mfg" ControlToValidate="mbMfgTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">COLOUR <div class="required" style="display:inline">*</div> <br /> <asp:TextBox ID="mbcolorTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="mbColorReq" runat="server" CssClass="required" ErrorMessage="please enter masterbatch color" ControlToValidate="mbColorTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin" style="padding-bottom:25px;">COLOR CODE <br/><asp:TextBox ID="mbcolorcodeTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="return validatePage()"/>
            <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

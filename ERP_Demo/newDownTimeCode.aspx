<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newDownTimeCode.aspx.cs" Inherits="ERP_Demo.newDownTimeCode" %>
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
    <asp:Table ID="Table1" runat="server" Height="30%" Width="25%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="2"><h3>DOWN TIME MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">DOWN TIME CODE <div class="required" style="display:inline">*</div><br />
            
            <asp:TextBox ID="downTimeCodeTextBox" runat="server"></asp:TextBox>
            <br /><asp:RequiredFieldValidator ID="downtimecodereq" CssClass="required" runat="server" ErrorMessage="please enter down time code" ControlToValidate="downTimeCodeTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">DOWN TIME TYPE <div class="required" style="display:inline">*</div><br />
                <asp:TextBox ID="downTimeTypeTextBox" runat="server"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="downtimetypereq" CssClass="required" runat="server" ErrorMessage="please down time type" ControlToValidate="downTimeTypeTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server"> 
            <asp:TableCell runat="server" ColumnSpan="2" CssClass="margin">
                <asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="custButtonCss" OnClientClick="return validatePage()"/>
                <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

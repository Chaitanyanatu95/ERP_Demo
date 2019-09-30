<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newWorkerMaster.aspx.cs" Inherits="ERP_Demo.newWorkerMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    function ValidateCheckBoxList(sender, args) {
        var checkBoxList = document.getElementById("<%=rightsToBeAllocatedCheckBoxList.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }
</script>
        <asp:Table ID="Table1" runat="server" Height="25%" Width="90%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="7"><h3>WORKER MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">EMPLOYEE NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">EMPLOYEE ID</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">USER ID</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">USER PASSWORD</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">RIGHTS TO BE ALLOCATED</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="empNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="empIdTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="userIdTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="userPasswordTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server">
                <asp:CheckBoxList ID="rightsToBeAllocatedCheckBoxList" runat="server" RepeatDirection="Horizontal" style="margin-left:40px; margin-top:17px;">
                    <asp:ListItem Value="Admin"></asp:ListItem>
                    <asp:ListItem Value="Editor"></asp:ListItem>
                    <asp:ListItem Value="Worker"></asp:ListItem>
                    <asp:ListItem Value="Extra"></asp:ListItem>
                </asp:CheckBoxList>
                <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one right!"
    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" style="margin-left:40px;" />

            </asp:TableCell><asp:TableCell runat="server" ><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" /></asp:TableCell>
            <asp:TableCell runat="server" ><asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

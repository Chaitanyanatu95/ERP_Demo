<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newWorkerMaster.aspx.cs" Inherits="ERP_Demo.newWorkerMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            var checkBoxList = document.getElementById("<%=rightsToBeAllocatedCheckBoxList.ClientID %>");
            var checkbox = checkBoxList.getElementsByTagName("input");
            var label = checkBoxList.getElementsByTagName("label");
            var counter=0;
            for (var i = 0; i < checkbox.length; i++)
            {
                if (checkbox[i].checked) {
                    counter++;
                    if (label[i].innerHTML == 'Selected Access')
                    {
                        document.getElementById("<%=selectedAccessDropDownList.ClientID %>").style.display = "block";
                    }
                    else
                    {
                        document.getElementById("<%=selectedAccessDropDownList.ClientID %>").style.display = "none";
                    }
                }
                else
                {
                        document.getElementById("<%=selectedAccessDropDownList.ClientID %>").style.display = "none";
                }
            }
        } );

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
            <asp:TableCell runat="server" ColumnSpan="7"><h3>EMPLOYEE MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">EMPLOYEE NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">EMPLOYEE ID</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">USER ID</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">USER PASSWORD</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin" ColumnSpan="3">RIGHTS TO BE ALLOCATED</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="empNameTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="empNameReq" CssClass="required" runat="server" ErrorMessage="please enter emp name" ControlToValidate="empNameTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="empIdTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="empIdReq" CssClass="required" runat="server" ErrorMessage="please enter emp id" ControlToValidate="empIdTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="userIdTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="userIdReq" CssClass="required" runat="server" ErrorMessage="please enter user id" ControlToValidate="userIdTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="userPasswordTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="userPassReq" CssClass="required" runat="server" ErrorMessage="please user password" ControlToValidate="userPasswordTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server">
                <div class="required" style="padding-left:28em">*</div>
                <asp:CheckBoxList ID="rightsToBeAllocatedCheckBoxList" runat="server" RepeatDirection="Horizontal" style="margin-left:40px; margin-top:17px;" AutoPostBack="true">
                    <asp:ListItem Value="Full Access"></asp:ListItem>
                    <asp:ListItem Value="Transactions"></asp:ListItem>
                    <asp:ListItem Value="Reports"></asp:ListItem>
                    <asp:ListItem Value="Selected Access"></asp:ListItem>
                </asp:CheckBoxList>
                <asp:DropDownList ID="selectedAccessDropDownList" runat="server" style="text-align:center; margin-left:auto; margin-right:auto; margin-top:2em">
                    <asp:ListItem Text="Product Category" Value="Product Category"></asp:ListItem>
                    <asp:ListItem Text="UOM" Value="UOM"></asp:ListItem>
                    <asp:ListItem Text="Raw Material" Value="Raw Material"></asp:ListItem>
                    <asp:ListItem Text="Masterbatch" Value="Masterbatch"></asp:ListItem>
                    <asp:ListItem Text="Post Operation" Value="Post Operation"></asp:ListItem>
                    <asp:ListItem Text="Packaging" Value="Packaging"></asp:ListItem>
                    <asp:ListItem Text="Rejection" Value="Rejection"></asp:ListItem>
                    <asp:ListItem Text="Shift" Value="Shift"></asp:ListItem>
                    <asp:ListItem Text="Machine" Value="Machine"></asp:ListItem>
                    <asp:ListItem Text="Down Time Code" Value="Down Time Code"></asp:ListItem>
                    <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                    <asp:ListItem Text="Worker" Value="Worker"></asp:ListItem>
                    <asp:ListItem Text="Vendor" Value="Vendor"></asp:ListItem>
                    <asp:ListItem Text="Parts" Value="Parts"></asp:ListItem>
                </asp:DropDownList>
                <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select transaction rights!"
    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" style="margin-left:40px;" />
            </asp:TableCell><asp:TableCell runat="server" ><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="confirm('Do you want to save?');"/></asp:TableCell>
            <asp:TableCell runat="server" ><asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

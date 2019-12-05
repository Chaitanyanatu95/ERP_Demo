<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newMachineMaster.aspx.cs" Inherits="ERP_Demo.newMachineMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            var machineSpec = document.getElementById('<%= lblMachineSpec.ClientID %>').innerText;

            if (!machineSpec) {
                document.getElementById('<%= lblMachineSpec.ClientID %>').style.display = "none";
                document.getElementById('<%= btnMachineSpecs.ClientID %>').style.display = "none";
            }
        }
        function machineSpecValue() {
            var machineSpecFile = document.getElementById('<%= machineFileUpload.ClientID %>');
            if (machineSpecFile.value != null) {
                document.getElementById('<%= btnMachineSpecs.ClientID %>').style.display = "inline";
            }
        }

        function machineSpecFileValidation() {
            var fileInput = document.getElementById('<%= machineFileUpload.ClientID %>');
            if (fileInput.value != null) {
                var filePath = fileInput.value;
                var allowedExtensions = /(\.xls|\.pdf|\.xlsx)$/i;
                if (!allowedExtensions.exec(filePath)) {
                    document.getElementById('<%= errorMachineFile.ClientID %>').innerHTML = 'Please upload file having extensions .xls/.pdf/.xlsx only.'.fontcolor('Red');
                    fileInput.value = '';
                    return false;
                }
                else {
                    //Image preview
                    if (fileInput.files && fileInput.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('<%= btnMachineSpecs.ClientID %>').style.display = "inline";
                            document.getElementById('<%= errorMachineFile.ClientID %>').innerHTML = "";
                        };
                        reader.readAsDataURL(fileInput.files[0]);
                    }
                }
            }
        }
    </script>
    <asp:Table ID="Table1" runat="server" Height="20%" Width="65%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="5" CssClass="CustomerHeader"><h3>MACHINE MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" TableSection="TableBody">
            <asp:TableCell runat="server" CssClass="margin">M/C NO</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">M/C NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin" ColumnSpan="2">M/C SPECS <br /> Supported Formats: xls,xlsx,pdf</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" TableSection="TableBody">
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="machineNoTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="machineNoReq" CssClass="required" runat="server" ErrorMessage="please enter machine no" ControlToValidate="machineNoTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><div class="required" style="padding-left:12em">*</div><asp:TextBox ID="machineNameTextBox" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="machineNameReq" CssClass="required" runat="server" ErrorMessage="please enter machine name" ControlToValidate="machineNameTextBox"></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin"><br /><br />
                <asp:FileUpload runat="server" ID="machineFileUpload" onchange="return machineSpecFileValidation();"/>
                <asp:Label ID="lblMachineSpec" runat="server" style="display:inline; color:blue;"></asp:Label>
                <asp:ImageButton ID="btnMachineSpecs" runat="server" ImageUrl="~/Images/cancel.png" Height="20" Width="20" OnClick="btnMachineSpecs_Click" OnClientClick="return confirm('Do you want to delete?');"/><br />
               <asp:Label ID="errorMachineFile" runat="server"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="confirm('Do you want to save?');" />
            <asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
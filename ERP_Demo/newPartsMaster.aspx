<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newPartsMaster.aspx.cs" Inherits="ERP_Demo.newPartsMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        window.onload = function ()
        {
            var temp = document.getElementById('<%= lblFile.ClientID %>').innerText;
            var moldFile = document.getElementById('<%= lblMoldFile.ClientID %>').innerText;
            if (!temp) {
                document.getElementById('<%= lblFile.ClientID %>').style.display = "none";
                document.getElementById('<%= btnFile.ClientID %>').style.display = "none";
            }
            if (!moldFile) {
                document.getElementById('<%= lblMoldFile.ClientID %>').style.display = "none";
                document.getElementById('<%= btnMoldFile.ClientID %>').style.display = "none";
            }
        }

        function fileValue() {
            var temp2 = document.getElementById('<%= partPhotoFileUpload.ClientID %>');
            if (temp2.value != null) {
                document.getElementById('<%= btnFile.ClientID %>').style.display = "inline";
            }
        }

        function moldFileValue() {
            var moldFile = document.getElementById('<%= moldSpecFileUpload.ClientID %>');
            if (moldFile.value != null) {
                document.getElementById('<%= btnMoldFile.ClientID %>').style.display = "inline";
            }
        }

    </script>
    <div style="display:block;margin-left:auto;margin-right:auto;height:40px;width:80%;background-color:whitesmoke;padding-top:5px;">
        <div style="display:inline-block;text-align:center;border:1px solid black;background-color:SkyBlue;width:20%;height:33px;padding-top:7px; color:black">
            <b>STEP 1</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 2</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 3</b>
        </div>
    </div>
    <br />
    <asp:Table ID="Table1" runat="server" Height="70%" Width="80%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="4"><h3>PART ENTRY MASTER</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="partId" runat="server" VerticalAlign="Bottom">
            <asp:TableCell runat="server" CssClass="margin">PART NO.</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">PART NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">CUSTOMER NAME</asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">CUSTOMER PART NO</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">
                <asp:TextBox ID="partNoTextBox" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox></asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                <asp:TextBox ID="partNameTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="partNameReq" ControlToValidate="partNameTextBox" ErrorMessage="Please enter part name!" CssClass="required" SetFocusOnError="true"/></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                    <asp:DropDownList ID="customerNameDropDownList" DataTextField="customer_name" DataValueField="customer_name" runat="server"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator runat="server" ID="custNameReq" ControlToValidate="customerNameDropDownList" ErrorMessage="Please select customer name!" CssClass="required" SetFocusOnError="true"/></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                        <asp:TextBox ID="custPartNoTextBox" runat="server"></asp:TextBox>
                        </asp:TableCell></asp:TableRow>
            <asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">PRODUCT CATEGORY</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">MOLD NAME</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">MOLD MFG YEAR</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">MOLD LIFE IN NOS.</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">
                <asp:DropDownList ID="familyDropDownList" DataTextField="family" DataValueField="family" runat="server"></asp:DropDownList><br />
                <asp:RequiredFieldValidator runat="server" ID="prodCategoryReq" ControlToValidate="familyDropDownList" ErrorMessage="Please select product category!" CssClass="required" SetFocusOnError="true"/></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                    <asp:TextBox ID="moldNameTextBox" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator runat="server" ID="moldNameReq" ControlToValidate="moldNameTextBox" ErrorMessage="Please enter mold name!" CssClass="required" SetFocusOnError="true"/></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                        <asp:TextBox ID="moldYearTextBox" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                            <asp:TextBox ID="moldLifeTextBox" runat="server"></asp:TextBox><br />
                            <asp:RequiredFieldValidator runat="server" ID="moldLifeReq" ControlToValidate="moldLifeTextBox" ErrorMessage="Please enter mold life!" CssClass="required" SetFocusOnError="true"/></asp:TableCell></asp:TableRow><asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">NO OF CAVITIES</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">UNIT</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">PART WEIGHT (In g)</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">SHOT WT ( in g)</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">
                <asp:TextBox ID="noOfCavitiesTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="noOfCavitiesTextBox" ErrorMessage="Please enter no of cavities!" CssClass="required" SetFocusOnError="true"/></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                    <asp:DropDownList ID="unitMeasurementDropDownList" DataTextField="abbreviation" DataValueField="abbreviation" runat="server"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="unitMeasurementDropDownList" ErrorMessage="Please select unit!" CssClass="required" SetFocusOnError="true"/></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                        <asp:TextBox ID="partWeightTextBox" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator runat="server" ID="partWeightReq" ControlToValidate="partWeightTextBox" ErrorMessage="Please enter part weight!" CssClass="required" SetFocusOnError="true"/></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                            <asp:TextBox ID="shotWeightTextBox" runat="server"></asp:TextBox><br />
                            <asp:RequiredFieldValidator runat="server" ID="shotWeightReq" ControlToValidate="shotWeightTextBox" ErrorMessage="Please enter shot weight!" CssClass="required" SetFocusOnError="true"/></asp:TableCell></asp:TableRow><asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">CYC. TIME (sec)</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">JIG/FIXTURE (REQ)</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">PRODUCTION PER HOUR IN NOS. </asp:TableCell><asp:TableCell runat="server"  CssClass="margin"></asp:TableCell></asp:TableRow><asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">
                <asp:TextBox ID="cycleTimeTextBox" runat="server" OnTextChanged="moldProductionCycleTextBox_TextChanged" AutoPostBack="true"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="cycleTimeReq" ControlToValidate="cycleTimeTextBox" ErrorMessage="Please enter cycle time!" CssClass="required" SetFocusOnError="true" /></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                    <asp:DropDownList ID="jigReqDropDownList" runat="server">
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                        <asp:ListItem Value="YES">YES</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                    <asp:TextBox ID="moldProductionCycleTextBox" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                        <asp:Button OnClick="NextPage_Click" runat="server" ID="nextPage" CssClass="nextPage" Text="NEXT" OnClientClick="confirm('Do you want to proceed?');" ></asp:Button>
                        &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to Cancel?');"  />
                    </asp:TableCell></asp:TableRow><asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">PHOTO</asp:TableCell><asp:TableCell runat="server"  CssClass="margin">PROCESS SHEET</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">
                <asp:Label ID="lblFile" runat="server" style="display:inline;"></asp:Label>
                <asp:FileUpload ID="partPhotoFileUpload" runat="server" onchange="javascript: fileValue();"/>
                <asp:ImageButton ID="btnFile" runat="server" ImageUrl="~/Images/cancel.png" Height="20" Width="20" OnClick="btnFile_Click" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.png)$"
                ControlToValidate="partPhotoFileUpload" runat="server" ForeColor="Red" ErrorMessage="Please select a valid excel or .jpg/.png extension file."
                Display="Dynamic" SetFocusOnError="true" />
            </asp:TableCell><asp:TableCell runat="server"  CssClass="margin">
                <asp:Label ID="lblMoldFile" runat="server" style="display:inline;"></asp:Label>
                <asp:FileUpload ID="moldSpecFileUpload" runat="server" onchange="javascript: moldFileValue();"/>
                <asp:ImageButton ID="btnMoldFile" runat="server" ImageUrl="~/Images/cancel.png" Height="20" Width="20" OnClick="btnMoldFile_Click"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx|.pdf)$"
                ControlToValidate="moldSpecFileUpload" runat="server" ForeColor="Red" ErrorMessage="Please select a valid excel or .xls/.xlsx/.pdf extension file."
                Display="Dynamic" SetFocusOnError="true" />
            </asp:TableCell></asp:TableRow><asp:TableFooterRow ID="rowAfterProcess" runat="server">
            <asp:TableCell ID="cellAfterProcess" runat="server"  CssClass="margin"><asp:Label ID="lblAfterProcess" runat="server" Font-Bold="true"></asp:Label> </asp:TableCell></asp:TableFooterRow></asp:Table></asp:Content>
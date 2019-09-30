<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewDetails.aspx.cs" Inherits="ERP_Demo.viewDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style> 
        .tableRawMaterial{
            margin: 0 auto;
        }
       .tableRawMaterial td,th{
            border:1px dashed black;
            text-align:center;
            width:120px;
            height:60px;
            margin: 0 auto;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            onMasterBatch();
            onAltRM();
            onAltMB();
        }

        function onMasterBatch()
        {
            var temp = document.getElementById('<%= dataMasterbatch.ClientID %>').innerText;
            if (temp != "YES") {
                document.getElementById('<%= tdMasterbatchName.ClientID %>').style.display = "none";
                document.getElementById('<%= tdMasterbatchGrade.ClientID %>').style.display = "none";
                document.getElementById('<%= tdMasterbatchColor.ClientID %>').style.display = "none";
                document.getElementById('<%= tdMasterbatchColorCode.ClientID %>').style.display = "none";
                document.getElementById('<%= tdMasterbatchMfg.ClientID %>').style.display = "none";
                document.getElementById('<%= rowMasterbatchName.ClientID %>').style.display = "none";
                document.getElementById('<%= rowMasterbatchGrade.ClientID %>').style.display = "none";
                document.getElementById('<%= rowMasterbatchColor.ClientID %>').style.display = "none";
                document.getElementById('<%= rowMasterbatchColorCode.ClientID %>').style.display = "none";
                document.getElementById('<%= rowMasterbatchMfg.ClientID %>').style.display = "none";
            }
        }

        function onAltRM()
        {
            var temp = document.getElementById('<%= dataAltRawMaterial.ClientID %>').innerText;
            if (temp != "YES") {
                document.getElementById('<%= tdAltRawMaterialName.ClientID %>').style.display = "none";
                document.getElementById('<%= tdAltRawMaterialGrade.ClientID %>').style.display = "none";
                document.getElementById('<%= tdAltRawMaterialColor.ClientID %>').style.display = "none";
                document.getElementById('<%= tdAltRawMaterialMake.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltRawMaterialName.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltRawMaterialGrade.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltRawMaterialColor.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltRawMaterialMake.ClientID %>').style.display = "none";
            }
        }

        function onAltMB()
        {
            var temp = document.getElementById('<%= dataAltMasterbatch.ClientID %>').innerText;
            if (temp != "YES") {
                document.getElementById('<%= tdAltMasterbatchName.ClientID %>').style.display = "none";
                document.getElementById('<%= tdAltMasterbatchGrade.ClientID %>').style.display = "none";
                document.getElementById('<%= tdAltMasterbatchColor.ClientID %>').style.display = "none";
                document.getElementById('<%= tdAltMasterbatchMfg.ClientID %>').style.display = "none";
                document.getElementById('<%= tdAltMasterbatchColorCode.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltMasterbatchName.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltMasterbatchGrade.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltMasterbatchColor.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltMasterbatchMfg.ClientID %>').style.display = "none";
                document.getElementById('<%= rowAltMasterbatchColorCode.ClientID %>').style.display = "none";
            }
        }

        function PrintPage() {

            var printContent = document.getElementById('<%= pnlGridView.ClientID %>');
            var printContent2 = document.getElementById('<%= pnlGridView1.ClientID %>');
            var printContent3 = document.getElementById('<%= pnlGridView2.ClientID %>');

            var printWindow = window.open("Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');
            printWindow.document.write('<html><head><style> .tableRawMaterial { margin: 0 auto; }'
                + '.tableRawMaterial td,th{ border: 1px solid black; text-align: center; width: 100px; height: 50px; margin: 0 auto; }'
                +'</style >'
                + '<title> Details | PB Plastics </title></head><body>');
            printWindow.document.write('<table class="tableRawMaterial" width="90%">');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.write('</table></br>');
            printWindow.document.write('<table class="tableRawMaterial" width="90%">');
            printWindow.document.write(printContent2.innerHTML);
            printWindow.document.write('</table></br>');
            printWindow.document.write('<table class="tableRawMaterial" width="100%">');
            printWindow.document.write(printContent3.innerHTML);
            printWindow.document.write('</table></body></html>');

            printWindow.document.close();

            printWindow.focus();

            printWindow.print();

        }

        function BackPage() {
                window.location = "displayParts.aspx";
        }

</script><br />
    <table width="80%" id="pnlGridView" runat="server" class="tableRawMaterial">

    <tr>
        <td colspan="15" style="font-weight:900; background-color:SkyBlue;">
            <h4><asp:Label ID="partNameLabel" runat="server"/></h4>
        </td>
    </tr>
    <tr>
        <td colspan="15">
            <asp:Image ID="partPhoto" runat="server" Height="100" Width="100" />
        </td>
    </tr>
        <tr>
            <th><asp:Label ID="lblPartNo" runat="server" Text="Part No"/></th>
            <th><asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"/></th>
            <th><asp:Label ID="lblCustomerPartNo" runat="server" Text="Customer Part No"/></th>
            <th><asp:Label ID="lblProductCategory" runat="server" Text="Product Category"/></th>
            <th><asp:Label ID="lblMoldName" runat="server" Text="Mold Name"/></th>
            <th><asp:Label ID="lblMoldMfgYear" runat="server" Text="Mold Mfg Year"/></th>
            <th><asp:Label ID="lblMoldLife" runat="server" Text="Mold Life"/></th>
            <th><asp:Label ID="lblNoOfCavities" runat="server" Text="No Of Cavities"/></th>
            <th><asp:Label ID="lblUOM" runat="server" Text="UOM"/></th>
            <th><asp:Label ID="lblPartWeight" runat="server" Text="Part Weight"/></th>
            <th><asp:Label ID="lblShotWeight" runat="server" Text="Shot Weight"/></th>
            <th><asp:Label ID="lblCycleTime" runat="server" Text="Cycle Time"/></th>
            <th><asp:Label ID="lblJigFixReq" runat="server" Text="Jig Fix Req"/></th>
            <th><asp:Label ID="lblProductionInPcs" runat="server" Text="Production In Pcs"/></th>
            <th><asp:Label ID="lblMouldSpecSheet" runat="server" Text="Process Sheet"/></th>
        </tr>
        <tr>
            <td><asp:Label ID="textPartNo"  runat="server" /></td>
            <td><asp:Label ID="textCustomerName" runat="server" /></td>
            <td><asp:Label ID="textCustomerPartNo" runat="server" /></td>
            <td><asp:Label ID="textProductCategory" runat="server" /></td>
            <td><asp:Label ID="textMoldName" runat="server" /></td>
            <td><asp:Label ID="textMoldMfgYear" runat="server" /></td>
            <td><asp:Label ID="textMoldLife" runat="server" /></td>
            <td><asp:Label ID="textNoOfCavities" runat="server" /></td>
            <td><asp:Label ID="textUOM" runat="server" /></td>
            <td><asp:Label ID="textPartWeight" runat="server" /></td>
            <td><asp:Label ID="textShotWeight" runat="server" /></td>
            <td><asp:Label ID="textCycleTime" runat="server" /></td>
            <td><asp:Label ID="textJigFixReq" runat="server" /></td>
            <td><asp:Label ID="textProductionInPcs" runat="server" /></td>
            <td><asp:ImageButton ID="imageMouldSpecSheet" runat="server" ImageUrl="~/Images/xls.png" OnClick="imageMouldSpecSheet_Click" Height="40" Width="40"/><asp:Literal ID="ltEmbed" runat="server" /></td>
        </tr>
</table>  
    <br />
        <table width="80%" id="pnlGridView1" runat="server" class="tableRawMaterial">
            <tr>
                <th><asp:Label ID="lblRawMaterial" runat="server" Text="Raw Material"/></th>
                <th><asp:Label ID="lblRawMaterialGrade" runat="server" Text="Material Grade"/></th>
                <th><asp:Label ID="lblRawMaterialColor" runat="server" Text="Material Color"/></th>
                <th><asp:Label ID="lblRawMaterialMake" runat="server" Text="Material Make"/></th>
                <th id="tdMasterbatch"><asp:Label ID="lblMasterbatch" runat="server" Text="Masterbatch"/></th>
                <th id="tdAltRawMaterial"><asp:Label ID="lblAltRawMaterial" runat="server" Text="Alt Raw Material"/></th>
                <th id="tdMasterbatchName"><asp:Label ID="lblMasterbatchName" runat="server" Text="Masterbatch Name"/></th>
                <th id="tdMasterbatchGrade"><asp:Label ID="lblMasterbatchGrade" runat="server" Text="Masterbatch Grade"/></th>
                <th id="tdMasterbatchMfg"><asp:Label ID="lblMasterbatchMfg" runat="server" Text="Masterbatch MFG"/></th>
                <th id="tdMasterbatchColor"><asp:Label ID="lblMasterbatchColor" runat="server" Text="Masterbatch Color"/></th>
                <th id="tdMasterbatchColorCode"><asp:Label ID="lblMasterbatchColorCode" runat="server" Text="Masterbatch Color Code"/></th>
            </tr>
            <tr>
                <td><asp:Label ID="dataRawMaterial" runat="server" /></td>
                <td><asp:Label ID="dataRawMaterialGrade" runat="server" /></td>
                <td id="rowRawMaterialColor"><asp:Label ID="dataRawMaterialColor" runat="server" /></td>
                <td id="rowRawMaterialMake"><asp:Label ID="dataRawMaterialMake" runat="server" /></td>
                <td id="rowMasterbatch"><asp:Label ID="dataMasterbatch" runat="server" /></td>
                <td id="rowAltRawMaterial"><asp:Label ID="dataAltRawMaterial" runat="server" /></td>
                <td id="rowMasterbatchName"><asp:Label ID="dataMasterbatchName" runat="server" /></td>
                <td id="rowMasterbatchGrade"><asp:Label ID="dataMasterbatchGrade" runat="server" /></td>
                <td id="rowMasterbatchMfg"><asp:Label ID="dataMasterbatchMfg" runat="server" /></td>
                <td id="rowMasterbatchColor"><asp:Label ID="dataMasterbatchColor" runat="server" /></td>
                <td id="rowMasterbatchColorCode"><asp:Label ID="dataMasterbatchColorCode" runat="server" /></td>
            </tr>
        </table>
        <br />
        <table width="80%" id="pnlGridView2" runat="server" class="tableRawMaterial">
            <tr>
                <th id="tdAltRawMaterialName"><asp:Label ID="lblAltRawMaterialName" runat="server" Text="Alt RM Name"/></th>
                <th id="tdAltRawMaterialGrade"><asp:Label ID="lblAltRawMaterialGrade" runat="server" Text="Alt RM Grade"/></th>
                <th id="tdAltRawMaterialColor"><asp:Label ID="lblAltRawMaterialColor" runat="server" Text="Alt RM Color"/></th>
                <th id="tdAltRawMaterialMake"><asp:Label ID="lblAltRawMaterialMake" runat="server" Text="Alt RM Make"/></th>
                <th id="tdAltMasterbatch"><asp:Label ID="lblAltMasterbatch" runat="server" Text="Alt MB"/></th>
                <th id="tdAltMasterbatchName"><asp:Label ID="lblAltMasterbatchName" runat="server" Text="Alt MB Name"/></th>
                <th id="tdAltMasterbatchGrade"><asp:Label ID="lblAltMasterbatchGrade" runat="server" Text="Alt MB Grade"/></th>
                <th id="tdAltMasterbatchMfg"><asp:Label ID="lblAltMasterbatchMfg" runat="server" Text="Alt MB MFG"/></th>
                <th id="tdAltMasterbatchColor"><asp:Label ID="lblAltMasterbatchColor" runat="server" Text="Alt MB Color"/></th>
                <th id="tdAltMasterbatchColorCode"><asp:Label ID="lblAltMasterbatchColorCode" runat="server" Text="Alt MB Color Code"/></th>
                <th><asp:Label ID="lblPostOperation" runat="server" Text="Post Operation"/></th>
                <th><asp:Label ID="lblPackagingDetails" runat="server" Text="Packaging Details"/></th>
            </tr>
            <tr>
                <td id="rowAltRawMaterialName"><asp:Label ID="dataAltRawMaterialName" runat="server" /></td>
                <td id="rowAltRawMaterialGrade"><asp:Label ID="dataAltRawMaterialGrade" runat="server" /></td>
                <td id="rowAltRawMaterialColor"><asp:Label ID="dataAltRawMaterialColor" runat="server" /></td>
                <td id="rowAltRawMaterialMake"><asp:Label ID="dataAltRawMaterialMake" runat="server" /></td>
                <td id="rowAltMasterbatch"><asp:Label ID="dataAltMasterbatch" runat="server" /></td>
                <td id="rowAltMasterbatchName"><asp:Label ID="dataAltMasterbatchName" runat="server" /></td>
                <td id="rowAltMasterbatchGrade"><asp:Label ID="dataAltMasterbatchGrade" runat="server" /></td>
                <td id="rowAltMasterbatchMfg"><asp:Label ID="dataAltMasterbatchMfg" runat="server" /></td>
                <td id="rowAltMasterbatchColor"><asp:Label ID="dataAltMasterbatchColor" runat="server" /></td>
                <td id="rowAltMasterbatchColorCode"><asp:Label ID="dataAltMasterbatchColorCode" runat="server" /></td>
                <td id="rowPostOperation"><asp:Label ID="dataPostOperation" runat="server" /></td>
                <td id="rowPackagingDetails"><asp:Label ID="dataPackagingDetails" runat="server" /></td>
            </tr>
        </table>
    <br/><br/>
<center>
    <asp:Button ID="print" runat="server" CssClass="nextPage" Text="Print" OnClientClick="javascript:PrintPage()"/>
    <asp:Button ID="back" runat="server" CssClass="nextPage" Text="Back" PostBackUrl="~/displayParts.aspx"/>
</center>
</asp:Content>
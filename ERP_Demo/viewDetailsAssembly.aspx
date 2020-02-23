<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewDetailsAssembly.aspx.cs" Inherits="ERP_Demo.viewDetailsAssembly" %>
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
         function PrintPage() {

            var printContent = document.getElementById('<%= pnlGridView.ClientID %>');
            var printContent2 = document.getElementById('<%= pnlGridView1.ClientID %>');

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
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();

        }

        function BackPage() {
                window.location = "displayAssemble.aspx";
        }

</script><br />
    <table width="80%" id="pnlGridView" runat="server" class="tableRawMaterial">

    <tr>
        <td colspan="15" style="font-weight:900; background-color:SkyBlue;">
            <h4><asp:Label ID="assNameLabel" runat="server"/></h4>
        </td>
    </tr>
    <tr>
        <th><asp:Label ID="lblAssNo" runat="server" Text="Part No"/></th>
        <th><asp:Label ID="lblUOM" runat="server" Text="UOM"/></th>
        <th><asp:Label ID="lblAssWeight" runat="server" Text="Part Weight"/></th>
        <th><asp:Label ID="lblTargetQuant" runat="server" Text="Shot Weight"/></th>
        <th><asp:Label ID="lblAssemblyFileUpload" runat="server" Text="Process Sheet"/></th>
    </tr>
    <tr>
        <td><asp:Label ID="textPartNo"  runat="server" /></td>
        <td><asp:Label ID="textUOM" runat="server" /></td>
        <td><asp:Label ID="textAssWeight" runat="server" /></td>
        <td><asp:Label ID="textTargetQuant" runat="server" /></td>
        <td><asp:ImageButton ID="imageMouldSpecSheet" runat="server" ImageUrl="~/Images/xls.png" OnClick="imageMouldSpecSheet_Click" Height="40" Width="40"/><asp:Literal ID="ltEmbed" runat="server" /></td>
    </tr>
</table>  
    <br />
        <table width="15%" id="pnlGridView1" runat="server" class="tableRawMaterial">
            <tr>
                <th><asp:Label ID="lblChildPart" runat="server" Text="Raw Material"/></th>
                <th><asp:Label ID="lblChildPartQty" runat="server" Text="Material Grade"/></th>
            </tr>
            <tr>
                <td><asp:Label ID="dataChildPart" runat="server" /></td>
                <td><asp:Label ID="dataChildPartQty" runat="server" /></td>
            </tr>
        </table>
<center>
    <asp:Button ID="print" runat="server" CssClass="nextPage" Text="Print" OnClientClick="javascript:PrintPage()"/>
    <asp:Button ID="back" runat="server" CssClass="nextPage" Text="Back" OnClick="back_Click"/>
</center>
</asp:Content>
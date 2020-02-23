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
             var printContent2 = document.getElementById('<%= childPartGridView.ClientID %>');

            var printWindow = window.open("Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');
            printWindow.document.write('<html><head><style> .tableRawMaterial { margin: 0 auto; }'
                + '.tableRawMaterial td,th{ border: 1px solid black; text-align: center; width: 100px; height: 50px; margin: 0 auto; }'
                + '</style>'
                + '<title> Assembly Details | PB Plastics </title></head><body>');
            printWindow.document.write('<table class="tableRawMaterial" width="90%">');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.write('</table></br>');
             printWindow.document.write('<table class="tableRawMaterial" width="50%"><tr>');
            printWindow.document.write(printContent2.innerHTML);
            printWindow.document.write('</tr></table></br>');
            printWindow.document.close();
            printWindow.focus();
             printWindow.print();
             Application["assemblyNo"] = null;
             Application["viewDetailsId"] = null;

        }
</script>
    <br />
    <table width="40%" id="pnlGridView" runat="server" class="tableRawMaterial">

    <tr>
        <td colspan="5" style="font-weight:900; background-color:SkyBlue;">
            <h4><asp:Label ID="assNameLabel" runat="server"/></h4>
        </td>
    </tr>
    <tr>
        <th><asp:Label ID="lblAssNo" runat="server" Text="Assembly No"/></th>
        <th><asp:Label ID="lblUOM" runat="server" Text="UOM"/></th>
        <th><asp:Label ID="lblAssWeight" runat="server" Text="Assembly Weight"/></th>
        <th><asp:Label ID="lblTargetQuant" runat="server" Text="Target Quantity"/></th>
        <th><asp:Label ID="lblAssemblyFileUpload" runat="server" Text="Attachment"/></th>
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
    <asp:GridView ID="childPartGridView" ShowFooter="False" DataKeyNames="id"
    ShowHeaderWhenEmpty="True" runat="server" AutoGenerateColumns="false" BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CssClass="Table1"
        OnLoad="childPartDataGrid_Load" Width="20%">
    <HeaderStyle CssClass="CustomerHeader" Height="5px" ForeColor="black" Font-Size="Small" />
    <RowStyle Font-Size="Smaller" ForeColor="black" BackColor="WhiteSmoke"/>
        <Columns>
            <asp:TemplateField HeaderText="CHILD PART" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblChildPart" Text='<%# Eval("child_part") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CHILD PART QTY" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblChildPartQty" Text='<%# Eval("child_part_qty") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
<center>
    <asp:Button ID="print" runat="server" CssClass="nextPage" Text="Print" OnClientClick="javascript:PrintPage()"/>
    <asp:Button ID="back" runat="server" CssClass="nextPage" Text="Back" OnClick="back_Click"/>
</center>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newWorkOrderMaster.aspx.cs" Inherits="ERP_Demo.newWorkOrderMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Height="100%" Width="100%" CssClass="Table1">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="4"><h3>Work Order Master</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server">CUSTOMER NAME.</asp:TableCell>
            <asp:TableCell runat="server">CUSTOMER PO NO</asp:TableCell>
            <asp:TableCell runat="server">CUSTOMER PO DELIVERY DATE</asp:TableCell>
            <asp:TableCell runat="server">EXPECTED DELIVERY DATE</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server"><asp:DropDownList ID="customerNameDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="customerPoNoTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="customerPoDelDateTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="expectedDelDateTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">WORK ORDER NO</asp:TableCell>
            <asp:TableCell runat="server">PART NAME</asp:TableCell>
            <asp:TableCell runat="server">QUANTITY REQUIRED IN NO's.</asp:TableCell>
            <asp:TableCell runat="server">STOCK QUANTITY</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server"><asp:TextBox ID="workOrderNoTextBox" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:DropDownList ID="partNameDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="quantityReqInNosTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="stockQuantityTextbox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">MACHINE NO</asp:TableCell>
            <asp:TableCell runat="server">If to supplier then select</asp:TableCell>
            <asp:TableCell runat="server">R/M REQUIRED</asp:TableCell>
            <asp:TableCell runat="server">MB REQUIRED</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server"><asp:DropDownList ID="mmachineNoDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
            <asp:TableCell runat="server"><asp:DropDownList ID="supplierDropDownList" runat="server"></asp:DropDownList></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="rmRequiredTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="mbRequiredTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">PACKING DETAILS</asp:TableCell>
            <asp:TableCell runat="server">JIG/FIXTURE REQUIREMENT</asp:TableCell>
            <asp:TableCell runat="server">MATERIAL ISSUED (kgs) </asp:TableCell>
            <asp:TableCell runat="server">EXPECTED QUANTITY</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server"><asp:TextBox ID="packingDetailsTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="jigReqTextBox" runat="server" ReadOnly="true">DEFAULT VALUE</asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="materialIssuedTextBox" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server"><asp:TextBox ID="expectedQuantityTextBox" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server"></asp:TableCell>
            <asp:TableCell runat="server"></asp:TableCell>
            <asp:TableCell runat="server"></asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="Save" Font-Size="Small" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayPartsWorker.aspx.cs" Inherits="ERP_Demo.displayPartsWorker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayPartsTable" runat="server" HorizontalAlign="Center" CssClass="Table1">
        <asp:TableRow runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellLabel"><asp:Label ID="machineLabel" runat="server"><h5><u>PARTS MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellLink" runat="server"><asp:Button ID="machineButton" runat="server" Text="ADD NEW" Font-Size="Small"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellList" runat="server" style="padding-left:450px;padding-right:300px;"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke"><h5><u>PARTS LIST</u></h5></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellSearch" runat="server"><asp:Label ID="Label2" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label>
            <asp:TextBox ID="searchTextBox" runat="server" Height="18px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="partsGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True" OnPageIndexChanging="partsGridView_PageIndexChanging"
                OnRowCommand="parts_RowCommand" OnRowCancelingEdit="parts_RowCancelingEdit"
                CssClass="Table1" AllowPaging="True" PageSize="25" HorizontalAlign="Center">
                <%-- Theme Properties --%>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                <HeaderStyle CssClass="CustomerHeader" Height="5px"  ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="blue" HorizontalAlign="center" />
                <RowStyle ForeColor="black" Font-Size="Small" BackColor="WhiteSmoke"/>
                <Columns>
                    <asp:TemplateField HeaderText="PART NO" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("part_no") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PART NAME" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="lblPartName" Text='<%# Eval("part_name") %>' runat="server"/>
                            <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server"
                                TargetControlID="lblPartName" PopupControlID="panelPopUp"
                                PopDelay="20" OffsetX="310" OffsetY="-160">
                            </cc1:HoverMenuExtender>
                            <asp:Panel ID="panelPopUp" runat="server">
                                <asp:Image ImageUrl='<%# Eval("part_photo") %>' runat="server" Height="300" Width="300" />
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="MORE DETAILS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="170">
                       <ItemTemplate>
                           <asp:Button CssClass="nextPage" ID="viewDetailsButton" Text="Details" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="viewDetails" Height="27"/>
                       </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
    </asp:GridView>
</asp:Content>


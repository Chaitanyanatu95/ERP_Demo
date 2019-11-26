<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayPartsWorker.aspx.cs" Inherits="ERP_Demo.displayPartsWorker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayPartsTable" runat="server" CssClass="Table1">
        <asp:TableRow ID="rowPartsDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellParts" ColumnSpan="3"><asp:Label ID="partsLabel" runat="server"><h3>Parts Details</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowPartsLink" runat="server">
            <asp:TableCell ID="cellPartsSearch" runat="server"><asp:Label ID="searchLabel" runat="server">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellPartsSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>

     <asp:GridView ID="partsGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True" OnPageIndexChanging="partsGridView_PageIndexChanging"
                OnRowCommand="parts_RowCommand" OnRowCancelingEdit="parts_RowCancelingEdit"
                alternatingrowstyle-backcolor="Linen" headerstyle-backcolor="SkyBlue"
                CellPadding="4" CssClass="Table1" Font-Size="Medium" AllowPaging="True" PageSize="15">
                <%-- Theme Properties --%>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                <PagerStyle BackColor="White" ForeColor="blue" HorizontalAlign="center" />
                <Columns>
                    <asp:TemplateField HeaderText="Part No" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("part_no") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Part Name" ItemStyle-Width="200">
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
                   <asp:TemplateField HeaderText="More Details" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="19">
                       <ItemTemplate>
                           <asp:Button CssClass="nextPage" ID="viewDetailsButton" Text="Details" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="viewDetails" Height="27"/>
                       </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
    </asp:GridView>
</asp:Content>


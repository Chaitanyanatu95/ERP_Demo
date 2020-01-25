<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayParts.aspx.cs" Inherits="ERP_Demo.displayParts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayPartsTable" runat="server" CssClass="Table1">
        <asp:TableRow ID="rowPartsDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellParts" ColumnSpan="3"><asp:Label ID="partsLabel" runat="server"><h3><u>PARTS DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowPartsLink" runat="server">
            <asp:TableCell ID="cellPartsLink" runat="server"><asp:Button ID="partsButton" runat="server" Text="ADD NEW" Font-Size="X-Small" OnClick="partsButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellPartsSearch" runat="server"><asp:Label ID="searchLabel" runat="server" Font-Size="Smaller">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellPartsText" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
            <asp:TableCell ID="cellPartsSearchButton" runat="server">&nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="X-Small" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="partsGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True" OnPageIndexChanging="partsGridView_PageIndexChanging"
                OnRowCommand="parts_RowCommand" OnRowCancelingEdit="parts_RowCancelingEdit" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                OnRowDeleting="parts_RowDeleting" CssClass="Table1" Font-Size="Medium" AllowPaging="True" PageSize="15">
                <%-- Theme Properties --%>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="blue" HorizontalAlign="center" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>
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
                   <asp:TemplateField HeaderText="MORE DETAILS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="19">
                       <ItemTemplate>
                           <asp:Button CssClass="nextPage" ID="viewDetailsButton" Text="Details" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="viewDetails" Height="27"/>
                       </ItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText ="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" Width="20px" Height="20px" CommandArgument='<%#Eval("part_no")+","+ Eval("part_name")%>' OnClientClick="return confirm('Do you want to Edit?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText ="DELETE" HeaderStyle-Width="75px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="DELETE" Width="20px" Height="20px" OnClientClick="return confirm('Do you want to Delete?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
    </asp:GridView>
        <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayParts.aspx.cs" Inherits="ERP_Demo.displayParts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayPartsTable" runat="server" HorizontalAlign="Center" CssClass="Table1">
        <asp:TableRow runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellLabel"><asp:Label ID="machineLabel" runat="server"><h5><u>PART MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellLink" runat="server"><asp:Button ID="machineButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="partsButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellList" runat="server" style="padding-left:550px;padding-right:380px;"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke"><h5><u>PART LIST</u></h5></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellSearch" runat="server"><asp:Label ID="Label2" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label>
            <asp:TextBox ID="searchTextBox" runat="server" Height="18px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="partsGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True" OnPageIndexChanging="partsGridView_PageIndexChanging"
                OnRowCommand="parts_RowCommand" OnRowCancelingEdit="parts_RowCancelingEdit" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                OnRowDeleting="parts_RowDeleting" CssClass="Table1" AllowPaging="True" PageSize="25" HorizontalAlign="Center" Width="50%">
                <%-- Theme Properties --%>
                <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="&nbsp;&nbsp;Last" NextPageText="Next" PreviousPageText="&nbsp;&nbsp;Previous"/>
                <HeaderStyle CssClass="CustomerHeader" Height="15px" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="blue" HorizontalAlign="Center" />
                <RowStyle ForeColor="Black" Font-Size="Smaller"/>
                <Columns>
                    <asp:TemplateField HeaderText="PART NO">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("part_no") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PART NAME">
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
                           <asp:Button CssClass="nextPage" ID="viewDetailsButton" Text="Details" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="viewDetails"/>
                       </ItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText ="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" Width="15px" Height="15px" CommandArgument='<%#Eval("part_no")+","+ Eval("part_name")%>' OnClientClick="return confirm('Do you want to Edit?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText ="DELETE" HeaderStyle-Width="75px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="DELETE" Width="17px" Height="17px" OnClientClick="return confirm('Do you want to Delete?');" />
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


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayVendor.aspx.cs" Inherits="ERP_Demo.displayVendor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayVendorTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowVendorDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellVendor" ColumnSpan="3"><asp:Label ID="vendorLabel" runat="server"><h3><u>VENDOR DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowVendorLink" runat="server">
            <asp:TableCell ID="cellVendorLink" runat="server"><asp:Button ID="vendorButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="vendorButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellVendorSearch" runat="server"><asp:Label ID="searchLabel" runat="server">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellVendorSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="vendorGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowCommand="vendorGridView_RowCommand" OnRowCancelingEdit="vendorGridView_RowCancelingEdit"
                OnRowDeleting="vendorGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" CssClass="Table1" Width="90%">
                <%-- Theme Properties --%>
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>  
                <Columns>
                    <asp:TemplateField HeaderText="VENDOR ID">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_id") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VENDOR NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_name") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VENDOR ADDRESS 1">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_address_one") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VENDOR ADDRESS 2">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_address_two") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VENDOR PHONE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_contact") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VENDOR EMAIL-ID">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_email") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VENDOR CONTACT">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_contact_person") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VENDOR GST">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("vendor_gst_details") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("id") %>' Width="20px" Height="20px" OnClientClick="return confirm('Do you want to edit?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="DELETE" HeaderStyle-Width="75px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="DELETE" Width="20px" Height="20px" OnClientClick="return confirm('Do you want to delete?');"/>
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

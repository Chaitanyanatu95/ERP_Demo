<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayPackaging.aspx.cs" Inherits="ERP_Demo.displayPackaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayPackagingTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowPackagingDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellPackaging" ColumnSpan="3"><asp:Label ID="packagingLabel" runat="server"><h3><u>PACKAGING DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowPackagingLink" runat="server">
            <asp:TableCell ID="cellPackagingLink" runat="server"><asp:Button ID="packagingButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="packagingButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellPackagingSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellPackagingSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="packagingGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="packagingGridView_RowEditing" OnRowCancelingEdit="packagingGridView_RowCancelingEdit"
                OnRowCommand="packagingGridView_RowCommand" OnRowDeleting="packagingGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CssClass="Table1" Width="30%">
                <%-- Theme Properties --%>
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>
                <Columns>
                    <asp:TemplateField HeaderText="PACKAGING TYPE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("packaging_type") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SIZE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("size") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText ="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("id") %>' Width="20px" Height="20px" OnClientClick="return confirm('Do you want to edit?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText ="DELETE" HeaderStyle-Width="75px">
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

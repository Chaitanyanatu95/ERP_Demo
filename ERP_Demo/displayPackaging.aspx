<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayPackaging.aspx.cs" Inherits="ERP_Demo.displayPackaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayPackagingTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowPackagingDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellPackaging" ColumnSpan="3"><asp:Label ID="packagingLabel" runat="server"><h3>Packaging Details</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowPackagingLink" runat="server">
            <asp:TableCell ID="cellPackagingLink" runat="server"><asp:Button ID="packagingButton" runat="server" Text="ADD" Font-Size="Small" OnClick="packagingButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellPackagingSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellPackagingSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="packagingGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="packagingGridView_RowEditing" OnRowCancelingEdit="packagingGridView_RowCancelingEdit"
                OnRowCommand="packagingGridView_RowCommand" OnRowDeleting="packagingGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="0px" CellPadding="4" CssClass="Table1" Width="30%" Font-Size="Medium">
                <%-- Theme Properties --%>
                <HeaderStyle BackColor="#DFDDDD" Font-Bold="True" ForeColor="black" Font-Size="Medium" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />        
                <Columns>
                    <asp:TemplateField HeaderText="Packaging Type">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("packaging_type") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("size") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
    </asp:GridView>
        <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

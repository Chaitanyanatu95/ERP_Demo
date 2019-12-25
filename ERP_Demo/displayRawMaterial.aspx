<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayRawMaterial.aspx.cs" Inherits="ERP_Demo.displayRawMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayRawMaterialTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowRawMaterialDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellRawMaterial" ColumnSpan="3"><asp:Label ID="rawMaterialLabel" runat="server"><h3><u>RAW MATERIAL DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowRawMaterialLink" runat="server">
            <asp:TableCell ID="cellRawMaterialLink" runat="server"><asp:Button ID="rawMaterialButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="rawMaterialButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellRawMaterialSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp;</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellRawMaterialSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
            </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="rawMaterialGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="rawMaterialGridView_RowEditing" OnRowCancelingEdit="rawMaterialGridView_RowCancelingEdit"
                OnRowCommand="rawMaterialGridView_RowCommand" OnRowDeleting="rawMaterialGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="Table1" Width="70%">
                <%-- Theme Properties --%>
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>
                <FooterStyle BackColor="WhiteSmoke" />
                <Columns>
                    <asp:TemplateField HeaderText="RAW MATERIAL NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_name") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMName" Text='<%# Eval("material_name") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RAW MATERIAL GRADE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_grade") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMGrade" Text='<%# Eval("material_grade") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RAW MATERIAL COLOR">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_color") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMColor" Text='<%# Eval("material_color") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RAW MATERIAL MAKE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_make") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMMake" Text='<%# Eval("material_make") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("id") %>' Width="20px" Height="20px" OnClientClick="return confirm('Do you want to edit?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DELETE" HeaderStyle-Width="75px">
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

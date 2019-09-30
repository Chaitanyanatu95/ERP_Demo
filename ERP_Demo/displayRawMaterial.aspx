<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayRawMaterial.aspx.cs" Inherits="ERP_Demo.displayRawMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayRawMaterialTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowRawMaterialDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellRawMaterial" ColumnSpan="3"><asp:Label ID="rawMaterialLabel" runat="server"><h3>RawMaterial Details</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowRawMaterialLink" runat="server">
            <asp:TableCell ID="cellRawMaterialLink" runat="server"><asp:Button ID="rawMaterialButton" runat="server" Text="ADD" Font-Size="Small" OnClick="rawMaterialButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellRawMaterialSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp;</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellRawMaterialSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
            </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="rawMaterialGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="rawMaterialGridView_RowEditing" OnRowCancelingEdit="rawMaterialGridView_RowCancelingEdit"
                OnRowCommand="rawMaterialGridView_RowCommand" OnRowDeleting="rawMaterialGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="0px" CellPadding="4" CssClass="Table1" Width="70%" Font-Size="Medium">
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
                    <asp:TemplateField HeaderText="RawMaterial Name">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_name") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMName" Text='<%# Eval("material_name") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RawMaterial Grade">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_grade") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMGrade" Text='<%# Eval("material_grade") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RawMaterial Color">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_color") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMColor" Text='<%# Eval("material_color") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RawMaterial Make">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_make") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMMake" Text='<%# Eval("material_make") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" CommandArgument='<%# Eval("id") %>' ToolTip="Edit" Width="20px" Height="20px"/>
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

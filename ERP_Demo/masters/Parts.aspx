<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parts.aspx.cs" Inherits="ERP_Demo.masters.Parts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <h2>Parts Master</h2>
        <asp:GridView ID="PartsGrid" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="id"
            ShowHeaderWhenEmpty="true"
            OnRowCommand="PartsGrid_RowCommand" OnRowEditing="PartsGrid_RowEditing" OnRowCancelingEdit="PartsGrid_RowCancelingEdit"
            OnRowUpdating="PartsGrid_RowUpdating" OnRowDeleting="PartsGrid_RowDeleting"
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="PartsGrid_SelectedIndexChanged">
            <%-- Theme Properties --%>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

            <Columns>
                <asp:TemplateField HeaderText="Part Name">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("part_name") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPartName" Text='<%# Eval("part_name") %>' runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPartNameFooter" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Part Weight">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("part_weight") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPartWeight" Text='<%# Eval("part_weight") %>' runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPartWeightFooter" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mold Name">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("mold_name") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMoldName" Text='<%# Eval("mold_name") %>' runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtMoldNameFooter" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Drawing No.">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("drawing_no") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDrawingNo" Text='<%# Eval("drawing_no") %>' runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDrawingNoFooter" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No. of cavities">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("no_of_cavities") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNo_Cavities" Text='<%# Eval("no_of_cavities") %>' runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNo_CavitiesFooter" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Part Photo">
                    <ItemTemplate>
                        <asp:Image ID="PartPhoto" runat="server" ImageUrl='<%# Eval("part_photo") %>' Height="80px" Width="100px" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Image ID="imgPartPhoto" runat="server" ImageUrl='<%# Eval("part_photo") %>' Height="80px" Width="100px" />
                        <asp:FileUpload ID="editFileUpload" runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:FileUpload ID="newFileUpload" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                        <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                        <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
        <br />
        <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
    </center>
</asp:Content>

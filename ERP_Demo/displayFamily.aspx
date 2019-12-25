<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayFamily.aspx.cs" Inherits="ERP_Demo.displayFamily" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayFamilyTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowFamilyDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="FamilyCell" ColumnSpan="3"><asp:Label ID="familyLabel" runat="server"><h3><u>PRODUCT CATEGORY</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowFamilyLink" runat="server">
            <asp:TableCell ID="cellFamilyLink" runat="server"><asp:Button ID="familyButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="familyButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellFamilySearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellFamilySearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="familyGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowCommand="familyGridView_RowCommand" OnRowCancelingEdit="familyGridView_RowCancelingEdit"
                OnRowDeleting="familyGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CssClass="Table1" Width="20%">
                <%-- Theme Properties --%>
                <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <FooterStyle BackColor="WhiteSmoke" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>   
                <Columns>
                    <asp:TemplateField HeaderText="PRODUCT CATEGORY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Family") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtfamily" Text='<%# Eval("Family") %>' runat="server" />
                        </EditItemTemplate>
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
            <asp:Label ID="lblDownTimeCodeSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblDownTimeCodeErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

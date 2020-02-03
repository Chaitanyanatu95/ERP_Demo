﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayAssemble.aspx.cs" Inherits="ERP_Demo.displayAssemble" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayFamilyTable" runat="server" HorizontalAlign="Center" CssClass="Table1">
        <asp:TableRow runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellLabel"><asp:Label ID="customerLabel" runat="server" ><h5><u>ASSEMBLE MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellLink" runat="server"><asp:Button ID="assembleButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="assembleButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellList" runat="server" style="padding-left:440px;padding-right:290px;"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke" ><h5><u>ASSEMBLE LIST</u></h5></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellSearch" runat="server"><asp:Label ID="Label2" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label>
            <asp:TextBox ID="searchTextBox" runat="server" Height="18px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="assembleGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowCommand="assembleGridView_RowCommand" OnRowCancelingEdit="assembleGridView_RowCancelingEdit"
                OnRowDeleting="assembleGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CssClass="Table1" Width="20%" PageSize="15" AllowPaging="true" OnPageIndexChanging="assembleGridView_PageIndexChanging">
                <%-- Theme Properties --%>
                <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="&nbsp;&nbsp;Last" NextPageText="Next" PreviousPageText="&nbsp;&nbsp;Previous"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle Font-Size="Small" ForeColor="black" BackColor="WhiteSmoke"/>   
                <Columns>
                    <asp:TemplateField HeaderText="ASSEMBLE PART">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("assemble_part") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtfamily" Text='<%# Eval("assemble_part") %>' runat="server" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText ="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("id") %>' Width="15px" Height="15px" OnClientClick="return confirm('Do you want to edit?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText ="DELETE" HeaderStyle-Width="75px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="DELETE" Width="17px" Height="17px" OnClientClick="return confirm('Do you want to delete?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
    </asp:GridView>
        <center>
            <asp:Label ID="lblFamilySuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblFamilyErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

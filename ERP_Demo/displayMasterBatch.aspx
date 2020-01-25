<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayMasterBatch.aspx.cs" Inherits="ERP_Demo.displayMasterBatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayMasterbatchTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowMasterbatchDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellMasterbatch" ColumnSpan="3"><asp:Label ID="masterbatchLabel" runat="server"><h3><u>MASTERBATCH DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowMasterbatchLink" runat="server">
            <asp:TableCell ID="cellMasterbatchLink" runat="server"><asp:Button ID="masterbatchButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="masterbatchButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellMasterbatchSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp;</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellMasterbatchSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="masterbatchGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="masterbatchGridView_RowEditing" OnRowCancelingEdit="masterbatchGridView_RowCancelingEdit"
                OnRowCommand="masterbatchGridView_RowCommand" OnRowDeleting="masterbatchGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CellPadding="4" CssClass="Table1" Width="70%">
                <%-- Theme Properties --%>
                <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" Height="40px" BackColor="WhiteSmoke"/>   
                <Columns>
                    <asp:TemplateField HeaderText="MASTERBATCH NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_name") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBName" Text='<%# Eval("mb_name") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH GRADE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_grade") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBGrade" Text='<%# Eval("mb_grade") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH MFG">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_mfg") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBMfg" Text='<%# Eval("mb_mfg") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH COLOR">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_color") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBColor" Text='<%# Eval("mb_color") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH COLOR CODE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_color_code") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBCCode" Text='<%# Eval("mb_color_code") %>' runat="server" />
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
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

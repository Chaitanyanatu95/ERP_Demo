<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayDownTimeCode.aspx.cs" Inherits="ERP_Demo.displayDownTimeCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayDownTimeCodeTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowDownTimeCodeDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="downTimeCodeCell" ColumnSpan="3"><asp:Label ID="downTimeCodeLabel" runat="server"><h3>Down Time Code Details</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowDownTimeCodeLink" runat="server">
            <asp:TableCell ID="cellDownTimeCodeLink" runat="server"><asp:Button ID="customerButton" runat="server" Text="ADD" Font-Size="Small" OnClick="customerButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellDownTimeCodeSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellDownTimeCodeSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="downtimeGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowCommand="downtimeGridView_RowCommand"
                OnRowCancelingEdit="downtimeGridView_RowCancelingEdit"
                OnRowDeleting="downtimeGridView_RowDeleting"
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
                    <asp:TemplateField HeaderText="Down Time Code">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("down_time_code") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Down Time Type">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("down_time_type") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("Id") %>' Width="20px" Height="20px" OnClientClick="return confirm('Do you want to edit?');"/>
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


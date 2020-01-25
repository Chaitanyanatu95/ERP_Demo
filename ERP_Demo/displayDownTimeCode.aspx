<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayDownTimeCode.aspx.cs" Inherits="ERP_Demo.displayDownTimeCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayDownTimeCodeTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowDownTimeCodeDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="downTimeCodeCell" ColumnSpan="3"><asp:Label ID="downTimeCodeLabel" runat="server"><h3><u>DOWN TIME DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowDownTimeCodeLink" runat="server">
            <asp:TableCell ID="cellDownTimeCodeLink" runat="server"><asp:Button ID="customerButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="customerButton_Click"></asp:Button>
            </asp:TableCell>
            <asp:TableCell ID="cellDownTimeCodeSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp;</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellDownTimeCodeSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="downtimeGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowCommand="downtimeGridView_RowCommand"
                OnRowCancelingEdit="downtimeGridView_RowCancelingEdit"
                OnRowDeleting="downtimeGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px"  CssClass="Table1" Width="30%">
                <%-- Theme Properties --%>
                <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="false" Font-Size="Small"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>
                <Columns>
                    <asp:TemplateField HeaderText="DOWN TIME CODE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("down_time_code") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOWN TIME TYPE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("down_time_type") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText ="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("Id") %>' Width="20px" Height="20px" OnClientClick="return confirm('Do you want to edit?');"/>
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


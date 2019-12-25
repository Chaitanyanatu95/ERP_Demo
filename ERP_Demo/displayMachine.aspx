<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayMachine.aspx.cs" Inherits="ERP_Demo.displayMachine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayMachineTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowMachineDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="MachineCell" ColumnSpan="3"><asp:Label ID="MachineLabel" runat="server"><h3><u>MACHINE DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowMachineLink" runat="server">
            <asp:TableCell ID="cellMachineLink" runat="server"><asp:Button ID="machineButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="machineButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellMachineSearch" runat="server"><asp:Label ID="searchLabel" runat="server">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellMachineSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="machineGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="machineGridView_RowEditing" OnRowCancelingEdit="machineGridView_RowCancelingEdit"
                OnRowCommand="machineGridView_RowCommand" OnRowDeleting="machineGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="Black" BorderStyle="solid" BorderWidth="1px" CellPadding="4" CssClass="Table1" Width="50%">
                <%-- Theme Properties --%>
                <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>    
                <FooterStyle BackColor="WhiteSmoke" />
                <Columns>
                    <asp:TemplateField HeaderText="MACHINE NO">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("machine_no") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MACHINE NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("machine_name") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MACHINE FILE UPLOAD">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("machine_file_upload") %>' runat="server" />
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

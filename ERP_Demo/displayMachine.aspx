<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayMachine.aspx.cs" Inherits="ERP_Demo.displayMachine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayMachineTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowMachineDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="MachineCell" ColumnSpan="3"><asp:Label ID="MachineLabel" runat="server"><h3>Machine Details</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowMachineLink" runat="server">
            <asp:TableCell ID="cellMachineLink" runat="server"><asp:Button ID="machineButton" runat="server" Text="ADD" Font-Size="Small" OnClick="machineButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellMachineSearch" runat="server"><asp:Label ID="searchLabel" runat="server">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellMachineSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="machineGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="machineGridView_RowEditing" OnRowCancelingEdit="machineGridView_RowCancelingEdit"
                OnRowCommand="machineGridView_RowCommand" OnRowDeleting="machineGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="0px" CellPadding="4" CssClass="Table1" Width="40%" Font-Size="Medium">
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
                    <asp:TemplateField HeaderText="Machine No">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("machine_no") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Machine Name">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("machine_name") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Machine File Upload">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("machine_file_upload") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" CommandArgument='<%# Eval("id") %>' ToolTip="Edit" Width="20px" Height="20px"/>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px"/>
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

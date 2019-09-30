<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayShift.aspx.cs" Inherits="ERP_Demo.displayShift" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayShiftTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowShiftDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="ShiftCell" ColumnSpan="3"><asp:Label ID="ShiftLabel" runat="server"><h3>Shift</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowShiftLink" runat="server">
            <asp:TableCell ID="cellShiftLink" runat="server"><asp:Button ID="shiftButton" runat="server" Text="ADD" Font-Size="Small" OnClick="shiftButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellShiftSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellShiftSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="shiftGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="shiftGridView_RowEditing" OnRowCancelingEdit="shiftGridView_RowCancelingEdit"
                OnRowCommand="shiftGridView_RowCommand" OnRowDeleting="shiftGridView_RowDeleting"
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
                    <asp:TemplateField HeaderText="Shift Time">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("shift_time") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShiftTime" Text='<%# Eval("shift_time") %>' runat="server" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shift Hours">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("hours") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShiftHours" Text='<%# Eval("hours") %>' runat="server" />
                        </EditItemTemplate>
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


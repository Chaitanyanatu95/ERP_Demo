<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayWorker.aspx.cs" Inherits="ERP_Demo.displayWorker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayWorkerTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowWorkerDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellWorker" ColumnSpan="3"><asp:Label ID="workerLabel" runat="server"><h3>Worker Details</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowWorkerLink" runat="server">
            <asp:TableCell ID="cellWorkerLink" runat="server"><asp:Button ID="workerButton" runat="server" Text="ADD" Font-Size="Small" OnClick="workerButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellWorkerSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellWorkerSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="workerGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="workerGridView_RowEditing" OnRowCancelingEdit="workerGridView_RowCancelingEdit"
                OnRowCommand="workerGridView_RowCommand" OnRowDeleting="workerGridView_RowDeleting"
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
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("worker_name") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWName" Text='<%# Eval("worker_name") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Id">
                        <ItemTemplate>
                            <asp:Label ID="lblWId" Text='<%# Eval("worker_id") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWId" Text='<%# Eval("worker_id") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Id">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("user_id") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUId" Text='<%# Eval("user_id") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Password">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("user_password") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUPassword" Text='<%# Eval("user_password") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Admin Right">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Admin") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights1" Text='<%# Eval("Admin") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Editor Right">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Editor") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights2" Text='<%# Eval("Editor") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Right">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Worker") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights3" Text='<%# Eval("Worker") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Extra Right">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Extra") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights4" Text='<%# Eval("Extra") %>' runat="server" />
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

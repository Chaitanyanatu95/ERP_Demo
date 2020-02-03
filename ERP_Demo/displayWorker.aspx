<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayWorker.aspx.cs" Inherits="ERP_Demo.displayWorker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayWorkerTable" runat="server" HorizontalAlign="Center" CssClass="Table1">
        <asp:TableRow ID="rowCustomerDisplay" runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellWorker"><asp:Label ID="customerLabel" runat="server" ><h5><u>WORKER MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellWorkerLink" runat="server"><asp:Button ID="workerButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="workerButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellWorkerList" runat="server" style="padding-left:450px;padding-right:300px;"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke"><h5><u>WORKER LIST</u></h5></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellWorkerSearch" runat="server">
                <asp:Label ID="Label2" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label>
                <asp:TextBox ID="searchWorkerTextBox" runat="server" Height="18px"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="workerGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True" AllowPaging="true"
                OnRowEditing="workerGridView_RowEditing" OnRowCancelingEdit="workerGridView_RowCancelingEdit"
                OnRowCommand="workerGridView_RowCommand" OnRowDeleting="workerGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="Table1" Width="90%" PageSize="15" OnPageIndexChanging="workerGridView_PageIndexChanging">
                <%-- Theme Properties --%>
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="&nbsp;&nbsp;Last" NextPageText="Next" PreviousPageText="&nbsp;&nbsp;Previous"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Font-Size="Small"/>      
                <Columns>
                    <asp:TemplateField HeaderText="EMPLOYEE NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("worker_name") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWName" Text='<%# Eval("worker_name") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMPLOYEE ID">
                        <ItemTemplate>
                            <asp:Label ID="lblWId" Text='<%# Eval("worker_id") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWId" Text='<%# Eval("worker_id") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="USER ID">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("user_id") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUId" Text='<%# Eval("user_id") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="USER PASSWORD">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("user_password") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUPassword" Text='<%# Eval("user_password") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Full Access">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Full_Access") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights1" Text='<%# Eval("Full_Access") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transactions">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Transactions") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights2" Text='<%# Eval("Transactions") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reports">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Reports") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights3" Text='<%# Eval("Reports") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selected Access">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Selected_Access") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights4" Text='<%# Eval("Selected_Access") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Access">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Access") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRights4" Text='<%# Eval("Access") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("id") %>' Width="15px" Height="15px" OnClientClick="return confirm('Do you want to edit?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DELETE" HeaderStyle-Width="75px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="DELETE" Width="17px" Height="17px" OnClientClick="return confirm('Do you want to delete?');"/>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayMachine.aspx.cs" Inherits="ERP_Demo.displayMachine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayMachineTable" runat="server" HorizontalAlign="Center" CssClass="Table1">
        <asp:TableRow runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellLabel"><asp:Label ID="machineLabel" runat="server"><h5><u>MACHINE MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellLink" runat="server"><asp:Button ID="machineButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="machineButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellList" runat="server" style="padding-left:450px;padding-right:300px;"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke"><h5><u>MACHINE LIST</u></h5></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellSearch" runat="server"><asp:Label ID="Label2" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label>
            <asp:TextBox ID="searchTextBox" runat="server" Height="18px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="machineGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="machineGridView_RowEditing" OnRowCancelingEdit="machineGridView_RowCancelingEdit"
                OnRowCommand="machineGridView_RowCommand" OnRowDeleting="machineGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="Black" BorderStyle="solid" BorderWidth="1px" CellPadding="4" CssClass="Table1" Width="50%" AllowPaging="true" PageSize="25" OnPageIndexChanging="machineGridView_PageIndexChanging">
                <%-- Theme Properties --%>
                <HeaderStyle Height="5px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="&nbsp;&nbsp;Last" NextPageText="Next" PreviousPageText="&nbsp;&nbsp;Previous"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Font-Size="Small"/>    
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
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

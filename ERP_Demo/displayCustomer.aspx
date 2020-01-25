<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayCustomer.aspx.cs" Inherits="ERP_Demo.displayCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayCustomerTable" runat="server" CssClass="Table1" HorizontalAlign="Center">
        <asp:TableRow ID="rowCustomerDisplay" runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellCustomer"><asp:Label ID="customerLabel" runat="server" ><h5><u>CUSTOMER MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellCustomerLink" runat="server"><asp:Button ID="customerButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="customerButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="TableCell1" runat="server" ColumnSpan="1"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke" style="padding:0px 870px 0px 870px"><h4><u>CUSTOMER LIST</u></h4></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellCustomerSearch" runat="server" CssClass="heading"  ><asp:Label ID="searchLabel" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellCustomerSearchText" runat="server"><asp:TextBox ID="searchTextBox" runat="server" Height="18px"></asp:TextBox></asp:TableCell>
            <asp:TableCell ID="cellCustomerSearchButton" runat="server">&nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="customerGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowCommand="customer_RowCommand" OnRowCancelingEdit="customer_RowCancelingEdit"
                OnRowDeleting="customer_RowDeleting" BackColor="#DFDDDD" BorderColor="black" AllowPaging="true" BorderStyle="solid" BorderWidth="1px" 
                CssClass="Table1" Width="90%" PageSize="15" OnPageIndexChanging="customerGridView_PageIndexChanging">
                <%-- Theme Properties --%>
                <HeaderStyle Height="30px" CssClass="CustomerHeader" Font-Bold="false" Font-Size="Small"/>
                <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="center" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>
                <Columns>
                    <asp:TemplateField HeaderText="CUSTOMER NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("customer_name") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCName" Text='<%# Eval("customer_name") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CUSTOMER ADDRESS 1">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("customer_address_one") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCAddOne" Text='<%# Eval("customer_address_one") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CUSTOMER ADDRESS 2">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("customer_address_two") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCAddTwo" Text='<%# Eval("customer_address_two") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CUSTOMER PHONE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("customer_contact") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCContact" Text='<%# Eval("customer_contact") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CUSTOMER EMAIL-ID">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("customer_email") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCEmail" Text='<%# Eval("customer_email") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CUSTOMER CONTACT">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("customer_contact_person") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCContactPerson" Text='<%# Eval("customer_contact_person") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CUSTOMER GST">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("customer_gst_details") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCGstDetails" Text='<%# Eval("customer_gst_details") %>' runat="server" />
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayCustomer.aspx.cs" Inherits="ERP_Demo.displayCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayCustomerTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowCustomerDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="cellCustomer" ColumnSpan="3"><asp:Label ID="customerLabel" runat="server"><h3><u>CUSTOMER DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowCustomerLink" runat="server">
            <asp:TableCell ID="cellCustomerLink" runat="server"><asp:Button ID="customerButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="customerButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellCustomerSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellCustomerSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="customerGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowCommand="customer_RowCommand" OnRowCancelingEdit="customer_RowCancelingEdit"
                OnRowUpdating="customer_RowUpdating" OnRowDeleting="customer_RowDeleting"
                BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CssClass="Table1" Width="90%">
                <%-- Theme Properties --%>
                <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="false" Font-Size="Small"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>
                <FooterStyle BackColor="WhiteSmoke" />
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

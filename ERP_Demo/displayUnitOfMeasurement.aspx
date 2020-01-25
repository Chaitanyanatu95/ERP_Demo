<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayUnitOfMeasurement.aspx.cs" Inherits="ERP_Demo.displayUnitOfMeasurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayUnitOfMeasurementTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowUnitOfMeasurementDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="UnitOfMeasurementCell" ColumnSpan="3"><asp:Label ID="UnitOfMeasurementLabel" runat="server"><h3><u>UOM DETAILS</u></h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowUnitOfMeasurementLink" runat="server">
            <asp:TableCell ID="cellUnitOfMeasurementLink" runat="server"><asp:Button ID="unitOfMeasurementButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="unitOfMeasurementButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellUnitOfMeasurementSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellUnitOfMeasurementSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="unitOfMeasurementGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="unitOfMeasurementGridView_RowEditing" OnRowCancelingEdit="unitOfMeasurementGridView_RowCancelingEdit"
                OnRowCommand="unitOfMeasurementGridView_RowCommand" OnRowDeleting="unitOfMeasurementGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="Table1" Width="25%">
                <%-- Theme Properties --%>
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/> 
                <Columns>
                    <asp:TemplateField HeaderText="UNIT OF MEASUREMENT">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("unit_of_measurement") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUnitOfMeasurement" Text='<%# Eval("unit_of_measurement") %>' runat="server" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ABBREVIATION">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("abbreviation") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAbbreviation" Text='<%# Eval("abbreviation") %>' runat="server" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("id") %>' Width="20px" Height="20px" OnClientClick="return confirm('Do you want to edit?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DELETE" HeaderStyle-Width="75px">
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

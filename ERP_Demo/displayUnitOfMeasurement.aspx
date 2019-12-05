<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayUnitOfMeasurement.aspx.cs" Inherits="ERP_Demo.displayUnitOfMeasurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayUnitOfMeasurementTable" runat="server" Height="100" CssClass="Table1">
        <asp:TableRow ID="rowUnitOfMeasurementDisplay" runat="server" TableSection="TableHeader" HorizontalAlign="Center">
            <asp:TableCell ID="UnitOfMeasurementCell" ColumnSpan="3"><asp:Label ID="UnitOfMeasurementLabel" runat="server"><h3>Unit Of Measurement</h3></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowUnitOfMeasurementLink" runat="server">
            <asp:TableCell ID="cellUnitOfMeasurementLink" runat="server"><asp:Button ID="unitOfMeasurementButton" runat="server" Text="ADD" Font-Size="Small" OnClick="unitOfMeasurementButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellUnitOfMeasurementSearch" runat="server" CssClass="heading"><asp:Label ID="searchLabel" runat="server">&nbsp; Search:- &nbsp</asp:Label></asp:TableCell>
            <asp:TableCell ID="cellUnitOfMeasurementSearchButton" runat="server"><asp:TextBox ID="searchTextBox" runat="server" ></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="unitOfMeasurementGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="unitOfMeasurementGridView_RowEditing" OnRowCancelingEdit="unitOfMeasurementGridView_RowCancelingEdit"
                OnRowCommand="unitOfMeasurementGridView_RowCommand" OnRowDeleting="unitOfMeasurementGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="0px" CellPadding="4" CssClass="Table1" Width="25%" Font-Size="Medium">
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
                    <asp:TemplateField HeaderText="Unit Of Measurement">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("unit_of_measurement") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUnitOfMeasurement" Text='<%# Eval("unit_of_measurement") %>' runat="server" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Abbreviation">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("abbreviation") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAbbreviation" Text='<%# Eval("abbreviation") %>' runat="server" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" CommandArgument='<%#Eval("id") %>' Width="20px" Height="20px" OnClientClick="return confirm('Do you want to edit?');"/>
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

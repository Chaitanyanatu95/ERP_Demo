<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayUnitOfMeasurement.aspx.cs" Inherits="ERP_Demo.displayUnitOfMeasurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayUnitOfMeasurementTable" runat="server" HorizontalAlign="Center" CssClass="Table1">
        <asp:TableRow runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellLabel"><asp:Label ID="uomLabel" runat="server" ><h5><u>UOM MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellLink" runat="server"><asp:Button ID="uomButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="unitOfMeasurementButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellList" runat="server" style="padding-left:450px;padding-right:300px;"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke" ><h5><u>UOM LIST</u></h5></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellSearch" runat="server"><asp:Label ID="Label2" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label>
            <asp:TextBox ID="searchTextBox" runat="server" Height="18px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="unitOfMeasurementGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="unitOfMeasurementGridView_RowEditing" OnRowCancelingEdit="unitOfMeasurementGridView_RowCancelingEdit"
                OnRowCommand="unitOfMeasurementGridView_RowCommand" OnRowDeleting="unitOfMeasurementGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="Table1" Width="25%" AllowPaging="true" PageSize="15" OnPageIndexChanging="unitOfMeasurementGridView_PageIndexChanging">
                <%-- Theme Properties --%>
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="&nbsp;&nbsp;Last" NextPageText="Next" PreviousPageText="&nbsp;&nbsp;Previous"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Font-Size="Small"/> 
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

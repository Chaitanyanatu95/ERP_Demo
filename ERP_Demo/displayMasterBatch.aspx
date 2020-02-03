<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="displayMasterBatch.aspx.cs" Inherits="ERP_Demo.displayMasterBatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="displayMasterbatchTable" runat="server" HorizontalAlign="Center" CssClass="Table1">
        <asp:TableRow runat="server" TableSection="TableHeader">
            <asp:TableCell ID="cellLabel"><asp:Label ID="mbLabel" runat="server" ><h5><u>MASTERBATCH MASTER</u></h5></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellLink" runat="server"><asp:Button ID="mbButton" runat="server" Text="ADD NEW" Font-Size="Small" OnClick="masterbatchButton_Click"></asp:Button></asp:TableCell>
            <asp:TableCell ID="cellList" runat="server" style="padding-left:450px;padding-right:300px;"><asp:Label ID="Label1" runat="server" Font-Size="Small" BackColor="WhiteSmoke" ><h5><u>MASTERBATCH LIST</u></h5></asp:Label></asp:TableCell>
            <asp:TableCell ID="cellSearch" runat="server"><asp:Label ID="Label2" runat="server" Font-Size="Small">&nbsp Search:- &nbsp</asp:Label>
            <asp:TextBox ID="searchTextBox" runat="server" Height="18px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="searchButton" runat="server" Font-Size="Smaller" OnClick="searchButton_Click" Text="Search" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
     <asp:GridView ID="masterbatchGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id"
                ShowHeaderWhenEmpty="True"
                OnRowEditing="masterbatchGridView_RowEditing" OnRowCancelingEdit="masterbatchGridView_RowCancelingEdit"
                OnRowCommand="masterbatchGridView_RowCommand" OnRowDeleting="masterbatchGridView_RowDeleting"
                BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CellPadding="4" CssClass="Table1" Width="70%" AllowPaging="true" PageSize="15" OnPageIndexChanging="masterbatchGridView_PageIndexChanging">
                <%-- Theme Properties --%>
                <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="&nbsp;&nbsp;Last" NextPageText="Next" PreviousPageText="&nbsp;&nbsp;Previous"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle ForeColor="black" Font-Size="Small" BackColor="WhiteSmoke"/>   
                <Columns>
                    <asp:TemplateField HeaderText="MASTERBATCH NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_name") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBName" Text='<%# Eval("mb_name") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH GRADE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_grade") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBGrade" Text='<%# Eval("mb_grade") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH MFG">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_mfg") %>' runat="server" Width="200"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBMfg" Text='<%# Eval("mb_mfg") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH COLOR">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_color") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBColor" Text='<%# Eval("mb_color") %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MASTERBATCH COLOR CODE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("mb_color_code") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMBCCode" Text='<%# Eval("mb_color_code") %>' runat="server" />
                        </EditItemTemplate>
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

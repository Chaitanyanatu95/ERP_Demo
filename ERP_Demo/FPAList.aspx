<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FPAList.aspx.cs" Inherits="ERP_Demo.FPAList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="fpaGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
        ShowHeaderWhenEmpty="True" BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" OnPageIndexChanging="fpaGridView_PageIndexChanging" AllowPaging="true" PageSize="10" BorderWidth="1px" CssClass="Table1" Width="90%">
        <%-- Theme Properties --%>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Previous"/>
        <%--<PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" /> --%>
        <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
        <PagerStyle BackColor="WhiteSmoke" ForeColor="Blue" HorizontalAlign="Center" CssClass="spacing"/>
        <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px"/>    
        <FooterStyle BackColor="WhiteSmoke"/>
                <Columns>
                    <asp:TemplateField HeaderText="WORKER NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("operator_name") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OPERATOR NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("worker_name") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PART NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("part_name") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DATE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("date") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SHIFT DETAILS">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("shift") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OPERATION TYPE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("operation_type") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TOTAL QTY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("total_qty") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NO OF PARTS / HR">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("no_of_parts") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TOTAL TIME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("total_time") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OK QTY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("exp_qty") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REJ QTY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("rej_qty") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EFFICIENCY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("efficiency") %>' runat="server" />
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
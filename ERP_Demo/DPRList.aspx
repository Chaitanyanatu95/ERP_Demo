<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DPRList.aspx.cs" Inherits="ERP_Demo.DPRList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="productionGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id" OnRowDeleting="productionGridView_RowDeleting"
        ShowHeaderWhenEmpty="True" BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="Table1" Width="90%" OnPageIndexChanging="productionGridView_PageIndexChanging" AllowPaging="true" PageSize="10">
                <%-- Theme Properties --%>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="blue" HorizontalAlign="Center" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px" />    
                <FooterStyle BackColor="WhiteSmoke"/>
                <Columns>
                    <asp:TemplateField HeaderText="PART NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("part_name") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DATE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("date_dpr") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OPERATOR NAME">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("operator_name") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="R/M GRADE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("material_grade") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MACHINE NO">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("machine_no") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SHIFT DETAILS">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("shift_details") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EXP QTY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("exp_qty") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NO OF SHOTS">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("no_of_shots") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REJECTION PCS">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("rejection_pcs") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REJECTION KGS">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("rejection_kgs") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACTUAL QTY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("act_qty") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOWNTIME HOURS">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("downtime_hrs") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOWNTIME CODE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("down_time_code") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EFFICIENCY">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("efficiency") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" DPR STATUS">
                        <ItemTemplate>
                             <asp:Label ID="postStatusFlag" Text='<%# Eval("post_opr_req") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" FPA STATUS">
                        <ItemTemplate>
                             <asp:Label ID="fpaStatusFlag" Text='<%# Eval("fpa_status") %>' runat="server" />
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
        <br/>
        <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
    </center>
    </asp:Content>
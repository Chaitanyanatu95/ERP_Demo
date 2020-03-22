<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DPRList.aspx.cs" Inherits="ERP_Demo.DPRList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="productionGridView" runat="server" AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="id" OnRowCommand="productionGridView_RowCommand" OnRowDeleting="productionGridView_RowDeleting"
        ShowHeaderWhenEmpty="True" BackColor="#DFDDDD" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="Table1" Width="90%" OnPageIndexChanging="productionGridView_PageIndexChanging" AllowPaging="true" PageSize="10">
                <%-- Theme Properties --%>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                <HeaderStyle CssClass="CustomerHeader" Height="50px" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                <PagerStyle BackColor="White" ForeColor="blue" HorizontalAlign="Center" />
                <RowStyle ForeColor="black" BackColor="WhiteSmoke" Height="40px" />    
                <FooterStyle BackColor="WhiteSmoke"/>
                <Columns>
                    <asp:TemplateField HeaderText="Sr.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PART NAME">
                        <ItemTemplate>
                            <asp:Label ID="partName" Text='<%# Eval("part_name") %>' runat="server" />
                            <asp:HiddenField ID="dprNo" Value='<%# Eval("dpr_no") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DATE">
                        <ItemTemplate>
                            <asp:Label ID="dateDpr" Text='<%# Eval("date_dpr") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OPERATOR NAME">
                        <ItemTemplate>
                            <asp:Label ID="operatorName" Text='<%# Eval("operator_name") %>' runat="server" />
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
                            <asp:Label ID="shiftDetails" Text='<%# Eval("shift_details") %>' runat="server" />
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
                    <asp:TemplateField HeaderText ="EDIT" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="EDIT" Width="15px" Height="15px" CommandArgument='<%#Eval("part_name")+","+ Eval("operator_name")+","+ Eval("date_dpr")+","+ Eval("shift_details")+","+ Eval("id")+","+ Eval("fpa_status")+","+ Eval("dpr_no")%>' OnClientClick="return confirm('Do you want to Edit?');" />
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
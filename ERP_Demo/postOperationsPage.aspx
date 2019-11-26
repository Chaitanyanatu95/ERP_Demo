<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="postOperationsPage.aspx.cs" Inherits="ERP_Demo.postOperationsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            extraBtnImg();
            onPostOperationSelected();
            onPackagingDetailsSelected();                
        }
        function extraBtnImg() {
            var extraBtnImg = document.getElementById('<%= listUploadedFiles.ClientID %>').innerText;
            if (!extraBtnImg) {
                document.getElementById('<%= imgBtnExtra.ClientID %>').style.display = "none";
            }
        }
        function onPostOperationSelected() {
            var isPostOperation = $('#<%=postOperationDropDownList.ClientID %> option:selected').text();
                
            if (isPostOperation == "YES") {
                $("#<%=postOperationGrid.ClientID %>").show();
            }
            else{
                $("#<%=postOperationGrid.ClientID %>").hide();
            }
        }
        function onPackagingDetailsSelected() {
            var isPackagingDetails = $('#<%=packagingDetailsDropDownList.ClientID %> option:selected').text();
                
            if (isPackagingDetails == "YES") {
                $("#<%=packagingDetailsGrid.ClientID %>").show();
            }
            else{
                $("#<%=packagingDetailsGrid.ClientID %>").hide();
            }
        }
    </script>
    <div style="display:block;margin-left:auto;margin-right:auto;height:40px;width:80%;background-color:whitesmoke;padding-top:5px;">
        <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; color:black">
            <b>STEP 1</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 2</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:SkyBlue;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 3</b>
        </div>
    </div>
    <br />
    <asp:Table runat="server" HorizontalAlign="Center" CssClass="Table1" style="margin-top:50px;">
    <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server">POST OPERATION (REQ)</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server"><asp:DropDownList ID="postOperationDropDownList" runat="server" onchange="onPostOperationSelected()">
                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                          </asp:DropDownList>
            </asp:TableCell>
            </asp:TableRow>
    </asp:table>
    <asp:GridView ID="postOperationGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="postOperationGrid_RowCommand" ShowFooter="true"
        OnRowDeleting="postOperationGrid_RowDeleting" CssClass="tableClass" DataKeyNames="id" alternatingrowstyle-backcolor="Linen" headerstyle-backcolor="SkyBlue">
           <Columns>
                <asp:TemplateField HeaderText="TYPE">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("type") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="postOperationTypeDropDownList" runat="server" DataSourceID="SqlDataSource1" DataTextField="type" DataValueField="type"></asp:DropDownList><asp:RequiredFieldValidator ID="typeReq" CssClass="required" runat="server" ErrorMessage="please select type" ControlToValidate="postOperationTypeDropDownList"></asp:RequiredFieldValidator>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [type] FROM [post_operation_master]">
                        </asp:SqlDataSource>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TARGET QUANTITY / HR">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("target_quantity") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtTargetQuantityFooter" runat="server"/><asp:RequiredFieldValidator ID="targetQuantReq" CssClass="required" runat="server" ErrorMessage="please enter target quantity" ControlToValidate="txtTargetQuantityFooter"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PROCESS DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("process_description") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtProcessDescriptionFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PHOTO">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("photo") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:FileUpload ID="photoUploadFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to Delete?');" Height="20px" Width="20px" runat="server"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ImageUrl="~\Images\addnew.png" CommandName="Add" OnClientClick="return confirm('Do you want to Add?');" Height="20px" Width="20px" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
    <asp:Table runat="server" CssClass="Table1">
            <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
                <asp:TableCell runat="server">PACKAGING DETAILS (REQ)</asp:TableCell>
                </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">
                <asp:DropDownList ID="packagingDetailsDropDownList" runat="server" onchange="onPackagingDetailsSelected()">
                        <asp:ListItem Value="YES">YES</asp:ListItem>
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                    </asp:DropDownList>
            </asp:TableCell>
             </asp:TableRow>
        </asp:Table>
    <asp:GridView ID="packagingDetailsGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="packagingDetailsGrid_RowCommand" OnRowDeleting="packagingDetailsGrid_RowDeleting" CssClass="tableClass" ShowFooter="true" DataKeyNames="id" alternatingrowstyle-backcolor="Linen" headerstyle-backcolor="SkyBlue">
            <Columns>
                <asp:TemplateField HeaderText="PACKAGING TYPE">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("type") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="packagingDropDownList" runat="server" DataSourceID="SqlDataSource1" DataTextField="packaging_type" DataValueField="packaging_type" OnSelectedIndexChanged="packagingTypeChanged" AutoPostBack="true"/><asp:RequiredFieldValidator ID="packagingTypeReq" CssClass="required" runat="server" ErrorMessage="please select packaging type" ControlToValidate="packagingDropDownList"></asp:RequiredFieldValidator>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [packaging_type] FROM [packaging_master]"></asp:SqlDataSource>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SIZE">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("size") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPostSizeFooter" DataTextField="size" DataValueField="size" runat="server" ReadOnly="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QTY PER PACKAGE (NOS)">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("qty_per_package") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPostQuantityFooter" runat="server"/><asp:RequiredFieldValidator ID="postQuantityReq" CssClass="required" runat="server" ErrorMessage="please enter quantity" ControlToValidate="txtPostQuantityFooter"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PHOTO">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("photo") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:FileUpload ID="postPhotoUploadFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to Delete?');" Height="20px" Width="20px" runat="server"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ImageUrl="~\Images\addnew.png" CommandName="Add" OnClientClick="return confirm('Do you want to Add?');" Height="20px" Width="20px" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    <br />
    <center>
        <asp:Label ID="labelMultipleFiles" runat="server" style="display:block;">EXTRA FILES</asp:Label>
        <asp:FileUpload runat="server" AllowMultiple="true" ID="UploadMultipleFiles" style="display:inline;"/>
        <asp:Button ID="uplaoadedFile" runat="server" OnClick="uploadFile_Click" Text="SAVE FILES" style="display:inline;" OnClientClick="return confirm('Do you want to save files?');" />
        <asp:Label ID="listUploadedFiles" runat="server" ></asp:Label>
        <asp:ImageButton ID="imgBtnExtra" runat="server" height="20" Width="20" style="display:inline;" OnClick="imgBtnExtra_Click"/>
        </center>
    <asp:table runat="server" CssClass="Table1" HorizontalAlign="Center" style="padding-top:30px;">
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE PART" OnClick="SaveBtn_Click"  CssClass="nextPage" OnClientClick="return confirm('Do you want to Save?');" /></asp:TableCell>
            <asp:TableCell ID="backCell" runat="server">&nbsp;&nbsp;&nbsp;<asp:Button Text="BACK" CssClass="nextPage" runat="server" OnClientClick="javascript:window.history.go(-1);return false;" CausesValidation="false"/></asp:TableCell>
            <asp:TableCell runat="server" >&nbsp;&nbsp;&nbsp;<asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click"  CssClass="nextPage" CausesValidation="false" OnClientClick="return confirm('Do you want to Cancel?');" /></asp:TableCell>
        </asp:TableRow> 
    </asp:table>
</asp:Content>

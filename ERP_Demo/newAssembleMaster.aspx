<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="newAssembleMaster.aspx.cs" Inherits="ERP_Demo.newAssembleMaster" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>
<head>
    <title></title>
    <script type="text/javascript">
        window.onload = function () {
            var assemblySpec = document.getElementById('<%= lblAssemblySpec.ClientID %>').innerText;

            if (!assemblySpec) {
                document.getElementById('<%= lblAssemblySpec.ClientID %>').style.display = "none";
                document.getElementById('<%= btnAssemblySpecs.ClientID %>').style.display = "none";
            }
        }

        function validationOnAssemblyOperation()
        {
            var quantity = document.getElementById('<%=((TextBox)assemblyGridView.FooterRow.FindControl("txtQuantityFooter")).ClientID %>');
            var postType = document.getElementById("<%=assemblyGridView.FooterRow.FindControl("partNameDropDownListFooter").ClientID %>");
            var getPost = postType.options[postType.selectedIndex].text;

            if (getPost == "Select Part Name")
            {
                document.getElementById("<%=assemblyGridView.FooterRow.FindControl("assImgBtn").ClientID %>").style.display = "none";
                document.getElementById("<%=assemblyGridView.FooterRow.FindControl("errorType").ClientID %>").innerHTML = "Please select part name".fontcolor("red");
                quantity.readOnly = false;
                return false;
            }
            else if (getPost != "Select Part Name" && quantity.value == "")
            {
                document.getElementById("<%=assemblyGridView.FooterRow.FindControl("assImgBtn").ClientID %>").style.display = "block";
                document.getElementById("<%=assemblyGridView.FooterRow.FindControl("errorTarget").ClientID %>").innerHTML = "Please enter quantity.".fontcolor("red");
                quantity.readOnly = false;
                return false;
            }
            else if (getPost == "N/A")
            {
                document.getElementById("<%=assemblyGridView.FooterRow.FindControl("assImgBtn").ClientID %>").style.display = "none";
                return true;
            }
            else if (getPost != "Select Part Name" && quantity.value != "")
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        function assemblyFileValidation()
        {
            var fileInput = document.getElementById('<%=assemblyFileUpload.ClientID %>');
            if (fileInput.value != null) {
                var filePath = fileInput.value;
                var allowedExtensions = /(\.xls|\.pdf|\.xlsx)$/i;
                if (!allowedExtensions.exec(filePath)) {
                    document.getElementById('<%= errorAssemblyFile.ClientID %>').innerHTML = 'Please upload file having extensions .xls/.pdf/.xlsx only.'.fontcolor('Red');
                    fileInput.value = '';
                    return false;
                }
                else
                {
                    //Image preview
                    if (fileInput.files && fileInput.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('<%= btnAssemblySpecs.ClientID %>').style.display = "inline";
                            document.getElementById('<%= errorAssemblyFile.ClientID %>').innerHTML = "";
                        };
                        reader.readAsDataURL(fileInput.files[0]);
                    }
                }
            }
        }

        function finalAssemblyValidation()
        {
            var postType = document.getElementById("<%=assemblyGridView.FooterRow.FindControl("partNameDropDownListFooter").ClientID %>");
            var getPost = postType.options[postType.selectedIndex].text;
            if (getPost == "N/A") {
                document.getElementById("<%=assemblyGridView.FooterRow.FindControl("assImgBtn").ClientID %>").style.display = "none";
                return true;
            }
            else {
                document.getElementById("<%=assemblyGridView.FooterRow.FindControl("errorType").ClientID %>").innerHTML = "Please Select N/A or Part Name".fontcolor("red");
                return false;
            }
        }

        function validationOnThisPage() 
        {
            if (Page_ClientValidate()) {
                var a = finalAssemblyValidation();
                //var b = qtyValueCheck();
                if (a)
                {
                    return confirm('Do you want to save?');
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    </script>
</head>
    <body>
    <table runat="server" class="tableClass">
        <tr>
            <th colspan="2" style="text-align:center; background-color: SkyBlue;">
                <h3>ASSEMBLY DETAILS</h3>
            </th>
        </tr>
        <tr>
            <td>ASSEMBLY NO:</td> 
            <td><asp:TextBox ID="assemblyNo" runat="server" ReadOnly="true" BackColor="WhiteSmoke"></asp:TextBox></td> 
        </tr>
        <tr>
            <td>ASSEMBLY NAME:</td> 
            <td><asp:TextBox ID="assemblePartName" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="assPartNameValidator" runat="server" ErrorMessage="Please provide part name." ControlToValidate="assemblePartName" CssClass="required"></asp:RequiredFieldValidator>
            </td> 
        </tr>
        <tr>
            <td>UOM:</td>
            <td><asp:DropDownList ID="uomDropDownList" runat="server" DataTextField="unit_of_measurement" DataValueField="unit_of_measurement"></asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="uomValidator" runat="server" CssClass="required" ErrorMessage="Please select UOM" ControlToValidate="uomDropDownList" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
        </tr>
        </table>
        <table id="secondTable" runat="server" class="tableClass" >
            <tr>
                <td colspan="2" style="border:0;">
                    <asp:GridView ID="assemblyGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id"
                    ShowHeaderWhenEmpty="True" OnRowCommand="assemblyGridView_RowCommand" OnRowDeleting="assemblyGridView_RowDeleting"
                    BackColor="#DFDDDD" BorderColor="black" BorderStyle="solid" BorderWidth="1px" CellPadding="4" CssClass="Table1" Width="70%" HorizontalAlign="Center">
                        <HeaderStyle Height="50px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                        <RowStyle Font-Size="Small" ForeColor="black" BackColor="WhiteSmoke"/>   
                        <FooterStyle Font-Size="Small" ForeColor="black" BackColor="WhiteSmoke"/>
                        <Columns>
                            <asp:TemplateField HeaderText="PART NAME">
                                <ItemTemplate>
                                    <asp:Label ID="partNameLabel" Text='<%# Eval("child_part") %>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="partNameDropDownListFooter" runat="server" DataTextField="part_name" DataValueField="part_name" DataSourceID="SqlDataSource1" AutoPostBack="true" OnSelectedIndexChanged="partNameDropDownListFooter_SelectedIndexChanged"></asp:DropDownList>
                                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [part_name] FROM [parts_master]">
                                    </asp:SqlDataSource>
                                    <asp:Label ID="errorType" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY">
                                <ItemTemplate>
                                    <asp:Label ID="qtyLabel" Text='<%# Eval("child_part_qty") %>' runat="server"/>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtQuantityFooter" runat="server" ReadOnly="true" Width="47px"/>
                                    <br /><asp:Label ID="errorTarget" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText ="DELETE" HeaderStyle-Width="75px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="delImgBtn" ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="DELETE" Width="17px" Height="17px" OnClientClick="return confirm('Do you want to delete?');"/>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:ImageButton ID="assImgBtn" ImageUrl="~\Images\addnew.png" OnClientClick="return validationOnAssemblyOperation()" CommandName="Add" ToolTip="ADD" Height="20px" Width="20px" runat="server"/>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                 <td style="border:0; text-align:center" colspan="2">
                            TOTAL ASSEMBLY WT. (g)
                     <asp:TextBox ID="assWt" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>
                 </td>
            </tr>
        </table>
        <table runat="server" class="tableClass">
            <tr>
                <td>
                    <asp:Label Text="ASSEMBLY TARGET <br>QUANTITY / HR" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="targetQtyTextBox" runat="server" Width="70px"></asp:TextBox><br />
                     <asp:Label ID="targetQuantLbl" runat="server" CssClass="required"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="assemblyFileLabel" runat="server" Text="ATTACHMENT"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="assemblyFileUpload" runat="server" onchange="return assemblyFileValidation();" />
                    <asp:Label ID="lblAssemblySpec" runat="server" style="display:inline; color:blue;"></asp:Label>
                    <asp:ImageButton ID="btnAssemblySpecs" runat="server" ImageUrl="~/Images/cancel.png" Height="20" Width="20" OnClick="btnAssemblySpecs_Click" OnClientClick="return confirm('Do you want to delete?');"/><br />
                    <asp:Label ID="errorAssemblyFile" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <asp:Button ID="saveBtn" runat="server" Text="SAVE" OnClick="saveBtn_Click" CssClass="nextPage" OnClientClick="return validationOnThisPage()"  />
                    <asp:Button ID="cancelBtn" Text="CANCEL" runat="server" OnClick="cancelBtn_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/>
                </td>
            </tr>
        </table>
        <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
    </body>
</html>
</asp:Content>
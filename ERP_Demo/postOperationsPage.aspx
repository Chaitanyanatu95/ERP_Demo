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

        function validationOnPostOperation() {
            var isPostOperation = $('#<%=postOperationDropDownList.ClientID %> option:selected').text();
            if (isPostOperation == "YES") {
                var targetQuantity = document.getElementById('<%=((TextBox)postOperationGrid.FooterRow.FindControl("txtTargetQuantityFooter")).ClientID %>');
                var postType = document.getElementById("<%=postOperationGrid.FooterRow.FindControl("postOperationTypeDropDownList").ClientID %>");
                var getPost = postType.options[postType.selectedIndex].text;

                if (getPost == "Select Type") {
                    document.getElementById("<%=postOperationGrid.FooterRow.FindControl("postImgBtn").ClientID %>").style.display = "block";
                    document.getElementById("<%=postOperationGrid.FooterRow.FindControl("errorType").ClientID %>").innerHTML = "Please select type.".fontcolor("red");
                    targetQuantity.readOnly = false;
                    return false;
                }
                else if (getPost != "Select Type" && targetQuantity.value == "") {
                    document.getElementById("<%=postOperationGrid.FooterRow.FindControl("postImgBtn").ClientID %>").style.display = "block";
                    document.getElementById("<%=postOperationGrid.FooterRow.FindControl("errorTarget").ClientID %>").innerHTML = "Please enter target quantity.".fontcolor("red");
                    targetQuantity.readOnly = false;
                    return false;
                }
                else if (getPost == "N/A") {
                    document.getElementById("<%=postOperationGrid.FooterRow.FindControl("postImgBtn").ClientID %>").style.display = "none";
                    return true;
                }
                else if (getPost != "Select Type" && targetQuantity.value != "") {
                    return true;
                }
                else {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        function validationOnPackagingOperation() {
            var isPackaging = $('#<%=packagingDetailsDropDownList.ClientID %> option:selected').text();
            if (isPackaging == "YES") {
                var packType = document.getElementById("<%=packagingDetailsGrid.FooterRow.FindControl("packagingDropDownList").ClientID %>");
                var getPack = packType.options[packType.selectedIndex].text;

                if (getPack == "Select Packaging") {
                    document.getElementById("<%=packagingDetailsGrid.FooterRow.FindControl("errorPackType").ClientID %>").innerHTML = "Please select packaging.".fontcolor("red");
                    return false;
                }
                else if (getPack == "N/A")
                {
                    document.getElementById("<%=packagingDetailsGrid.FooterRow.FindControl("packImgBtn").ClientID %>").style.display = "none";
                    return true;
                }
                else if (getPost != "Select Packaging" && targetQuantity.value != "")
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else {
                return true;
            }
        }

        function fileValidation() {
            var fileInput = document.getElementById('<%= postOperationGrid.FooterRow.FindControl("photoUploadFooter").ClientID %>');
            var fileInput2 = document.getElementById('<%= packagingDetailsGrid.FooterRow.FindControl("postPhotoUploadFooter").ClientID %>');

            if (fileInput.value != null || fileInput2.value != null)
            {
                var filePath = fileInput.value;
                var filePath2 = fileInput2.value;
                var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
                if (filePath)
                {
                    if (!allowedExtensions.exec(filePath))
                    {
                        document.getElementById('<%= postOperationGrid.FooterRow.FindControl("errorFile").ClientID %>').innerHTML = 'Please upload file having extensions .jpeg/.jpg/.png/.gif only.'.fontcolor('Red');
                        fileInput.value = '';
                        return false;
                    }
                }
                else if (filePath2)
                {
                    if (!allowedExtensions.exec(filePath2))
                    {
                        document.getElementById('<%= packagingDetailsGrid.FooterRow.FindControl("errorFile2").ClientID %>').innerHTML = 'Please upload file having extensions .jpeg/.jpg/.png/.gif only.'.fontcolor('Red');
                        fileInput2.value = '';
                        return false;
                    }
                }
                else
                {
                    //Image preview
                    if (fileInput.files && fileInput.files[0])
                    {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('<%=  postOperationGrid.FooterRow.FindControl("errorFile").ClientID %>').innerHTML = "";
                            document.getElementById('imagePreview').innerHTML = '<img src="' + e.target.result + '" height="40" width="40"/>';
                        };
                        reader.readAsDataURL(fileInput.files[0]);
                    }
                    else if (fileInput2.files && fileInput2.files[0])
                    {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('<%=  packagingDetailsGrid.FooterRow.FindControl("errorFile2").ClientID %>').innerHTML = "";
                            document.getElementById('imagePreview2').innerHTML = '<img src="' + e.target.result + '" height="40" width="40"/>';
                        };
                        reader.readAsDataURL(fileInput2.files[0]);
                    }
                }
            }
        }

        function finalPostValidation() {
            var isPostOperation = $('#<%=postOperationDropDownList.ClientID %> option:selected').text();
            if (isPostOperation == "YES")
            {
                var postType = document.getElementById("<%=postOperationGrid.FooterRow.FindControl("postOperationTypeDropDownList").ClientID %>");
                var getPost = postType.options[postType.selectedIndex].text;
                if (getPost == "N/A")
                {
                    return true;
                }
                else
                {
                    alert("Please fill post operation data");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        function finalPackValidation() {
            var isPackagingDetails = $('#<%=packagingDetailsDropDownList.ClientID %> option:selected').text();
            if (isPackagingDetails == "YES")
            {
                var packType = document.getElementById("<%=packagingDetailsGrid.FooterRow.FindControl("packagingDropDownList").ClientID %>");
                var getPack = packType.options[packType.selectedIndex].text;
                if (getPack == "N/A")
                {
                    return true;
                }
                else
                {
                    alert("Please fill packaging details data");
                    return false;
                }
            }
            else
            {
                return true;
            }
         }

         function validationOnDropDown() {
            var postOpnDropDown = $('#<%=postOperationDropDownList.ClientID %> option:selected').text();
            var packagingDropDown = $('#<%=packagingDetailsDropDownList.ClientID %> option:selected').text();
             if (postOpnDropDown == "SELECT") {
                 alert("Please select dropdown for post operation");
                 return false;
             }
             else if (packagingDropDown == "SELECT") {
                 alert("Please select dropdown for packaging");
                 return false;
             }
             else {
                 return true;
             }
         }

         function validationOnThisPage()
         {
            var a = finalPostValidation();
            var b = finalPackValidation();
            var c = validationOnDropDown();
            if (a && b && c) {
                return confirm('Do you want to save?');
            }
            else
            {
                return false;
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
            <asp:TableCell runat="server">POST OPERATION (REQ) <div class="required" style="display:inline">*</div></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server"><asp:DropDownList ID="postOperationDropDownList" runat="server" onchange="onPostOperationSelected()">
                                                <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                          </asp:DropDownList>
            </asp:TableCell>
            </asp:TableRow>
    </asp:table>
    <asp:GridView ID="postOperationGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="postOperationGrid_RowCommand" ShowFooter="true"
        OnRowDeleting="postOperationGrid_RowDeleting" CssClass="tableClass" DataKeyNames="id" headerstyle-backcolor="SkyBlue" HorizontalAlign="Center">
           <Columns>
                <asp:TemplateField HeaderText="TYPE" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="postOpnTypeLabel" Text='<%# Eval("type") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="postOperationTypeDropDownList" runat="server" DataSourceID="SqlDataSource1" DataTextField="type" DataValueField="type" onchange="validationOnPostOperation()" OnSelectedIndexChanged="postOperationTypeDropDownList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=PB-LAPTOP;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [type] FROM [post_operation_master]">
                        </asp:SqlDataSource>
                        <br /><asp:Label ID="errorType" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TARGET QUANTITY / HR" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="targetQuantityLabel" Text='<%# Eval("target_quantity") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtTargetQuantityFooter" runat="server"/>
                        <br /><asp:Label ID="errorTarget" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PROCESS DESCRIPTION" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="processDescLabel" Text='<%# Eval("process_description") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtProcessDescriptionFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PHOTO <br/> Supported Formats: .jpeg, .png, .gif" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("photo") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:FileUpload ID="photoUploadFooter" runat="server" onchange="return fileValidation();"/>
                        <asp:Label ID="errorFile" runat="server"></asp:Label>
                        <div id="imagePreview" style="margin-left:auto; margin-right:auto"></div>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to Delete?');" Height="20px" Width="20px" runat="server"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="postImgBtn" ImageUrl="~\Images\addnew.png" CommandName="Add" OnClientClick="return validationOnPostOperation()" Height="20px" Width="20px" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
    <asp:Table runat="server" CssClass="Table1">
            <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
                <asp:TableCell runat="server">PACKAGING DETAILS (REQ) <div class="required" style="display:inline">*</div></asp:TableCell>
                </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">
                <asp:DropDownList ID="packagingDetailsDropDownList" runat="server" onchange="onPackagingDetailsSelected()">
                        <asp:ListItem Value="SELECT">SELECT</asp:ListItem>    
                        <asp:ListItem Value="YES">YES</asp:ListItem>
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                    </asp:DropDownList>
            </asp:TableCell>
             </asp:TableRow>
        </asp:Table>
    <asp:GridView ID="packagingDetailsGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="packagingDetailsGrid_RowCommand" OnRowDeleting="packagingDetailsGrid_RowDeleting" CssClass="tableClass" ShowFooter="true" DataKeyNames="id" headerstyle-backcolor="SkyBlue" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField HeaderText="PACKAGING TYPE" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="packTypeLabel" Text='<%# Eval("type") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="packagingDropDownList" runat="server" DataSourceID="SqlDataSource1" DataTextField="packaging_type" DataValueField="packaging_type" OnSelectedIndexChanged="packagingTypeChanged" AutoPostBack="true"/>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=PB-LAPTOP;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [packaging_type] FROM [packaging_master]"></asp:SqlDataSource>
                        <br /><asp:Label ID="errorPackType" runat="server"></asp:Label>
                        </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SIZE" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="packSizeLabel" Text='<%# Eval("size") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPostSizeFooter" DataTextField="size" DataValueField="size" runat="server" ReadOnly="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QTY PER PACKAGE (NOS)" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("qty_per_package") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPostQuantityFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PHOTO <br/> Supported Formats: .jpeg, .png, .gif" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("photo") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:FileUpload ID="postPhotoUploadFooter" runat="server"/>
                        <asp:Label ID="errorFile2" runat="server"></asp:Label>
                        <div id="imagePreview2" style="margin-left:auto; margin-right:auto"></div>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to Delete?'); " Height="20px" Width="20px" runat="server"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="packImgBtn" ImageUrl="~\Images\addnew.png" CommandName="Add" OnClientClick="return validationOnPackagingOperation();" Height="20px" Width="20px" runat="server"/>
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
        <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE PART" OnClick="SaveBtn_Click"  CssClass="nextPage" OnClientClick="return validationOnThisPage();" /></asp:TableCell>
            <asp:TableCell ID="backCell" runat="server">&nbsp;&nbsp;&nbsp;<asp:Button Text="BACK" CssClass="nextPage" runat="server" OnClientClick="javascript:window.history.go(-1);return false;" CausesValidation="false"/></asp:TableCell>
            <asp:TableCell runat="server" >&nbsp;&nbsp;&nbsp;<asp:Button Text="CANCEL" runat="server" OnClick="Cancel_Click"  CssClass="nextPage" CausesValidation="false" OnClientClick="return confirm('Do you want to Cancel?');" /></asp:TableCell>
        </asp:TableRow> 
    </asp:table>
     <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

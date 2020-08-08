<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newPartsMaster.aspx.cs" Inherits="ERP_Demo.newPartsMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.onload = function ()
        {
            var temp = document.getElementById('<%= lblFile.ClientID %>').innerText;
            var moldFile = document.getElementById('<%= lblMoldFile.ClientID %>').innerText;
            if (!temp) {
                document.getElementById('<%= lblFile.ClientID %>').style.display = "none";
                document.getElementById('<%= btnFile.ClientID %>').style.display = "none";
            }
            if (!moldFile) {
                document.getElementById('<%= lblMoldFile.ClientID %>').style.display = "none";
                document.getElementById('<%= btnMoldFile.ClientID %>').style.display = "none";
            }
        }

        function moldFileValue() {
            var moldFile = document.getElementById('<%= moldSpecFileUpload.ClientID %>');
            if (moldFile.value != null) {
                document.getElementById('<%= btnMoldFile.ClientID %>').style.display = "inline";
            }
        }
       
        function validationPartMaster() {
            if (Page_ClientValidate()) {
                var jigFix = document.getElementById('<%= jigReqDropDownList.ClientID%>');
                if (jigFix.value == "SELECT") {
                    document.getElementById('<%= jigFixLabel.ClientID %>').innerHTML = "Please select valid option.";
                    return false;
                }
                else {
                    document.getElementById('<%= jigFixLabel.ClientID %>').innerHTML = "";
                    return confirm('Do you want to save?');
                }
            }
        }

        function fileValidation() {
            var fileInput = document.getElementById('<%= partPhotoFileUpload.ClientID %>');
            if (fileInput.value != null) {
                var filePath = fileInput.value;
                var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
                if (!allowedExtensions.exec(filePath)) {
                    document.getElementById('<%= errorFile.ClientID %>').innerHTML = 'Please upload file having extensions .jpeg/.jpg/.png/.gif only.'.fontcolor('Red');
                    fileInput.value = '';
                    return false;
                }
                else {
                    //Image preview
                    if (fileInput.files && fileInput.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('<%= btnFile.ClientID %>').style.display = "inline";
                            document.getElementById('<%= errorFile.ClientID %>').innerHTML = "";
                            document.getElementById('imagePreview').innerHTML = '<img src="' + e.target.result + '" height="40" width="40"/>';
                        };
                        reader.readAsDataURL(fileInput.files[0]);
                    }
                }
            }
        }

        function moldFileValidation() {
            var fileInput = document.getElementById('<%= moldSpecFileUpload.ClientID %>');
            if (fileInput.value != null) {
                var filePath = fileInput.value;
                var allowedExtensions = /(\.xls|\.pdf|\.xlsx)$/i;
                if (!allowedExtensions.exec(filePath)) {
                    document.getElementById('<%= errorMoldFile.ClientID %>').innerHTML = 'Please upload file having extensions .xls/.pdf/.xlsx only.'.fontcolor('Red');
                    fileInput.value = '';
                    return false;
                }
                else {
                    //Image preview
                    if (fileInput.files && fileInput.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('<%= btnMoldFile.ClientID %>').style.display = "inline";
                            document.getElementById('<%= errorMoldFile.ClientID %>').innerHTML = "";
                        };
                        reader.readAsDataURL(fileInput.files[0]);
                    }
                }
            }
        }
    </script>
    <div style="display:block;margin-left:auto;margin-right:auto;height:40px;width:80%;background-color:whitesmoke;padding-top:5px;">
        <div style="display:inline-block;text-align:center;border:1px solid black;background-color:SkyBlue;width:20%;height:33px;padding-top:7px; color:black">
            <b>STEP 1</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 2</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 3</b>
        </div>
    </div>
    <br />
    <asp:Table ID="Table1" runat="server" Height="70%" Width="80%" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="4"><h3>PART ENTRY DETAILS</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="partId" runat="server" VerticalAlign="Bottom">
            <asp:TableCell runat="server" CssClass="margin" style="padding-bottom:30px;">PART NO <br />
                <asp:TextBox ID="partNoTextBox" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">PART NAME <div class="required" style="display:inline">*</div> <br />
                <asp:TextBox ID="partNameTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="partNameReq" ControlToValidate="partNameTextBox" ErrorMessage="Please enter part name!" CssClass="required" SetFocusOnError="true"/>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">CUSTOMER NAME <div class="required" style="display:inline">*</div><br />
                <asp:DropDownList ID="customerNameDropDownList" DataTextField="customer_name" DataValueField="customer_name" runat="server"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator runat="server" ID="custNameReq" ControlToValidate="customerNameDropDownList" ErrorMessage="Please select customer name!" CssClass="required" SetFocusOnError="true"/>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin" style="padding-bottom:25px;">CUSTOMER PART NO <br />  <asp:TextBox ID="custPartNoTextBox" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
            <asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">PRODUCT CATEGORY <div class="required" style="display:inline">*</div><br />
                <asp:DropDownList ID="familyDropDownList" DataTextField="family" DataValueField="family" runat="server"></asp:DropDownList><br />
                <asp:RequiredFieldValidator runat="server" ID="prodCategoryReq" ControlToValidate="familyDropDownList" ErrorMessage="Please select product category!" CssClass="required" SetFocusOnError="true"/>
            </asp:TableCell>
                <asp:TableCell runat="server"  CssClass="margin">MOLD NAME <div class="required" style="display:inline">*</div><br />
                    <asp:TextBox ID="moldNameTextBox" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator runat="server" ID="moldNameReq" ControlToValidate="moldNameTextBox" ErrorMessage="Please enter mold name!" CssClass="required" SetFocusOnError="true"/>
                </asp:TableCell>
                <asp:TableCell runat="server"  CssClass="margin">MOLD MFG YEAR <div class="required" style="display:inline">*</div><br />
                    <asp:TextBox ID="moldYearTextBox" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator runat="server" ID="moldYearReq" ControlToValidate="moldYearTextBox" ErrorMessage="Please enter mold year!" CssClass="required" SetFocusOnError="true"/>
                </asp:TableCell>
                <asp:TableCell runat="server"  CssClass="margin">MOLD LIFE IN NOS. <br />
                    <asp:TextBox ID="moldLifeTextBox" runat="server"></asp:TextBox>
                </asp:TableCell></asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">NO OF CAVITIES <div class="required" style="display:inline">*</div> <br />
                <asp:TextBox ID="noOfCavitiesTextBox" runat="server" OnTextChanged="noOfCavitiesTextBox_TextChanged" AutoPostBack="true"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="noOfCavitiesTextBox" ErrorMessage="Please enter no of cavities!" CssClass="required" SetFocusOnError="true"/>
            </asp:TableCell>
            <asp:TableCell runat="server"  CssClass="margin">UNIT <div class="required" style="display:inline">*</div><br />
                <asp:DropDownList ID="unitMeasurementDropDownList" DataTextField="abbreviation" DataValueField="abbreviation" runat="server"></asp:DropDownList><br />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="unitMeasurementDropDownList" ErrorMessage="Please select unit!" CssClass="required" SetFocusOnError="true"/>
            </asp:TableCell>
            <asp:TableCell runat="server"  CssClass="margin">PART WEIGHT (In g) <div class="required" style="display:inline">*</div><br />
                <asp:TextBox ID="partWeightTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="partWeightReq" ControlToValidate="partWeightTextBox" ErrorMessage="Please enter part weight!" CssClass="required" SetFocusOnError="true"/>
            </asp:TableCell>
            <asp:TableCell runat="server"  CssClass="margin">SHOT WT ( in g) <div class="required" style="display:inline">*</div><br />
                <asp:TextBox ID="shotWeightTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="shotWeightReq" ControlToValidate="shotWeightTextBox" ErrorMessage="Please enter shot weight!" CssClass="required" SetFocusOnError="true"/>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">CYC. TIME (sec) <div class="required" style="display:inline">*</div><br />
                <asp:TextBox ID="cycleTimeTextBox" runat="server" OnTextChanged="moldProductionCycleTextBox_TextChanged" AutoPostBack="true"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="cycleTimeReq" ControlToValidate="cycleTimeTextBox" ErrorMessage="Please enter cycle time!" CssClass="required" SetFocusOnError="true" />
            </asp:TableCell>
             <asp:TableCell runat="server"  CssClass="margin">PRODUCTION PER HOUR IN NOS.<br />
                <asp:TextBox ID="moldProductionCycleTextBox" runat="server" BackColor="WhiteSmoke" ReadOnly="true"></asp:TextBox><br />
                <asp:Label ID="productionLbl" runat="server"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="margin">JIG/FIXTURE (REQ) <div class="required" style="display:inline">*</div><br />
                <asp:DropDownList ID="jigReqDropDownList" runat="server">
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                        <asp:ListItem Value="YES">YES</asp:ListItem>
                    </asp:DropDownList><br />
                <asp:Label ID="jigFixLabel" runat="server" CssClass="required"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server"  CssClass="margin">SAMPLE PART NO <br />
                <asp:TextBox ID="samplePartNo" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server"  CssClass="margin">PHOTO <br /> Supported Formats: jpg,png,jpeg,gif <br />
                <asp:Label ID="lblFile" runat="server"></asp:Label>
                <asp:FileUpload ID="partPhotoFileUpload" runat="server" onchange="return fileValidation();"/>
                <asp:Label ID="errorFile" runat="server"></asp:Label>
                <div id="imagePreview"></div>
                <asp:ImageButton ID="btnFile" runat="server" ImageUrl="~/Images/cancel.png" Height="20" Width="20" OnClick="btnFile_Click" OnClientClick="return confirm('Do you want to delete')"/>
            </asp:TableCell>
            <asp:TableCell runat="server"  CssClass="margin">PROCESS SHEET <br /> Supported Formats: xls,csv,pdf <br />
                <asp:Label ID="lblMoldFile" runat="server" style="display:inline;"></asp:Label>
                <asp:FileUpload ID="moldSpecFileUpload" runat="server" onchange="return moldFileValidation();"/>
                <asp:Label ID="errorMoldFile" runat="server"></asp:Label>
                <asp:ImageButton ID="btnMoldFile" runat="server" ImageUrl="~/Images/cancel.png" Height="20" Width="20" OnClick="btnMoldFile_Click" OnClientClick="return confirm('Do you want to delete?');"/>
            </asp:TableCell>
            <asp:TableCell runat="server"  CssClass="margin" ColumnSpan="2" RowSpan="2">
                        <asp:Button OnClick="NextPage_Click" runat="server" ID="nextPage" CssClass="nextPage" Text="NEXT" OnClientClick="return validationPartMaster();" ></asp:Button>
                        &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to Cancel?');"  />
                    </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center><br /><br />
</asp:Content>
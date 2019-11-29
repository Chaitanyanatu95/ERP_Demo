<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rawMaterialPage.aspx.cs" Inherits="ERP_Demo.rawMaterialPage" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <script type="text/javascript">
            window.onload = function () {
                onMasterBatchSelected();
                onAltRawMaterialSelected();
                onAltRMMasterBatchSelected();
            }

            function onMasterBatchSelected() {
                var isMasterBatchSelected = $('#<%=masterbatchDropDownList.ClientID %> option:selected').text();
                
                if (isMasterBatchSelected == "YES") {
                    $("#<%=rawMaterialMasterBatchTable.ClientID %>").show();
                }
                else {
                    $("#<%=rawMaterialMasterBatchTable.ClientID %>").hide();
                }
            }

            function onAltRawMaterialSelected() {
                var isAltRawMaterialSelected = $('#<%=altRawMaterialDropDownList.ClientID %> option:selected').text();
                
                if (isAltRawMaterialSelected == "YES") {
                    $("#<%=altRawMaterialTable.ClientID %>").show();
                }
                else{
                    $("#<%=altRawMaterialTable.ClientID %>").hide();
                    $("#<%=altRawMaterialMasterBatchTable.ClientID %>").hide();
                }
            }

            function onAltRMMasterBatchSelected() {
                var isAltRMMasterBatchSelected = $('#<%=altRawMaterialMasterBatchDropDownList.ClientID %> option:selected').text();
                
                if (isAltRMMasterBatchSelected == "YES") {
                    $("#<%=altRawMaterialMasterBatchTable.ClientID %>").show();
                }
                else{
                    $("#<%=altRawMaterialMasterBatchTable.ClientID %>").hide();
                }
            }

            function validationOnFields() {
                //masterbatch
                var isMasterBatchSelected = $('#<%=masterbatchDropDownList.ClientID %> option:selected').text();
                if (isMasterBatchSelected == "YES") {
                    var mbName = document.getElementById("<%=mbNameDropDownList.ClientID%>");
                    var getText = mbName.options[mbName.selectedIndex].text;
                    if (getText == "Select Masterbatch") {
                        document.getElementById("<%=mbNameLabel.ClientID%>").innerHTML = "Please select masterbatch.".fontcolor("red");
                        return false;
                    }

                    var mbGrade = document.getElementById("<%=mbGradeDropDownList.ClientID%>");
                    var getText = mbGrade.options[mbGrade.selectedIndex].text;
                    if (getText == "Select MasterBatch Grade") {
                        document.getElementById("<%=mbGradeLabel.ClientID%>").innerHTML = "Please select grade.".fontcolor("red");
                        return false;
                    }
                }

                //altrawmaterial
                var isAltRawMaterialSelected = $('#<%=altRawMaterialDropDownList.ClientID %> option:selected').text();
                if (isAltRawMaterialSelected == "YES") {
                    var altRmName = document.getElementById("<%=altRMDropDownList.ClientID%>");
                    var getText = altRmName.options[altRmName.selectedIndex].text;
                    if (getText == "Select Raw Material") {
                        document.getElementById("<%=altRMLabel.ClientID%>").innerHTML = "Please select raw material.".fontcolor("red");
                        return false;
                    }

                    var altRmGrade = document.getElementById("<%=altRawMaterialGradeDropDownList.ClientID%>");
                    var getText = altRmGrade.options[altRmGrade.selectedIndex].text;
                    if (getText == "Select Raw Material Grade") {
                        document.getElementById("<%=altRMGradeLabel.ClientID%>").innerHTML = "Please select grade.".fontcolor("red");
                        return false;
                    }

                    //alt rm masterbatch
                    var isAltRMMasterBatchSelected = $('#<%=altRawMaterialMasterBatchDropDownList.ClientID %> option:selected').text();
                    if (isAltRMMasterBatchSelected == "YES") {
                        var altRmMbName = document.getElementById("<%=altRMBNameDropDownList.ClientID%>");
                        var getText = altRmMbName.options[altRmMbName.selectedIndex].text;
                        if (getText == "Select Masterbatch") {
                            document.getElementById("<%=altRMBNameLabel.ClientID%>").innerHTML = "Please select masterbatch.".fontcolor("red");
                            return false;
                        }

                        var altRmMbGrade = document.getElementById("<%=altRMBGradeDropDownList.ClientID%>");
                        var getText = altRmMbGrade.options[altRmMbGrade.selectedIndex].text;
                        if (getText == "Select MasterBatch Grade") {
                            document.getElementById("<%=altRMBGradeLabel.ClientID%>").innerHTML = "Please select grade.".fontcolor("red");
                            return false;
                        }
                    }
                }
            }

        </script>
         <div style="display:block;margin-left:auto;margin-right:auto;height:40px;width:80%;background-color:whitesmoke;padding-top:5px;">
        <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; color:black">
            <b>STEP 1</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:SkyBlue;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 2</b>
        </div>
        <div style="display:inline-block; height:1px; width:200px; background-color:black; padding-top:7px; margin-left:-4px;"></div>
         <div style="display:inline-block;text-align:center;border:1px solid black;background-color:transparent;width:20%;height:33px;padding-top:7px; margin-left:-4px; color:black">
            <b>STEP 3</b>
        </div>
    </div>
        <br />
                                        <%-- RAW MATERIAL --%>
        <asp:Table ID="rawMaterialTable" runat="server" CssClass="Table1" Height="30%" Width="70%">
            <asp:TableRow runat="server" HorizontalAlign="Center" BackColor="SkyBlue" Height="25%">
                <asp:TableCell runat="server" Font-Bold="true" ColumnSpan="6">RAW MATERIAL DETAILS</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="margin">RAW MATERIAL</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">R/M Grade</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">R/M MAKE</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">COLOUR</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MASTERBATCH </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">ALT RAW MATERIAL(REQ)</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" CssClass="spacingtop">
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:DropDownList ID="rawMaterialDropDownList" DataTextField="material_name" DataValueField="material_name" runat="server" onselectedindexchanged="rawMaterialNameChanged" AutoPostBack="true"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator runat="server" id="rawMaterialNameReq" controltovalidate="rawMaterialDropDownList" errormessage="Please select raw material!" CssClass="required" />
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:DropDownList ID="rmGradeDropDownList" DataTextField="material_grade" DataValueField="material_grade" runat="server" onselectedindexchanged="rawMaterialGradeChanged" AutoPostBack="true"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator runat="server" id="rawMaterialGradeReq" controltovalidate="rmGradeDropDownList" errormessage="Please select raw material grade!" CssClass="required" />
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:TextBox ID="rmMakeTextBox" DataTextField="material_make" DataValueField="material_make" runat="server" ReadOnly="true" ></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin"><asp:TextBox ID="colourTextBox" DataTextField="material_color" DataValueField="material_color" runat="server" ReadOnly="true" ></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:DropDownList ID="masterbatchDropDownList" runat="server" onchange="onMasterBatchSelected()">
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                          </asp:DropDownList><br />
                    <asp:RequiredFieldValidator runat="server" id="masterBatchReq" controltovalidate="masterbatchDropDownList" errormessage="Please select option!" CssClass="required" />
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:DropDownList ID="altRawMaterialDropDownList" runat="server" onchange="onAltRawMaterialSelected()">
                        <asp:ListItem Value="NO">NO</asp:ListItem> 
                        <asp:ListItem Value="YES">YES</asp:ListItem>
                    </asp:DropDownList><br />
                <asp:RequiredFieldValidator runat="server" id="altRawMaterialReq" controltovalidate="altRawMaterialDropDownList" errormessage="Please select option!" CssClass="required" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
                                    <%-- RAW MATERIAL MASTERBATCH --%>
        
        <asp:Table ID="rawMaterialMasterBatchTable" runat="server" CssClass="Table1" Height="55%" Width="70%">
            <asp:TableRow runat="server" HorizontalAlign="Center" BackColor="SkyBlue">
                <asp:TableCell runat="server" Font-Bold="true" ColumnSpan="5">
                    <asp:Label runat="server" ID="masterBatchHeaderLabel" >MASTERBATCH DETAILS</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="masterBatchLabel" runat="server">
                <asp:TableCell runat="server" CssClass="margin">MB NAME</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB GRADE</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB MFG</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB COLOR</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB COLOR CODE</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="masterBatchTextBox" runat="server" CssClass="spacingtop">
                <asp:TableCell runat="server" CssClass="margin">
                    <div class="required" style="padding-left:12em">*</div>
                    <asp:DropDownList ID="mbNameDropDownList" DataTextField="mb_name" DataValueField="mb_name" runat="server" onselectedindexchanged="masterBatchNameChanged" AutoPostBack="true"></asp:DropDownList><br /><br />
                    <asp:Label ID="mbNameLabel" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <div class="required" style="padding-left:12em">*</div>
                    <asp:DropDownList ID="mbGradeDropDownList" DataTextField="mb_grade" DataValueField="mb_grade" runat="server" onselectedindexchanged="masterBatchGradeChanged" AutoPostBack="true"></asp:DropDownList><br /><br />
                    <asp:Label ID="mbGradeLabel" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin" >
                    <asp:TextBox ID="mbMfgTextBox" DataTextField="mb_mfg" DataValueField="mb_mfg" runat="server" ReadOnly="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:TextBox ID="mbColorTextBox" DataTextField="mb_color" DataValueField="mb_color" runat="server" ReadOnly="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:TextBox ID="mbColorCodeTextBox" DataTextField="mb_color_code" DataValueField="mb_color_code" runat="server" ReadOnly="true"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
                                        
                                    <%-- ALTERNATE RAW MATERIAL --%>

            <asp:Table ID="altRawMaterialTable" runat="server" CssClass="Table1" Height="52%" Width="70%">
                <asp:TableRow runat="server" HorizontalAlign="Center" ID="altRMDetailsRow" BackColor="SkyBlue">
                    <asp:TableCell runat="server" Font-Bold="true" ColumnSpan="5">
                        <asp:Label runat="server" ID="altRMDetailsLabel">ALT RAW MATERIAL DETAILS</asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="altRawMaterialLabel" runat="server" VerticalAlign="Bottom">
                    <asp:TableCell runat="server" CssClass="margin">RAW MATERIAL</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">R/M Grade</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">R/M MAKE</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">COLOUR</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">ALT. MASTERBATCH</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" CssClass="spacingtop">
                    <asp:TableCell runat="server" CssClass="margin">
                        <div class="required" style="padding-left:12em">*</div>
                        <asp:DropDownList ID="altRMDropDownList" DataTextField="material_name" DataValueField="material_name" runat="server" onselectedindexchanged="altRawMaterialNameChanged" AutoPostBack="true"></asp:DropDownList><br /><br />
                        <asp:Label ID="altRMLabel" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">
                        <div class="required" style="padding-left:12em">*</div>
                        <asp:DropDownList ID="altRawMaterialGradeDropDownList" DataTextField="material_grade" DataValueField="material_grade" runat="server" onselectedindexchanged="altRawMaterialGradeChanged" AutoPostBack="true"></asp:DropDownList><br /><br />
                        <asp:Label ID="altRMGradeLabel" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">
                        <asp:TextBox ID="altRawMaterialMakeTextBox" DataTextField="material_make" DataValueField="material_make" runat="server" ReadOnly="true"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">
                        <asp:TextBox ID="altColourTextBox" DataTextField="material_color" DataValueField="material_color" runat="server" ReadOnly="true"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell runat="server" CssClass="margin">
                        <asp:DropDownList ID="altRawMaterialMasterBatchDropDownList" runat="server" onchange="onAltRMMasterBatchSelected()">
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            <asp:ListItem Value="YES">YES</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
                                        <%-- ALTERNATE RAW MATERIAL MASTERBATCH --%>
        <asp:Table ID="altRawMaterialMasterBatchTable" runat="server" CssClass="Table1" Height="11%" Width="70%">
            <asp:TableRow runat="server" HorizontalAlign="Center" BackColor="SkyBlue">
                <asp:TableCell runat="server" ColumnSpan="5"><asp:Label runat="server" ID="altRMMasterBatchHeaderLabel" Font-Bold="true"  HorizontalAlign="Center">ALT RAW MATERIAL MASTERBATCH</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="altRawMaterialMasterBatchLabel" runat="server" VerticalAlign="Bottom">
                <asp:TableCell runat="server" CssClass="margin">MB NAME</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB GRADE</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB MFG</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB COLOR</asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">MB COLOR CODE</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" CssClass="margin">
                    <div class="required" style="padding-left:12em">*</div>
                    <asp:DropDownList ID="altRMBNameDropDownList" DataTextField="mb_name" DataValueField="mb_name" runat="server" OnSelectedIndexChanged="altMasterBatchNameChanged" AutoPostBack="true"></asp:DropDownList><br />
                    <asp:Label ID="altRMBNameLabel" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <div class="required" style="padding-left:12em">*</div>
                    <asp:DropDownList ID="altRMBGradeDropDownList" DataTextField="mb_grade" DataValueField="mb_grade" runat="server" OnSelectedIndexChanged="altMasterBatchGradeChanged" AutoPostBack="true"></asp:DropDownList><br />
                    <asp:Label ID="altRMBGradeLabel" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:TextBox ID="altRMBMfgTextBox" runat="server" ReadOnly="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:TextBox ID="altRMBColorTextBox" runat="server" ReadOnly="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" CssClass="margin">
                    <asp:TextBox ID="altRMBColorCodeTextBox" runat="server" ReadOnly="true"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
        <asp:Table runat="server" HorizontalAlign="Center" CssClass="partTable1" Height="15%"> 
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Button OnClick="NextPage_Click1" runat="server" CssClass="nextPage" Text="NEXT" OnClientClick="return validationOnFields();"></asp:Button>
                </asp:TableCell>
                <asp:TableCell runat="server" >&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="BACK" CssClass="nextPage" runat="server" OnClick="Unnamed_Click" CausesValidation="false"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FPA.aspx.cs" Inherits="ERP_Demo.FPA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <head>
        <script type="text/javascript">
            function validationPage()
            {
                var rejectionQuantity = document.getElementById('<%=prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter").ClientID %>'); 
                var partName = document.getElementById('<%=partNameDropDownList.ClientID %>');
                var getPart = partName.options[partName.selectedIndex].text;
                if (getPart != "Select Part Name")
                {
                    var postType = document.getElementById('<%=prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList").ClientID %>');
                    var getPost = postType.options[postType.selectedIndex].text;

                    if (getPost == "Select Rejection") {
                        for (var i = 0; i < grid.rows.length - 1; i++) {
                            document.getElementById('<%=prodRejHisGrid.FooterRow.FindControl("checkRejectionLbl").ClientID%>').innerHTML = "Please select rejection code.".fontcolor("red");
                        }
                        return false;
                    }
                    else if (getPost != "Select Rejection" && rejectionQuantity.value == "") {
                        for (var i = 0; i < grid.rows.length - 1; i++) {
                            document.getElementById('<%=prodRejHisGrid.FooterRow.FindControl("AddImgBtn").ClientID%>').style.display = "block";
                            document.getElementById('<%=prodRejHisGrid.FooterRow.FindControl("checkRejQuantLbl").ClientID%>').innerHTML = "Please enter rejection quantity.".fontcolor("red");
                        }
                        rejectionQuantity.readOnly = false;
                        return false;
                    }
                    else if (getPost == "N/A") {
                        document.getElementById('<%=prodRejHisGrid.FooterRow.FindControl("AddImgBtn").ClientID%>').style.display = "none";
                        return true;
                    }
                    else if (getPost != "Select Rejection" && rejectionQuantity.value != "") {
                        return true;
                    }
                    else {
                        return true;
                    }
                }
            }
        </script>
        <title>pb plastics | fpa</title>
    </head>
         <div id="page">
        <table runat="server" class="tableClass" style="width:80%; height:70%">
            <tr>
                <th runat="server" style="background-color:skyblue;text-align:center" colspan="9"><h3>FPA Worker</h3></th>
            </tr>
            <tr>
                <td style="font-weight:700; color:black; text-align:center"><asp:Label ID="lblFpaNo" runat="server">FPA No <div class="required" style=" display:inline;">*</div></asp:Label></td>
                <td style="font-weight:700; color:black; text-align:center"><asp:Label ID="lblWorkerName" runat="server">Worker Name <div class="required" style=" display:inline;">*</div></asp:Label></td>
                <td style="font-weight:700; color:black; text-align:center"><asp:Label ID="lblShift" runat="server">Shift <div class="required" style=" display:inline;">*</div></asp:Label></td>
                <td style="font-weight:700; color:black; text-align:center"><asp:Label ID="lblDate" runat="server">Date <div class="required" style=" display:inline;">*</div></asp:Label></td>
                <td style="font-weight:700; color:black; text-align:center"><asp:Label ID="lblProductionType" runat="server">Production Type</asp:Label></td>
                <td style="font-weight:700; color:black; text-align:center" colspan="2"><asp:Label ID="lblPartName" runat="server" style="font-weight:700; color:black; text-align:center">Part Name <div class="required" style=" display:inline;">*</div></asp:Label></td>
           </tr>
            <tr>
                <td style="text-align:center"><asp:TextBox ID="fpaTextBox" BackColor="WhiteSmoke" style="font-weight:500; color:black; text-align:center" runat="server" ReadOnly="true"></asp:TextBox></td>
                <td style="text-align:center"><asp:DropDownList ID="workerNameDropDownList" DataValueField="worker_name" DataTextField="worker_name" runat="server"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="workerReq" runat="server" ControlToValidate="workerNameDropDownList" CssClass="required" ErrorMessage="Please select worker." onchange="return validationPage()"></asp:RequiredFieldValidator></td>
                <td style="text-align:center"><asp:DropDownList ID="workerShiftDetails" DataValueField="shift_time" DataTextField="shift_time" runat="server"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="workerShiftDetails" CssClass="required" ErrorMessage="Please select shift."></asp:RequiredFieldValidator></td>
                <td style="text-align:center"><asp:TextBox TextMode="Date" id="dateSelectionTextBox" runat="server" style=" color:black; text-align:center" size="10"/>
                <br /><asp:RequiredFieldValidator ID="dateReq" runat="server" ControlToValidate="dateSelectionTextBox" CssClass="required" ErrorMessage="Please select date."></asp:RequiredFieldValidator></td>
                <td style="font-weight:300; color:black; text-align:center">
                    <asp:DropDownList ID="productionTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="productionTypeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="INHOUSE"></asp:ListItem>
                        <asp:ListItem Text="OUTSOURCED"></asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="productionTypeDropDownList" CssClass="required" ErrorMessage="Please select type."></asp:RequiredFieldValidator>
                </td>
                <td colspan="2" style="text-align:center"><asp:DropDownList ID="partNameDropDownList" DataValueField="part_name" DataTextField="part_name" onselectedindexchanged="partNameChanged" runat="server" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="partNameReq" runat="server" ControlToValidate="partNameDropDownList" CssClass="required" ErrorMessage="Please select part name."></asp:RequiredFieldValidator></td>
           </tr>
            <tr>
                <td style="font-weight:700; color:black; text-align:center" colspan="3">
                    <asp:Label ID="Label1" runat="server">Production History</asp:Label>
                </td>
                <td style="font-weight:300; color:black; text-align:center" rowspan="2">
                    <asp:Label ID="lblOperationType" runat="server" style="font-weight:700;" >Operation Type <div class="required" style=" display:inline;">*</div></asp:Label><br />
                    <asp:DropDownList ID="operationTypeList" runat="server" DataValueField="type" DataTextField="type" OnSelectedIndexChanged="operationTypeListChanged" AutoPostBack="true">
                    </asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="operationTypeList" CssClass="required" ErrorMessage="Please select operation."></asp:RequiredFieldValidator>
                </td>
                <td style="font-weight:300; color:black; text-align:center" rowspan="2">
                    <asp:Label ID="lblnoofparts" runat="server" style="font-weight:700;">Target Qty / hr <div class="required" style=" display:inline;">*</div></asp:Label><br />
                    <asp:TextBox ID="noOfPartsTextBox" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="noOfPartsTextBox" CssClass="required" ErrorMessage="Please enter parts."></asp:RequiredFieldValidator>
                </td>
                <td style="font-weight:300; color:black; text-align:center" rowspan="2">
                    <asp:Label ID="lblTotalTime" runat="server" style="font-weight:700;">Total Time <div class="required" style=" display:inline;">*</div></asp:Label><br />
                    <asp:TextBox ID="timeTextBox" runat="server" Width="50px"></asp:TextBox>
                    <br />
                    <asp:CustomValidator runat="server" id="timeCustom" controltovalidate="timeTextBox" onservervalidate="timeCustom_ServerValidate" errormessage="Value should be greater than 0!" /><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="timeTextBox" CssClass="required" ErrorMessage="Please enter time."></asp:RequiredFieldValidator>
                </td>
                <td style="font-weight:300; color:black; text-align:center" rowspan="2">
                    <asp:Label ID="lblAcceptedQty" runat="server" style="font-weight:700;">OK Quantity <div class="required" style=" display:inline;">*</div></asp:Label><br />
                    <asp:TextBox ID="actualQtyTextBox" runat="server" Width="50px" OnTextChanged="actualQtyTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="actualQtyTextBox" CssClass="required" ErrorMessage="Please enter OK quantity."></asp:RequiredFieldValidator><br />
                    <asp:Label ID="errorLbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-weight:300; color:black; text-align:center"><asp:Label ID="lblShiftDetails" runat="server" style="font-weight:700;">Shift <div class="required" style=" display:inline;">*</div></asp:Label><br /><asp:DropDownList ID="shiftDetailsDropDownList" DataValueField="shift_details" DataTextField="shift_details" runat="server"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="shiftDetailsDropDownList" CssClass="required" ErrorMessage="Please select shift."></asp:RequiredFieldValidator></td>
                <td style="font-weight:300; color:black; text-align:center"><asp:Label ID="lblEmployeeName" runat="server" style="font-weight:700;">Operator Name <div class="required" style=" display:inline;">*</div><br /></asp:Label><asp:DropDownList ID="operatorNameDropDownList" DataValueField="operator_name" DataTextField="operator_name" runat="server" AutoPostBack="true" OnSelectedIndexChanged="operatorChanged"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="operatorNameDropDownList" CssClass="required" ErrorMessage="Please select operator name."></asp:RequiredFieldValidator></td>
                <td style="font-weight:300; color:black; text-align:center"><asp:Label ID="lblProductionDate" runat="server" style="font-weight:700;">Date <div class="required" style=" display:inline;">*</div></asp:Label><br /><asp:DropDownList ID="dateDropDownList" DataValueField="date_dpr" DataTextField="date_dpr" runat="server" OnSelectedIndexChanged="dateChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dateDropDownList" CssClass="required" ErrorMessage="Please select date."></asp:RequiredFieldValidator></td>
                </tr>
            </table>
             <table runat="server" class="tableClass" style="width:80%; height:70%">
                 <tr><td style="font-weight:300; color:black; text-align:center; width:600px">
                    <asp:Label ID="lblProdRejectionQty" runat="server" style="font-weight: 700;color: black;text-align: center;"><h5><u>PRODUCTION REJECTION QUANTITY</u></h5></asp:Label><br />
                    <asp:GridView ID="prodRejHisGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="prodRejHisGrid_RowCommand" 
                    OnRowDeleting="prodRejHisGrid_RowDeleting" DataKeyNames="id" ShowFooter="True" CssClass="Table1" Width="80%">
                        <HeaderStyle Height="5px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                        <RowStyle Font-Size="Small" ForeColor="black" BackColor="WhiteSmoke"/>   
                        <FooterStyle Font-Size="Small" ForeColor="black" BackColor="WhiteSmoke"/>
                        <Columns>
                     <asp:TemplateField HeaderText="REJECTION CODE">
                        <ItemTemplate>
                            <asp:Label ID="rejectionCodeLbl" Text='<%# Eval("rejection_code") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="rejectionCodeDropDownList" runat="server" DataSourceID="SqlDataSource1" DataTextField="code" DataValueField="code" OnSelectedIndexChanged="rejectionCodeDropDownList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [code] FROM [rejection_master]"></asp:SqlDataSource>
                            <asp:Label ID="checkRejectionLbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REJECTION QUANTITY">
                        <ItemTemplate>
                            <asp:Label ID="rejectionQuantityLbl" Text='<%# Eval("rejection_quantity") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRejQuantityFooter" runat="server" ReadOnly="true"/>
                            <asp:Label ID="checkRejQuantLbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to Delete?');"  Height="20px" Width="20px" runat="server"/>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="AddImgBtn" ImageUrl="~\Images\addnew.png" CommandName="Add" OnClientClick="return validationPage();"  Height="20px" Width="20px" runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td style="font-weight:300; color:black; text-align:center">
                    <asp:Label ID="lblFpaRejectionQty" runat="server" style="font-weight:700;">FPA Rejection Quantity <div class="required" style=" display:inline;">*</div></asp:Label><br />
                    <asp:TextBox ID="FpaRejectionQtyTextBox" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="FpaRejectionQtyTextBox_TextChanged"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="FpaRejectionQtyTextBox" CssClass="required" ErrorMessage="Please enter FPA rejection"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2" style="font-weight:300; color:black; text-align:center">
                    <asp:Label ID="lblEfficiency" runat="server" style="font-weight:700; color:black; text-align:center">Efficiency <div class="required" style=" display:inline;">*</div></asp:Label>
                    <br />
                    <asp:TextBox ID="efficiencyTextBox" Width="40px" BackColor="WhiteSmoke" ReadOnly="true" runat="server" style="font-weight:500; color:black; text-align:center"></asp:TextBox>
                </td>
            </tr>
            </table>
             <br />
                <center> 
                    <asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click"  CssClass="nextPage" OnClientClick="return validationPage();"/>
                            &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');" OnClick="Cancel_Click" CausesValidation="false" />
                   </center>
                </div>
                <center>
                    <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
                    <br />
                    <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
                </center>
</asp:Content>

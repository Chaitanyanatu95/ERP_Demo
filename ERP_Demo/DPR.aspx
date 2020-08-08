<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DPR.aspx.cs" Inherits="ERP_Demo.DPR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <head>
        <title></title>
        <script type="text/javascript">
            function validationOnPostOperation() {
                var code = document.getElementById("<%=downTimeGridView.FooterRow.FindControl("downTimeDropDownListFooter").ClientID %>");
                var downTime = document.getElementById('<%=((TextBox)downTimeGridView.FooterRow.FindControl("txtdownTimeFooter")).ClientID %>');
                var getCode = code.options[code.selectedIndex].text;

                if (getCode == "Select Code") {
                    document.getElementById("<%=downTimeGridView.FooterRow.FindControl("checkdownTimeCodeLbl").ClientID %>").innerHTML = "Please select code or N/A.".fontcolor("red");
                    downTime.readOnly = false;
                    return false;
                }
                else if (getCode != "Select Code" && downTime.value == "" && getCode != "N/A") {
                    document.getElementById("<%=downTimeGridView.FooterRow.FindControl("checkdownTimeLbl").ClientID %>").innerHTML = "Please enter time.".fontcolor("red");
                    downTime.readOnly = false;
                    return false;
                }
                else if (getCode == "N/A")
                {
                    return true;
                }
                else if (getCode != "Select Code" && downTime.value != "")
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            function validationOnThisPage() 
            {
                var a = validationOnPostOperation();
                var b = Page_ClientValidate();
                if (a && b) {
                    return confirm('Do you want to save?');
                }
                else {
                    return false;
                }
            }
        </script>
    </head>
    <body>
    <asp:Table ID="Table1" runat="server" CssClass="tableClass">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" BackColor="SkyBlue">
            <asp:TableCell runat="server" ColumnSpan="6"><h3>DPR OPERATOR</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="1"><asp:Label ID="Label1" runat="server" style="font-weight:700; color:black"> Dpr No<div class="required" style="display:inline">*</div></asp:Label><br/>
               <asp:TextBox ID="dprNo" runat="server" BackColor="WhiteSmoke" style="font-weight:500; color:black; text-align:center" ReadOnly="true"/>
            <br />
            </asp:TableCell><asp:TableCell runat="server" ColumnSpan="2"><asp:Label ID="employeeName" runat="server" style="font-weight:700; color:black">Operator Name <div class="required" style="display:inline">*</div></asp:Label><br /><br />
                <asp:DropDownList ID="operatorNameDropDownList" DataTextField="worker_name" DataValueField="worker_name" runat="server" AutoPostBack="true"></asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="operatorNameDropDownList" CssClass="required" ErrorMessage="Please select worker name."></asp:RequiredFieldValidator>
            </asp:TableCell><asp:TableCell runat="server" ColumnSpan="2"><asp:Label ID="Date" runat="server" style="font-weight:700; color:black"> Date <div class="required" style="display:inline">*</div></asp:Label><br />
               <asp:TextBox ID="dateTextBox" runat="server" TextMode="Date" style="font-weight:500; color:black; text-align:center"/>
            <br /><asp:RequiredFieldValidator ID="dateReq" runat="server" ControlToValidate="dateTextBox" CssClass="required" ErrorMessage="Please select date."></asp:RequiredFieldValidator>
            </asp:TableCell></asp:TableRow><asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom" >
            <asp:TableCell runat="server" ColumnSpan="1"><div style="font-weight:700; color:black">Part Name <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="partNameDropDownList" DataTextField="part_name" DataValueField="part_name" runat="server" onselectedindexchanged="partNameChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="partNameDropDownList" CssClass="required" ErrorMessage="Please select part name."></asp:RequiredFieldValidator>
            </asp:TableCell><asp:TableCell runat="server" ColumnSpan="2"><div style="font-weight:700; color:black">Material Grade <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="materialGradeDropDownList" runat="server" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="materialGradeDropDownList" CssClass="required" ErrorMessage="Please select grade."></asp:RequiredFieldValidator>
            </asp:TableCell><asp:TableCell runat="server" ColumnSpan="1"><div style="font-weight:700; color:black">Machine Used <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="machineUsedDropDownList" DataTextField="machine_no" DataValueField="machine_no" runat="server" onselectedindexchanged="machineUsedChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="machineUsedDropDownList" CssClass="required" ErrorMessage="Please select machine used."></asp:RequiredFieldValidator>
            </asp:TableCell><asp:TableCell runat="server" ColumnSpan="2"><div style="font-weight:700; color:black">Shift Details <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="shiftDetailsDropDownList" DataTextField="shift_time" DataValueField="shift_time" runat="server" onselectedindexchanged="shiftDetailsChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="shiftDetailsDropDownList" CssClass="required" ErrorMessage="Please select shift."></asp:RequiredFieldValidator>
            </asp:TableCell></asp:TableRow><asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server" style="padding-bottom:40px;"><div style="font-weight:700; color:black">EXP. QTY. (PCS) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="expQuantityTextBox" BackColor="WhiteSmoke" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell><asp:TableCell runat="server"><div style="font-weight:700; color:black">No. of Shots (Start Counter) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="noShotsStartTextBox" runat="server" AutoPostBack="true" OnTextChanged="noShotsStartTextBox_TextChanged" ReadOnly="true"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="noShotsStartTextBox" CssClass="required" ErrorMessage="Please enter start counter."></asp:RequiredFieldValidator>
            </asp:TableCell><asp:TableCell runat="server"><div style="font-weight:700; color:black">No. of Shots (End Counter) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="noShotsEndTextBox" runat="server" OnTextChanged="noShotsEndTextBox_TextChanged" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="noShotsEndTextBox" CssClass="required" ErrorMessage="Please enter end counter."></asp:RequiredFieldValidator>
                <br /><asp:Label ID="validationShots" runat="server" CssClass="required"></asp:Label>
            </asp:TableCell><asp:TableCell runat="server" style="padding-bottom:40px;" ColumnSpan="2"><div style="font-weight:700; color:black">Total No. of Shots <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="noShotsTextBox"  runat="server" BackColor="WhiteSmoke" ReadOnly="true" OnTextChanged="noShotsEndTextBox_TextChanged"></asp:TextBox>
                <br />
            </asp:TableCell></asp:TableRow><asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">Rejection (PCS) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="rejectionPCSTextBox" runat="server" OnTextChanged="rejectionPCSTextBox_TextChanged" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rejectionPCSTextBox" CssClass="required" ErrorMessage="Please enter rejection in pcs."></asp:RequiredFieldValidator>
            </asp:TableCell><asp:TableCell runat="server" style="padding-bottom:37px;"><div style="font-weight:700; color:black">Rejection (KGS)</div>
                <br /><asp:TextBox ID="rejectionKGSTextBox" runat="server"></asp:TextBox>
            </asp:TableCell><asp:TableCell runat="server" style="padding-bottom:40px;"><div style="font-weight:700; color:black">ACT. QTY. (PCS) <div class="required" style="display:inline;">*</div></div><br />
                <asp:TextBox ID="actQuantityTextBox" BackColor="WhiteSmoke" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell><asp:TableCell runat="server" ColumnSpan="2" RowSpan="2"><asp:Button runat="server" Text="SAVE DETAILS" OnClick="SaveBtn_Click" CssClass="nextPage" OnClientClick="return validationOnThisPage();"/> &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');"/>
            </asp:TableCell></asp:TableRow><asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server">
                <asp:GridView ID="downTimeGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="downTimeGrid_RowCommand" 
                 OnRowDeleting="downTimeGrid_RowDeleting" DataKeyNames="id" ShowFooter="True"  Width="20%">
                        <HeaderStyle Height="5px" CssClass="CustomerHeader" Font-Bold="True" ForeColor="black" Font-Size="Small" />
                        <RowStyle Font-Size="Small" ForeColor="black" BackColor="WhiteSmoke"/>   
                        <FooterStyle Font-Size="Small" ForeColor="black" BackColor="WhiteSmoke"/>
                                <Columns>
                                     <asp:TemplateField HeaderText="DOWN TIME CODE">
                                        <ItemTemplate>
                                            <asp:Label ID="downTimeCodeLbl" Text='<%# Eval("down_time_code") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="downTimeDropDownListFooter" runat="server" DataSourceID="SqlDataSource1" DataTextField="down_time_type" DataValueField="down_time_type" OnSelectedIndexChanged="downTimeCodeDropDownList_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Server=tcp:erppbp.database.windows.net,1433;Initial Catalog=Pbplastics;Persist Security Info=False;User ID=erppbp;Password=Pranav_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [down_time_type] FROM [down_time_master]"></asp:SqlDataSource>
                                            <asp:Label ID="checkdownTimeCodeLbl" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOWN TIME(HRS)">
                                        <ItemTemplate>
                                            <asp:Label ID="downTimeLbl" Text='<%# Eval("down_time_hours") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtdownTimeFooter" runat="server" ReadOnly="true"/>
                                            <asp:Label ID="checkdownTimeLbl" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to Delete?');"  Height="20px" Width="20px" runat="server"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="AddImgBtn" ImageUrl="~\Images\addnew.png" CommandName="Add" OnClientClick="return validationOnPostOperation();"  Height="20px" Width="20px" runat="server"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                </asp:GridView>
            </asp:TableCell><asp:TableCell runat="server" ColumnSpan="2"><div style="font-weight:700; color:black"> EFFICIENCY <div class="required" style="display:inline;">*</div></div><br />
                <asp:TextBox ID="efficiencyTextBox" BackColor="WhiteSmoke" runat="server" ReadOnly="true" style="font-weight:700; color:black"></asp:TextBox>
            </asp:TableCell></asp:TableRow></asp:Table><center>
        <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
        <br/>
        <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
    </center>
        </body>
    </html>
</asp:Content>

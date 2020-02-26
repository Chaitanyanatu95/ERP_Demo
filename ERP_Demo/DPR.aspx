<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DPR.aspx.cs" Inherits="ERP_Demo.DPR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validationOnThisPage() 
        {
            if (Page_ClientValidate())
            {
                return confirm('Do you want to save?');
            }
            else
            {
                return false;
            }
        }
    </script>
    <asp:Table ID="Table1" runat="server" CssClass="tableClass" Width="85%" Height="70%" style="padding-top:70px;">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" BackColor="SkyBlue">
            <asp:TableCell runat="server" ColumnSpan="6"><h3>DPR OPERATOR</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="3"><asp:Label ID="employeeName" runat="server" style="font-weight:700; color:black">Operator Name <div class="required" style="display:inline">*</div></asp:Label><br /><br /><asp:DropDownList ID="operatorNameDropDownList" DataTextField="worker_name" DataValueField="worker_name" runat="server" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="operatorNameDropDownList" CssClass="required" ErrorMessage="Please select worker name."></asp:RequiredFieldValidator></asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="3"><asp:Label ID="Date" runat="server" style="font-weight:700; color:black">Date <div class="required" style="display:inline">*</div></asp:Label><br />
               <input type="date" id="dateSelection" runat="server" style="font-weight:500; color:black; text-align:center"/>
                <br /><asp:RequiredFieldValidator ID="dateReq" runat="server" ControlToValidate="dateSelection" CssClass="required" ErrorMessage="Please select date."></asp:RequiredFieldValidator></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom" >
            <asp:TableCell runat="server" ColumnSpan="1"><div style="font-weight:700; color:black">Part Name <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="partNameDropDownList" DataTextField="part_name" DataValueField="part_name" runat="server" onselectedindexchanged="partNameChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="partNameDropDownList" CssClass="required" ErrorMessage="Please select part name."></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="2"><div style="font-weight:700; color:black">Material Grade <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="materialGradeDropDownList" runat="server" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="materialGradeDropDownList" CssClass="required" ErrorMessage="Please select grade."></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="1"><div style="font-weight:700; color:black">Machine Used <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="machineUsedDropDownList" DataTextField="machine_no" DataValueField="machine_no" runat="server" onselectedindexchanged="machineUsedChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="machineUsedDropDownList" CssClass="required" ErrorMessage="Please select machine used."></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="2"><div style="font-weight:700; color:black">Shift Details <div class="required" style="display:inline">*</div></div><br />
                <asp:DropDownList ID="shiftDetailsDropDownList" DataTextField="shift_time" DataValueField="shift_time" runat="server" onselectedindexchanged="shiftDetailsChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="shiftDetailsDropDownList" CssClass="required" ErrorMessage="Please select shift."></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server" style="padding-bottom:40px;"><div style="font-weight:700; color:black">EXP. QTY. (PCS) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="expQuantityTextBox" BackColor="WhiteSmoke" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">No. of Shots (Start Counter) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="noShotsStartTextBox" runat="server" AutoPostBack="true" OnTextChanged="noShotsStartTextBox_TextChanged" ReadOnly="true"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="noShotsStartTextBox" CssClass="required" ErrorMessage="Please enter start counter."></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">No. of Shots (End Counter) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="noShotsEndTextBox" runat="server" OnTextChanged="noShotsEndTextBox_TextChanged" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="noShotsEndTextBox" CssClass="required" ErrorMessage="Please enter end counter."></asp:RequiredFieldValidator>
                <br /><asp:Label ID="validationShots" runat="server" CssClass="required"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" style="padding-bottom:40px;"><div style="font-weight:700; color:black">Total No. of Shots <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="noShotsTextBox"  runat="server" BackColor="WhiteSmoke" ReadOnly="true" OnTextChanged="noShotsEndTextBox_TextChanged"></asp:TextBox>
                <br />
            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">Rejection (PCS) <div class="required" style="display:inline">*</div></div><br />
                <asp:TextBox ID="rejectionPCSTextBox" runat="server" OnTextChanged="rejectionPCSTextBox_TextChanged" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rejectionPCSTextBox" CssClass="required" ErrorMessage="Please enter rejection in pcs."></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" style="padding-bottom:37px;"><div style="font-weight:700; color:black">Rejection (KGS)</div>
                <br /><asp:TextBox ID="rejectionKGSTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server" style="padding-bottom:40px;"><div style="font-weight:700; color:black">ACT. QTY. (PCS) <div class="required" style="display:inline;">*</div></div><br />
                <asp:TextBox ID="actQuantityTextBox" BackColor="WhiteSmoke" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" ><div style="font-weight:700; color:black">Down Time Code <div class="required" style="display:inline;">*</div></div><br />
                <asp:DropDownList ID="downTimeCodeDropDownList" DataTextField="down_time_type" DataValueField="down_time_type" onselectedindexchanged="downTimeCodeDropDownList_SelectedIndexChanged" runat="server" AutoPostBack="true">
                </asp:DropDownList>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="downTimeCodeDropDownList" CssClass="required" ErrorMessage="Please select down time code."></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" ><div style="font-weight:700; color:black">Down Time(Hrs) <div class="required" style="display:inline;">*</div></div><br />
                <asp:TextBox ID="downTimeTextBox" runat="server" OnTextChanged="downTimeTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="downTimeTextBox" CssClass="required" ErrorMessage="Please enter down time."></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell runat="server" style="padding-bottom:40px;"><div style="font-weight:700; color:black">EFFICIENCY <div class="required" style="display:inline;">*</div></div><br />
                <asp:TextBox ID="efficiencyTextBox" BackColor="WhiteSmoke" runat="server" ReadOnly="true" style="font-weight:700; color:black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="2"><asp:Button runat="server" Text="SAVE DETAILS" OnClick="SaveBtn_Click"  CssClass="nextPage" OnClientClick="return validationOnThisPage();"/> &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
        <br/>
        <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
    </center>
</asp:Content>

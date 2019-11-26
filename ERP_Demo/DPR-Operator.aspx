<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DPR-Operator.aspx.cs" Inherits="ERP_Demo.Dpr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <html>
    <head>
        <title> pb plastics | DPR</title>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css">
        <script type="text/javascript" src = "https://code.jquery.com/jquery-1.10.2.js"></script>
        <script type="text/javascript" src = "https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
        <script type="text/javascript">
             $( function() {
                 $("#<%=dateSelectionTextBox.ClientID %>").datepicker({ showAnim: "fold", dateFormat: "dd-mm-yy"});
            } );
        </script>
    </head>
    <asp:Table ID="Table1" runat="server" CssClass="tableClass" Width="85%" Height="70%" style="padding-top:70px;">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" BackColor="SkyBlue">
            <asp:TableCell runat="server" ColumnSpan="6"><h3>DPR OPERATOR</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="3"><asp:Label ID="employeeName" runat="server" style="font-weight:700; color:black">Employee Name</asp:Label><br /><asp:TextBox ID="workerNameTextBox" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="3"><asp:Label ID="Date" runat="server" style="font-weight:700; color:black">Date</asp:Label><br />
                <input type="text" id="dateSelectionTextBox" runat="server" style="font-weight:700; color:black; text-align:center"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom" >
            <asp:TableCell runat="server" ColumnSpan="1"><div style="font-weight:700; color:black">Part Name</div>
                <asp:DropDownList ID="partNameDropDownList" DataTextField="part_name" DataValueField="part_name" runat="server" onselectedindexchanged="partNameChanged" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="2"><div style="font-weight:700; color:black">Material Grade</div>
                <asp:DropDownList ID="materialGradeDropDownList" runat="server" DataTextField="rm_grade" DataValueField="rm_grade" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="1"><div style="font-weight:700; color:black">Machine Used</div>
                <asp:DropDownList ID="machineUsedDropDownList" DataTextField="machine_no" DataValueField="machine_no" runat="server" onselectedindexchanged="machineUsedChanged"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="2"><div style="font-weight:700; color:black">Shift Details</div>
                <asp:DropDownList ID="shiftDetailsDropDownList" DataTextField="shift_time" DataValueField="shift_time" runat="server" onselectedindexchanged="shiftDetailsChanged" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">EXP. QTY. (PCS)</div>
                <asp:TextBox ID="expQuantityTextBox" runat="server" ReadOnly="true"></asp:TextBox>

            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">No. of Shots (Start Counter)</div>
                <asp:TextBox ID="noShotsStartTextBox" runat="server"></asp:TextBox>

            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">No. of Shots (End Counter)</div>
                <asp:TextBox ID="noShotsEndTextBox" runat="server" OnTextChanged="noShotsEndTextBox_TextChanged" AutoPostBack="true"></asp:TextBox><br />

            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">Total No. of Shots</div>
                <asp:TextBox ID="noShotsTextBox" runat="server" ReadOnly="true" OnTextChanged="noShotsEndTextBox_TextChanged"></asp:TextBox>
                <br /><asp:Label ID="validationShots" runat="server" CssClass="required"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">Rejection (PCS)</div>
                <asp:TextBox ID="rejectionPCSTextBox" runat="server" OnTextChanged="rejectionPCSTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">Rejection (KGS)</div>
                <asp:TextBox ID="rejectionKGSTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">ACT. QTY. (PCS)</div>
                <asp:TextBox ID="actQuantityTextBox" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" ><div style="font-weight:700; color:black">Down Time Code</div>
                <asp:DropDownList ID="downTimeCodeDropDownList" DataTextField="down_time_type" DataValueField="down_time_type" onselectedindexchanged="downTimeCodeDropDownList_SelectedIndexChanged" runat="server" AutoPostBack="true">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server" ><div style="font-weight:700; color:black">Down Time(Hrs)</div>
                <asp:TextBox ID="downTimeTextBox" runat="server" OnTextChanged="downTimeTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server"><div style="font-weight:700; color:black">EFFICIENCY</div>
                <asp:TextBox ID="efficiencyTextBox" runat="server" ReadOnly="true" style="font-weight:700; color:black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="2"><asp:Button runat="server" Text="SAVE DETAILS" OnClick="SaveBtn_Click"  CssClass="nextPage" OnClientClick="confirm('Do you want to save?');"/> &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to cancel?');"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
        <br/>
        <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
    </center>
</html>
</asp:Content>

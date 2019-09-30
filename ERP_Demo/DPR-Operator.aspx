<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DPR-Operator.aspx.cs" Inherits="ERP_Demo.Dpr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" CssClass="tableClass" Width="84%" Height="60%" style="padding-top:80px;">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" BackColor="SkyBlue">
            <asp:TableCell runat="server" ColumnSpan="6"><h3>DPR OPERATOR</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server" ColumnSpan="1"><asp:Label ID="Date" runat="server">Date</asp:Label><br />
                <input type="Date" id="dateSelection" runat="server" /></asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="1"><asp:Label ID="materialQtyLabel" Text="" runat="server" ForeColor="#3399ff"></asp:Label><br /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server">Part Name</asp:TableCell>
            <asp:TableCell runat="server">Material Grade</asp:TableCell>
            <asp:TableCell runat="server">Machine Used</asp:TableCell>
            <asp:TableCell runat="server">Shift Details</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">
                <asp:DropDownList ID="partNameDropDownList" DataTextField="part_name" DataValueField="part_name" runat="server" onselectedindexchanged="partNameChanged" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:DropDownList ID="materialGradeDropDownList" runat="server" DataTextField="rm_grade" DataValueField="rm_grade" AutoPostBack="true">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:DropDownList ID="machineUsedDropDownList" DataTextField="machine_no" DataValueField="machine_no" runat="server" onselectedindexchanged="machineUsedChanged"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:DropDownList ID="shiftDetailsDropDownList" DataTextField="shift_time" DataValueField="shift_time" runat="server" onselectedindexchanged="shiftDetailsChanged" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server">EXP. QTY. (PCS)</asp:TableCell>
            <asp:TableCell runat="server">No. of Shots (Start Counter)</asp:TableCell>
            <asp:TableCell runat="server">No. of Shots (End Counter)</asp:TableCell>
            <asp:TableCell runat="server">Total No. of Shots </asp:TableCell>
            <asp:TableCell runat="server">Rejection (PCS)</asp:TableCell>
            <asp:TableCell runat="server">Rejection (KGS)</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">
                <asp:TextBox ID="expQuantityTextBox" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox ID="noShotsStartTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
             <asp:TableCell runat="server">
                <asp:TextBox ID="noShotsEndTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
           <asp:TableCell runat="server">
                <asp:TextBox ID="noShotsTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox ID="rejectionPCSTextBox" runat="server" AutoPostBack="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox ID="rejectionKGSTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">
            <asp:TableCell runat="server">ACT. QTY. (PCS)</asp:TableCell>
            <asp:TableCell runat="server">Down Time(Hrs)</asp:TableCell>
            <asp:TableCell runat="server">Down Time Code</asp:TableCell>
            <asp:TableCell runat="server">EFFICIENCY</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
             <asp:TableCell runat="server">
                <asp:TextBox ID="actQuantityTextBox" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox ID="downTimeTextBox" runat="server" AutoPostBack="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:DropDownList ID="downTimeCodeDropDownList" DataTextField="down_time_code" DataValueField="down_time_code" runat="server" >
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox ID="efficiencyTextBox" runat="server" ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server"><asp:Button runat="server" Text="SAVE DETAILS" OnClick="SaveBtn_Click"  CssClass="nextPage"/></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
        <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
        <br />
        <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
    </center>
</asp:Content>

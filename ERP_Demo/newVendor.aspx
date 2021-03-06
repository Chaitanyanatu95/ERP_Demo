﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="newVendor.aspx.cs" Inherits="ERP_Demo.newVendor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validatePage() {
            if (Page_ClientValidate()) {
                return confirm('Do you want to save?');
            }
            else {
                return false;
            }
        }
    </script>
    <asp:Table ID="Table1" runat="server" CssClass="tableClass" Height="40%" Width="30%">
        <asp:TableRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" CssClass="CustomerHeader">
            <asp:TableCell runat="server" ColumnSpan="4"><h3>VENDOR DETAILS</h3></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="margin">VENDOR ID<br/>
            <asp:TextBox runat="server" ID="vendorIdTextBox" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="vendorNameLabel" runat="server" CssClass="margin">VENDOR NAME <br />
                <div class="required" style="padding-left:12em">*</div>
                <asp:TextBox ID="vendorNameTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="vendorNameReq" CssClass="required" runat="server" ErrorMessage="Please enter vendor name" ControlToValidate="vendorNameTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell ID="vendorAddressOneLabel" runat="server" CssClass="margin">ADDRESS ONE <br />
            <div class="required" style="padding-left:12em">*</div>
                <asp:TextBox TextMode="multiline" Columns="40" Rows="5" ID="vendorAddressOneTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="vendorAddressOneReq" CssClass="required" runat="server" ErrorMessage="Please enter address" ControlToValidate="vendorAddressOneTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell ID="vendorAddressTwoLabel" runat="server" CssClass="margin">ADDRESS TWO <br />
                <asp:TextBox TextMode="multiline" Columns="40" Rows="5" ID="vendorAddressTwoTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server">
            <asp:TableCell ID="vendorContactLabel" runat="server" CssClass="margin">CONTACT NO <br />
                <div class="required" style="padding-left:12em">*</div>
                <asp:TextBox ID="vendorContactNoTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="vendorContactReq" CssClass="required" runat="server" ErrorMessage="Please enter contact no" ControlToValidate="vendorContactNoTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell ID="vendorEmailLabel" runat="server" CssClass="margin">EMAIL-ID<br />
                <div class="required" style="padding-left:12em">*</div>
                <asp:TextBox ID="vendorEmailIdTextBox" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="vendorEmailIdTextBox" CssClass="required" ErrorMessage="Please enter valid Email-Id"></asp:RegularExpressionValidator>
                <br /><asp:RequiredFieldValidator ID="vendorEmailReq" CssClass="required" runat="server" ErrorMessage="Please enter Email-Id" ControlToValidate="vendorEmailIdTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell ID="vendorContactPersonLabel" runat="server" CssClass="margin">CONTACT PERSON<br />  
                <div class="required" style="padding-left:12em">*</div>
                <asp:TextBox ID="vendorContactPersonTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="vendorContactPersonReq" CssClass="required" runat="server" ErrorMessage="Please enter contact person" ControlToValidate="vendorContactPersonTextBox"></asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell ID="vendorGstNumberLabel" runat="server" CssClass="margin">GST NUMBER<br />
                <div class="required" style="padding-left:12em">*</div>
                <asp:TextBox ID="vendorGstDetailsTextBox" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="vendorGstReq" CssClass="required" runat="server" ErrorMessage="Please enter GST NO." ControlToValidate="vendorGstDetailsTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server"  
                        ControlToValidate="vendorGstDetailsTextBox" ErrorMessage="GST Number should be 15 in length..!"  
                        ValidationExpression="^[0-9,A-Z,a-z]{15}$" CssClass="required"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="saveRowLabel" runat="server"> 
            <asp:TableCell ID="saveCellLabel" runat="server" ColumnSpan="4" CssClass="margin">
                <asp:Button ID="saveButtonLabel" runat="server" Text="SAVE" OnClick="SaveBtn_Click" CssClass="custButtonCss" OnClientClick="return validatePage()" /> 
                <asp:Button ID="cancelLabel" Text="CANCEL" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
</asp:Content>

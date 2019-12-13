<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fpa.aspx.cs" Inherits="ERP_Demo.Fpa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<html>
<head>
    <script type="text/javascript">
        window.onload = function () {
            
        }
        
        </script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css">
    <script type="text/javascript" src = "https://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src = "https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <style>
        table{
            padding-top:15px;
        }
        td{
            text-align:center;
        }
    </style>
    <title>pb plastics | fpa</title>
</head>
<body>
     <div id="page">
    <table runat="server" class="tableClass" style="width:60%; height:70%">
        <tr>
            <th runat="server" style="background-color:skyblue;text-align:center" colspan="7"><h3>FPA Worker</h3></th>
        </tr>
        <tr>
            <td><asp:Label ID="lblOperatorName" runat="server" style="font-weight:700; color:black;">Employee Name <div class="required" style=" display:inline;">*</div></asp:Label></td>
            <td><asp:Label ID="lblPartName" runat="server" style="font-weight:700; color:black; text-align:center">Part Name <div class="required" style=" display:inline;">*</div></asp:Label></td>
            <td colspan="4"><asp:Label ID="lblProductionHistory" runat="server" style="font-weight:700; color:black; text-align:center">Production History</asp:Label></td>
       </tr>
        <tr>
            <td><asp:DropDownList ID="operatorNameDropDownList" DataValueField="worker_name" DataTextField="worker_name" runat="server"></asp:DropDownList></td>
            <td><asp:DropDownList ID="partNameDropDownList" DataValueField="part_name" DataTextField="part_name" onselectedindexchanged="partNameChanged" runat="server" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="partNameReq" runat="server" ControlToValidate="partNameDropDownList" CssClass="required" ErrorMessage="Please select part name."></asp:RequiredFieldValidator></td>
            <td><asp:Label ID="lblShiftDetails" runat="server" style="font-weight:700; color:black; text-align:center">Shift <div class="required" style=" display:inline;">*</div></asp:Label><br /><asp:DropDownList ID="shiftDetailsDropDownList" DataValueField="shift_details" DataTextField="shift_details" OnSelectedIndexChanged="shiftChanged" AutoPostBack="true" runat="server"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="shiftDetailsDropDownList" CssClass="required" ErrorMessage="Please select shift."></asp:RequiredFieldValidator></td>
            <td><asp:Label ID="lblEmployeeName" runat="server" style="font-weight:700; color:black; text-align:center">Employee Name <div class="required" style=" display:inline;">*</div><br /></asp:Label><asp:DropDownList ID="workerNameDropDownList" DataValueField="worker_name" DataTextField="worker_name" runat="server" AutoPostBack="true" OnSelectedIndexChanged="workerChanged"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="workerNameDropDownList" CssClass="required" ErrorMessage="Please select part name."></asp:RequiredFieldValidator></td>
            <td><asp:Label ID="lblProductionDate" runat="server" style="font-weight:700; color:black; text-align:center">Date <div class="required" style=" display:inline;">*</div></asp:Label><br /><asp:DropDownList ID="dateDropDownList" DataValueField="date_dpr" DataTextField="date_dpr" runat="server" OnSelectedIndexChanged="dateChanged" AutoPostBack="true"></asp:DropDownList><br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dateDropDownList" CssClass="required" ErrorMessage="Please select date."></asp:RequiredFieldValidator></td>
            <td><asp:Label ID="lblTotalQty" runat="server" style="font-weight:700; color:black; text-align:center">Total Qty <div class="required" style=" display:inline;">*</div></asp:Label><br /><asp:TextBox ID="totalQtyTextBox" runat="server" Width="70"></asp:TextBox></td>
       </tr>
        </table><br/>
         <table runat="server" class="tableClass" style="width:60%; height:70%">
        <tr>
            <td><asp:Label ID="lblOperationType" runat="server" style="font-weight:700; color:black; text-align:center">Operation Type <div class="required" style=" display:inline;">*</div></asp:Label><br />
                <asp:DropDownList ID="operationTypeList" runat="server" DataValueField="type" DataTextField="type" OnSelectedIndexChanged="operationTypeListChanged" AutoPostBack="true">
                </asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="operationTypeList" CssClass="required" ErrorMessage="Please select operation."></asp:RequiredFieldValidator>
            </td>
            <td><asp:Label ID="lblnoofparts" runat="server" style="font-weight:700; color:black; text-align:center">Target Qty / hr <div class="required" style=" display:inline;">*</div></asp:Label><br />
                <asp:TextBox ID="noOfPartsTextBox" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="noOfPartsTextBox" CssClass="required" ErrorMessage="Please enter parts."></asp:RequiredFieldValidator>
            </td>
            <td ><asp:Label ID="lblTotalTime" runat="server" style="font-weight:700; color:black; text-align:center">Total Time <div class="required" style=" display:inline;">*</div></asp:Label><br />
                <asp:TextBox ID="timeTextBox" runat="server" Width="50px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="timeTextBox" CssClass="required" ErrorMessage="Please enter time."></asp:RequiredFieldValidator>
            </td>
            <td><asp:Label ID="lblAcceptedQty" runat="server" style="font-weight:700; color:black; text-align:center">OK Quantity <div class="required" style=" display:inline;">*</div></asp:Label><br />
                <asp:TextBox ID="actualQtyTextBox" runat="server" Width="50px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="actualQtyTextBox" CssClass="required" ErrorMessage="Please enter OK quantity."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblProdRejectionQty" runat="server" style="font-weight:700; color:black; text-align:center">Prod Rejection Quantity</asp:Label><br />
               <asp:GridView ID="prodRejHisGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="prodRejHisGrid_RowCommand" 
        OnRowDeleting="prodRejHisGrid_RowDeleting" DataKeyNames="id" ShowFooter="true" CssClass="Table1" Width="30%">
            <Columns>
                 <asp:TemplateField HeaderText="REJECTION CODE">
                    <ItemTemplate>
                        <asp:Label ID="rejectionCodeLbl" Text='<%# Eval("rejection_code") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="rejectionCodeDropDownList" runat="server" DataSourceID="SqlDataSource1" DataTextField="code" DataValueField="code"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [code] FROM [rejection_master]"></asp:SqlDataSource>
                        <asp:Label ID="checkRejectionLbl" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="REJECTION QUANTITY">
                    <ItemTemplate>
                        <asp:Label ID="rejectionQuantityLbl" Text='<%# Eval("rejection_quantity") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtRejQuantityFooter" runat="server"/>
                        <asp:Label ID="checkRejQuantLbl" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to Delete?');"  Height="20px" Width="20px" runat="server"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ImageUrl="~\Images\addnew.png" CommandName="Add" OnClientClick="return validationProdRej()"  Height="20px" Width="20px" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></td>
            <td><asp:Label ID="lblFpaRejectionQty" runat="server" style="font-weight:700; color:black; text-align:center">FPA Rejection Quantity</asp:Label><br />
                <asp:TextBox ID="FpaRejectionQtyTextBox" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="FpaRejectionQtyTextBox_TextChanged"></asp:TextBox></td>
            <td colspan="2"><asp:Label ID="lblWipQty" runat="server" style="font-weight:700; color:black; text-align:center">WIP Quantity <div class="required" style=" display:inline;">*</div></asp:Label><br />
                <asp:TextBox ID="wipQtyTextBox" runat="server" Width="50px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
             <tr>
                 <td colspan="4">
                     <asp:Label ID="lblEfficiency" runat="server" style="font-weight:700; color:black; text-align:center">Efficiency <div class="required" style=" display:inline;">*</div></asp:Label>
                     <br /><asp:TextBox ID="efficiencyTextBox" Width="50px" ReadOnly="true" runat="server"></asp:TextBox>
                 </td>
             </tr>
        </table>
         <br />
        
               <center> 
            <asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click"  CssClass="nextPage" OnClientClick="return validationFinalProdRej()"/>
                    &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClientClick="return confirm('Do you want to cancel?');" OnClick="Cancel_Click" CausesValidation="false" />
               </center>
            </div>
                
    <center>
               
    
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
    </body>
</html>
    </asp:Content>

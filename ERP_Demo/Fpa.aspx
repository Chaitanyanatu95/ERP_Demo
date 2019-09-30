<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fpa.aspx.cs" Inherits="ERP_Demo.Fpa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<html>
<head>
    <style>
        textarea {
            resize: none;
        }
        table{
            margin-left:100px;
            height:100%;
            width:100%;
        }
    </style>
    <title>pbplastics | fpa</title>
    <script type="text/javascript">
        function Warning() {
                confirm("Are you sure?");
            }
        function changeColor() {
            document.getElementById("selectPartNameCell").textContent.changeColor = "green";
        }
            window.onload = function () {
                    onProductionTagDetailsSelected();                 
        }
        function onProductionTagDetailsSelected() {
                var isProductTagDetails = $('#<%=productionTagDropDownList.ClientID %> option:selected').text();
                
                if (isProductTagDetails == "YES") {
                    $("#<%=prodTagDetailsGrid.ClientID %>").show();
                }
                else{
                    $("#<%=prodTagDetailsGrid.ClientID %>").hide();
                }
            }
    </script>
</head>
<body>
     <div id="page">
    <table>
        <tr>
            <td><asp:Label ID="lblOperatorName" runat="server">Operator Name</asp:Label></td>
            <td><asp:Label ID="lblPartName" runat="server">Part Name</asp:Label></td>
            <td><asp:Label ID="lblDate" runat="server">Date</asp:Label></td>
            <td><asp:Label ID="lblOperationType" runat="server">Operation Type</asp:Label></td>
       </tr>
        <tr>
            <td><asp:TextBox ID="operatorNameTextBox" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td><asp:DropDownList ID="partNameDropDownList" DataValueField="part_name" DataTextField="part_name" onselectedindexchanged="partNameChanged" onclick="changeColor()" runat="server" AutoPostBack="true"></asp:DropDownList></td>
            <td><asp:TextBox ID="dateTextBox" runat="server"></asp:TextBox></td>
            <td>
                <asp:DropDownList ID="operationTypeList" runat="server">
                    <asp:ListItem Text="Finishing"></asp:ListItem>
                    <asp:ListItem Text="Packing"></asp:ListItem>
                    <asp:ListItem Text="Assembly"></asp:ListItem>
                </asp:DropDownList>
            </td>
       </tr>
        <tr>
            <td><asp:Label ID="lblTotalQty" runat="server">Total Quantity</asp:Label></td>
            <td><asp:Label ID="lblnoofparts" runat="server">No.of.parts<br/>per hr</asp:Label></td>
            <td><asp:Label ID="lblTotalTime" runat="server">Total Time</asp:Label></td>
            <td><asp:Label ID="lblAcceptedQty" runat="server">Accepted Quantity</asp:Label></td>
       </tr>
        <tr>
            <td><asp:TextBox ID="totalQtyTextBox" runat="server" Width="50px"></asp:TextBox></td>
            <td><asp:TextBox ID="noOfPartsTextBox" runat="server" Width="57px" ></asp:TextBox></td>
            <td><asp:TextBox ID="timeTextBox" runat="server" Width="50px"></asp:TextBox></td>
            <td><asp:TextBox ID="acceptedQtyTextBox" runat="server" Width="50px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblRejectionQty" runat="server">Rejection Quantity</asp:Label></td>
            <td><asp:Label ID="lblWipQty" runat="server">WIP Quantity</asp:Label></td>
            <td><asp:Label ID="lblRejectionCode" runat="server">Rejection Code</asp:Label></td>
            
        </tr>
        <tr>
            <td><asp:TextBox ID="rejectionQtyTextBox" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="rejectionQtyTextBox_TextChanged"></asp:TextBox></td>
            <td><asp:TextBox ID="wipQtyTextBox" runat="server" Width="50px" ReadOnly="true"></asp:TextBox></td>
            <td>
                <!-- Calculate efficiency while generating report on admin side-->
                <asp:DropDownList ID="rejectionCodeList" runat="server">
                    <asp:ListItem Text="Select DropDown Value" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="1"></asp:ListItem>
                    <asp:ListItem Text="2"></asp:ListItem>
                    <asp:ListItem Text="3"></asp:ListItem>
                </asp:DropDownList></td>
            <td></tr>
        <tr>
            <td><asp:Label ID="lblProductionTag" runat="server">Production Tag</asp:Label></td>
            </tr>
        </table>
        <asp:Table runat="server" CssClass="Table1" Width="20%">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ID="selectPartNameCell" runat="server" ColumnSpan="2" ForeColor="#0099ff" Font-Bold="true"><u>Note:- Please Select Part Name First</u></asp:TableCell>
           </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server" HorizontalAlign="Center">PRODUCTION TAG (REQ)</asp:TableCell>
            <asp:TableCell runat="server" ColumnSpan="3"><asp:DropDownList ID="productionTagDropDownList" runat="server" onchange="onProductionTagDetailsSelected()">
                    <asp:ListItem Value="NO">NO</asp:ListItem>
                    <asp:ListItem Value="YES">YES</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
   </asp:Table>

<asp:GridView ID="prodTagDetailsGrid" runat="server" AutoGenerateColumns="false" OnRowCommand="prodTagDetailsGrid_RowCommand" 
        OnRowDeleting="prodTagDetailsGrid_RowDeleting" DataKeyNames="id" ShowFooter="true" CssClass="Table1" Width="30%">
            <Columns>
                 <asp:TemplateField HeaderText="EMP NO">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("emp_no") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtEmpNoFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DATE">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("date") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDateFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SHIFT">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("shift") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtShiftFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("qty") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:TextBox ID="txtQtyFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~\Images\delete.png" CommandName="Delete" OnClientClick="Warning();" Height="20px" Width="20px" runat="server"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ImageUrl="~\Images\addnew.png" CommandName="Add" Height="20px" Width="20px" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView><br />
               <center> 
            <asp:Button runat="server" Text="SAVE" OnClick="SaveBtn_Click"  CssClass="nextPage"/>
                    &nbsp;&nbsp;&nbsp; <asp:Button Text="CANCEL" runat="server" CssClass="nextPage" OnClick="Cancel_Click" CausesValidation="false" />
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

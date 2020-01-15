<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" Inherits="ERP_Demo.reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <html>
        <head>
            <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css">
            <script type="text/javascript" src = "https://code.jquery.com/jquery-1.10.2.js"></script>
            <script type="text/javascript" src = "https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
            <script type="text/javascript">
                $( function() {
                    $("#<%=dateSelectionTextBox.ClientID %>").datepicker({ showAnim: "fold", dateFormat: "dd-mm-yy" });
                    $("#<%=dateSelectionTextBox2.ClientID %>").datepicker({ showAnim: "fold", dateFormat: "dd-mm-yy" });
                });
             </script>
            <title runat="server">pb plastics | Reports</title>
        </head>
        <body>
            <table runat="server" class="tableClass" style="width:50%; height:70%">
                <tr>
                    <th runat="server" style="background-color:skyblue;text-align:center" colspan="7"><h3>REPORTS</h3></th>
                </tr>
                <tr>
                    <td style="text-align:center;font-weight:700; color:black;"><asp:Label ID="workerNameLabel" runat="server" style="">WORKER NAME</asp:Label></td>
                    <td style="text-align:center;font-weight:700; color:black;"><asp:Label ID="shiftNameLabel" runat="server">SHIFT NAME</asp:Label></td>
                    <td style="text-align:center;font-weight:700; color:black;"><asp:Label ID="dateSelectLabel" runat="server">DATE FROM</asp:Label></td>
                    <td style="text-align:center;font-weight:700; color:black;"><asp:Label ID="dateSelectLabel2" runat="server">DATE TO</asp:Label></td>
                    <td style="text-align:center;font-weight:700; color:black;"><asp:Label ID="partNameLabel" runat="server">PART NAME</asp:Label></td>
                    <td rowspan="2" style="text-align:center;"> 
                        <asp:Button ID="generateReportsBtn" runat="server" OnClick="generateReportsBtn_Click" Text="GENERATE REPORT"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;"><asp:DropDownList ID="workerNameDropDownList" runat="server" DataTextField="worker_name" DataValueField="worker_name"></asp:DropDownList></td>
                    <td style="text-align:center;"><asp:DropDownList ID="shiftDropDownList" runat="server" DataTextField="shift_time" DataValueField="shift_time"></asp:DropDownList></td>
                    <td style="text-align:center;"><asp:TextBox ID="dateSelectionTextBox" runat="server"></asp:TextBox></td>
                    <td style="text-align:center;"><asp:TextBox ID="dateSelectionTextBox2" runat="server"></asp:TextBox></td>
                    <td  style="text-align:center;"><asp:DropDownList ID="partNameDropDownList" runat="server" DataTextField="part_name" DataValueField="part_name"></asp:DropDownList></td>
                </tr>
                <tr>  
                <td colspan="6">  
                    <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
                </td>  
            </tr>
            </table> 
            <center>
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
        </center>
        </body>
    </html>
    </asp:Content>

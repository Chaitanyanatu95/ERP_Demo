<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="newAssembleMaster.aspx.cs" Inherits="ERP_Demo.newAssembleMaster" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>
<html>
<head>
    <title></title>
</head>
<body>
    <table runat="server" class="tableClass">
        <tr>
            <td>ASSEMBLY NAME:</td> 
            <td><asp:TextBox ID="assemblePart" runat="server"></asp:TextBox></td> 
        </tr>
        <tr>
            <td>UOM:</td>
            <td><asp:DropDownList ID="uomDropDownList" runat="server" DataTextField="unit_of_measurement" DataValueField="unit_of_measurement"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>NO OF CHILD PARTS:</td>
            <td><asp:TextBox ID="countTextBox" runat="server" Width="70px"></asp:TextBox>
            <asp:Button ID="countbtn" OnClick="countbtn_Click" runat="server" Text="ASSEMBLE"/></td>
        </tr>
        </table>
        <table id="secondTable" runat="server" class="tableClass">
            <tr>
                <td style="border:0;">
                    <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <tr>
                        <th style="text-align:center">PART NAME</th>
                        <th style="text-align:center"> QTY</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                               <%#Container.ItemIndex+1 %>
                                <asp:DropDownList ID="DropDownListField" DataTextField="part_name" DataValueField="part_name" DataSourceID="SqlDataSource1" runat="server" />
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT [part_name] FROM [parts_master]">
                                </asp:SqlDataSource>
                           </td>
                            <td>
                                <asp:TextBox ID="qtyTextBox" runat="server" Height="21px" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <SeparatorTemplate>
                    </SeparatorTemplate>
                    </asp:Repeater>
                </td>
                <td><asp:Button ID="calculateBtn" runat="server" Text="CALCULATE WT" OnClick="calculateBtn_Click"/></td>
                 <td colspan="2">
                            TOTAL ASSEMBLY WT. <asp:TextBox ID="assWt" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>
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
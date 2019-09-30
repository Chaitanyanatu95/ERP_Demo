<%@ page language="c#" autoeventwireup="true" codebehind="Login.aspx.cs" inherits="ERP_Demo.Login" %>
<!doctype html>
    <html>
        <head>
            <title>pbplastics | login</title>
                <style>
                    div.container4 {
                        height: 12em;
                        position: relative;
                    }
                    div.container4 table {
                        margin: 0;
                        background: lightgray;
                        position: absolute;
                        font-size:medium;
                        top: 60%;
                        left: 50%;
                        width: 25%;
                        margin-right: -50%;
                        transform: translate(-50%, -50%) 
                    }
                    div.container4 table td{
                        text-align:center;
                        padding-bottom:10px;
                        padding-left:0px;
                    }
                </style>
        </head>
            <body>
                <div style="text-align: center; background-color: aliceblue; height:90px;">
                   <img src="~/images/logoc.png" alt="some text" width=60 height=60 runat="server">
                    <h1 style="display:inline;"> Plastic Injection Molding </h1></div>
                <div class=container4>
                   <form id="form1" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">log in</td>
                            </tr>
                            <tr>
                                <td ><asp:label id="usernamelabel" runat="server" text="user name:"></asp:label></td>
                                <td ><asp:textbox id="userlabel" runat="server"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td ><asp:label id="passwordlabel" runat="server" text="password:"></asp:label></td>
                                <td ><asp:textbox id="passlabel" runat="server" textmode="password"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:label id="errlabel" runat="server" text="invalid user credentials" forecolor="red"></asp:label></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:button id="loginbutton" runat="server" text="login" onclick="loginbutton_click1" /></td>
                            </tr>
                         </table>
                    </form>
                </div>
            </body>
    </html>
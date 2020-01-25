<%@ page language="c#" autoeventwireup="true" codebehind="Login.aspx.cs" inherits="ERP_Demo.Login" %>
<!doctype html>
    <html>
        <head>
            <title>pbplastics | login</title>
                <style>
                    div.container4 {
                        height: 19em;
                        position: relative;
                    }
                    div.container4 table {
                        margin: 0;
                        background: lightgray;
                        position: absolute;
                        font-size: medium;
                        top: 60%;
                        left: 50%;
                        width: 25%;
                        margin-right: -50%;
                        transform: translate(-50%, -50%) 
                    }
                    div.container4 table td{
                        text-align:center;
                        font-weight:500;
                        font-size:16px;
                        padding-bottom:10px;
                        padding-left:0px;
                    }
                    div.container4 table td #loginbutton{
                        border-color: #2196F3;
                        color: dodgerblue;
                    }
                    div.container4 table td #loginbutton:hover{
                        background: #2196F3;
                        color: white;
                    }
                    .main{
                       display: flex;
                       height:100px;
                       position: relative;
                       background-color:aliceblue;
                    }
                    .btn {
                        border: 1px solid black;
                        background-color: white;
                        color: black;
                        padding: 3px 7px;
                        font-size: 16px;
                        cursor: pointer;
                    }
                </style>
        </head>
            <body style="font-family:Raleway;">
                <div class="main" >
                    <img src="~/Images/logoc.png" alt="Plastic Injection Molding" height="90" runat="server" style="                            display: inline-block;
                            text-align: center;
                            margin-left: auto;
                            margin-right: 0
                    " />
                    <h1 style="display:inline; margin-left:0; margin-right:auto; margin-top:48px;">PB PLASTICS ERP</h1>
                    </div>
                <div class=container4>
                   <form id="form1" runat="server">
                        <table>
                            <tr>
                                <td colspan="2"><u>LOG IN</u></td>
                            </tr>
                            <tr>
                                <td ><asp:label id="usernamelabel" runat="server" text="USERNAME:"></asp:label></td>
                                <td ><asp:textbox id="userlabel" runat="server"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td ><asp:label id="passwordlabel" runat="server" text="PASSWORD:"></asp:label></td>
                                <td ><asp:textbox id="passlabel" runat="server" textmode="password"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:label id="errlabel" runat="server" text="Invalid user credentials" forecolor="red"></asp:label></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:button CssClass="btn" id="loginbutton" runat="server" text="Login" onclick="loginbutton_click1" /></td>
                            </tr>
                         </table>
                    </form>
                </div>
                 <center>
                <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
                <br />
                <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
                </center>
            </body>
    </html>
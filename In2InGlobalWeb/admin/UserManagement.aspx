﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.UserManagement" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="vendor/jquery/jquery.min.js"></script>
    <script type="text/javascript">
</script>

     <link href='https://fonts.googleapis.com/css?family=Work+Sans:300,400,600&Inconsolata:400,700' rel='stylesheet' type='text/css' />
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/gridview.css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
</head>
<body>   
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 20px;">
             <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px;">User Management</div>
                <table style="width: 100%; background-color: azure;">
                    <tr>
                        <td style="width: 80%;">
                            <center>
                                <div style="width: 50%; border: 1px solid black; border-radius: 5px;margin-top:30px;">
                                    <table>
                                        <tr>
                                            <td>First Name</td>
                                            <td>
                                                <input type="text" id="txtFName" runat="server" value="" /></td>
                                            <td>Last Name</td>
                                            <td>
                                                <input type="text" id="txtLName" runat="server" value="" /></td>
                                        </tr>
                                        <tr>
                                            <td>Company Name</td>
                                            <td>                                                
                                                <asp:DropDownList ID="ddlCompanyName" Width="100%" runat="server" DataTextField="Company Name"></asp:DropDownList>
                                            </td>
                                            <td>Role Name</td>
                                            <td>
                                                 <asp:DropDownList ID="ddlRoleName" Width="100%" runat="server" DataTextField="RoleName"></asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Email<br />
                                                <span style="font-size: xx-small">( user name )</span></td>
                                            <td>
                                                <input type="text" id="txtEmail" runat="server" value="" /></td>
                                            <td>Password</td>
                                            <td>
                                                <input type="password" id="txtPassword" autocomplete="off" runat="server" value="" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div style="margin-top: 30px;">
                                                    <center>
                                                        <input type="button" class="button" value="Save" onclick="SaveUser();" />
                                                        <input type="button" class="button" style="margin-left: 10px;" value="Cancel" onclick="ClearAll();" />
                                                    </center>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                            <div style="width: 50%; border: 1px solid black; border-radius: 5px;margin-top:20px;margin-bottom:20px;">
                                <asp:GridView ID="grdUsers" runat="server" OnPageIndexChanging="grdUsers_PageIndexChanging" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                    AllowPaging="True" PageSize="4">
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                                </center>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
     <script src="js/jquery.js"></script>
    <script src="js/jquery.nice-select.min.js"></script>
    <script src="js/fastclick.js"></script>
    <script src="js/prism.js"></script>

    <script>
        $(document).ready(function () {
            $('select:not(.ignore)').niceSelect();
            FastClick.attach(document.body);            
        });
        function SaveUser() {

           alert("A new user will be added using provided information.")
        }
        function ClearAll() {

            $('#txtFName').val('');
            $('#txtLName').val('');                        
            $('#txtEmail').val('');            
            $('input[type="password"]#txtPawword').val('');
        }
    </script>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-64633646-1', 'auto');
        ga('send', 'pageview');
       
    </script>
     <style type="text/css">
        body {
            background-color: azure;
        }
    </style>
</body>
</html>

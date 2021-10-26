﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InternalLanding.aspx.cs" Inherits="In2InGlobal.presentation.admin.InternalLanding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>document.getElementsByTagName("html")[0].className += " js";</script>
    <link rel="stylesheet" href="admin/assets/css/style.css" />

</head>
<body onload="loadIframe();">
    <form status="1" id="form1" runat="server">
        <header class="cd-main-header js-cd-main-header" style="background-color: #03989e;height:83px;">
            <!--
            <div class="rounded-circle" style="cursor:pointer;">
                
                    <img alt="Logged In.." src="../images/in2ingloballogo.png" />
                    <span id="spnUserName" style="color:white;text-transform:uppercase;font-size:medium;vertical-align:text-top;" runat="server"></span>
                
            </div>-->
            <div style="position:fixed;left:10px;top:10px;color:yellow;"><img src="../images/in2ingloballogo.png" style="width:40%;" /></div>
            <ul class="cd-nav__list js-cd-nav__list">

                <li class="cd-nav__item cd-nav__item--has-children cd-nav__item--account js-cd-item--has-children">
                    <div class="rounded-circle" style="cursor: pointer;">
                        <img alt="Logged In.." src="admin/assets/img/loggedInAvatar.jpg" />
                        <a style="margin-left:-10px;" href="admin/login.aspx">Logout</a>
                    </div>
                    <%--<ul class="cd-nav__sub-list" style="padding-top:-10px;">
                        <li class="cd-nav__sub-item"><a href="#" onclick="javascript:OpenPage('admin/MyProfile.aspx');">My Profile</a></li>
                        <li class="cd-nav__sub-item"><a href="admin/login.aspx">Logout</a></li>
                    </ul>--%>
                </li>
            </ul>
        </header>
        <!-- .cd-main-header -->

        <main class="cd-main-content" style="100%;">
            <nav class="cd-side-nav js-cd-side-nav" style="padding-top: 70px;">
                <ul class="cd-side__list js-cd-side__list">
                    <!-- <li class="cd-side__label"><span>Main</span></li>-->
                    <li class="cd-side__item cd-side__item--has-children cd-side__item--overview js-cd-item--has-children">
                        <a href="" onclick="javascript:OpenPage('admin/MyProfile.aspx');">My Profile</a>
                    </li>

                    <li class="cd-side__item cd-side__item--has-children cd-side__item--comments js-cd-item--has-children">
                        <a href="#" id="ancFileMan" runat="server">File Management</a>
                    </li>

                    <li class="cd-side__item cd-side__item--has-children cd-side__item--comments js-cd-item--has-children">
                        <a href="#" id="ancAnalytics" runat="server">Analytics</a>
                    </li>
                     <li class="cd-side__item cd-side__item--has-children cd-side__item--comments js-cd-item--has-children">
                        <a href="#" id="usrMngmnt" runat="server">User Management</a>
                    </li>
                </ul>
            </nav>

            <div style="margin-top:50px;">               
                <iframe style="width: 100%; height: 84.8%" id="frmTarget" src=""></iframe>
                <div style="position: sticky; margin-left: auto;">
                    <div style="background: #212121;padding: 30px 0;">
                    <center>
                        <footer style="font-size: small;color:#96a1b5"><i>Copyright © In2In Global 2021</i></footer>
                    </center>
                        </div>
                </div>
            </div>
            
            <!-- .content-wrapper -->
        </main>
        <!-- .cd-main-content -->
        <script src="admin/assets/js/util.js"></script>
        <!-- util functions included in the CodyHouse framework -->
        <script src="admin/assets/js/menu-aim.js"></script>
        <script src="admin/assets/js/main.js"></script>
        <script src="jquery/jquery-1.7.1.js"></script>
        <script type="text/javascript">
            function OpenPage(page) {
                var iframe = $("#frmTarget");
                iframe.attr("src", page);
            }
            function loadIframe() {

                const urlParams = new URLSearchParams(window.location.search);
                const myParam = urlParams.get('target');
                var uri = "admin/MyProfile.aspx?target=" + myParam;
                OpenPage(uri);
            }
        </script>
    </form>
</body>
</html>

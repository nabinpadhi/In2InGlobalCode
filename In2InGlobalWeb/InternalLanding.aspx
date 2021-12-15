<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InternalLanding.aspx.cs" Inherits="In2InGlobal.presentation.admin.InternalLanding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>document.getElementsByTagName("html")[0].className += " js";</script>
    <link rel="stylesheet" href="admin/assets/css/style.css" />
    <link rel="stylesheet" href="css/style.css" />
      <link rel="stylesheet" type="text/css" href="NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="NewJEasyUI/themes/icon.css" />
    <style type="text/css">
        
        ::-webkit-scrollbar {

            width: 12px;
        }

        ::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
            border-radius: 10px;
        }

        ::-webkit-scrollbar-thumb {
            border-radius: 10px;
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5);
        }

        div.foo:hover::-webkit-scrollbar {
            background: lightblue;
             border-radius: 10px;
        }
    </style>
</head>
<body onload="loadIframe();">
    <form status="1" id="form1" runat="server">
        <div class="new-container">
        <header class="cd-main-header js-cd-main-header" style="background-color: #03989e;height:83px;">           
            <div style="position:fixed;left:10px;top:10px;color:yellow;"><img src="../images/in2ingloballogo.png" style="width:40%;" /></div>
            <ul class="cd-nav__list js-cd-nav__list">

                <li class="cd-nav__item cd-nav__item--has-children cd-nav__item--account js-cd-item--has-children">
                    <div class="rounded-circle">
                         <div style="position: sticky; margin-left: auto;">                             
                        <img style="margin-left:15px;" alt="Logged In.." src="admin/assets/img/loggedInAvatar.jpg" /></div>
                        <a href="admin/login.aspx"><span>Logout</span></a>
                    </div>                   
                </li>
            </ul>
        </header>
       
        <main class="cd-main-content" style="width:100%; position:fixed;left:0px;top:10px;">
            <nav class="cd-side-nav js-cd-side-nav" style="padding-top: 70px;height:100%;">
                <ul class="cd-side__list js-cd-side__list">                    
                    <li class="cd-side__item cd-side__item--has-children cd-side__item--user js-cd-item--has-children">
                        <a href="#" onclick="javascript:OpenPage('admin/MyProfile.aspx');">My Profile</a>
                    </li>
                     <li class="cd-side__item cd-side__item--has-children cd-side__item--overview js-cd-item--has-children">
                    <a href="#" id="comMngmnt" runat="server">Company Management</a>
                    </li>
                    
                    <li class="cd-side__item cd-side__item--has-children cd-side__item--overview js-cd-item--has-children">
                         <a href="#" id="usrMngmnt" runat="server">User Management</a>                       
                    </li>                      
                    <li class="cd-side__item cd-side__item--has-children cd-side__item--comments js-cd-item--has-children" style="display:none;">
                        <a href="#" id="projtMngmt" runat="server">Project Management</a>
                    </li>
                     <li class="cd-side__item cd-side__item--has-children cd-side__item--overview js-cd-item--has-children">
                    <a href="#" id="tmpltMngmnt" runat="server">Template Management</a>
                    </li>
                  <li class="cd-side__item cd-side__item--has-children cd-side__item--comments js-cd-item--has-children">
                        <a href="#" id="ancFileMan" runat="server">File Management</a>
                    </li>
                    <li runat="server" id="liAnalytics" class="cd-side__item cd-side__item--has-children cd-side__item--comments js-cd-item--has-children">
                        <a href="#" id="ancAnalytics" runat="server">Analytics</a>
                        <div runat="server" id="divConfiguration" class="cd-side__item" style="margin-left: 25px; padding-top: 4px;padding-bottom: 4px;"><a href="#" id="ancConfiguration" style="font-size:small;color:aqua;" runat="server">Configuration</a></div>
                        <div class="foo" style="margin-left: 30px; padding-top: 4px;height:150px;width:120px;overflow-y:auto;overflow-x:hidden;">
                            <ul class="js-cd-side__list" runat="server" id="AnalyticsProjectList">
                            </ul>
                        </div>
                    </li>
                </ul>
            </nav>
           
            <div class="mainPageDiv" style="padding-top:20px;bottom:0px; background-color:azure;">  
                 <center>
                    <div class="csvPageDivParent" style="display:none; text-align:left; border: 1px solid black;border-radius:5px; width:47.5%;height:450px;margin-bottom:30px;background-color:silver;">                
                        <div class="panel-header panel-header-noborder window-header" style="width:100%;"><div class="panel-title panel-with-icon" style="">CSV Data Viewer</div><div class="panel-icon icon-readfile"></div><div class="panel-tool"><a class="panel-tool-close" href="#"></a></div></div>
                        <iframe style="width: 99.9%;height:93%;" id="frmCSVPage" src="about:blank"></iframe>
                    </div>
                 </center>
                  <center>
                    <div class="zohoPageDivParent" style="display:none; text-align:left; border: 1px solid black;border-radius:5px; width:99%;height:530px;margin-left:5px;margin-right:5px; margin-bottom:30px;background-color:silver;">                
                        <div class="panel-header panel-header-noborder window-header" style="width:100%;"><div class="panel-title panel-with-icon" style="">Analytics Data Viewer</div><div class="panel-icon icon-readfile"></div><div class="panel-tool"><a class="panel-tool-close" href="#"></a></div></div>
                        <iframe style="width: 99.9%;height:93%;" id="frmZohoPage" src="about:blank"></iframe>
                    </div>
                 </center>
                <iframe style="width: 100%; height: 84.8%" id="frmTarget" src="about:blank"></iframe>
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
    </div>
      
            </form>
     <!-- .cd-main-content -->
        <script src="admin/assets/js/util.js"></script>
        <!-- util functions included in the CodyHouse framework -->
        <script src="admin/assets/js/menu-aim.js"></script>
        <script src="admin/assets/js/main.js"></script>
        <script src="jquery/jquery-1.7.1.js"></script>
     <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" lang="javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('.csvPageDivParent').hide();
                $('.panel-tool-close').click(function () {

                    var iframe = $("#frmCSVPage");
                    iframe.attr("src", "about:blank"); 
                    $('.csvPageDivParent').hide();
                    $('.zohoPageDivParent').hide();
                    $("#frmTarget").show();
                });
            });
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
            function ShowDiv(fn) {

                var iframe = $("#frmCSVPage");
                iframe.attr("src", "admin/DisplayCSV.aspx?csvfp=" + fn); 
                $('.csvPageDivParent').show();               
                $("#frmTarget").hide();

            }
            function ShowZohoAnalytics(pageLink) {

                var iframe = $("#frmZohoPage");
                iframe.attr("src", pageLink);
                $('.zohoPageDivParent').show();
                $("#frmTarget").hide();

            }
        </script>     
</body>
</html>

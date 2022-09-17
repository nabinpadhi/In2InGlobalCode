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

        .holds-the-iframe {
            /*background: url(admin/img/load-indicator.gif) center center no-repeat;*/
        }
    </style>
</head>
<body onload="loadIframe();">

    <form status="1" id="form1" runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="pdnlFileMgnt" runat="server">
            <ContentTemplate>
                 <asp:Button runat="server" ID="btnRefAnalytics" Text="" Style="display: none;" OnClientClick="UpdateURL();" OnClick="btnRefAnalytics_Click" />

                <div class="new-container">
                    <div class="NavViewer" style="border-radius: 3px; position: fixed; left: 0px; top: 80px; width: 30px; height: 20px; font-weight: bolder; color: brown; z-index: 99999; cursor: pointer; text-align: center;">
                        <img src="images/op-menu.png" style="width: 20px; left: -5px; position: relative;" />
                    </div>
                    <main class="cd-main-content" style="width: 100%; position: fixed; left: 0px; top: 0px;">
                        <img id="navOverlayImg" style="display: none; z-index: 9999; width:150px;height:auto; position: fixed; top: 200px; left: 550px;" src="admin/img/processingNew.gif" />
                        <div id="navOverlay" style="display: none; z-index: 9991; padding-top: 0px; width: 100%; background-color:rgba(1,1,1,0.3); height: 100%; position: fixed; left: 0px; top: 0px;">
                        </div>

                        <nav class="cd-side-nav js-cd-side-nav" style="width: 220px; position: fixed; top: 0px; height: 100%;">

                            <div style="position: fixed; left: 40px; top: 2px; color: yellow;">
                                <img src="images/in2ingloballogo.png" style="width: 27%;">
                            </div>

                            <table style="text-indent: 10px; color: white; font-weight: 600; font-family: sans-serif; text-transform: capitalize; font-size: 13px; position: fixed; top: 120px; left: -20px;">
                                <tr>
                                    <td rowspan="2">
                                        <center>
                                            <div class="rounded-circle">
                                                <div>
                                                    <img style="margin-left: 30px;" alt="Logged In.." src="admin/assets/img/loggedInAvatar.jpg" />
                                                </div>
                                            </div>
                                    </td>
                                    <td>Welcome</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700; font-size: 14px; vertical-align: text-top;"><span id="spnMyName" runat="server"></span></td>
                                </tr>
                            </table>

                            <ul class="cd-side__list js-cd-side__list" style="padding-top: 220px;">
                                <li class="cd-side__item cd-side__item--has-children cd-side__item--users js-cd-item--has-children">
                                    <a href="#" runat="server" id="ancMyProfile">My Profile</a>
                                </li>
                                <li class="cd-side__item cd-side__item--has-children cd-side__item--companymgnt js-cd-item--has-children">
                                    <a href="#" id="comMngmnt" runat="server">Company Management</a>
                                </li>

                                <li class="cd-side__item cd-side__item--has-children cd-side__item--usermgnt js-cd-item--has-children">
                                    <a href="#" id="usrMngmnt" runat="server">User Management</a>
                                </li>
                                <li class="cd-side__item cd-side__item--has-children cd-side__item--comments js-cd-item--has-children" style="display: none;">
                                    <a href="#" id="projtMngmt" runat="server">Project Management</a>
                                </li>
                                <li class="cd-side__item cd-side__item--has-children cd-side__item--template js-cd-item--has-children">
                                    <a href="#" id="tmpltMngmnt" runat="server">Template Management</a>
                                </li>
                                <li class="cd-side__item cd-side__item--has-children cd-side__item--file js-cd-item--has-children">
                                    <a href="#" id="ancFileMan" runat="server">File Management</a>
                                </li>
                                <li runat="server" id="liAnalytics" class="cd-side__item cd-side__item--analytics cd-side__item--has-children js-cd-item--has-children">
                                   
                                    <a href="#" id="ancAnalytics" runat="server" style="cursor: context-menu;">Analytics</a>
                                    <div runat="server" id="divConfiguration" class="cd-side__item cd-side__item--configuration" style="margin-left: 25px; padding-top: 4px; padding-bottom: 4px;"><a href="#" id="ancConfiguration" style="font-size: 10px; color: aqua;" runat="server">Configuration</a></div>
                                    <div class="foo" style="margin-left: 30px; padding-top: 4px; height: 185px; width: auto; overflow-y: auto; overflow-x: hidden;">
                                        <ul class="js-cd-side__list" runat="server" id="AnalyticsProjectList">
                                        </ul>
                                    </div>
                                </li>
                                <li>
                                    <div class="rounded-circle">
                                        <div style="position: fixed; bottom: 20px; left: 50px;">
                                            <a href="admin/login.aspx" style="position: relative; margin-left: 23px; top: -15px; vertical-align: text-top; font-family: sans-serif; font-size: 14px;"><span style="vertical-align: super;">Logout</span>&nbsp;&nbsp;<img style="height: 20px; width: 20px;" src="img/icons/logout-icon.png" /></a>
                                        </div>
                                    </div>
                                </li>
                            </ul>

                        </nav>

                        <div class="mainPageDiv" style="padding-top: 0px; bottom: 0px;">
                            <center>
                                <div class="csvPageDivParent" style="display: none; text-align: left; border: 1px solid black; border-radius: 5px; position: fixed; left: 5px; top: 5px; background-color: silver;">
                                    <div class="panel-header panel-header-noborder window-header" style="width: 100%;">
                                        <div class="panel-title panel-with-icon" style="">CSV Data Viewer</div>
                                        <div class="panel-icon icon-readfile"></div>
                                        <div class="panel-tool"><a class="panel-tool-close" href="#"></a></div>
                                    </div>
                                    <div class="holds-the-iframe" style="width: 99.9%; height: 100%; top: 20px;">
                                        <iframe id="frmCSVPage" style="width: inherit; padding: 30px; height: 100%; overflow: hidden;" src="about:blank"></iframe>
                                    </div>
                                </div>
                            </center>

                            <center>
                                <div class="zohoPageDivParent" style="display: none; text-align: left; border: 1px solid black; border-radius: 5px; width: 99%; height: 98%; position: fixed; left: 5px; top: 0px; background-color: silver;">
                                    <div class="panel-header panel-header-noborder window-header" style="width: 100%;">
                                        <div class="panel-title panel-with-icon" style="">Analytics Data Viewer</div>
                                        <div class="panel-icon icon-readfile"></div>
                                        <div class="panel-tool"><a class="panel-tool-close" href="#"></a></div>
                                    </div>
                                    <div class="holds-the-iframe" style="width: 99.9%; height: 95%; top: 20px;">
                                        <iframe style="width: 99.7%; margin: 3px; height: 99%;" id="frmZohoPage" src="about:blank"></iframe>
                                    </div>

                                </div>
                            </center>
                            <div class="holds-the-iframe" id="frmTargetHF" style="height: 99.9%; top: 3px;">
                                <iframe style="width: inherit; height: inherit; border: 1px solid gray;" id="frmTarget" src="about:blank"></iframe>
                            </div>

                            <center>
                                <div class="exceptionDivParent" style="display: none; text-align: left; border: 1px solid black; border-radius: 5px; width: 400px; height: 300px; margin-top: 100px; margin-left: 300px; background-color: silver;">
                                    <div class="panel-header panel-header-noborder window-header" style="width: 100%;">
                                        <div class="panel-title panel-with-icon" style="">Error While Processing Request</div>
                                        <div class="panel-icon icon-no"></div>
                                        <div class="panel-tool"><a class="panel-tool-close" href="#"></a></div>
                                    </div>
                                    <div style="width: inherit; height: inherit; text-align: center; position: relative;">
                                        <img style="margin-top: 30px;" src="img/ohNo.png" /><br />
                                        <span style="color: red; font-size: medium; font-weight: bold"><i>Oh No!</i> Something has gone wrong</span>
                                    </div>
                                </div>
                            </center>

                        </div>
                    </main>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    <!-- .cd-main-content -->
    <script src="admin/assets/js/util.js"></script>
    <!-- util functions included in the CodyHouse framework -->
    <script src="admin/assets/js/menu-aim.js"></script>
    <script src="admin/assets/js/main.js"></script>
    <script src="jquery/jquery-1.7.1.js"></script>
    <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" lang="javascript"></script>
     <script src="admin/js/fastclick.js" type="text/javascript" lang="javascript"></script>
    <script src="admin/js/prism.js" type="text/javascript" lang="javascript"></script>
    <script type="text/javascript">
        var width = $(window).width();
        var height = $(window).height();
        var navbarWidth = $('.cd-side-nav').width() - 10;
        var activeMenu = null;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        $(document).ready(function () {
           
            DoManualReadyDocument();
            window.parent.$('#navOverlayImg').hide();
            window.parent.$('#navOverlay').hide();

        });
        function InitializeRequest(sender, args) {
            window.parent.$('#navOverlayImg').show();
            window.parent.$('#navOverlay').show();

        }

        function EndRequest(sender, args) {
            window.parent.$('#navOverlayImg').hide();
            window.parent.$('#navOverlay').hide();
        }
        function DoManualReadyDocument() {
            var gridcol = $("#frmTarget").contents().find('.AspNet-GridView.specifyCol');//.css('width',520);
            gridcol.css('width', 520);
            $('.NavViewer').css("top", height / 2);
            $('.NavViewer').hide();
            $('.footerDiv').css('width', width)
            $('.NavViewer').click(function () {
                $('.cd-side-nav').show();
                $('.NavViewer').hide();

                $('.panel-tool-close').click();

                $('.cd-side-nav').css('top:0px;');


                $('.zohoPageDivParent').css("width", width - 180);
                $('.csvPageDivParent').css("width", width / 2);

                $('#frmCSVPage').css("width", (width / 2) - 2);
                $('#frmCSVPage').css("height", "inherit");
                $('.holds-the-iframe').css("width", width - 230);
                $('.holds-the-iframe').css("left", 210);
                $('#frmTarget').css("width", width - 230);
                $('#frmTarget').css("height", height - 10);

                $('.cd-main-content').css("top", "30px");

                $('ul.cd-side__list a').css('color', 'rgb(255, 255, 255)');
                $(activeMenu).css('color', 'rgb(227, 221, 61)');
            });

            $('.cd-side-nav a').click(function () {
                if ($(this).attr('id') != 'ancAnalytics') {
                    $('.cd-side-nav').hide();
                    $('.NavViewer').show();
                    $('#navOverlayImg').show();
                    $('#navOverlay').show();
                    $('.holds-the-iframe').css("position", "fixed");
                    /*$('.holds-the-iframe').css("top", "3px");*/
                    $('.holds-the-iframe').css("left", "5px");
                    $('.holds-the-iframe').css("width", width - 20);
                    $('#frmTarget').css("width", width - 30);
                    $('#frmTarget').css("height", height - 10);
                    $('#frmTarget').css("top", 20);
                    $('#frmTarget').css("left", 20);
                    $('#frmTarget').css("margin-left", "15px");

                    $('.cd-main-content').css("top", "0px");

                    $('.zohoPageDivParent').css("width", width - 20);
                    $('.csvPageDivParent').css("width", width - 10);
                    $('.csvPageDivParent').css("height", height - 10);

                    $('#frmCSVPage').css("width", width - 12);
                    $('#frmCSVPage').css("height", height - 40);
                    $('#frmCSVPage').css("over5flow", "hidden");



                }
            });

            $('.csvPageDivParent').hide();
            $('.panel-tool-close').click(function () {

                var iframe = $("#frmCSVPage");
                iframe.attr("src", "about:blank");
                $('.csvPageDivParent').hide();
                $('.zohoPageDivParent').hide();
                $('.exceptionDivParent').hide();
                $('#frmTargetHF').show();
                $("#frmTarget").show();
            });
            $('#frmCSVPage').on('load', function () {
                $('#navOverlayImg').hide();
                $('#navOverlay').hide();
            })
            $('.cd-side-nav a').click();
        }
        function OpenPage(page, org) {

            var iframe = $("#frmTarget");
            iframe.attr("src", page);
            activeMenu = org;
            $('#navOverlayImg').hide();
            $('#navOverlay').hide();
        }
        $('#frmTarget').on('load', function () {
            $('#navOverlayImg').hide();
            $('#navOverlay').hide();
        });
        function loadIframe() {

            const urlParams = new URLSearchParams(window.location.search);
            const myParam = urlParams.get('target');
            var uri = "admin/MyProfile.aspx";
            OpenPage(uri, $('#ancMyProfile'));
        }
        function ShowDiv(fn) {

            var iframe = $("#frmCSVPage");
            iframe.attr("src", "admin/DisplayCSV.aspx?csvfp=" + fn);
            //$('.cd-side-nav a').click();
            $('#ancFileMan').click();
            $('.csvPageDivParent').show();
            $("#frmTarget").hide();
            $('#navOverlayImg').hide();
            $('#frmTargetHF').hide();

        }
        function ShowZohoAnalytics(pageLink) {

            $("#frmTarget").hide();
            $('#navOverlay').hide();
            $('#navOverlayImg').hide();
            $('#frmTargetHF').hide();
            $('.cd-side-nav a').click();
            var iframe = $("#frmZohoPage");
            iframe.attr("src", pageLink);
            $('.zohoPageDivParent').show();


        }
        function HideZohoAnalytics() {

            $("#frmTarget").show();
            $('#navOverlay').hide();
            $('#navOverlayImg').hide();
            $('.cd-side-nav a').click();
            $('.zohoPageDivParent').hide();
        }
        function ShowException() {
            $('.exceptionDivParent').css("margin-left", (width / 2) - 200);
            $('.exceptionDivParent').show();
            $("#frmTargetHF").hide();
        }
        //used to refresh the Analytics project list after file uploaded successfully.
        function RefreshAnalytics() {
            var iframe = $('#frmTarget');
            var gridcol = iframe.contents().find('.AspNet-GridView.specifyCol');
            gridcol.css('width', 520);            
            DoManualReadyDocument();
            $('#ancFileMan').trigger("click");
            $('.NavViewer').trigger("click");
        }
        //used to highlight "File Management after postback.
        function UpdateURL() {

            $('#ancFileMan').attr("onclick", "OpenPage('admin/FileManagement.aspx?FM=true',this);");
            return true;
        }
    </script>

    <style type="text/css">
        .panel-tool {
            cursor: pointer;
        }

            .panel-tool:hover {
                background-color: red;
            }
    </style>

</body>

</html>

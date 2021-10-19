<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TPMLanding.aspx.cs" Inherits="InGlobal.presentation.User.TPMLanding" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ITTPM-IT Technical Project Managers By KSS</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="" charset="UTF-8" />
    <link rel="stylesheet" href="../css/landing-zistyle.css" />
    <link rel="Stylesheet" href="../css/sratstyle.css" />
    <link rel="stylesheet" href="../css/landing-font-awesome.min.css" />
    <link rel="stylesheet" href="../css/Master.css" />
    <script type="text/javascript" src="../NewJEasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="../jquery/jquery.msgBox.js"></script>
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <%-- Spedo pop up --%>
    <link type="text/css" href="../../scripts/jquery/jQpop/data/speedo-notify/speedo.notify.fx.css"
        rel="stylesheet" />
    <link type="text/css" href="../../scripts/jquery/jQpop/data/speedo-notify/skins/agapa/agapa.css"
        rel="stylesheet" />
    <!-- Included CSS Files -->
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/global.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/forms.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/login.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/blog.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/account.css"
        rel="stylesheet" />
    <link href='../../scripts/jquery/jQpop/Stylesheets/google-font.css' rel='stylesheet'
        type='text/css' />
    <!-- Kss Spedo-pop window (login /Signup) -->
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/landings-jquery-speedo-popup.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/demo_effects.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.fx.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/default/default.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/light/light.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/trap/trap.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/metro/metro.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/lightbox/lightbox.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/notify-glass/notify-glass.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/ignito/ignito.css"
        rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/blueglass/blueglass.css"
        rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/dark/dark.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/winter/winter.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/snow/snow.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/rain/rain.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/clouds/clouds.css" />
    <link type="text/css" rel="stylesheet" href="../../scripts/jquery/jQpop/data/highlighter/css/shCore.css" />
    <link type="text/css" rel="stylesheet" href="../../scripts/jquery/jQpop/data/highlighter/css/shThemeDefault.css" />
    <script type="text/javascript" src="../../scripts/jquery/jQpop/Scripts/jquery-2.0.1.min.js"></script>
    <!--[if lt IE 9]>
		<script type="text/javascript" src="../../scripts/jquery/jquery-1.7.1.min.js"></script>
	<![endif]-->
    <!--[if gte IE 9]>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/Scripts/jquery-2.0.1.min.js"></script>
    <!--<![endif]-->
    <!-- IE Fix for HTML5 Tags -->
    <!--[if lt IE 9]>
		<script src="../../scripts/jquery/html5.js"></script>
	<![endif]-->
    <!-- Ionize JS Lang object -->
    <script type="text/javascript">        var Lang = [];
        Lang.get = function (key) { return this[key]; };
        Lang.set = function (key, value) { this[key] = value; };
    </script>
    <script type="text/javascript">
        var base_url = '';
        var page_url = 'scripts/jquery/jQpop/products/speedo-popup-jquery-plugin/demo';
    </script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-notify/speedo.notify.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/themes/agapa/assets/js/script.js"></script>
    <!-- if JS needs to get the theme URL, we give it to him -->
    <script type="text/javascript" async="async">
        var theme_url = 'scripts/jquery/jQpop/themes/agapa/';
    </script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/winter/winter.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/snow/snow.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/rain/rain.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/clouds/clouds.js"></script>
    <link href='../../css/style.css' rel='stylesheet' type='text/css' />
    <link href='../../css/garagedoor.css' rel='stylesheet' type='text/css' />
    <script src="../../scripts/garagedoorjQuery.js" type="text/javascript"></script>

      
    <%-- EO Spedo pop up--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divMenuCaption').hide();
            $('.kssMenuItem').hover(function () {
                var captionHTML = "<div class='FunctionText'>" + $(this).attr('caption') + "</div>";

                if ($(this).attr('caption') == "MOM" || $(this).attr('caption') == "Articles") {


                    captionHTML = "<div class='FunctionText' style='padding-top:10px;'>" + $(this).attr('caption') + "</div>";

                }
                $('#divMenuCaption').html(captionHTML);
                $('#divMenuCaption').show();
                var thisLeft = $(this).css('margin-left');
                var thisTop = $(this).css('margin-top');
                thisTop = thisTop.replace("px", "");
                if ($(this).attr('caption') == "Projects") {
                    thisTop = thisTop - 230;
                }
                else {
                    thisTop = thisTop - 220;
                }

                thisLeft = thisLeft.replace("px", "");
                thisLeft = parseInt(thisLeft) + 75;

                thisLeft = thisLeft + 'px';
                // alert(thisLeft);
                $('#divMenuCaption').css("margin-left", thisLeft);
                $('#divMenuCaption').css("margin-top", thisTop + 'px');

            }, function () {
                $('#divMenuCaption').hide();
            });

            $('.kssMenuItem').click(function () {

                // alert($(this).attr('page'));
                SetIFrameSource($(this).attr('page'));
            });
        });

        function SetIFrameSource(url) {
            var cid = "frmtpm";
            var myframe = document.getElementById(cid);
            if (myframe !== null) {
                if (myframe.src) {
                    myframe.src = url;
                }
                else if (myframe.contentWindow !== null && myframe.contentWindow.location !== null) {
                    myframe.contentWindow.location = url;
                }
                else { myframe.setAttribute('src', url); }
            }
        }
        $(function () {
            $('.icon-book').trigger('click');
        });
        function IsChildPage() {
            return true;
        }
        function Logmeout() {
            //$('#btnLogout').trigger('click');
            window.location = '../../index.aspx?a=lo';
        }
        function NoSession() {
            $('#btnNoSession').trigger('click');
        }
    </script>
    <%--<script type="text/javascript" src="../../scripts/jquery/jquery-1.7.1.min.js"></script>--%>
    <script src="../jquery/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script src="../jquery/jqdashboard/plugins/jquery.panel.js" type="text/javascript"></script>
    <script src="../jquery/jqdashboard/plugins/jquery.window.js" type="text/javascript"></script>
    <script src="../jquery/jqdashboard/plugins/jquery.dialog.js" type="text/javascript"></script>
    <script src="../jquery/jqdashboard/plugins/jquery.draggable.js" type="text/javascript"></script>
    <script src="../jquery/jqdashboard/plugins/jquery.droppable.js" type="text/javascript"></script>
    <script src="../jquery/jqdashboard/plugins/jquery.resizable.js" type="text/javascript"></script>
    <script src="../scripts/JQuery.Sparkline.js" type="text/javascript"></script>
    <script src="../jquery/jqdashboard/plugins/jquery.messager.js" type="text/javascript"></script>
</head>
<body>
    <div id="ziwrap_left">
        <input type="checkbox" name="zimenu" id="zimenu_left">
        <div id="zioverlay">
            <div id="landing-menu-div">
                <iframe frameborder="0" id="frmtpm" style="width: 100%; height: 100%;" src="LandingDefault.aspx">
                </iframe>
            </div>
        </div>
        <div id="zicircle">
            <a class="kssMenuItem" caption="Project Management" href="#" page="tpm/projects.aspx">
                <i class="icon-sitemap"></i></a><a class="kssMenuItem" caption="Task Management"
                    href="#" page="tpm/task.aspx"><i class="icon-tasks"></i></a><a class="kssMenuItem"
                        caption="MOM" href="#" page="tpm/mom.aspx"><i class="icon-laptop"></i>
            </a><a class="kssMenuItem" caption="Questions & Answers" href="#" page="tpm/qans.aspx">
                <i class="icon-question-sign"></i></a><a class="kssMenuItem" caption="Articles" href="#"
                    page="tpm/articles.aspx"><i class="icon-file-text"></i></a>
        </div>
        <label for="zimenu_left" id="zitrigger">
            <i class="icon-book"></i>
        </label>
        <div id="divMenuCaption">
        </div>
    </div>
    <form runat="server" id="frmLanding">
    <asp:Button runat="server" ID="btnLogout" OnClick="Logout" />
    <asp:Button runat="server" ID="btnNoSession" OnClick="doNoSession" />
    </form>
    <script type="text/javascript">
        
        
    </script>
</body>
</html>

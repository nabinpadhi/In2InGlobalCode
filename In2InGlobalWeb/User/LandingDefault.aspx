<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingDefault.aspx.cs" Inherits="InGlobal.presentation.User.LandingDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../NewJEasyUI/jquery.min.js"></script>
    <link rel="stylesheet" href="../css/landing-default-font-awesome.min.css" />
    
    <!-- Included CSS Files -->
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/themes/agapa/assets/css/global.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/themes/agapa/assets/css/forms.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/themes/agapa/assets/css/login.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/themes/agapa/assets/css/blog.css"  rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/themes/agapa/assets/css/account.css" rel="stylesheet" />
    <link href='../scripts/jquery/jQpop/Stylesheets/google-font.css' rel='stylesheet' type='text/css' />
    
    <!-- Kss Spedo-pop window (login /Signup) -->
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/themes/agapa/assets/css/landings-jquery-speedo-popup.css" rel="stylesheet" />
    
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/themes/agapa/assets/css/demo_effects.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.fx.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/default/default.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/light/light.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/trap/trap.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/metro/metro.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/lightbox/lightbox.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/notify-glass/notify-glass.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/ignito/ignito.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/blueglass/blueglass.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/dark/dark.css" />
    <link rel="stylesheet" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/winter/winter.css" />
    <link rel="stylesheet" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/snow/snow.css" />
    <link rel="stylesheet" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/rain/rain.css" />
    <link rel="stylesheet" type="text/css" href="../scripts/jquery/jQpop/data/speedo-popup/skins/clouds/clouds.css" />
    <link type="text/css" rel="stylesheet" href="../scripts/jquery/jQpop/data/highlighter/css/shCore.css" />
    <link type="text/css" rel="stylesheet" href="../scripts/jquery/jQpop/data/highlighter/css/shThemeDefault.css" />

    <script type="text/javascript" src="../scripts/jquery/jQpop/Scripts/jquery-2.0.1.min.js"></script>
    <!--[if lt IE 9]>
		<script type="text/javascript" src="../scripts/jquery/jquery-1.7.1.min.js"></script>
	<![endif]-->
    <!--[if gte IE 9]>
    <script type="text/javascript" src="../scripts/jquery/jQpop/Scripts/jquery-2.0.1.min.js"></script>
    <!--<![endif]-->
    <!-- IE Fix for HTML5 Tags -->
    <!--[if lt IE 9]>
		<script src="Scripts/jquery/html5.js"></script>
	<![endif]-->
    <!-- Ionize JS Lang object -->
    <script type="text/javascript">        var Lang = [];
        Lang.get = function (key) { return this[key]; };
        Lang.set = function (key, value) { this[key] = value; };
    </script>
    <script type="text/javascript">
        var base_url = '';
        var page_url = './scripts/jquery/jQpop/products/speedo-popup-jquery-plugin/demo';
    </script>
    <script type="text/javascript" src="../scripts/jquery/jQpop/data/speedo-notify/speedo.notify.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jQpop/themes/agapa/assets/js/script.js"></script>
    <!-- if JS needs to get the theme URL, we give it to him -->
    <script type="text/javascript" async="async">
        var theme_url = './scripts/jquery/jQpop/themes/agapa/';
    </script>
    <script type="text/javascript" src="../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jQpop/data/speedo-popup/skins/winter/winter.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jQpop/data/speedo-popup/skins/snow/snow.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jQpop/data/speedo-popup/skins/rain/rain.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jQpop/data/speedo-popup/skins/clouds/clouds.js"></script>
    <script type="text/javascript">
    $(document).ready()
        {
            
        }
        $(function () {

            
            var div = $("#InfoSectionDiv");

            $('.CircleDiv,.CircleDivCaption').hover(function () {

                $('#' + $(this).attr("pid")).css("border", "solid 2px black");
                $('#' + $(this).attr("pid")).css("background-color", "#e6e8f8");                
                $('#' + $(this).attr("pid")).css("color", "Black");
                $('#' + $(this).attr("pid")).css("-webkit-animation-name", "spin");
                $('#' + $(this).attr("pid")).css("-moz-animation-name", "spin");
                $('#' + $(this).attr("pid")).css("-ms-animation-name", "spin");
                div.animate({ height: '150px', opacity: '1' }, "slow");
                div.animate({ width: '400px', opacity: '1' }, "slow");

                div.html("My Chamber <br><hr style='width:50%'></hr><br>" + $(this).attr("Info"));

                $(this).css("border", "solid 2px black");
                $(this).css("background-color", "#e6e8f8");
                $(this).css("color", "Black");
                $(this).css("-webkit-animation-name", "spin");
                $(this).css("-moz-animation-name", "spin");
                $(this).css("-ms-animation-name", "spin");

            },
function () {
    $('#' + $(this).attr("pid")).css("border", "solid 2px yellow");
    $('#' + $(this).attr("pid")).css("color", "White");
    $('#' + $(this).attr("pid")).css("-webkit-animation-name", "");
    $('#' + $(this).attr("pid")).css("-moz-animation-name", "");
    $('#' + $(this).attr("pid")).css("-ms-animation-name", "");
    $('#' + $(this).attr("pid")).css("background-color", "blue");
    //$('#' + $(this).attr("pid")).css("cursor", "pointer");
    $('#' + $(this).attr("pid")).css("-webkit-transition", "all 5.1s ease-in-out");
    $('#' + $(this).attr("pid")).css("-moz-transition", "all 5.1s ease-in-out");
    $('#' + $(this).attr("pid")).css("-o-transition", "all 5.1s ease-in-out");
    $('#' + $(this).attr("pid")).css("-ms-transition", "all 5.1s ease-in-out");
    $('#' + $(this).attr("pid")).css("transition", "all 5.1s ease-in-out");

    $(this).css("border", "solid 2px yellow");
    $(this).css("background-color", "blue");
    $(this).css("color", "White");
    $(this).css("-webkit-animation-name", "");
    $(this).css("-moz-animation-name", "");
    $(this).css("-ms-animation-name", "");
});
            $(document).click(function () {

                div.animate({ height: '20px', opacity: '1' }, "slow");
                div.animate({ width: '100px', opacity: '1' }, "slow");
                div.html("My Chamber");
            });
        });
        function OpenHouse(url) {
            window.parent.SetIFrameSource(url);
        }
        function CheckIsInitialPwd() {
            var session = document.getElementById("hdnSessionState").value;
            
            if (session == "dead") {
                $('#lSO').trigger("click");
            }
            var val =document.getElementById("hdnIsInitialPassword").value;
            if (val == 'True') {
                ShowChangePassword();
                AddTitle("Change Password","300");
            }
            else if (val == 'NoSession') {
                
            }
        }
        function AddTitle(caption, width) {

            var rawWidth = width;
            rawWidth = rawWidth.replace('px', '');
            var left = ((rawWidth / 2) - 10) + 'px';
            var Popcaption = "<label style='font-weight:bold;margin-left:" + left + ";margin-top:5px'>";
            var wincap = $(Popcaption).text(caption);
            $('.speedo-popup-drag-area').append(wincap);
        }
        function ShowChangePassword() {
            var skinName = "winter";
            var css3Effects = "leftSpeedIn";
            var effect = "fade";
            var href = "ChangePwd.aspx";
            var width = 300;
            var height = 158;

            changePwd = $(document).speedoPopup(
			    {
			        href: href,
			        height: height,
			        width: width,
			        theme: skinName,
			        unload: true,
			        draggable: true,
			        close: true,			        
			        effectIn: effect,
			        effectOut: effect,
			        css3Effects: css3Effects
			    });
			}
			function HidePWDWindow() {
			    $('.speedo-ui-close').trigger('click');
			}
			$(function () {
			    if (window.parent.IsChildPage() == false) {
			        window.location = "TPMLanding.aspx";
			    }
			});
			function IsChildPage() {
			    return false;
			}
    </script>
    <style type="text/css">
    .Logout
    {
         position:absolute;
         bottom:15px;
         right:15px;
         color:yellow;
    }
    .Logout:hover
    {         
         color:white;
    }
    </style>
</head>
<body style="background-color:Gray" onload="CheckIsInitialPwd();">
    <form id="form1" runat="server">
    <center>
    <div style="width:99%;">    
        <table style="width:100%;padding-top:170px">            
            <tr align="center">
                <td style="width:20%;">
                <div id="CircleDivSiteMap" class="CircleDiv" Info="<ul class='MyChamberList'><li>Find the project status.</li><li>Add/Remove Team member.</li><li>Add/Remove project.</li><li>Generate project report.</li><li>Share project status with client.</li></ul>">
                        <div class="icon-sitemap"></div></div>
                        <div pid="CircleDivSiteMap" onclick="OpenHouse('tpm/projects.aspx');"  class='CircleDivCaption' Info="<ul class='MyChamberList'><li>Find the project status.</li><li>Add/Remove Team member.</li><li>Add/Remove project.</li><li>Generate project report.</li><li>Share project status with client.</li></ul>">Risk Assessment</div>
                </td>
                <td style="width:20%;">
                <div id="CircleDivTasks" class="CircleDiv" Info="<ul class='MyChamberList'><li>Find the team member's assigned task(s) status.</li><li>Assign new task to team member(s).</li><li> Transfer a assigned task to another team member(s).</li><li>Generate task report.</li></ul>">
                        <div class="icon-tasks"></div></div>
                        <div pid="CircleDivTasks" class='CircleDivCaption' onclick="OpenHouse('tpm/task.aspx');" Info="<ul class='MyChamberList'><li>Find the team member's assigned task(s) status.</li><li>Assign new task to team member(s).</li><li> Transfer a assigned task to another team member(s).</li><li>Generate task report.</li></ul>">Task Management</div>
                </td>
                <td  style="width:20%;">
                <div id="CircleDivLaptop" class="CircleDiv" Info="<ul class='MyChamberList'><li>Manage minutes of the meeting.</li><li>Share MOM with team members.</li></ul>">
                        <div class="icon-laptop"></div></div>   
                        <div pid="CircleDivLaptop" class='CircleDivCaption' onclick="OpenHouse('tpm/mom.aspx');" Info="<ul class='MyChamberList'><li>Manage minutes of the meeting.</li><li>Share MOM with team members.</li></ul>">Compliance Management</div>
                </td>
                
                <td style="width:20%;">
                <div id="CircleDivQuestion" class="CircleDiv" Info="<ul class='MyChamberList'><li>Help someone by answering their questions.</li><li>Submit your question(s) and get the answer(s).</li></ul>">
                        <div class="md-icon dp48"></div></div>
                        <div pid="CircleDivQuestion" class='CircleDivCaption' onclick="OpenHouse('tpm/qans.aspx');" Info="<ul class='MyChamberList'><li>Help someone by answering their questions.</li><li>Submit your question(s) and get the answer(s).</li></ul>">Profiling</div>
                </td>
                <td  style="width:20%;">
                <div id="CircleDivFileText" class="CircleDiv" Info="<ul class='MyChamberList'><li>Manage your articles.</li>Share your articles.</li><li>Publish your own articles.</li></ul>">
                        <div class="icon-file-text"></div></div>
                        <div pid="CircleDivFileText" class='CircleDivCaption' onclick="OpenHouse('tpm/articles.aspx');" Info="<ul class='MyChamberList'><li>Manage your articles.</li>Share your articles.</li><li>Publish your own articles.</li></ul>">Reports</div>
                </td>
            </tr>
           
        </table>       
    </div>
    <input type="hidden" id="hdnIsInitialPassword" runat="server" value="" />
    <input type="hidden" id="hdnSessionState" runat="server" value="" />
    <div class="InfoSection" id="InfoSectionDiv">My Chamber</div>
    <p></p>

    <a class="Logout" href="#" onclick="JavaScript:window.parent.Logmeout();">Logout</a>
    <a id="lSO" href="#" onclick="JavaScript:window.parent.NoSession();">_</a>
    </center>
    </form>
</body>
</html>

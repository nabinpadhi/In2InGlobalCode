<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mom.aspx.cs" Inherits="InGlobal.presentation.User.tpm.mom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      
    <%--<link href="../../styles/sratstyle.css" rel="Stylesheet" type="text/css" />--%>
    <link href="../../styles/ittpm.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../css/Master.css" />   
    <link rel="stylesheet" type="text/css" href="../../themes/redmond/jquery-ui-1.8.18.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/ui.jqgrid.css" />
    <link rel="stylesheet" type="text/css" href="../../jqdashboard/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../styles/jquery.tooltip.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../styles/screen.css" />
    <link rel="stylesheet" type="text/css" href="../../jqdashboard/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../styles/jquery.loadmask.css" />
    <link rel="stylesheet" type="text/css" href="../../themes/redmond/MultiseletedDropDown.css" />
      
    <link type="text/css" href="../../scripts/jquery/jQpop/data/speedo-notify/speedo.notify.fx.css" rel="stylesheet" />
    <link type="text/css" href="../../scripts/jquery/jQpop/data/speedo-notify/skins/agapa/agapa.css" rel="stylesheet" />
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
    <link href='../../scripts/jquery/jQpop/Stylesheets/google-font.css' rel='stylesheet' type='text/css' />
     <!-- Kss Spedo-pop window (login /Signup) -->
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/landings-jquery-speedo-popup.css"
        rel="stylesheet" />
        
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/demo_effects.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.fx.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/default/default.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/light/light.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/trap/trap.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/metro/metro.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/lightbox/lightbox.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/notify-glass/notify-glass.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/ignito/ignito.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/blueglass/blueglass.css" rel="stylesheet" />
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
    <script type="text/javascript">
        function ShowTeamMember(subgrid_id, row_id) {
            showSubGrid_jqTeamMember(subgrid_id, row_id, "<div style=\"margin-top:2px;width:99.1%;background-color:#9FC6E1;\" class=\"SubgridControlHeader\"> Team Members  </div>", "jqTeamMember");
            //showSubGrid_jqGridNavigationDetails(subgrid_id,row_id, "", "jqGridNavigationDetails");

        }
        $(function () {
            if (window.parent.IsChildPage() == false) {
                window.location = "../TPMLanding.aspx";
            }
        });
        function IsChildPage() {
            return false;
        }
        function ShowMemberTask(subgrid_id, row_id) {

            showSubGrid_jqTasks(subgrid_id, row_id, "<div style=\"margin-top:2px;width:99.1%;background-color:#9FC6E1;\" class=\"SubgridControlHeader\"> Member's Task  </div>", "jqTasks");
            //showSubGrid_jqGridNavigationDetails(subgrid_id,row_id, "", "jqGridNavigationDetails");

        }
        function CheckSession() {
            var session = document.getElementById("hdnSessionState").value;

            if (session == "dead") {
                $('#lSO').trigger("click");
            }
        }
    </script>
   
</head>

<body style="background-color:#e6e8f8;" onload="CheckSession();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmgrScopeUnit" EnablePageMethods="true" runat="server"
        LoadScriptsBeforeUI="true">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="../../jquery/jquery-1.7.1.min.js" />
                <asp:ScriptReference Path="../../jquery/jquery-ui-1.8.18.custom.min.js" />
                <asp:ScriptReference Path="../../jquery/jqgrid/i18n/grid.locale-en.js" />
                <asp:ScriptReference Path="../../jquery/jqgrid/jquery.jqGrid.min.js" />
                <asp:ScriptReference Path="../../jquery/MultiseletedDropDown.js" />
                <asp:ScriptReference Path="../../jquery/jquery.multiselect.filter.js" />
                <asp:ScriptReference Path="../../jquery/jqdashboard/js/jquery.easyui.min.js" />
                <asp:ScriptReference Path="../../scripts/ErrorMessage.js" />
                <asp:ScriptReference Path="../../scripts/Validation.js" />
                <asp:ScriptReference Path="../../jquery/ui/jquery.ui.autocomplete.js" />
                <asp:ScriptReference Path="../../jquery/jquery.loadmask.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <div>
        <center>
         <table width="100%;">
            <tr>     
            <td style="width:33%"></td>
            <td style="width:34%"><h2>Minutes Of Meeting</h2></td>           
                <td style="width:33%;text-align: right;">
        
                </td>
                
                </tr></table>
        <br />
        </center>
    </div>
    <input type="hidden" id="hdnSessionState" runat="server" value="" />
<a style="position:absolute;bottom:0px;right:0px;" href="#" class="LogoutLink" onclick="JavaScript:window.parent.Logmeout();">&nbsp;&nbsp;Logout&nbsp;&nbsp;</a>
 <a id="lSO" href="#" onclick="JavaScript:window.parent.NoSession();">_</a>
    </form>
</body>
</html>

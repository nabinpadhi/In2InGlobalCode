<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RADashboard.aspx.cs" Inherits="SMARTRA.WEB.RADashboard" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<meta http-equiv="X-UA-Compatible" content="IE=9" />
<title>PCI Risk Assessment - Dashboard</title>
    <link rel="stylesheet" type="text/css" href="themes/default/easyui.css">	
	<link rel="stylesheet" type="text/css" href="themes/icon.css">
	<link rel="stylesheet" type="text/css" href="portal.css">
    <link rel="stylesheet" type="text/css" href="../../themes/redmond/jquery-ui-1.8.18.custom.css">
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.core.js"></script>    
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.button.js"></script>
	<script type="text/javascript" src="js/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="jquery.portal.js"></script>
    
	<style type="text/css">
		.footerMenu
        {   
			width:615px;
			height:33px;
			border-bottom:1px solid #ccc;
			background:url('../../themes/redmond/images/ui-bg_glass_75_d0e5f5_1x400.png') repeat-x;
	        position:relative;
	        border:1px solid #99BBE8;	
            -webkit-border-radius:10px 10px 0px 0px;
            -moz-border-radius:10px 10px 0px 0px;
            border-radius:10px 10px 0px 0px; 
            margin: 0px auto; 
        }   
		.t-list{
			padding:5px;
		}
		.menuButton
		{
		    height:25px;
		    position:relative;		    
		    
		}
		.menuButtons
		{            
            margin-top:3px;
            margin-left:10px;
            width:100%; 
            position:relative;
		 }
	</style>
    
	<script type="text/javascript">
	    $(function () {
	        $('#pp').portal({
	            border: false,
	            fit: false
	        });	        

	    });
        /*
	    function add() {
	        for (var i = 0; i < 3; i++) {
	            var p = $('<div/>').appendTo('body');
	            p.panel({
	                title: 'Title' + i,
	                content: '<div style="padding:5px;">Content' + (i + 1) + '</div>',
	                height: 100,
	                closable: true,
	                collapsible: true
	            });
	            $('#pp').portal('add', {
	                panel: p,
	                columnIndex: i
	            });
	        }
	        $('#pp').portal('resize');
	    }
	    function remove() {
	        $('#pp').portal('remove', $('#pgrid'));
	        $('#pp').portal('resize');
	    }*/
	    $(function () {
	        $("a", ".menuButtons").button();

	    });
	    function resetMainTableHeight(isCollaped) {
	        if (!isCollaped) {
	            //alert($("#pp").find('>table').outerHeight());
	            //$("#mainPortalTable").height($("#pp").find('>table').outerHeight());
	            $('#pp').height($("#pp").find('>table').outerHeight());
	            //$('#pp').height($("#pp").outerHeight());
	            //$(window).height($("#pp").outerHeight());
	        }
	        else {

	           // alert($("#mainPortalTable").find('>table').outerHeight());
                //$("#mainPortalTable").height($("#mainPortalTable").find('>table').outerHeight());
	            //$('#pp').height($("#mainPortalTable").height());
	            //$("#pp").find('>table').height($('#pp').height());
                //$('#pp').height($("#pp").find('>table').outerHeight());
	            //$('#pp').portal('resize');
            }
        }	       
	</script>
</head>
<body class="easyui-layout">
	<div region="center" border="false">
		<div id="pp" style="position:relative;">
			<div style="width:30%;">
				<div title="Top 5 Assets - High Risk" collapsible='true' style="height:200px;padding:5px;margin:0px auto">
					<img height="155px" src="images/tab-1.png"></img>
                    <div>..more</div>
			    </div>
			    <div title="Top 5 Threats - High Risk"  collapsible='true' style="height:200px;padding:5px;">
			    	
			    </div>
                <div title="Top 5 Vulnerabilities - High Risk"  collapsible='true' style="height:200px;padding:5px;">
			    	
			    </div>
                <div title="Top 5 Vulnerabilities - High Risk"  collapsible='true' style="height:200px;padding:5px;">
			    	
			    </div>
			</div>
			<div style="width:30%;">
				<div title="Vulnerabilities Count - By LOV " collapsible='true' style="height:200px;padding:5px;">
					
			    </div>
			    <div title="Threats Count - By LHOT"  collapsible='true' style="height:200px;padding:5px;">
			    	
			    </div>
                <div title="Risk Treatment Plan"  collapsible='true' style="height:200px;padding:5px;">
			    	
			    </div>
                <div title="Action Item Status"  collapsible='true' style="height:200px;padding:5px;">
			    	
			    </div>
			</div>
			<div style="width:30%;">
				<div title="MOR" collapsible='true' style="height:200px;text-align:center;">
					<img height="160" src="images/chart-1.png"></img>
				</div>
				<div title="LOV" collapsible='true' style="height:200px;text-align:center;">
					<img height="160" src="images/chart-3.png"></img>
				</div>
                <div title="LHOT" collapsible='true' style="height:200px;text-align:center;">
					<img height="160" src="images/chart-2.png"></img>
				</div>
			</div>
		</div>
	</div>
    <div region="south" border="false" style="height:35px;">           
		<div class="footerMenu"> 
            <div class="menuButtons">
                <a class="menuButton" href="#">Scoping</a>
                 <a class="menuButton" href="#">Asset Review</a>
                 <a class="menuButton" href="#">Risk Scenarios</a>
                 <a class="menuButton" href="#">Risk Register</a>           
                 <a  class="menuButton" href="#">RTP</a>           
                 <a class="menuButton" href="#">Report</a>           
             </div>          
        </div>     
	</div>

</body>

</html>
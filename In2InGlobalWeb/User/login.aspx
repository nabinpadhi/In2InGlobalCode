<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="InGlobal.presentation.In2InGlobalLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
    #LoginDiv{
     position:absolute;
     top:20%;     
     margin:0px;
     vertical-align:middle;
     
}
#aForgotpassword:hover{
  border-width:1px;
  border-color:black;
  border-style:solid;
  -webkit-border-radius: 4px; 
    -moz-border-radius: 4px;  
    border-radius: 4px;  
  background-color: Black ;
  color:Red;
}
    </style>
    
   <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css"/>
	<link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css"/>
	<link rel="stylesheet" type="text/css" href="../NewJEasyUI/demo.css"/>
	<script type="text/javascript" src="../NewJEasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="../jquery/jquery.msgBox.js"></script>
      <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        
        function ShowHidden() {
            $('#tabLogin').show();
        }
    </script>
    
    
	<script type="text/javascript" src="../NewJEasyUI/jquery.easyui.min.js"></script>

    <script type="text/javascript" src="../scripts/ErrorMessage.js"></script>
    <script type="text/javascript" src="../scripts/Validation.js"></script>
</head>
<body style="background-color:Black;color:White">

    <form id="form1" runat="server">
    <center>  
    
    <div id="LoginDiv">
    <asp:ScriptManager ID="srptMgrLogin" runat="server" 
        AsyncPostBackTimeout="3600" ScriptMode="Release">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updtPnlLogin" runat="server" 
        UpdateMode="Conditional">
        <ContentTemplate>
        <table id="tabLogin" style="width:91%;color:White" cellpadding="0" cellspacing="0">
            <tr align="center">
                <td style="width:35%;text-align:right">User ID</td>
                <td style="width:20%;text-align:center">:</td>
                <td style="width:45%">
                <asp:TextBox runat="server" id="txtUserName" Text="nabinpadhi@yahoo.com" style="width:100%;border-color:black;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;" type="text" />
                </td>
            </tr>
            <tr align="center">
                <td style="width:35%;text-align:right">Password</td>
                <td style="width:20%;text-align:center">:</td>
                <td style="width:45%"><asp:TextBox id="txtPassword" runat="server" Text="test123" style="width:100%;border-color:black;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;" type="password" /></td>
            </tr>
            
             <tr align="center">
                <td colspan="3" style="height: 10px">
                    
                </td>
            </tr>
            <tr align="center">
                <td colspan="3" style="height: 20px">
                    <asp:Button Text="Login" runat="server" type="button" Style="background-color: green;
                        color: yellow; width: 50px; border-color: black; border-style: solid; -webkit-border-radius: 4px;
                        -moz-border-radius: 4px; border-radius: 4px;" ID="cmdLogin" OnClick="cmdLogin_Click"
                        OnClientClick="return ValidateLogin();" />&nbsp;&nbsp;&nbsp;&nbsp;<input type="button"
                            style="background-color: green; color: yellow; width: 50px; border-color: black;
                            border-style: solid; -webkit-border-radius: 4px; -moz-border-radius: 4px; border-radius: 4px;"
                            value="Reset" onclick="ResetLogin();" />
                </td>
            </tr>
             <tr align="center">
                <td colspan="3" style="height: 10px">
                    
                </td>
            </tr>
            <tr align="center">
                <td colspan="3" style="height: 20px">
                    <a id="aForgotpassword" style="color:White;" href="#">&nbsp;Forgot Password&nbsp;</a>
                </td>
            </tr>
             <tr align="center">
                <td colspan="3" style="height: 20px">
                  
                </td>
            </tr>
            <tr align="center">
                <td colspan="3" style="height: 20px">
                    <input type="checkbox" checked="checked" style="border-color: Yellow; border-style: solid;
                        -webkit-border-radius: 4px; -moz-border-radius: 4px; border-radius: 4px;">
                        Remember me in this computer</input>
                </td>
            </tr>
        </table>
        <input type="hidden" runat="server" class="ServerResponse" id="hdnServerResponse" value="" />
        
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </center>
    
    </form>
   
    
 <script type="text/javascript">
     var prm = Sys.WebForms.PageRequestManager.getInstance();
     prm.add_initializeRequest(InitializeRequest);
     prm.add_endRequest(EndRequest);
     var postBackElement;
     var xPos, yPos;
     function InitializeRequest(sender, args) {
         if (prm.get_isInAsyncPostBack()) {
             args.set_cancel(true);
         }
         postBackElement = args.get_postBackElement();

         if (postBackElement.id == "cmdLogin") {


             var parentControl = document.getElementsByTagName("body")[0]; //document.getElementById('divMain');
             StartProcess(parentControl, parentControl.style.top, parentControl.style.left);
         }
     }
     function EndRequest(sender, args) {

         var response = $('#hdnServerResponse').val();
         var parentControl = document.getElementsByTagName("body")[0]; //document.getElementById('divMain');

         $('#dvLoginBox').css("display", "block");
         EndProcess(parentControl);
         if (postBackElement.id == "cmdLogin") {
             if (response == "next") {

                 $.msgBox({
                     title: "Welcome",
                     content: "<b></br>Sucessfully Logedin.</b></br></br></br><b>Please wait a while...</b>",
                     type: "info",
                     showButtons: false,
                     opacity: 0.9,                     
                     autoClose: true,
                     afterClose: function () {
                         window.parent.HideLoginWindow();
                         
                     }
                 });
                 $('.msgBox').css('left', '22px');
                 
             }
             else {
                 ResetLogin();
                 ShowError('Invalid login credentials.');
             }

         }


     }
     function ResetLogin() {
         $('#txtUserName').val("");
         $('#txtPassword').val("");
     }
     function StartProcess(elm, top, left) {

         //body = document.getElementsByTagName("body");
         //body.style.background-color = "white";
         _width = elm.offsetWidth;
         _height = elm.offsetHeight;
         _top = elm.offsetTop;
         _left = elm.offsetLeft;
         overlay = document.createElement("div");
         overlay.id = "proccessingDIV";
         overlay.style.width = _width + "px";
         overlay.style.height = "100%"; //_height + "px";
         overlay.style.position = "absolute";
         overlay.style.background = "white";
         overlay.style.top = _top + "px";
         overlay.style.left = _left + "px";
         overlay.align = "center";
         overlay.style.valign = "middle";
         overlay.style.filter = "alpha(opacity=50)";
         overlay.style.opacity = "0.5";
         overlay.style.mozOpacity = "0.5";
         var imageleft = (_width / 2) - 100; // 20;
         var imagetop = (_height / 2) + 10; //70;
         overlay.innerHTML = "<img style='position:absolute;top:" + imagetop + "px;left:" + imageleft + "px;' src=\"../images/processing.gif\" alt=\"\" />";
         //document.getElementsByTagName("body")[0].appendChild(overlay);
         elm.appendChild(overlay);

     }
     function EndProcess(perCon) {
         overlay = document.getElementById('proccessingDIV');
         if (overlay) {
             perCon.removeChild(overlay);
             $('#tabLogin').hide(); //animate({marginLeft: '300px' }, "slow");
             
         }
     }
     function ValidateLogin() {
         $('#tabLogin').hide();
         Error_Message = "";
         Error_Count = 1;
         CheckNull(document.getElementById("txtUserName").value, TPM_1);
         CheckNull(document.getElementById("txtPassword").value, TPM_2);

         if (Error_Message != "") {
             ShowError(Error_Message);
             return false;
         }
         else {
             return true;
         }
     }
     $(document).ready(function () {
            TabChange();
        });
        function TabChange() {
            $('#txtUserName').bind('keypress', function (event) {

                var key = !event.charCode ? event.which : event.charCode;                
                if (key == 13) {
                    $('#txtPassword').focus();
                    event.preventDefault();
                    return false;
                }
            });
            $('#txtPassword').bind('keypress', function (event) {

                var key = !event.charCode ? event.which : event.charCode;
                if (key == 13) {
                    $('#cmdLogin').trigger("click");
                    return false;
                }
            });
     }   
    </script>
</body>
</html>

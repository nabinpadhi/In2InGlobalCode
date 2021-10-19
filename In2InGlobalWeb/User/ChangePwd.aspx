<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="InGlobal.presentation.ChangePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css"/>
	<link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css"/>
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" type="text/css" href="../NewJEasyUI/demo.css"/>
	<script type="text/javascript" src="../NewJEasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="../jquery/jquery.msgBox.js"></script>      
     <script type="text/javascript" src="../NewJEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../scripts/ErrorMessage.js"></script>
        <script type="text/javascript" src="../scripts/Validation.js"></script>
    <style type="text/css">
        .ui .chv
        {
            text-align: center;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            display: block;
            margin: 0;
            height: auto;
            background: green;
            text-align: center;
            vertical-align: middle;
        }
        
        
        .table
        {
            display: table;
        }
        .table-cell
        {
            display: table-cell;
        }
        .align-middle
        {
            vertical-align: middle;
            text-align: center;
        }
        .full-height
        {
            height: 100%;
        }
        .full-width
        {
            width: 100%;
        }
    </style>
</head>
<body style="background-color: Black; color: White">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="srptMgrLogin" runat="server" AsyncPostBackTimeout="3600" ScriptMode="Release">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updtPnlLogin" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" class="ui">
                <div class="chv">
                    <div class="table full-width full-height">
                        <div class="table-cell align-middle">
                            <table style="vertical-align: middle; width: 100%">
                                <tr>
                                    <td style="width: 45%; text-align: right">
                                        New Password
                                    </td>
                                    <td style="width: 5%; text-align: center">
                                        :
                                    </td>
                                    <td style="width: 40%;">
                                        <asp:TextBox ID="txtNewPwd" runat="server" Text="" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        Confirm Password
                                    </td>
                                    <td style="text-align: center">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConfirmPwd" runat="server" Text="" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <center>
                                            <asp:Button ID="cmdChangePwd" OnClientClick="return ValidatePWD();" Text="Change" runat="server" OnClick="cmdChangePwd_Click" /></center>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <input type="hidden" id="hdnStatus" runat="server" value="" />
        </ContentTemplate>
    </asp:UpdatePanel>
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

            if (postBackElement.id == "cmdChangePwd") {


                var parentControl = document.getElementsByTagName("body")[0];
                
                    StartProcess(parentControl, parentControl.style.top, parentControl.style.left);
             
            }
        }
        function EndRequest(sender, args) {

            var response = $('#hdnStatus').val();
            var parentControl = document.getElementsByTagName("body")[0]; //document.getElementById('divMain');
            EndProcess(parentControl);
            if (postBackElement.id == "cmdChangePwd") {
                
                if (response == "success") {
                    
                    $.msgBox({
                        title: "Change Password",
                        content: "<b></br>Password updated sucessfully.</b>",
                        type: "info",
                        showButtons: false,
                        padding: 0,
                        margin: 0,
                        opacity: 0.9,
                        autoClose: true,
                        afterClose: function () {
                            window.parent.HidePWDWindow();

                        }
                    });


                }
                else {
                    ResetPWD();
                    ShowError('Password updated failed.Try again later.');
                }

            }


        }
        function ResetPWD() {
            $('#txtNewPwd').val("");
            $('#txtConfirmPwd').val("");
        }
        function StartProcess(elm, top, left) {

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
            }
        }
        function ValidatePWD() {
            
            Error_Message = "";
            Error_Count = 1;
            CheckNull(document.getElementById("txtNewPwd").value, TPM_8);
            ComparePassword(document.getElementById("txtNewPwd").value, document.getElementById("txtConfirmPwd").value, TPM_7);

            if (Error_Message != "") {
                ShowError(Error_Message);
                return false;
            }
            else {
                return true;
            }
        }
        function ShowHidden()
        {}
    </script>
</body>
</html>

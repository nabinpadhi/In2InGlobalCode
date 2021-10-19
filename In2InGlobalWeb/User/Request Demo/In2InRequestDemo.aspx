<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="In2InRequestDemo.aspx.cs" Inherits="InGlobal.presentation.tpmsignup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title></title>
    <style type="text/css">
        #SignupDiv {
            position: absolute;
            top: 13%;
            left: 32%;
            margin: 0px;
            vertical-align: middle;
        }
        .FormLabel {
        background-color: #c1c5ee; 
        width: 100%; 
        border-color: brown; 
        border-style: solid; 
        border-width: 1px; 
        -webkit-border-radius: 4px; 
        -moz-border-radius: 4px; 
        border-radius: 4px;
        padding-right:5px;
        color:#3d1247;
        }
        .FormField {
        width: 176px; 
        padding: 0.5px; 
        border-width: 1px; 
        background-color: White; 
        border-color: #a4c6fd; 
        border-style: solid; 
        -webkit-border-radius: 4px; 
        -moz-border-radius: 4px; 
        border-radius: 4px;
        }
        .FormFieldControl {
        height: 16px; 
        font-size: 10px; 
        width: 176px; 
        border-width: 0px;
         -webkit-border-radius: 4px; 
        -moz-border-radius: 4px; 
        border-radius: 4px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="../../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../NewJEasyUI/themes/icon.css" />
    <link href="../../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <script src="../../NewJEasyUI/jquery.min.js" type="text/javascript" language="javascript"></script>
    <script src="../../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" language="javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {

            $('.easyui-dateboxDOB').datebox({});
            RestrictInput();

        });
        function RestrictInput() {
            $('input').bind('keypress', function (event) {
                var regex = new RegExp("^[\\\\/ga-zA-Z0-9@s.]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
             
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }
        $(function () {
            
        });
    </script>
</head>
<body style="color: White">
    <form id="frmSignup" runat="server">
        <asp:ScriptManager ID="scriptmgrSignup" EnablePageMethods="true" runat="server"
            LoadScriptsBeforeUI="true">
            <CompositeScript>
                <Scripts>
                    <%--<asp:ScriptReference Path="../NewJEasyUI/jquery.min.js"/>
                    <asp:ScriptReference Path="../../NewJEasyUI/jquery.easyui.min.js" />--%>
                    <asp:ScriptReference Path="../../jquery/jquery.msgBox.js" />
                    <asp:ScriptReference Path="../../scripts/ErrorMessage.js" />
                    <asp:ScriptReference Path="../../scripts/Validation.js" />
                </Scripts>
            </CompositeScript>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updtPnlSignup" runat="server"
            UpdateMode="Always" EnableViewState="true" ChildrenAsTriggers="true">
            <ContentTemplate>
                <center>  <br /><br />                 
                        <table style="color:blueviolet">
                            <tr><td>
                           <h1>Contact Us</h1>
                           </td></tr>
                            <tr><td>
A customer is the most important visitor on our premises, he is not dependent on us. We are dependent on him. He is not an outsider in our business. He is part of it. We are not doing him a favor by serving him. He is doing us a favor by giving us an opportunity to do so.
<br /><h3> - Mahatma Gandhi</h3></td></tr>
                        </table>
                   
                </center>
                <input type="hidden" runat="server" class="ServerResponse" id="hdnServerResponse" value="" />                
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        var xPos, yPos;
        function ErrorWindowTopPosision(event) {
            _ewTop = event.clientY;
            _ewLeft = event.clientX - 200;
            _ewTop = _ewTop - 25; //- 100;

        }
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
            postBackElement = args.get_postBackElement();

            if (postBackElement.id == "cmdSignup") {


                var parentControl = document.getElementsByTagName("body")[0]; //document.getElementById('divMain');
                StartProcess(parentControl, parentControl.style.top, parentControl.style.left);

            }
        }

        function EndRequest(sender, args) {

            var response = $('#hdnServerResponse').val();
            var parentControl = document.getElementsByTagName("body")[0]; //document.getElementById('divMain');

            EndProcess(parentControl);
            if (postBackElement.id == "cmdSignup") {
                if (response == "next") {
                    window.parent.$('.speedo-ui-close').trigger('click');
                    setTimeout(function () {
                        window.parent.WelcomeTPM();
                    }, 1000);

                }
                else {

                    ShowError(response);
                }

            }


        }
        function ResetSignup() {
            $('#frmSignup')[0].reset();
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
            var imagetop = (_height / 2) + 170;
            overlay.innerHTML = "<img style='position:absolute;top:" + imagetop + "px;left:" + imageleft + "px;' src=\"../../images/processing.gif\" alt=\"http://ittpm.com\" />";
            //document.getElementsByTagName("body")[0].appendChild(overlay);
            elm.appendChild(overlay);

            if ($("#ddlCompanyName").val().indexOf('other-') > -1) {

                document.getElementById("hdnCompanyName").value = $('#txtOtherCompany').val();
            }
            else {
                document.getElementById("hdnCompanyName").value = $("#ddlCompanyName").val();
            }


        }
        function EndProcess(perCon) {

            overlay = document.getElementById('proccessingDIV');
            if (overlay) {
                perCon.removeChild(overlay);
            }
        }
        function AddTPM() {

            return ValidateSignup();
        }
        function ValidateSignup() {

            Error_Message = "";
            Error_Count = 1;

            CheckNull($("#txtFullName").val(), TPM_3);
            CheckNull($("[name='txtDOB']").val(), TPM_4);
            CheckNull($("#ddlCompanyName").val(), TPM_5);

            if ($("#ddlCompanyName").val().indexOf('other-') > -1) {
                CheckNull($("#txtOtherCompany").val(), TPM_5);
            }
            CheckNull($("#txtCompanyEmail").val(), TPM_6);
            if (Error_Message != "") {
                ShowError(Error_Message, 55);
                return false;
            }
            else {
                return true;
            }
        }
        function DisplayOtherField() {
            document.getElementById('hdnCompanyName').value = $("#ddlCompanyName").val();
            if ($("#ddlCompanyName").val().indexOf('other-') > -1) {
                $('#divOtherCompany').show();
            }
            else {
                $('#divOtherCompany').hide();
            }
        }
        function ShowHidden() { }

    </script>
</body>
</html>

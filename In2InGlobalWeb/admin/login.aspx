<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="In2InGlobal.presentation.admin.NewLogin" %>

<!DOCTYPE html>

 <html lang="en">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Login | In2inglobal</title>

                         <%--css--%>

    <link href="../Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <%--<link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">--%>
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link href="../FontAwesome/css/all.css" rel="stylesheet" />
    <link href="css/login.css" rel="stylesheet">
   <%-- <link href="css/admin.css" rel="stylesheet" />--%>
    <link href="../FontAwesome/css/fontawesome.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>

    <link href="css/NewLogin.css" rel="stylesheet" />
                         <%--js--%>
    <script src="../Bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" lang="javascript"></script>

    <script type="text/javascript">
    function ShowException() {

        document.getElementById('exceptionDivParent').style.display = "block";

    }
    function ShowHidden() { }
    </script>
</head>

<body>
    <center>
        <div id="exceptionDivParent" style="position:fixed; left:400px;top:50px; display: none; 
                                            text-align: left; border: 1px solid black; border-radius: 5px; 
                                            width: 400px; height: 300px; margin-top: 100px; background-color: silver;z-index:9999">
            <div class="panel-header panel-header-noborder window-header" style="width: 100%;">
                <div class="panel-title panel-with-icon" style="">Error While Processing Request</div>
                <div class="panel-icon icon-no"></div>
                <div class="panel-tool"><a class="panel-tool-close" href="#"></a></div>
            </div>
            <div style="width: inherit; height: inherit; text-align: center; position: relative;">
                <img style="margin-top: 30px;" src="../img/ohNo.png" /><br />
                <span style="color: red; font-size: medium; font-weight: bold"><i>Oh No!</i> Something has gone wrong</span></div>
        </div>
    </center>
   
    <div class="container">
        <div class="row no-gutters mx-auto p-2 " style="width:90%;" >
            <div class="col-lg-6 mt-4 p-3 mx-auto" align="center">  
                <img src="../images/Loginimage.png" class="img-fluid" style="width:90%;" />
            </div>
            <div class="col-lg-6 p-3 mt-4" >   
               <div class="p-2" id="sign_in_div">   
                    <div class="text-center mb-3" style="width:85%;">
                        <img src="../images/logo-development.png" class="img-fluid" style="width:45%;" />
                    </div>
                 <form id="loginForm" autocomplete="off" runat="server" class="form-group" style="width:85%;"  >
                     <div class="text-center mb-3 ">
                        <h1 class="title">Welcome back!</h1>
                     </div>
                      
                     <div class="input-group mb-3">
                         <span style="background-color:white" class="input-group-text">
                         <i class="fa fa-envelope" ></i></span>
                         <asp:TextBox ID="txtEmailId" AutoPostBack="true" autocomplete="off" name="email"  class="form-control validate" runat="server"
                             data-validate-msg="Email ID field is required"  OnTextChanged="txtEmailId_TextChanged" placeholder="Email Address" Height="30px" Font-Size="14px">
                         </asp:TextBox>
                     </div>
                     
                     <div class="input-group mb-3">
                         <span class="input-group-text" style="background-color:white" >
                             <i class="fa fa-building"></i>
                         </span>
                          <asp:TextBox ID="companyname" AutoPostBack="true" ReadOnly="true" fieldtype="readonly" cssclass="form-control" autocomplete="off" name="companyname"
                              placeholder = " Company" runat="server" Enabled="False"  Height="30px" Font-Size="14px"></asp:TextBox>
                     </div>

                      <div class="input-group mb-3">
                         <span class="input-group-text" style="background-color:white" >
                             <i class="fa fa-key"></i>
                         </span>

                            <input type="password" runat="server" class="form-control validate"  autocomplete="off" id="password" name="password"
                                placeholder = "Password" data-validate-msg="Password field is required" />

                            <div class="input-group-append">
                                <span class="input-group-text" style="background-color: white" onclick="password_show_hide();">
                                    <i class="fa fa-eye" id="show_eye"></i>
                                    <i class="fa fa-eye-slash d-none" id="hide_eye"></i>
                                </span>
                            </div>
                      </div>
                  
                     <div class="custom-checkbox mb-2">
                         <input type="checkbox" class="custom-input checkAll" style="width:0%;" id="remember" name="remember">
                         <label class="custom-label" for="remember">Remember me</label>

                         <a class="small" style="margin-left:120px;color:#17adb3;" id="forget_ps" href="javascript:void(0)">Forgot Password?</a> 
                     </div>

                     <div align="center"  >
                         <button class="custom-button loginButton button" id="loginbtn" name="loginbtn" runat="server" type="button" backcolor="#299192" width="150px">Login</button>
                     </div>
                     <div class="text-center mt-2">
                        <a class="small" style="color:#299192;" id="zohoLink" href="https://analytics.zoho.in/open-view/210664000000116357/1940ea90f91aeff6411ba0a13cb9d6f2"  target="_blank">Analytics Dashboard</a>
                     </div>
                </form>
               </div>

               <div class="p-5" id="forgot_ps_div" style="display: none;">
                   <div class="text-center p-3">
                <h1 class="title">Your password will be emailed to you right away!</h1>
              </div>
                   <form class="user" id="forgotForm">
                  <center>
                <div class="form-group p-3">
                  <input type="email" class="form-control validate_fp email" name="emailFP" id="emailFP" 
                      data-validate-msg="Email field is required"  autocomplete="off" placeholder="Enter Email Address" style="width:90%" height="30px">
                </div>
                <button class="custom-button forgotButton button align-items-center " type="button" style="width:30%;" >Send</button>
                      </center>
              </form>
                  <hr>
                   <div class="text-center">
                <a class="small" id="sign_in" href="javascript:void(0)">Sign In</a>
              </div>
               </div>
                
               <div class="p-2" id="password_div" style="display: none;">
              <div class="text-center">
                <h1 class="title">Enter your new password!</h1>
              </div>
              <form class="user" id="passwordForm">
                <div class="form-group">
                  <input type="hidden" name="user_id" value="0" id="ps_user_id">
                  <input type="hidden" name="otp" value="0" id="user_otp">
                  <input type="password" class="form-control form-control-user validate_ps" autocomplete="off" name="new_password" data-validate-msg="password field is required"  placeholder="Enter Your New Password">
                </div>
                <div class="form-group">
                  <input type="password" class="form-control form-control-user validate_ps" name="new_cpassword" data-validate-msg="Confirm password field is required"   autocomplete="off" placeholder="Enter Your New password Again">
                </div>
                <button class="btn btn-primary btn-user btn-block passwordButton" type="button">Change Password</button>
              </form>
              <hr>
            </div>
               
            </div> 
       </div>
        <div>
            <center>
               <footer class="p-3" style="font-size: small;color:grey "><i>All Rights Reserved.Copyright © In2In Global 2021</i></footer>
            </center>
        </div>
    </div>


<input type="hidden" name="hdnPageAction" id="hdnPageAction" value="" runat="server" />
	
<script src="vendor/jquery/jquery.min.js"></script>
<script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
<script src="vendor/jquery-easing/jquery.easing.min.js"></script>
<link rel="stylesheet" type="text/css" href="css/toastr.min.css">
<script src="js/toastr.min.js"></script>

<script type="text/javascript">
    var BASE_URL = 'Login.aspx';
    $(document).ready(function () {
       
        $('.panel-tool-close').click(function () {

            $('#exceptionDivParent').hide();

        });
    });
    function Login() {

        var return_status = function () {
            var tmp = null;
            var email = $('#txtEmailId').val();
            var password = $('#password').val();
            var dataValue = "{ emailid:'" + email + "',password:'" + password + "'}";
            $.ajax({
                'async': false,
                'type': "POST",
                'global': false,
                'dataType': 'json',
                contentType: 'application/json; charset=utf-8',
                'url': "Login.aspx/Dologin",
                'data': dataValue,
                beforeSend: function () {

                    $('.loginButton').prop('disabled', true);
                    $('.loginButton').html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Logging...');

                },
                'success': function (data) {
                    tmp = data.d;
                }
            });
            return tmp;
        }();

        if (return_status == "Success") {

            //$('.loginButton').html('Logged In');

            //toastr.success('Logged In', 'Success', { timeOut: 1000, progressBar: true, onHidden: function () { window.location.href = BASE_URL; } });
            location.href = '../InternalLanding.aspx';
        }
        else {
            $('.loginButton').prop('disabled', false);
            $('.loginButton').html('Login');
            toastr.error(return_status, 'Error', { timeOut: 2000, closeButton: true, progressBar: true });
        }

    }
    function Demo() {
        $('#email').val('ganesh@gmail.com');
    }
    function password_show_hide() {
        var x = document.getElementById("password");
        var show_eye = document.getElementById("show_eye");
        var hide_eye = document.getElementById("hide_eye");
        hide_eye.classList.remove("d-none");
        if (x.type === "password") {
            x.type = "text";
            x.style.fontFamily = "opan sans:sans series";
            x.style.fontSize = "11pt";
            x.style.height = "30px";
            show_eye.style.display = "none";
            hide_eye.style.display = "block";
        }
        else {
            x.type = "password";
            show_eye.style.display = "block";
            hide_eye.style.display = "none";
        }
    }
</script>
   
<script src="<%= String.Format("{0}dt={1}",ResolveUrl("js/login.js?"), DateTime.Now.Ticks) %>"></script>
<script src="js/jquery.cookie.js"></script>
 
</body>
</html>

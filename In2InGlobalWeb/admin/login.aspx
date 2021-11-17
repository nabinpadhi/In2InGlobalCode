<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="In2InGlobal.presentation.admin.login" %>

<!DOCTYPE html>


<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
 <title>Login | In2inglobal</title>
  <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
  <link href="css/admin.css" rel="stylesheet">
        <link href="css/login.css" rel="stylesheet">
   <!-- <link href="css/admin.css" rel="stylesheet">-->

</head> 
<body class="bg-gradient-primary">
    <div style="position:fixed;left:10px;top:10px;color:yellow;"><img src="../images/in2ingloballogo.png" style="width:50%;" /></div>
	<div class="login-page">        
		<div class="container m-auto">            
			<div class="row justify-content-center">
  <div class="col-xl-10 col-lg-12 col-md-9">
    <div class="card o-hidden border-0 shadow-lg my-5" style="border: 2px solid blue;border-radius: 50px;">
      <div class="card-body p-4" style="border: 2px solid blue;border-radius: 50px;">
        <div class="row">
          <div class="col-lg-6 d-none d-lg-block bg-login-image" style="border: 1px solid none;border-radius: 50px;">             
          </div>
          
          <div class="col-lg-6" style="background-color:#74eff7;border: 2px solid white;border-radius: 50px;">
            <div class="p-5" id="sign_in_div">
              <div class="text-center">
                <h1 class="sec-title">Welcome back!</h1>
              </div>
              <form class="user" id="loginForm" autocomplete="off" runat="server">              
                   <table class="user" style="width:100%;">
                      <tr>
                          <td style="width:50%;text-align:center;color:white;font-weight:bold;">
                              <div  style="background-color:#037f7f;width:80%;border-radius:3px;border:solid 1px #037f7f;">Email ID</div></td>
                          <td style="width:50%;">
                              <div>
                                  <input type="email" class="form-control validate" autocomplete="off" name="email" id="email" data-validate-msg="Email ID field is required" placeholder="Enter Your Email Id" />
                              </div>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align:center;color:white;font-weight:bold;">
                              <div style="background-color:#037f7f;width:80%;border-radius:3px;border:solid 1px #037f7f;">Company Name</div>
                          </td>
                          <td>
                              <div>
                                  <input type="text" class="form-control" readonly="readonly" autocomplete="off" name="companyname" id="companyname" />
                              </div>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align:center;color:white;font-weight:bold;">
                              <div style="background-color:#037f7f;width:80%;border-radius:3px;border:solid 1px #037f7f;">Activity</div>                              
                          </td>
                          <td>
                               <div>
                                  <asp:DropDownList ID="ddlActivity" Width="100%" runat="server" DataTextField="ActivityName"></asp:DropDownList>
                              </div>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align:center;color:white;font-weight:bold;">
                              <div style="background-color:#037f7f;width:80%;border-radius:3px;border:solid 1px #037f7f;">Password</div>
                          </td>
                          <td>
                            
                               <div>
                                  <input type="password" class="form-control validate" autocomplete="off" id="password" name="password" data-validate-msg="Password field is required" placeholder="Password" />
                              </div>
                          </td>
                      </tr>
                  </table>
                
                  
                <div class="custom-checkbox mb-3">
                  <input type="checkbox" class="custom-input checkAll" checked style="width:0%;" id="remember" name="remember">
                  <label class="custom-label" for="remember">Remember me</label>
                    <button class="custom-button loginButton button" id="loginbtn" name="loginbtn" type="button" style="margin-left:100px;">Login</button>
                </div>
              </form>
              <hr>
              <div class="text-center">
                <a class="small" id="forget_ps" href="javascript:void(0)">Forgot Password?</a>
              </div>
            </div>
            <div class="p-5" id="forgot_ps_div" style="display: none;">
              <div class="text-center">
                <h1 class="sec-title">We will sent your password to your email account!</h1>
              </div>
              <form class="user" id="forgotForm">
                <div class="form-group">
                  <input type="email" class="form-control form-control-user validate_fp email" name="emailFP" id="emailFP" data-validate-msg="Email field is required"  autocomplete="off" placeholder="Enter Email Address">
                </div>
                <button class="btn btn-primary btn-user btn-block forgotButton" type="button">Send</button>
              </form>
              <hr>
              <div class="text-center">
                <a class="small" id="sign_in" href="javascript:void(0)">Sign In</a>
              </div>
            </div>
            <div class="p-5" id="otp_div" style="display: none;">
              <div class="text-center">
                <h1 class="sec-title">Enter the OTP here!</h1>
              </div>
              <form class="user" id="otpForm">
                <div class="form-group">
                  <input type="hidden" name="user_id" value="0" id="otp_user_id">
                  <input type="password" class="form-control form-control-user validate_otp" name="otp" data-validate-msg="OTP field is required" autocomplete="off"  placeholder="Enter The OTP Here">
                </div>
                <div class="d-flex justify-content-center">
									<button class="btn btn-primary btn-user btn-block otpButton m-0 mr-1" type="button">Submit</button>
									<button class="btn btn-primary btn-user btn-block m-0 ml-1" id="otpresendButton" type="button">Re-Send</button>
								</div>
              </form>
              <hr>
            </div>
            <div class="p-5" id="password_div" style="display: none;">
              <div class="text-center">
                <h1 class="sec-title">Enter your new password!</h1>
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
      </div>
    </div>
  </div>
</div> 
		</div>
	</div>
    <input type="hidden" name="hdnPageAction" id="hdnPageAction" value="" runat="server" />
	
<script src="vendor/jquery/jquery.min.js"></script>
<script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
<script src="vendor/jquery-easing/jquery.easing.min.js"></script>
<link rel="stylesheet" type="text/css" href="css/toastr.min.css">
<script src="js/toastr.min.js"></script>
<script type="text/javascript">
    var BASE_URL = 'login.aspx'; 
    $(document).ready(function () {
        
        $("#email").change(function () {
            $('.loginButton').prop('disabled', false);
            var usercompanynameandrole = FillCompany($("#email").val());
            var usercompanyname = usercompanynameandrole.split(",")[0];
            var userrole = usercompanynameandrole.split(",")[1];
            if (usercompanyname != "") {
                if (userrole == "Admin") {
                    $('#companyname').val(usercompanyname);
                    $('#ddlActivity').val('All');
                    $('#ddlActivity').prop("disabled", true);
                }
                else {
                    $("#ddlActivity").removeAttr('disabled');
                    $('#companyname').val(FillCompany($("#email").val()));
                    $('#ddlActivity').val('File Management');
                }
            }
            else {
                $('#companyname').val("No Company");
                $('.loginButton').prop('disabled', true);
            }
        });
    });
    function FillCompany(email) {

        var return_companynameandrole = function () {
            var tmp = null;
            var dataValue = "{ emailid:'" + email + "'}";
            $.ajax({
                'async': false,
                'type': "POST",
                'global': false,
                'dataType': 'json',
                contentType: 'application/json; charset=utf-8',
                'url': "login.aspx/GetUserDetails",
                'data': dataValue,
                'success': function (data) {
                    tmp = data.d;
                }
            });
            return tmp;
        }();
        return return_companynameandrole;
    }

    function Login() {
       
        var return_status = function () {
            var tmp = null;
            var email = $('#email').val();
            var password = $('#password').val();
            var dataValue = "{ emailid:'" + email + "',password:'" + password+"'}";
            $.ajax({
                'async': false,
                'type': "POST",
                'global': false,
                'dataType': 'json',
                contentType: 'application/json; charset=utf-8',
                'url': "login.aspx/Dologin",
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

            $('.loginButton').html('Logged In');

            toastr.success('Logged In', 'Success', { timeOut: 1000, progressBar: true, onHidden: function () { window.location.href = BASE_URL; } });
            location.href = '../InternalLanding.aspx';
        }
        else {
            $('.loginButton').prop('disabled', false);
            $('.loginButton').html('Login');
            toastr.error(return_status, 'Error', { timeOut: 2000, closeButton: true, progressBar: true });
        }
        
    }
</script>
<script src="<%= String.Format("{0}dt={1}",ResolveUrl("js/login.js?"), DateTime.Now.Ticks) %>"></script>
<script src="js/jquery.cookie.js"></script>

</body>
</html>
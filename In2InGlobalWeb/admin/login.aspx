<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="In2InGlobal.presentation.admin.login" %>

<!DOCTYPE html>


<!DOCTYPE html>
<html lang="en">

<!-- Mirrored from in2inglobal.com/admin by HTTrack Website Copier/3.x [XR&CO'2014], Tue, 19 Oct 2021 05:41:20 GMT -->
<!-- Added by HTTrack --><meta http-equiv="content-type" content="text/html;charset=UTF-8" /><!-- /Added by HTTrack -->
<head>
<meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
 <title>Admin Login | In2inglobal</title>
  <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
  <link href="css/admin.css" rel="stylesheet">
        <link href="css/login.css" rel="stylesheet">
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
              <form class="user" id="loginForm">
                <div class="form-group">
                  <input type="email" class="form-control form-control-user validate" autocomplete="off" name="email" id="email" data-validate-msg="Email field is required"  placeholder="Enter Email Address">
                </div>
                <div class="form-group">
                  <input type="password" class="form-control form-control-user validate" autocomplete="off" id="password" name="password" data-validate-msg="Password field is required"  placeholder="Password">
                </div>
                <div class="custom-checkbox mb-3">
                  <input type="checkbox" class="custom-input checkAll" id="remember" name="remember" checked>
                  <label class="custom-label" for="remember">Remember me</label>
                </div>
                <button class="btn btn-primary btn-user btn-block loginButton" type="button">Login</button>
              </form>
              <hr>
              <div class="text-center">
                <a class="small" id="forget_ps" href="javascript:void(0)">Forgot Password?</a>
              </div>
            </div>
            <div class="p-5" id="forgot_ps_div" style="display: none;">
              <div class="text-center">
                <h1 class="sec-title">We will sent an OTP to your email account!</h1>
              </div>
              <form class="user" id="forgotForm">
                <div class="form-group">
                  <input type="email" class="form-control form-control-user validate_fp" name="email" data-validate-msg="Email field is required"  autocomplete="off" placeholder="Enter Email Address">
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

	<!-- Bootstrap core JavaScript-->
<script src="vendor/jquery/jquery.min.js"></script>
<script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
<!-- Core plugin JavaScript-->
<script src="vendor/jquery-easing/jquery.easing.min.js"></script>
<!-- Custom scripts for all pages-->
<!-- <script src="https://in2inglobal.com/assets/admin/js/admin.js"></script> -->

<link rel="stylesheet" type="text/css" href="css/toastr.min.css">
<script src="js/toastr.min.js"></script>


<script type="text/javascript">
    var BASE_URL = 'index.html';
</script>
<script src="js/login.js"></script>
<script src="js/jquery.cookie.js"></script>

</body>


<!-- Mirrored from in2inglobal.com/admin by HTTrack Website Copier/3.x [XR&CO'2014], Tue, 19 Oct 2021 05:41:25 GMT -->
</html>
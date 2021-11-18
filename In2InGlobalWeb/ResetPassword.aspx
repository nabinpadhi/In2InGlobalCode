<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="In2InGlobal.presentation.admin.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>document.getElementsByTagName("html")[0].className += " js";</script>
    <link rel="stylesheet" href="admin/assets/css/style.css" />
    <link rel="stylesheet" href="css/style.css" />
    <link href="css/resetpass.css" rel="stylesheet">

    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("admin/css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
      
</head>
<body>
    
        <div class="new-container">
        <header class="cd-main-header js-cd-main-header" style="background-color: #03989e;height:83px;">           
            <div style="position:fixed;left:10px;top:10px;color:yellow;"><img src="../images/in2ingloballogo.png" style="width:40%;" /></div>
            <ul class="cd-nav__list js-cd-nav__list">

                <li class="cd-nav__item cd-nav__item--has-children cd-nav__item--account js-cd-item--has-children">                               
                </li>
            </ul>
        </header>
        <main class="cd-main-content" style="width:100%; position:fixed;left:0px;top:10px;">
            <nav class="cd-side-nav js-cd-side-nav" style="padding-top: 70px;height:100%;">
                <ul class="cd-side__list js-cd-side__list">                    
                    <li class="cd-side__item cd-side__item--has-children cd-side__item--user js-cd-item--has-children">
                        
                    </li>
                </ul>
            </nav>

            <div style="padding-top: 20px; bottom: 0px; background-color: azure;">
                <div style="width: 100%; height: 84.8%" id="frmTarget">
                   
                     <center>
         <div style="width: 50%; border: 1px solid black; border-radius: 5px; margin-top: 30px;">
             <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px;height:40px;padding-top:10px;"><span class="menu_frame_title">Reset Your Login Password</span></div>
          <div style="position:relative;padding:10px 10px 10px 10px;">
           <div class="p-5" id="password_div">                       
                        <form class="user" id="passwordForm">
                            <div class="form-group">
                                <input type="hidden" runat="server" name="user_id" value="0" id="ps_user_id">                                
                                <input type="password" class="form-control form-control-user validate_ps" autocomplete="off" name="new_password" data-validate-msg="password field is required" placeholder="Enter Your New Password">
                            </div>
                            <div class="form-group">
                                <input type="password" class="form-control form-control-user validate_ps" id="new_cpassword" runat="server" name="new_cpassword" data-validate-msg="Confirm password field is required" autocomplete="off" placeholder="Enter Your New password Again">
                            </div>
                            <button class="btn btn-primary btn-user btn-block passwordButton" type="button">Change Password</button>
                        </form>                        
                    </div>

              </div>
             </div>
      </center>

                </div>
                <div style="position: sticky; margin-left: auto;">
                    <div style="background: #212121;padding: 30px 0;">
                    <center>
                        <footer style="font-size: small;color:#96a1b5"><i>Copyright © In2In Global 2021</i></footer>
                    </center>
                        </div>
                </div>
            </div>
            
            <!-- .content-wrapper -->
        </main>       
    </div>          
    
       <script src="admin/vendor/jquery/jquery.min.js"></script>
<script src="admin/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
<script src="admin/vendor/jquery-easing/jquery.easing.min.js"></script>
<link rel="stylesheet" type="text/css" href="admin/css/toastr.min.css">
<script src="admin/js/toastr.min.js"></script>
    <script src="<%= String.Format("{0}dt={1}",ResolveUrl("scripts/resetpass.js?"), DateTime.Now.Ticks) %>"></script>
        <script type="text/javascript">
            
        </script>     
</body>
</html>

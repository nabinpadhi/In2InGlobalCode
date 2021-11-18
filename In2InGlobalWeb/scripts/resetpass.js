


$(document).ready(function(){

});
	

$('#forgotForm input').keypress(function (e) {
 var key = e.which;
 if(key == 13)  // the enter key code
  {

  $('.forgotButton').click();
    return false;
 }
});

	$(".loginButton").click(function () {

		var enable = true;
		$( ".validate" ).each(function( index,element ) {
			if($(element).attr('name') == 'email' && $(element).val() != '' && IsEmail($(element).val())==false){

				toastr.error('please enter a valid email.', 'Error', {timeOut: 2000,closeButton:true,progressBar:true});
				enable = false;
				return false;
			}
			if($(element).val() == ''){
				toastr.error($(element).data('validate-msg'), 'Error', {timeOut: 2000,closeButton:true,progressBar:true});
				enable = false;
				return false;
			}
		});
		if (enable) {			
			Login();
		}
	});

    $(".forgotButton").click(function () {
       
        var enable = true;
        
		$( ".validate_fp" ).each(function( index,element ) {
			if($(element).attr('name') == 'email' && $(element).val() != '' && IsEmail($(element).val())==false){

				toastr.error('please enter a valid email.', 'Error', {timeOut: 2000,closeButton:true,progressBar:true});
				enable = false;
				return false;
			}
		});
        if (enable) {
            
            sendotp();
		}
	});

	$(".otpButton").click(function(){
		var enable = true;
		$( ".validate_otp" ).each(function( index,element ) {
			if($(element).attr('name') == 'otp' && $(element).val() == ''){
				toastr.error('please enter an OTP.', 'Error', {timeOut: 2000,closeButton:true,progressBar:true});
				enable = false;
				return false;
			}
		});
		if(enable){
			checkotp();
		}
	});

	$(".passwordButton").click(function(){
		var enable = true;
		if($('input[name=new_password]').val() == '' || $('input[name=new_cpassword]').val() == '' || $('input[name=new_password]').val() != $('input[name=new_cpassword]').val()){
			toastr.error('please enter Your new password in both field.', 'Error', {timeOut: 2000,closeButton:true,progressBar:true});
			enable = false;
			return false;
		}
		if(enable){
			change_password();
		}
	});


$('body').on('click','#forget_ps',function(){
	$('#sign_in_div').hide();
	$('#forgot_ps_div').show();
});

$('body').on('click','#sign_in',function(){
	$('#forgot_ps_div').hide();
	$('#sign_in_div').show();
});

$('body').on('click','#otpresendButton',function(){
	sendotp();
});


function IsEmail(email) {
  var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
  if(!regex.test(email)) {
    return false;
  }else{
    return true;
  }
}

function change_password(){
    var email = $('#ps_user_id').val();
    var password = $('#new_cpassword').val();
    var dataValue = "{ emailid:'" + email + "',password:'" + password + "'}";
	$.ajax({
        'async': false,
        'type': "POST",
        'global': false,
        'dataType': 'json',
        contentType: 'application/json; charset=utf-8',
        'url': "ResetPassword.aspx/ChangePass",
        'data': dataValue,
		beforeSend:function(){
			$('.passwordButton').prop('disabled',true);
			$('.passwordButton').html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Changing password...');
		},
		success:function(result){
			var data = jQuery.parseJSON(result.d);
			if(data.status == 1){				
                toastr.success(data.msg, 'Success', { timeOut: 1000, progressBar: true, progressBar: true });
                document.location.href = "admin/login.aspx";
			} else {
                toastr.error(data.msg, 'Error', { timeOut: 2000, closeButton: true, progressBar: true });
                document.location.href = "admin/login.aspx";
			}
		}
	});
}
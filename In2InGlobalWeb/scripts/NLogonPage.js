function ValidateLogin() {
    Error_Message = "";
    Error_Count = 1;
    //CheckNull(document.getElementById("txtUserName").value, Err_182);
    //CheckNull(document.getElementById("txtPassword").value, Err_183);
    if ($('#txtUserName').val() == null || Trim($('#txtUserName').val()) == "") {
        $('#lblErr').text("Please enter the user name.");
        return false;
    }
    else {
        if ($('#txtPassword').val() == null || Trim($('#txtPassword').val()) == "") {
            $('#lblErr').text("Please enter the password.");
            return false;
        }
        else {
            return true;
        }
    }
}
function validateEmailinfo() {
    Error_Message = "";
    Error_Count = 1;
    CheckNull(document.getElementById("txtRetPassword").value, Err_50)
    if (document.getElementById("txtRetPassword").value != "") {
        ValidateEmail(document.getElementById("txtRetPassword").value, Err_9)
    }
    if (document.getElementById("txtUserName").value != "") {
        ValidateStrings(document.getElementById("txtUserName").value, Err_273);
    }
    if (document.getElementById("txtPassword").value != "") {
        ValidateStrings(document.getElementById("txtPassword").value, Err_274);
    }

    if (Error_Message != "") {
        ShowError(Error_Message);
        return false;
    }
    else {
        return true;
    }
}
function OpenFile() {
    var downloadFilePath = "./SMART_FactSheet/Datasheet SMART5.2.pdf";
    var windowWidth = "10px";
    var windowHeight = "10px";
    var winLeft = ((screen.width / 2) - 100);
    var winTop = ((screen.height / 2) - 100);
    if (downloadFilePath.indexOf(".pdf") > 0 || downloadFilePath.indexOf(".PDF") > 0) {
        windowWidth = screen.width - 10;
        windowHeight = screen.height - ((screen.height / 100) * 10)
        winLeft = 0;
        winTop = 0;
    }
    window.open(downloadFilePath, "downloadFile", "height=" + windowHeight + "px,width=" + windowWidth + "px,status=yes,scrollbars=yes,toolbar=no,menubar=no,top=" + winTop + ",left=" + winLeft + ";");
}
function ActionStatus() {

    if (document.getElementById("hdnaction") != null && document.getElementById("hdnaction").value != "") {
        alert(document.getElementById("hdnaction").value);
        document.getElementById("hdnaction").value = "";
    }
}
function KillSessionAndContinue(w) {
    document.getElementById('hdnKS').value = w;
    document.forms[0].submit();
}
document.onload = ActionStatus();
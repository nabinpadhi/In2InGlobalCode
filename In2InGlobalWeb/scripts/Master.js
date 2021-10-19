function saveMenuItemState(menuId) {
    pageName = window.location.href.split("/");
    simple = pageName.length - 1;
    name = pageName[simple];
    aspx = name.split(".");
    aspx2 = aspx.length - 2;
    pageName = aspx[aspx2];


    $('li').each(function (index) {
        $(this).removeClass('ui-state-active');
        $(this).removeClass('ui-tabs-selected');
    });

    //Apply active style for selected sub-menu item
    var MeuItems = "rd" + pageName;
    $('#' + MeuItems).siblings(0).addClass('ui-state-active');

    if (menuId == 1) {
        $('#ancUserInfo').click();

    }
    else if (menuId == 2) {
        $('#ancStdData').click();
    }
    else if (menuId == 3) {
        $('#ancAdmin').click();

    }
    else if (menuId == 4) {
        $('#ancRAHome').click();

    }
    else if (menuId == 4) {
        $('#ancActionItemHome').click();

    }

}

function ShowValidationMessageWindow(winmsg) {

    $.messager.show({
        title: '<span style="color:red">IT-TPM - Error </span>',
        msg: winmsg,
        width: 338,
        showType: 'show'
    });
}

function ShowTPMIntimation(winmsg, what) {
    var _width = 338;
    if (winmsg != null) {
        switch (what) {
            case "Success":
                {

                    $.messager.show({
                        title: '<span style="color:Yellow">IT-TPM - Success</span>',
                        msg: winmsg,
                        width: _width,
                        showType: 'show'

                    });
                }
                break;
            case "Failed":
                {

                    $.messager.show({
                        title: '<span style="color:Red">IT-TPM - Failed</span>',
                        msg: winmsg,
                        width: _width,
                        showType: 'show'

                    });
                }
                break;
            case "Error":
                {

                    $.messager.show({
                        title: '<span style="color:Red">IT-TPM - Error</span>',
                        msg: winmsg,
                        width: _width,
                        showType: 'show'

                    });
                }
                break;
        }
    }

}
function ShowExceptionWindow() {

    var skinName = "ignito";
    var css3Effects = "pageTop";
    var effect = "fade";
    var href = "PageException.aspx";
    var width = 530;
    var height = 280;

    serverResponse = $("body").speedoPopup(
        {
            href: href,
            height: height,
            width: width,
            theme: skinName,
            unload: true,
            draggable: true,
            autoClose: false,
            css3Effects: css3Effects
        });
}
function ShowAlertWindow(winmsg) {
    $.messager.alert('<span style="color:red">IT TPM - ShowAlertWindow</span>', winmsg, "error");
}
function CheckSession() {
    var session = document.getElementById("hdnSessionState").value;

    if (session == "dead") {
        $('#lSO').trigger("click");
    }
}
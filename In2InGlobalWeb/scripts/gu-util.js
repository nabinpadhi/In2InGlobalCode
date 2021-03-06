window.google || (google = {});
window.google.update || (google.update = {});
google.update.c = [];

function _GU_OnloadHandlerAdd(a, b) {
    var c = google.update.c.length;
    "number" == typeof b && b < c && 0 <= b && (c = b);
    google.update.c.splice(c, 0, a)
}
function _GU_OnloadBody(a) {
    var b = window.google.update.c;
    if (b) for (var c = 0; c < b.length; ++c) try {
        b[c](a)
    } catch (e) { }
}

function _GU_getPlatform() {
    if (window.google.update.d) return window.google.update.d;
    window.google.update.d = "Win32" == navigator.platform ? "win" : "WinCE" == navigator.platform ? "wince" : /linux/i.test(navigator.platform) ? "linux" : /mac/i.test(navigator.platform) ? "mac" : "win";
    return window.google.update.d
}

function _GU_getBrowserId() {
    if (window.google.update.a) return window.google.update.a;
    window.google.update.a = 0; -1 != navigator.userAgent.indexOf("Opera") ? window.google.update.a = 0 : -1 != navigator.userAgent.indexOf("Firefox") ? window.google.update.a = 3 : -1 != navigator.userAgent.indexOf("MSIE") ? -1 != navigator.userAgent.indexOf("Windows CE") ? window.google.update.a = 0 : -1 == navigator.userAgent.indexOf("PPC") && -1 == navigator.userAgent.indexOf("In2InGlobalphone") && (window.google.update.a = 2) : -1 != navigator.userAgent.indexOf("Chrome") ?
        window.google.update.a = 4 : -1 != navigator.userAgent.indexOf("Safari") && (window.google.update.a = 0);
    return window.google.update.a
}
function _GU_setCookie(a, b, c) {
    a = a + "=" + escape(b); -1 != c && (b = new Date, b.setMinutes(b.getMinutes() + c), a += ";expires=" + b.toUTCString());
    document.cookie = a
}
function _GU_setSessionCookie(a, b) {
    _GU_setCookie(a, b, -1)
}

function _GU_getCookie(a) {
    if (document.cookie) {
        var b = document.cookie.indexOf(a + "=");
        if (-1 != b && (0 == b || ";" == document.cookie.substring(b - 2, b - 1))) return b = b + a.length + 1, a = document.cookie.indexOf(";", b), -1 == a && (a = document.cookie.length), unescape(document.cookie.substring(b, a))
    }
    return ""
}
function _GU_removeCookie(a) {
    _GU_setCookie(a, "", 0)
}
function _GU_areCookiesSupported() {
    _GU_setCookie("test", "1", 1);
    var a = "1" == _GU_getCookie("test");
    _GU_removeCookie("test");
    return a
}

function _GU_initIid() {
    var a = _GU_getCookie("iid");
    if (a) window.google.update.b = a;
    else if (_GU_areCookiesSupported()) {
        var a = window.google.update,
            b;
        b = function () {
            for (var a = 65536, a = Math.floor(Math.random() * a), a = a.toString(16); 4 > a.length; ) a = "0" + a;
            return a.toUpperCase()
        };
        b = "{" + b() + b() + "-" + b() + "-" + b() + "-" + b() + "-" + b() + b() + b() + "}";
        a.b = b;
        _GU_setSessionCookie("iid", window.google.update.b)
    } else window.google.update.b = "{11112222-3333-4444-5555-666677778888}"
}

function _GU_getIid() {
    window.google.update.b || _GU_initIid();
    return window.google.update.b
}
function _GU_experimentTripletToTagValue(a) {
    if ("string" != typeof a) return "";
    a = a.split(",");
    if (3 != a.length) return "";
    var b = Number(a[2]);
    return b ? (b *= 864E5, b = (new Date((new Date).getTime() + b)).toUTCString(), encodeURIComponent(a[0] + "=" + a[1] + "|" + b)) : ""
}

function _GU_experimentTripletArrayToTagElement(a, b) {
    if (!a) return "";
    var c = "";
    try {
        for (var e = b ? "omahaexperiments" : "experiments", f = [], d = 0; d < a.length; ++d) {
            var h = _GU_experimentTripletToTagValue(a[d]);
            h && f.push(h)
        }
        f.length && (c = "&" + e + "=" + f.join(encodeURIComponent(";")))
    } catch (i) { }
    return c
}
function _GU_createAppInfo(a, b, c, e, f) {
    var d = {};
    d.guid = a;
    d.name = b;
    d.needsAdmin = c;
    a = f ? _GU_experimentTripletArrayToTagElement(f) : "";
    d.customParams = (e || "") + a;
    return d
}

function GU_BuildTag(a, b) {
    b || (b = "");
    for (var c = "", e = 0; e < a.length; ++e) 0 == e ? (c += "appguid=" + a[e].guid, c += b) : c += "&appguid=" + a[e].guid, c += "&appname=" + encodeURIComponent(a[e].name).replace(/~/g, "%7E").replace(/\!/g, "%21").replace(/\*/g, "%2A").replace(/\(/g, "%28").replace(/\)/g, "%29").replace(/\'/g, "%27"), c += "&needsadmin=" + a[e].needsAdmin, a[e].customParams && (c += a[e].customParams);
    return c
}

function GU_buildGlobalExtra(a, b, c) {
    a = "&iid=" + _GU_getIid() + "&lang=" + a + "&browser=" + _GU_getBrowserId() + "&usagestats=";
    a = b ? a + "1" : a + "0";
    c && (a += _GU_experimentTripletArrayToTagElement(c, !0));
    return a
}

function _GU_isClickOnceAvailable() {
    function a(a) {
        var c = navigator.userAgent.match(/\.NET CLR [0-9.]+/g);
        if (null == c || 0 == c.length) return !1;
        a = a.split(".");
        if (0 == a.length) return !1;
        for (var e = 0; e < c.length; ++e) {
            var f = c[e].match(/\.NET CLR ([0-9.]+)/);
            if (!(null == f || 2 != f.length)) {
                var f = f[1].split("."),
                    d;
                if (d = 0 < f.length) {
                    var h = d = 0,
                        i = 0;
                    do h = d < a.length ? Number(a[d]) : 0, i = d < f.length ? Number(f[d]) : 0, ++d; while ((d < a.length || d < f.length) && h == i);
                    f = h < i ? -1 : h > i ? 1 : 0;
                    d = 0 >= f
                }
                if (d) return !0
            }
        }
        return !1
    }
    return 0 <= window.location.search.indexOf("noclickonce") ||
        2 != _GU_getBrowserId() ? !1 : a("2.0.0")
}
function _GU_isOneClickAvailable() {
    return window.google.update && window.google.update.oneclick
}
function _GU_SetupOneClick() {
    _GU_SetupOneClickVersions(["9", "8"])
}

function _GU_SetupOneClickVersions(a) {
    function b(a) {
        var b = document.createElement("object");
        b.type = "application/x-vnd.google.oneclickctrl." + a;
        b.id = "OneClickCtrl";
        b.style.position = "absolute";
        b.style.top = "-5000px";
        b.style.left = "-5000px";
        document.body.appendChild(b);
        return b
    }
    if ((!window.google.update || !window.google.update.oneclick) && "win" == _GU_getPlatform()) {
        for (var c = 0; c < a.length; ++c) {
            var e = a[c];
            try {
                new ActiveXObject("Google.OneClickCtrl." + e), window.google.update.oneclickPlugin_ = b(e)
            } catch (f) {
                var d =
                    navigator.mimeTypes["application/x-vnd.google.oneclickctrl." + e];
                d && d.enabledPlugin && (window.google.update.oneclickPlugin_ = b(e))
            }
            if (window.google.update.oneclickPlugin_) break
        }
        window.google.update.oneclickPlugin_ && !(0 <= window.location.search.indexOf("nooneclick")) && !window.google.update.oneclick && (window.google.update.oneclick = {
            getOneClickVersion: function () {
                try {
                    return window.google.update.oneclickPlugin_.GetOneClickVersion()
                } catch (a) {
                    return -1
                }
            },
            install: function (a, b, e, d, f, g) {
                var k = "//tools.google.com",
                    k = k + "/service/update2/installping";
                e = GU_buildGlobalExtra(b, e, g);
                e = '"' + GU_BuildTag(a, e) + '"';
                for (c = 0; c < a.length; ++c) {
                    g = k;
                    g += "?appid=" + encodeURIComponent(a[c].guid);
                    g += "&lang=" + encodeURIComponent(b);
                    g += "&iid=" + encodeURIComponent(_GU_getIid());
                    g += "&installsource=oneclick";
                    var l = new Image;
                    l.src = g
                }
                a = "/install " + e;
                try {
                    window.google.update.oneclickPlugin_.Install(a, d, f)
                } catch (j) {
                    d = 0;
                    try {
                        d = j.number, d || (a = "", a = j.message ? j.message : j, d = parseInt(a, "0x" == a.substring(0, 2) ? 16 : 10))
                    } catch (m) { }
                    if (isNaN(d) || 0 == d) d = -2;
                    f(d)
                }
            },
            launchAppCommand: function (a, b, c) {
                if (9 > window.google.update.oneclick.getOneClickVersion()) return !1;
                try {
                    return window.google.update.oneclickPlugin_.LaunchAppCommand(a, b, c), !0
                } catch (d) {
                    return !1
                }
            },
            getInstalledVersion: function (a, b) {
                var c = "";
                try {
                    c = window.google.update.oneclickPlugin_.GetInstalledVersion(a, b)
                } catch (d) { }
                return c
            }
        })
    }
}
function _GU_buildDlPath(a, b, c, e, f, d) {
    b = GU_buildGlobalExtra(b, c, d);
    a = GU_BuildTag(a, b);
    return e + "/tag/s/" + encodeURIComponent(a) + f
}

function _GU_buildDlPathNoTag(a, b, c, e, f) {
    return e + f
}
function _GU_buildClickOncePath(a, b, c, e, f, d) {
    b = GU_buildGlobalExtra(b, c, d);
    a = GU_BuildTag(a, b);
    return e + f + "?" + encodeURIComponent(a)
};
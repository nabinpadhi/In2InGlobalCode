<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleOnDotNet.aspx.cs" Inherits="InGlobal.presentation.blog.ArticleOnDotNet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .blogTitle {
        font-size: 250%;
        font-weight: bold;
        color: #471654;
        }
        .about-scott {
        width: 100%;
        padding-left: 25px;
        padding-right: 25px;
        box-sizing: border-box;
        -moz-box-sizing: border-box;
       
        padding-top: 1em;
        padding-bottom: 1em;
        margin-bottom: 1.5em;
        position: relative;
        overflow: hidden;
        }
        .bioBox {
        width: 360px;
        float: left;
        }
        .bioBoxInner {
        padding-left: 125px;
        background: transparent url('image/photo-scott-tall.jpg') no-repeat left top;
        }
        hr {
            
border-color: #e2842c;
color: #e2842c;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
       
                <div style="background-color: #f6f6ea;border-radius:25px;width:100%;">
                     <div style="width:98%;height:530px;overflow-y:auto">
                <table style="width:98%" cellspacing="10px">

                    <tr><td colspan="2" align="center">
                        <h1 class="blogTitle">Checklist: What NOT to do in ASP.NET</h1>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>About a year ago we thought it would be a good idea to do a talk on "What not to do in ASP.NET?" - basically an anti-patterns talks. We kept seeing folks falling into the same traps and wanted to be prescriptive as there's aspects to ASP.NET that are 10 years old and don't apply to today's internet, but there are also <a href="http://www.asp.net/get-started">new aspects to ASP.NET</a> that are only a year old, and perhaps haven't soaked into the zeitgeist quite yet. </p>
                            <p>Damian Edwards gave <a href="http://vimeo.com/68390507">his version of this talk at NDC 2013 and you can watch the video here</a> if you like, it's very entertaining.</p>
                            <p>We took the information we gathered from people like Damian, Levi Broderick and others, and <a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead">Tom FitzMacken put together a whitepaper on the topic</a>. It's not complete, but it covers some of the most common "gotchas" folks run into.</p>
                            <p>Here are the areas we call out in the whitepaper so far, with highlights below from me.</p>
                        </td>
                        <td style="width:200px">
                            <div class="about-scott">
	                            <div class="bioBox">
	                                <h4>About Scott</h4>
                                    <div class="bioBoxInner">
			                            <p>Scott Hanselman is a former professor, former Chief Architect in finance, now speaker, consultant, father, diabetic, and Microsoft employee. I am a failed stand-up comic, a cornrower, and a book author.</p>
                                        <a href="http://facebook.com/scott.hanselman" class="sm-link"><img src="image/icon-fb.png" alt="facebook"></a>
                                        <a href="http://twitter.com/shanselman" class="sm-link"><img src="image/icon-twitter.png" alt="twitter"></a>
                                        <a href="http://plus.google.com/108573066018819777334?rel=author" class="sm-link" rel="author"><img src="image/icon-gplus.png" alt="g+"></a>
                                        <a href="http://feeds.hanselman.com/ScottHanselman" class="sm-link"><img src="image/icon-rss.png" alt="subscribe"></a><br>
                                        <a href="http://hanselman.com/about">About</a> &nbsp; <a href="http://www.hanselman.com/newsletter">Newsletter</a>

                                    </div>
	                            </div>     
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <ul>   <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#standards">Standards Compliance</a>       <ul>       <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#adapters">Control Adapters</a> - Control adapters were a good idea in .NET 2, but it's best to use solid adaptive CSS and HTML techniques today. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#styleprop">Style Properties on Controls</a> - Set CSS classes yourself, don't use inline styles. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#callback">Page and Control Callbacks</a> - Page Callbacks pre-date standard AJAX techniques, so today, stick with SignalR, Web API, and JavaScript. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#browsercap">Browser Capability Detection</a> - Check for features, not for browsers whenever possible. </li>     </ul>   </li>    <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#security">Security</a>       <ul>       <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#validation">Request Validation</a> - While Request Validation is useful, it's not focused and it doesn't know exactly what you app is doing. Be smart and validate inputs with the full knowledge of what your app is trying to accomplish. Don't trust user input. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#cookieless">Cookieless Forms Authentication and Session</a> - Don't pass anything auth related in the query string. Cookieless auth will never be secure. Don't do it. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#viewstatemac">EnableViewStateMac</a> - This should never be false. Ever. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#medium">Medium Trust</a> - Medium trust isn't a security boundary you should count on. Put apps in separate app pools. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#appsettings">&lt;appSettings&gt;</a> - Don't disable security patches with appSettings. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#urlpathencode">UrlPathEncode</a> - This doesn't do what you think it does. Use UrlEncode. This method was very specific, poorly named, and is now totally obsolete. </li>     </ul>   </li>    <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#performance">Reliability and Performance</a>       <ul>       <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#presend">PreSendRequestHeaders and PreSendRequestContext</a> - Leave these alone making managed modules. These can be used with native modules, but not IHttpModules. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#asyncevents">Asynchronous Page Events with Web Forms</a> - Use Page.RegisterAsyncTask instead. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#fire">Fire-and-Forget Work</a> - Avoid using ThreadPool.QueueUserWorkItem as your app pool could disappear at any time. Move this work outside or use WebBackgrounder if you must. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#requestentity">Request Entity Body</a> - Stay out of Request.Form and Request.InputStream before your handler's execute event. It may not be ready to go. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#redirect">Response.Redirect and Response.End</a> - Be conscious of Thread.Aborts that will happen when you redirect. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#viewstatemode">EnableViewState and ViewStateMode</a> - There's no need to hate on ViewState. Turn it off everywhere, then turn it on <em>only for the individual controls that need it. </em></li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#sqlprovider">SqlMembershipProvider</a> - Consider using ASP.NET User Providers, or better yet, the new ASP.NET Identity system. </li>        <li><a href="http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet,-and-what-to-do-instead#long">Long Running Requests (&gt;110 seconds)</a> - ASP.NET isn't meant to handle long running requests that are a minute (or two) long. Use WebSockets or SignalR for connected clients, and use asynchronous I/O operations.</li>     </ul>   </li> </ul>
                          <p>I hope this helps someone out! </p>
                        </td></tr>
                    <tr><td colspan="2" align="center">
                        <hr />
                        </td>
                    </tr>
                    <tr><td colspan="2" align="center">
                        <h1 class="blogTitle">Know if mobile apps are secure...</h1>
                        </td>
                    </tr>
                    <tr><td colspan="2">
                       <p>You know how we're always telling out non-technical non-gender-specific spouses and parents to be safe and careful online? You know how we <a href="http://www.hanselman.com/blog/WhatGeeksNeedToTellOurParentsAboutShoppingOnlineSafelyAndSecurely.aspx">teach non-technical friends about the little lock in the browser</a> and making sure that <a href="http://www.hanselman.com/blog/HighAssuranceOrLdquoExtendedValidationrdquoEVSSLCertificates.aspx">their bank's little lock turned green</a>?</p>
                        <p>Well, we know that HTTPS and SSL don't imply trust, they imply (some) privacy. But we have some cues, at least, and after many years while a good trustable UI isn't there, at least web browsers TRY to expose information for technical security decisions. Plus, <a href="http://www.hanselman.com/blog/IfMalwareAuthorsEverLearnHowToSpellWereAllScrewedTheComingHTML5MalwareApocalypse.aspx">bad guys can't spell</a>.</p>
                        <h3>But what about mobile apps?</h3>
                        <p>I download a new 99 cent app and perhaps it wants a name and password. What standard UI is there to assure me that the transmission is secure? Do I just assume?</p>
                        <p>What about my big reliable secure bank? Their <a href="http://blogs.computerworld.com/application-security/23386/mobile-ios-banking-apps-are-miserably-insecure-leaky-messes">banking app is secure, right</a>? If they use SSL, that's cool, right? Well, are they sure who they are talking too?</p>
                        <blockquote>   <p><a href="http://blog.ioactive.com/2014/01/personal-banking-apps-leak-info-through.html">OActive Labs researcher Ariel Sanchez</a> <a href="http://blog.ioactive.com/2014/01/personal-banking-apps-leak-info-through.html">tested</a> 40 mobile banking apps from the "top 60 most influential banks in the world." </p>    <p><strong>40% of the audited apps did not validate the authenticity of SSL certificates presented. </strong>This makes them susceptible to man-in-the-middle (MiTM) attacks.</p>    <p>Many of the apps (90%) contained several non-SSL links throughout the application. This allows an attacker to intercept the traffic and inject arbitrary JavaScript/HTML code in an attempt to create a fake login prompt or similar scam.</p> </blockquote>
                        <p>If I use an app to log into another service, what assurance is there that they aren't storing my password in cleartext?&nbsp; </p>
                        <blockquote>   <p>It is easy to make mistakes such as storing user data (passwords/usernames) incorrectly on the device, in the vast majority of cases credentials get stored either unencrypted or have been encoded using methods such as base64 encoding (or others) and are rather trivial to reverse,” says <a href="http://www.theguardian.com/technology/2014/feb/12/feeling-smug-that-your-iphone-cant-be-hacked-not-so-fast">Andy Swift, mobile security researcher from penetration testing firm Hut3</a>.</p> </blockquote>
                        <p>I mean, if <a href="http://www.ibtimes.com/starbucks-stored-ios-app-passwords-location-data-clear-text-leaving-apple-iphone-app-users-1542614">Starbucks developers can't get it right</a> (they stored your password in the clear, on your device) then how can some random Jane or Joe Developer? What about cleartext transmission?</p>
                        <blockquote>   <p>"This mistake extends to sending data too, if developers rely on the device too much it becomes quite easy to forget altogether about the transmission of the data. Such data can be easily extracted and may include authentication tokens, raw authentication data or personal data. At the end of the day if not investigated, the end user has no idea what data the application is accessing and sending to a server somewhere." - <a href="http://www.theguardian.com/technology/2014/feb/12/feeling-smug-that-your-iphone-cant-be-hacked-not-so-fast">Andy Swift</a></p> </blockquote>
                        <p>I think that it's time for operating systems and SDKs to start imposing much more stringent best practices. Perhaps we really do need to move to an <a href="https://www.eff.org/https-everywhere">HTTPS Everywhere</a> Internet as the <a title="https://www.eff.org/https-everywhere" href="https://www.eff.org/https-everywhere">Electronic Frontier Foundation</a> suggests.</p>
                        <p>Transmission security doesn't mean that bad actors and malware can't find their way into App Stores, however. <a href="http://news.cnet.com/8301-13579_3-57598955-37/researchers-slip-malware-onto-apples-app-store-again/">Researchers have been able to develop, submit, and have approved bad apps in the iOS App Store</a>. I'm sure other stores have the same problems.</p>
                        <p>The <a href="http://www.nsa.gov/ia/_files/os/applemac/apple_ios_5_guide.pdf">NSA has a 37 page guide on how to secure your (iOS5) mobile device</a> if you're an NSA employee, and it mostly consists of two things: Check "secure or SSL" for everything and disable everything else. </p>
                        <p><strong>What do you think? </strong>Should App Stores put locks or certification badges on "secure apps" or apps that have passed a special review? Should a mobile OS impose a sandbox and reject outgoing non-SSL traffic for a certain class of apps? Is it too hard to code up SSL validation checks? '</p>
                        <p style="text-align:center"><i>Whose problem is this? I'm pretty sure it's not my Dad's.</i></p>
                        <hr>

                        </td>
                    </tr>
                </table>
                </div>
         </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAnalytics.aspx.cs" Inherits="In2InGlobal.presentation.admin.AdminAnalytics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href='https://fonts.googleapis.com/css?family=Work+Sans:300,400,600&Inconsolata:400,700' rel='stylesheet' type='text/css' />

    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top:10px;background-color:lightblue;">
            <table style="width: 100%; text-align:right;">
                <tr>
                    <td style="width: 16%;">Mail ID :</td>
                    <td style="width: 16%;">
                        <select name="usermailid" id="usermailid" runat="server">
                            <option value="">--Select Email ID--</option>
                            <option value="123test@test.com">123test@test.com</option>
                            <option value="xyz@in2inglobal.com">xyz@in2inglobal.com</option>
                            <option value="abc@gmail.com">abc@gmail.com</option>
                            <option value="me@world.co.us">me@world.co.us</option>
                        </select>

                    </td>
                   <td style="width: 16%;text-align:right;">Project ID :</td>
                    <td style="width: 16%;">
                        <select name="projectids" id="projectids" runat="server">
                            <option value="">--Select Project ID--</option>
                            <option value="P001">P001</option>
                            <option value="P002">P002</option>
                            <option value="P003">P003</option>
                            <option value="P004">P004</option>
                        </select>
                    </td>
                  <td style="width: 16%;">Dashboard Name :</td>
                    <td style="width: 20%;">
                        <select name="dashboardnames" id="dashboardname" runat="server">
                            <option value="">--Select Dashboard--</option>
                            <option value="Dashboard A001">Dashboard A001</option>
                            <option value="Dashboard A002">Dashboard A002</option>
                            <option value="Dashboard A003">Dashboard A003</option>
                            <option value="Dashboard A004">Dashboard A004</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <p>
            <h4>SPEND ANALYSIS DASHBOARD</h4>
            <br />
            <label style="margin-top: -35px; margin-left: 35px; font-size: xx-small;">A complete analysis on spend_usd</label>
        </p>
    </form>
     <script src="js/jquery.js"></script>
    <script src="js/jquery.nice-select.min.js"></script>
    <script src="js/fastclick.js"></script>
    <script src="js/prism.js"></script>

    <script>
        $(document).ready(function () {
            $('select:not(.ignore)').niceSelect();
            FastClick.attach(document.body);          
        });
    </script>

    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-64633646-1', 'auto');
        ga('send', 'pageview');
    </script>
</body>
</html>

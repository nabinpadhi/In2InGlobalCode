<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="In2InGlobal.presentation.admin.MyProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>document.getElementsByTagName("html")[0].className += " js";</script>
    <link href='https://fonts.googleapis.com/css?family=Work+Sans:300,400,600&Inconsolata:400,700' rel='stylesheet' type='text/css' />


    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />

    <style type="text/css">
       
        .GridViewImageAlignment {
            text-align: center;
        }

        .file_table {
            width: 100%;
        }

        .file_table_header {
            border-bottom: 1px solid #ffb215;
            padding: 8px;
        }

            .file_table_header th {
                background-color: #4472c4;
                color: white;
                border-bottom: 1px solid black;
            }

        .file_table tr td {
            border-bottom: 1px solid #ccc;
        }

        ul {
            list-style: none;
            padding: 0;
        }

        li {
            padding-left: 1.3em;
        }

            li:before {
                content: "\f00c"; /* FontAwesome Unicode */
                font-family: FontAwesome;
                display: inline-block;
                margin-left: -1.3em; /* same as padding-left set on li */
                width: 1.3em; /* same as padding-left set on li */
            }
    </style>
   
</head>
<body>

    <form id="form1" runat="server">

        <center>
            <div class="MainPageFrameDiv">
                <div class="pagination-ys"><span class="menu_frame_title">My Profile</span></div>
                <div style="position: relative; padding: 50px 30px 30px 30px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">
                                <center>
                                    <div  class="formDiv" style="width: 70%;border: 0px solid #d3d3d3; border-radius: 5px; margin-top: 10px;">
                                        <table style="width: 90%; vertical-align: top;">
                                            <tr style="padding: 10px; vertical-align: middle;">
                                                <td style="padding: 10px; width: 20%">User Name</td>
                                                <td style="width: 30%"><span fieldtype="readonly" value="" runat="server" id="username"></span></td>
                                                <td style="padding: 10px; width: 23%">Company Name</td>
                                                <td style="width: 28%"><span fieldtype="readonly" runat="server" id="companyname"></span></td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px">Email Id</td>
                                                <td><span value="" fieldtype="readonly" style="overflow-wrap: break-word;" runat="server" id="email" /></td>
                                                <td style="padding: 10px">Activity Access</td>
                                                <td><span value="" fieldtype="readonly" runat="server" id="activityaccess" /></td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px">Role</td>
                                                <td><span value="" fieldtype="readonly" runat="server" id="role" /></td>
                                                <td style="padding: 10px">Status</td>
                                                <td><span value="" fieldtype="readonly" runat="server" id="status" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </center>
    </form>
</body>
</html>

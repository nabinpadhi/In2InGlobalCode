<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageException.aspx.cs" Inherits="InGlobal.presentation.User.PageException" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>IT-TPM - Security Management& Risk Assessment Tool</title>
     <link href="../styles/sratstyle.css" rel="Stylesheet" type="text/css" />
    <link href="../styles/ittpm.css" type="text/css" rel="Stylesheet" />
    <link rel="stylesheet" type="text/css" href="../themes/redmond/jquery-ui-1.8.18.custom.css" />
    <script language="javascript" type="text/javascript">

        function CloseMe() {
            if (window.opener != null) {
                window.opener.location.reload(true);
            }
            else if (window.parent != null) {
                window.parent.location.reload(true);
            }
            window.close();
        }
    </script>

</head>
<body style="background-color: #324464;" id="ErrorBody" runat="server">
    <form id="form1" runat="server">
        <table width="95%" align="center">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr id="trHeader" runat="server">
                            <td>                               
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">                                    
                                    <tr>                                        
                                        <td valign="top" style="height: 71px;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 47%; height: 83px;">
                                                        <table align="center" width="100%">
                                                            <tr>
                                                                <td width="50%" style="height: 100%">
                                                                    <img src="../images/logo.png" alt="www.ittpm.com" width="200" height="96"/>
                                                                </td>
                                                                <td align="center" style="height: 100%; width: 50%;">
                                                                    <div id="div8" align="center" style="top: 50; left: 150; display: none; font-family: Arial;
                                                                        color: Blue; font-weight: bold; font-size: small">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                   
                                                </tr>
                                            </table>
                                        </td>                                        
                                    </tr>                                  
                                </table>
                            </td>
                        </tr>                       
                        <tr>
                            <td width="100%">
                                <div class="TdGrid ui-corner-all" style="width: 100%; margin: 0px; padding: 0px;
                                z-index: 9999">
                                <div id="divheader" runat="server" class="roundedHeader" style="height: 25px">
                                    <span class="HeaderTitle">Page Error</span>
                                </div>
                                    <table width="100%" style="text-align: left; margin: 0px;" cellpadding="5px" cellspacing="0px">
                                        <tr valign="top" id="tdSuccess" runat="server">
                                            <td width="100%">
                                                <h2 style="color: Red">
                                                    Oops! An error has occurred while processing your request.</h2>
                                            </td>
                                        </tr>
                                        <tr valign="top" id="tdSummited" runat="server">
                                            <td width="100%">
                                                <p>
                                                    <b>Please Go <a href="#" onclick="GoBack();" title="This step is recommended, Only if you have already logged into the application.">
                                                        back</a> and try again.</b></p>
                                                <ul>
                                                    <li>If difficulties persist, please contact the system administrator of IT-TPM.</li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>        
    </form>
</body>


</html>


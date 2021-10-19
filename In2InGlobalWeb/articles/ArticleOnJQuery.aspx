<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleOnJQuery.aspx.cs" Inherits="InGlobal.presentation.articles.ArticleOnJQuery" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
       <style type="text/css">

        .blogTitle {
        font-size: 250%;
        font-weight: bold;
        color: #471654;
        }
        .about-Kris-Borchers {
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
        padding-left: 85px;
        background: transparent url('image/Kris-Borchers.jpg') no-repeat left top;
        }
        hr {
            
border-color: #e2842c;
color: #e2842c;
}
    </style>
    </style>
</head>
<body>    
    <form id="form1" runat="server">
       
          <div style="background-color: #f6f6ea;border-radius:25px;width:100%;">
                     <div style="width:98%;height:530px;overflow-y:auto">
                <table style="width:98%" cellspacing="10px">
                    <tr>
                        <td runat="server" id="tdJqueryArticle">
                     
                        </td>
                    </tr>                  
                </table>
                </div>
         </div>
    </form>
</body>
</html>
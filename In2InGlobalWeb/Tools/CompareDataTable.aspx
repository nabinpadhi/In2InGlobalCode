<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompareDataTable.aspx.cs" Inherits="In2InGlobal.presentation.admin.CompareDataTable" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' type='text/css' href='Styles/StaticHeader.css' />
  
     <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" lang="javascript"></script>
    <script type='text/javascript' src='Styles/x.js'></script>

    <script type='text/javascript' src='Styles/xtableheaderfixed.js'></script>

    <script type='text/javascript'>
        xAddEventListener(window, 'load',
            function() { new xTableHeaderFixed('gvTheGrid', 'table-container', 0); }, false);
    </script>
    <style type="text/css">
        .PageNavLink{
            
            vertical-align:middle;    
            
        }
        .PageNavLink:link { COLOR: black; TEXT-DECORATION: none; font-weight: normal }
        .PageNavLink:visited { COLOR: black; TEXT-DECORATION: none; font-weight: normal }
        .PageNavLink:active { COLOR: black; TEXT-DECORATION: none }
        .PageNavLink:hover { COLOR: yellow; TEXT-DECORATION: none; font-weight: none }
    </style>
</head>
<body>
    <center>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="Templatescriptmanager" runat="server">
            </asp:ScriptManager>           
            <asp:UpdatePanel ID="pdnlTemplate" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                OLD data
                                <div id='oldTable-container'>
                                    <asp:GridView ID="grdOldData" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="true"
                                        BackColor="WhiteSmoke" HeaderStyle-CssClass="specify" AlternatingRowStyle-BackColor="Silver" HeaderStyle-Font-Size="Medium"
                                        CssClass="gvTheGrid">
                                    </asp:GridView>
                                </div>
                            </td>
                            <td>
                                Difference Data
                                 <div id='diffTable-container'>
                                    <asp:GridView ID="grdDiffData" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="true"
                                        BackColor="WhiteSmoke" HeaderStyle-CssClass="specify" AlternatingRowStyle-BackColor="Silver" HeaderStyle-Font-Size="Medium"
                                        CssClass="gvTheGrid">
                                    </asp:GridView>
                                </div>
                            </td>
                            <td>
                                New Data
                                <div id='newTable-container'>
                                    <asp:GridView ID="grdNewData" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="true"
                                        BackColor="WhiteSmoke" HeaderStyle-CssClass="specify" AlternatingRowStyle-BackColor="Silver" HeaderStyle-Font-Size="Medium"
                                        CssClass="gvTheGrid">
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>                     
                    </table>                   
                               
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </center>
   
    <style type="text/css">
         .specify {
       
        word-break: keep-all;       
        white-space: nowrap;
    }

    </style>
</body>
</html>

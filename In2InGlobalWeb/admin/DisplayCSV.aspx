<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayCSV.aspx.cs" Inherits="In2InGlobal.presentation.admin.DisplayCSV" %>

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
<body style="overflow: hidden;">
    <center>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="Templatescriptmanager" runat="server">
            </asp:ScriptManager>           
            <asp:UpdatePanel ID="pdnlTemplate" runat="server">
                <ContentTemplate>
                    <div style="position: absolute; left: 60px; padding-top: 5px;">
                                <asp:Label runat="server" Text="Record Count :[Loading Data...]" ID="lblRecordCnt"></asp:Label>
                            </div>
                            <div style="position: relative; padding-left: 200px;margin-right:30px; padding-top: 5px;float:right;">
                                <a runat="server" id="ancDownload" href="#" name="ancDownload">Download</a>
                            </div> 
                    <div class="gridParentDiv" style="float:left; border: 1px solid black; border-radius: 5px; margin-top: 20px; display: block;">
                            <asp:HiddenField ID="hdnSkip" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnTake" runat="server" Value="1000" />
                       
                        <asp:Button ID="btnLoadNewPage" runat="server" OnClientClick="return true;" OnClick="btnLoadNewPage_Click" style="display:none;" />
                        <center>
                            <div id='table-container'>
                                <asp:GridView ID="grdCSVData" style="width:98%;height:70%;" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="true"
                                    BackColor="WhiteSmoke" HeaderStyle-CssClass="specify" OnRowDataBound="grdCSVData_RowDataBound" AlternatingRowStyle-BackColor="Silver" HeaderStyle-Font-Size="Medium"
                                    OnPreRender="grdCSVData_PreRender" CssClass="gvTheGrid">
                                     
                                    </asp:GridView>
                        </center>
                                    <table id="gridPageTable" style="font-size:12px;text-align:center;vertical-align:middle;"><tr></tr></table>
                                </div>
                               
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </center>
    <script >
        var currentPage = 1;
        var width = $(window).width();
        var height = $(window).height();
        $(document).ready(function () {

            BuildPagination();
            ResetScreenSize();
        });
        function BuildPagination() {

            var recCnt = $('#lblRecordCnt').text().split('-')[1];
           
            var dynamicRequiredTD = "";
            var noTDRequired = Math.round(recCnt / 1000);
         
            if (noTDRequired > 10) {
                for (i = 1; i <= 10; i++) {
                    dynamicRequiredTD = dynamicRequiredTD + "<td><div id='pDiv" + i + "' style=\"width:20px;height:20px;boder:1px solid black;border-radius:2px;background-color:gray;\"><a class=\"PageNavLink\" href='#' onclick=LoadGridPage(" + i + ")>[" + i + "]</a></div></td>";
                }
            }
            else {

                for (i = 1; i <= noTDRequired; i++) {
                    dynamicRequiredTD = dynamicRequiredTD + "<td><div id='pDiv" + i + "' style=\"width:20px;height:20px;boder:1px solid black;border-radius:2px;background-color:gray;\"><a class=\"PageNavLink\" href='#' onclick=LoadGridPage(" + i + ")>[" + i + "]</a></div></td>";
                }
            }
            var PageRow = "<tr cellpadding=\"5px\" style=\"color: black;height:50px;\">" + dynamicRequiredTD + "</tr>";
            $('#gridPageTable tr:last').after(PageRow);
            $(currentPage).css("background-color", "yellow");
            $(currentPage).css("color", "blue");

        }
        function LoadGridPage(pageNo) {

            var skip = (parseInt(pageNo) * 1000) - 1000;
            var take =  1000;
            $('#hdnSkip').val(skip);
            $('#hdnTake').val(take);

            $('#btnLoadNewPage').trigger("click");
            var clickedPage = pageNo;
            clickedPage = "#pDiv" + pageNo;
            currentPage = clickedPage;                     
           
        }
        function ResetScreenSize() {

            $('.gridParentDiv').css("width", width - 32);
            $('.gridParentDiv').css("height", height - 100);
            $('#table-container').css("width", width - 42);
            $('#table-container').css("height", height - 180);
        }
    </script>
    <style type="text/css">
         .specify {
       
        word-break: keep-all;       
        white-space: nowrap;
    }

    </style>
</body>
</html>

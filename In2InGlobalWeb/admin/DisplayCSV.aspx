<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayCSV.aspx.cs" Inherits="In2InGlobal.presentation.admin.DisplayCSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
            border-collapse: collapse;
            background-color: #fff;
        }

            table th {
                background-color: #ff7f00;
                color: #fff;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border: 1px solid #ccc;
            }

            table, table table td {
                border: 0px solid #ccc;
            }

        .button {
            background-color: #0094ff; /* Blue */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
        }
    </style>

</head>
<body>
    <center>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="Templatescriptmanager" runat="server">
            </asp:ScriptManager>
            <asp:UpdateProgress ID="UpdatePnlTemplate" runat="server" AssociatedUpdatePanelID="pdnlTemplate">
                <ProgressTemplate>
                    <img src="img/uploading.gif" alt="Uploading..." />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="pdnlTemplate" runat="server">

                <ContentTemplate>
                    <div style="margin-right: 30px;float:left; border: 1px solid black; border-radius: 5px; margin-top: 20px; display: block;">
                        <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">CSV File Viewer</span></div>
                        <div class="blockMe">
                            <div style="position: absolute; left: 100px; padding-top: 5px;">
                                <asp:Label runat="server" Text="Record Count :100420" ID="lblRecordCnt"></asp:Label>
                            </div>
                            <div style="position: relative; padding-left: 200px;margin-right:30px; padding-top: 5px;float:right;">
                                <a runat="server" id="ancDownload" href="#" name="ancDownload">Download</a>
                            </div>

                            <div style="width: 100%; margin-top: 20px">
                                <asp:GridView Width="100%" ID="grdCSVData" ClientIDMode="Static" ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="1000" OnPageIndexChanging="grdCSVData_PageIndexChanging" EmptyDataText="Uploaded file doesn't contain any data to display" runat="server"></asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </center>
    <script type="text/javascript">
        function getQueryStringValue(uri, key) {
            var regEx = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var matches = uri.match(regEx);
            return matches == null ? null : matches[1];
        }

        function LoadCSVData() {

            var return_companynameandrole = function () {
                var tmp = null;
                $.ajax({
                    'async': false,
                    'type': "POST",
                    'global': false,
                    'dataType': 'json',
                    contentType: 'application/json; charset=utf-8',
                    'url': "DisplayCSV.aspx/LoadCSVData",
                    'data': '{}',
                    'success': function (data) {
                        tmp = data.d;
                    }
                });
                return tmp;
            }();
            return return_companynameandrole;
        }
    </script>
</body>
</html>

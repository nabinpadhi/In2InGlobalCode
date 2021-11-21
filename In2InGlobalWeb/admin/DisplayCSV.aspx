<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayCSV.aspx.cs" Inherits="In2InGlobal.presentation.admin.DisplayCSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />   
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
            border-collapse: collapse;
            background-color: #fff;
        }
        table th
        {
            background-color: #ff7f00;
            color: #fff;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border: 1px solid #ccc;
        }
        table, table table td
        {
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
        <div style="width: 95%; border: 1px solid black; border-radius: 5px; margin-top: 20px;display:block;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height:40px;padding-top:10px;"><span class="menu_frame_title">CSV File Viewer</span></div>
        <asp:GridView ID="grdCSVData" ShowHeaderWhenEmpty="true" EmptyDataText ="Uploaded file doesn't contain any data to display" runat="server">
        </asp:GridView>
            </div>
    </form>
          </center>
</body>
</html>
﻿<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AnalyticConfiguration.aspx.cs" Inherits="In2InGlobal.presentation.admin.AnalyticConfiguration" %>

<!DOCTYPE html>


<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <link href="css/gridview.css" rel="stylesheet" type="text/css" />
    <link href="css/Grid.css" rel="stylesheet" type="text/css" />
  <script lang="JavaScript">
  
</script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%;height:450px; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Analytic Configuration</span></div>
                <asp:ScriptManager ID="analyticconfigurationscriptmanager" runat="server">                    
                </asp:ScriptManager>               
                <asp:UpdatePanel  ID="pdnlanalyticconfiguration" runat="server">                       
                    <ContentTemplate> 
                       <div style="width:100%" id="analyticconfigurationDiv">
                       
                        <table style="width: 70%; background-color: azure;">
                            <tr>
                                <td>
                                    <center>
                                        <div style="width: 50%; border: 1px solid black; border-radius: 5px; margin-top: 10px;">
                                            <table style="padding-top:10px;">
                                                <tr>
                                                     <td>Company : </td>
                                                    <td>
                                                       <asp:DropDownList ID="ddlCompany" runat="server" Width="94%" AppendDataBoundItems="true" DataValueField="com_id" DataTextField="company_name">
                                                           <asp:ListItem>--Select a Company</asp:ListItem>
                                                       </asp:DropDownList>
                                                    </td>
                                                     <td>User : </td>
                                                    <td>
                                                       <asp:DropDownList ID="ddlUser" runat="server" Width="94%" AppendDataBoundItems="true" DataValueField="user_id" DataTextField="user_email">
                                                           <asp:ListItem>--Select a User--</asp:ListItem>
                                                       </asp:DropDownList>
                                                    </td>
                                                </tr>     
                                                 <tr>                                                                                                       
                                                    <td>Link : </td>
                                                     <td colspan="3">
                                                         <input type="text" value="" style="width:90%" runat="server" id="txtlink" name="txtlink" class="txtlink" />
                                                    </td>
                                                </tr>     
                                                <tr>
                                                    <td colspan="4">
                                                        <div style="margin-top: 30px;">
                                                            <center>
                                                                <asp:Button runat="server" ID="btnSave" CssClass="button" Text="Save" OnClientClick="return false" />
                                                                <input type="button" class="button" style="margin-left: 10px;" value="Cancel" onclick="ClearAll();" />
                                                            </center>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%;">                                    
                                    <center>
                                        <div style="width: 50%; height: 90%; border: 1px solid black; border-radius: 5px; margin-top: 10px; margin-bottom: 20px;"> 
                                            <div class="AspNet-GridView">
                                                 <asp:GridView runat="server" ID="grdAnalyticsLink" Width="100%"  
                                                     HeaderStyle-CssClass="AspNet-GridView" AllowPaging="True" DataKeyNames="company_id" PageSize="5" AutoGenerateColumns="false">
                                                     <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                                    <Columns>
                                                        <asp:BoundField DataField="company_id" ControlStyle-Width="94%" HeaderText="CompanyID" Visible="false" />                                                    
                                                        <asp:BoundField DataField="company_name" ControlStyle-Width="94%" HeaderText="Company Name" />
                                                        <asp:BoundField DataField="lob" ControlStyle-Width="94%" HeaderText="LOB" />                                                                                    
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit" >
                                                            <ItemTemplate>
                                                                <asp:Button ID="EditButton" CssClass="GridEditButton" runat="server" Text="" />               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>      
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete" >
                                                            <ItemTemplate>
                                                                <asp:Button ID="DeleteButton" CssClass="GridDeleteButton" runat="server" Text="" />               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>        
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign = "Center" CssClass="GridPager" />
                                                </asp:GridView>
                                                    </div>
                                        </div>
                                    </center>
                                </td>
                            </tr>
                        </table>
                       </div>
                   </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </center>
    </form>
    <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../scripts/ErrorMessage.js" type="text/javascript" lang="javascript"></script>
    <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/Validation.js?"), DateTime.Now.Ticks) %>" type="text/javascript" lang="javascript"></script>
    <script src="js/fastclick.js" type="text/javascript" lang="javascript"></script>
    <script src="js/prism.js" type="text/javascript" lang="javascript"></script>

     <link rel="stylesheet" href="css/jquery-ui.css">    
    <script src="js/jquery.min.js"></script>    
    <script src="js/jquery.easyui.min.js"></script>   
    <style type="text/css">
        body {
            background-color: azure;
        }
        .window-body.panel-body {
               color:silver;              
               padding-top:30px;
               text-align:center;
        }
        .panel-title
        {
            color: greenyellow;
            background-color: #8f0108;
            border: 0px solid #dddddd;    
            text-indent: 5px;    
            border-radius: 5px;
        }
        .l-btn-text
        {
            color:yellow;

        }
         .specify {
        overflow: hidden;
        text-overflow: ellipsis;
        max-height: 90px;
        height: 90px;
        word-break: break-all;
        word-wrap: break-word;
        display: block;
    }
    </style>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadDocument.aspx.cs" Inherits="InGlobal.presentation.User.tpm.UploadDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Trirand" Namespace="Trirand.Web.UI.WebControls" Assembly="Trirand.Web" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
       
    <%--<link href="../../styles/sratstyle.css" rel="Stylesheet" type="text/css" />--%>
    <link href="../../styles/ittpm.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../css/Master.css" />   
    <link rel="stylesheet" type="text/css" href="../../themes/redmond/jquery-ui-1.8.18.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/ui.jqgrid.css" />
    <link rel="stylesheet" type="text/css" href="../../jqdashboard/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../styles/jquery.tooltip.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../styles/screen.css" />
    <link rel="stylesheet" type="text/css" href="../../jqdashboard/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../styles/jquery.loadmask.css" />
    <link rel="stylesheet" type="text/css" href="../../themes/redmond/MultiseletedDropDown.css" />
    
    <script type="text/javascript" src="../../scripts/jquery/jQpop/Scripts/jquery-2.0.1.min.js"></script>
    <link href='../../css/style.css' rel='stylesheet' type='text/css' />
    <link href='../../css/garagedoor.css' rel='stylesheet' type='text/css' />
    <script src="../../scripts/garagedoorjQuery.js" type="text/javascript"></script>
    <script src="../../scripts/Master.js" type="text/javascript"></script>
    <script type="text/javascript">       
        $(function () {
            if (window.parent.parent.IsChildPage() == false) {
                window.location = "Projects.aspx";
            }
        });
        function IsChildPage() {
            return false;
        }      
    </script>
   
</head>

<body style="background-color:#e6e8f8;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmgrProjectDocuments" EnablePageMethods="true" runat="server"
        LoadScriptsBeforeUI="true">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="../../jquery/jquery-1.7.1.min.js" />
                <asp:ScriptReference Path="../../jquery/jquery-ui-1.8.18.custom.min.js" />
                <asp:ScriptReference Path="../../jquery/jqgrid/i18n/grid.locale-en.js" />
                <asp:ScriptReference Path="../../jquery/jqgrid/jquery.jqGrid.min.js" />
                <asp:ScriptReference Path="../../jquery/MultiseletedDropDown.js" />
                <asp:ScriptReference Path="../../jquery/jquery.multiselect.filter.js" />
                <asp:ScriptReference Path="../../jquery/jqdashboard/js/jquery.easyui.min.js" />
                <asp:ScriptReference Path="../../scripts/ErrorMessage.js" />
                <asp:ScriptReference Path="../../scripts/Validation.js" />
                <asp:ScriptReference Path="../../jquery/ui/jquery.ui.autocomplete.js" />
                <asp:ScriptReference Path="../../jquery/jquery.loadmask.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <div>
        <center>
         <table width="100%;">
           
             <tr>

                 <td>
                      <center>
                              <table style="width: 95%">
                <tr>
                    <td>                                              
                        <Trirand:JQGrid ID="jqProjectDocuments" OnDataRequesting="jqProjectDocuments_DataRequesting" runat="server"
                            Width="400" MultiSelect="false" Height="100%" OnRowDeleting="jqProjectDocuments_RowDeleting">
                            <Columns>
                                 <Trirand:JQGridColumn DataField="ID" Editable="false" PrimaryKey="true" Visible="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="ProjectID" Editable="false" DataType="String" Visible="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn Width="150" DataField="DocumentName" HeaderText="Document Name " Editable="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="UploadedBy" Width="150" HeaderText="Details" Editable="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn Width="100" HeaderText="Command " Editable="false">                                 
                                </Trirand:JQGridColumn>                               
                            </Columns>                            
                            <ToolBarSettings ShowRefreshButton="false" ShowSearchButton="false" ShowDeleteButton="true" ShowEditButton="false"  ShowInlineCancelButton="false"></ToolBarSettings>                                                        
                            <AppearanceSettings AlternateRowBackground="true" HighlightRowsOnHover="true" Caption="Project - Documents" />
                            <HierarchySettings HierarchyMode="Parent" />
                            <EditInlineCellSettings Enabled="true" />
                            <PagerSettings PageSize="5" PageSizeOptions="{}" />
                        </Trirand:JQGrid>                      
                    </td>
                </tr>
            </table>
                      </center>
                 </td>
             </tr>
         </table>
        <br />
        </center>
    </div>   
        <input type="hidden" id="hdnSessionState" runat="server" value="" />
    <a id="lSO" href="#" onclick="JavaScript:window.parent.parent.NoSession();">_</a> 
    </form>    
</body>
</html>

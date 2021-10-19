<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="task.aspx.cs" Inherits="InGlobal.presentation.User.tpm.task" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Trirand" Namespace="Trirand.Web.UI.WebControls" Assembly="Trirand.Web" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link href="../../styles/ittpm.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../css/Master.css" />
    <link rel="stylesheet" type="text/css" href="../../themes/redmond/jquery-ui-1.8.18.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/ui.jqgrid.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../styles/jquery.tooltip.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../styles/screen.css" />
    <link rel="stylesheet" type="text/css" href="../../jqdashboard/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../styles/jquery.loadmask.css" />
    <link rel="stylesheet" type="text/css" href="../../themes/redmond/MultiseletedDropDown.css" />
    <link rel="stylesheet" type="text/css" href="../../themes/black/easyui.css" />
  
     <!-- POP WINDOW SECTION -->
    <link type="text/css" href="../../scripts/jquery/jQpop/data/speedo-notify/speedo.notify.fx.css" rel="stylesheet" />
    <link type="text/css" href="../../scripts/jquery/jQpop/data/speedo-notify/skins/agapa/agapa.css" rel="stylesheet" />
    
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/global.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/forms.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/login.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/blog.css"  rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/account.css" rel="stylesheet" />
    <link href='../../scripts/jquery/jQpop/Stylesheets/google-font.css' rel='stylesheet' type='text/css' />
    

    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/landings-jquery-speedo-popup.css" rel="stylesheet" />
    
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/themes/agapa/assets/css/demo_effects.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.fx.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/default/default.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/light/light.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/trap/trap.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/metro/metro.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/lightbox/lightbox.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/notify-glass/notify-glass.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/ignito/ignito.css" rel="stylesheet" />
    <link media="screen" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/blueglass/blueglass.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/dark/dark.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/winter/winter.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/snow/snow.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/rain/rain.css" />
    <link rel="stylesheet" type="text/css" href="../../scripts/jquery/jQpop/data/speedo-popup/skins/clouds/clouds.css" />
    <link type="text/css" rel="stylesheet" href="../../scripts/jquery/jQpop/data/highlighter/css/shCore.css" />
    <link type="text/css" rel="stylesheet" href="../../scripts/jquery/jQpop/data/highlighter/css/shThemeDefault.css" />
    
    <script type="text/javascript" src="../../scripts/jquery/jQpop/Scripts/jquery-2.0.1.min.js"></script>
    <!--[if lt IE 9]>
		<script type="text/javascript" src="../../scripts/jquery/jquery-1.7.1.min.js"></script>
	<![endif]-->
    <!--[if gte IE 9]>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/Scripts/jquery-2.0.1.min.js"></script>
    <!--<![endif]-->
    <!-- IE Fix for HTML5 Tags -->
    <!--[if lt IE 9]>
		<script src="../../scripts/jquery/html5.js"></script>
	<![endif]-->
    <!-- Ionize JS Lang object -->
    <script type="text/javascript">        var Lang = [];
        Lang.get = function (key) { return this[key]; };
        Lang.set = function (key, value) { this[key] = value; };
    </script>
    <script type="text/javascript">
        var base_url = '';
        var page_url = './scripts/jquery/jQpop/products/speedo-popup-jquery-plugin/demo';
    </script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-notify/speedo.notify.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/themes/agapa/assets/js/script.js"></script>
    <!-- if JS needs to get the theme URL, we give it to him -->
    <script type="text/javascript" async="async">
        var theme_url = './scripts/jquery/jQpop/themes/agapa/';
    </script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/winter/winter.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/snow/snow.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/rain/rain.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jQpop/data/speedo-popup/skins/clouds/clouds.js"></script>
    <!-- End Of POP WINDOW SECTION -->

    <link href='../../css/style.css' rel='stylesheet' type='text/css' />
    <link href='../../css/garagedoor.css' rel='stylesheet' type='text/css' />
    <script src="../../scripts/garagedoorjQuery.js" type="text/javascript"></script>
     <script src="../../scripts/Master.js" type="text/javascript"></script>
    <%-- EO Spedo pop up--%>
    
    <script src="../../jquery/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script src="../../jquery/jqdashboard/plugins/jquery.panel.js" type="text/javascript"></script>
    <script src="../../jquery/jqdashboard/plugins/jquery.window.js" type="text/javascript"></script>
    <script src="../../jquery/jqdashboard/plugins/jquery.dialog.js" type="text/javascript"></script>
    <script src="../../jquery/jqdashboard/plugins/jquery.draggable.js" type="text/javascript"></script>
    <script src="../../jquery/jqdashboard/plugins/jquery.droppable.js" type="text/javascript"></script>
    <script src="../../jquery/jqdashboard/plugins/jquery.resizable.js" type="text/javascript"></script>
   
    <script src="../../jquery/jqdashboard/plugins/jquery.messager.js" type="text/javascript"></script>
</head>
<body style="background-color: #e6e8f8;" onload="CheckSession();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmgrProject" EnablePageMethods="true" runat="server"
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
                <asp:ScriptReference Path="../../scripts/jquery/jQpop/data/speedo-popup/speedo.popup.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    
    <div>
        <center>
         <table width="100%;">
            <tr>     
            <td><h2>Tasks Management</h2></td>                           
                
                </tr>
             <tr>

                 <td>
                      <center>
                              <table style="width: 95%">
                <tr>
                    <td>                       
                        <Trirand:JQGrid ID="jqTeamMember" OnDataRequesting="jqTeamMember_DataRequesting"
                            runat="server" MultiSelect="true"  OnCellBinding="jqTeamMember_CellBinding"  MultiSelectMode="SelectOnCheckBoxClickOnly"
                            Height="100%"  AutoWidth="true">
                            <Columns>
                                <Trirand:JQGridColumn DataField="UserID" HeaderText="" PrimaryKey="true" Visible="false"
                                    Editable="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="Name" Width="200"  HeaderText="Member Name " Editable="true">
                                <EditClientSideValidators>
                                        <Trirand:RequiredValidator />                                        
                                    </EditClientSideValidators>
                                </Trirand:JQGridColumn>                                
                                <Trirand:JQGridColumn DataField="EmailID" Width="200" HeaderText="Email" Editable="true">
                                <EditClientSideValidators>
                                        <Trirand:RequiredValidator />
                                        <Trirand:EmailValidator />
                                    </EditClientSideValidators>
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="YOE" Width="85" ShowToolTip="false" HeaderText="Experience(Yr)"
                                    Editable="true" TextAlign="Left">
                                    <EditClientSideValidators>
                                        <Trirand:RequiredValidator />
                                        <Trirand:IntegerValidator />
                                    </EditClientSideValidators>
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="Role" Width="130" ShowToolTip="True"  HeaderText="Role"
                                    Editable="true" EditType="DropDown" EditorControlID="ddlRole" TextAlign="Left">
                                    <EditClientSideValidators>
                                        <Trirand:RequiredValidator />
                                    </EditClientSideValidators>                                    
                                </Trirand:JQGridColumn>
                                
                                <Trirand:JQGridColumn DataField="ProjectID" Editable="false" Visible="false">                                   
                                </Trirand:JQGridColumn>
                            </Columns>
                            <ToolBarSettings>
                            </ToolBarSettings>
                            <HierarchySettings HierarchyMode="Parent" />
                            
                            <ClientSideEvents SubGridBeforeRowExpand="RestMemberTask" SubGridRowExpanded="ShowMemberTask" SubGridRowCollapsed="HideMemberTask"/>                          
                            <PagerSettings PageSize="150" PageSizeOptions="{}" />                          
                                <AppearanceSettings ShowRowNumbers="false" Caption="Project - Team Members" />
                        </Trirand:JQGrid>
                        <Trirand:JQGrid ID="jqTasks" OnDataRequesting="jqTasks_DataRequesting" runat="server"
                            Width="770" MultiSelect="true" OnRowAdding="jqTasks_RowAdding" OnRowEditing="jqTasks_RowEditing" 
                             MultiSelectMode="SelectOnCheckBoxClickOnly" Height="100%" OnRowDeleting="jqTasks_RowDeleting">
                            <Columns>
                                 <Trirand:JQGridColumn DataField="ID" Editable="false" PrimaryKey="true" Visible="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn Width="100" DataField="ProjectName" EditType="DropDown" DataType="String" 
                                    EditorControlID="ddlProject" HeaderText="Project " Editable="true">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn Width="170" DataField="TaskHeader" HeaderText="Task " Editable="true">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="TaskDetails" Width="300" HeaderText="Details" Editable="true">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn Width="10" DataField="AssignedBy" HeaderText="Assigned By " Editable="false" Visible="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn Width="100" DataField="FullName" HeaderText="Assigned By " Editable="false">
                                </Trirand:JQGridColumn>
                                  <Trirand:JQGridColumn Width="75" DataField="Status" HeaderText="Status " EditType="DropDown" DataType="String" 
                                    EditorControlID="ddlStatus" Editable="true"> </Trirand:JQGridColumn>
                            </Columns>
                            <ClientSideEvents  LoadComplete="jqTasks_LoadComplete()" AfterSubmitCell="jqTasks_AfterSubmitCell" 
                                AfterAddDialogRowInserted="jqTasks_AfterRowInserted" LoadDataError="jqTasks_LoadDataError" AfterDeleteDialogRowDeleted="jqTasks_RowDeleted" />
                            <ToolBarSettings ShowRefreshButton="false" ShowSearchButton="false" ShowDeleteButton="true" ShowEditButton="false"  ShowInlineCancelButton="true"></ToolBarSettings>                                                        
                            <AppearanceSettings AlternateRowBackground="true" HighlightRowsOnHover="true" Caption="Team Member - Task" />
                            <HierarchySettings HierarchyMode="Child" />
                            <EditInlineCellSettings Enabled="true" />
                            <PagerSettings PageSize="5" PageSizeOptions="{}" />
                        </Trirand:JQGrid>
                        `
                        <Trirand:JQDatePicker ID="endDate" runat="server" DateFormat="mm/dd/yyyy" AltFormat="mm/dd/yyyy" DisplayMode="ControlEditor" ShowOn="Focus" />
                        <Trirand:JQDatePicker ID="startDate" runat="server" DateFormat="mm/dd/yyyy" AltFormat="mm/dd/yyyy" DisplayMode="ControlEditor" ShowOn="Focus" />
                        <Trirand:JQDatePicker ID="targetEndDate" runat="server" DateFormat="mm/dd/yyyy" AltFormat="mm/dd/yyyy" DisplayMode="ControlEditor" ShowOn="Focus" />
                        <asp:DropDownList ID="ddlRole" Width="100px" runat="server">                            
                            <asp:ListItem value="JRDEV">Jr. Developer</asp:ListItem>
                            <asp:ListItem value="SRDEV">Sr. Developer</asp:ListItem>
                            <asp:ListItem value="ML">Module Lead</asp:ListItem>
                            <asp:ListItem value="TL">Team Lead</asp:ListItem>
                            <asp:ListItem value="PL">Project Lead</asp:ListItem>
                            <asp:ListItem value="PM">Project Manager</asp:ListItem>
                            <asp:ListItem value="TPM">Tech. Project Manager</asp:ListItem>
                            <asp:ListItem value="OTM">Other Team Member</asp:ListItem>
                        </asp:DropDownList>
                         <asp:DropDownList ID="ddlStatus" Width="100px" runat="server">                            
                            <asp:ListItem value="WIP">WIP</asp:ListItem>
                            <asp:ListItem value="Completed">Completed</asp:ListItem>
                            <asp:ListItem value="NotStated">Not Stated</asp:ListItem>                          
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlProject" Width="100px" runat="server">                                                       
                        </asp:DropDownList>
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
<a style="position:absolute; bottom:0px; right:0px; " href="#" class="LogoutLink" onclick="JavaScript:window.parent.Logmeout();">&nbsp;&nbsp;Logout&nbsp;&nbsp;</a>
 <a id="lSO" href="#" onclick="JavaScript:window.parent.NoSession();">_</a>
    </form>
    <script type="text/javascript">

        var expandedMemberTaskRIDs = new Array();
        function ShowMemberTask(subgrid_id, row_id) {
                       
            showSubGrid_jqTasks(subgrid_id, row_id, "", "jqTasks"); 
            expandedMemberTaskRIDs.push(row_id);
            $('.ui-icon.ui-icon-pencil').hide();
           
        }
        function HideMemberTask(subgrid_id, row_id) {
                      
            RemoveArrayItem(expandedMemberTaskRIDs,row_id);

        }
        function RemoveArrayItem(arr, item) {
            for (var i = arr.length; i--;) {
                if (arr[i] === item) {
                    arr.splice(i, 1);
                }
            }
        }
        function jqTeamMember_LoadComplete() {

                CheckSession();
               
                if (expandedMemberTaskRIDs != null) {
                    jQuery.each(expandedMemberTaskRIDs, function (index, rowId) {
                        $('#jqTeamMember').expandSubGridRow(rowId);
                    });
                }
                expandedMemberTaskRIDs = null;
            
        }
        function jqTasks_LoadComplete() {
            CheckSession();
            $('.ui-icon.ui-icon-pencil').hide();
            $('#jqTasks_ilcancel').css('padding-left', '7px');
            $('#jqTasks_ilcancel').attr('class', 'ui-pg-button ui-corner-all ui-state-disabled');
            $('#jqTasks_ilsave').attr('class', 'ui-pg-button ui-corner-all ui-state-disabled');
           
        }
        function jqTasks_AfterSubmitCell(serverresponse, rowid, cellname, value, iRow, iCol) {
            CheckSession();
            var ErrMsg = serverresponse.getResponseHeader("ErrorMsg");

            if (ErrMsg != null) {

                if (ErrMsg, indexOf("NoSession:") > -1) {

                    $('#lSO').trigger("click");
                }
                else if (ErrMsg == "Exception") {
                    window.parent.ShowExceptionWindow();
                    return [false, ""];
                }
                if (ErrMsg == "Success") {
                    window.parent.CallSuccess();
                    return [false, ""];
                }
                else {
                    ShowTPMIntimation(ErrMsg, 'Error');
                    $('#jqTeamMember').jqGrid().trigger("reloadGrid");
                    return [false, ""];
                }

            }

            return [true, ""];

        }

        function jqTasks_LoadDataError (xhr, status, error) {
            var ErrMsg = error.getResponseHeader("ErrorMsg");
            ShowTPMIntimation(ErrMsg, 'Error');
            CheckSession();
        }


        function jqTasks_RowDeleted(response, formData, formid) {
            var DeleteSuccess = response.getResponseHeader("DeleteSuccess");           
            var DeleteFailed = response.getResponseHeader("DeleteFailed");
            ShowTPMIntimation(DeleteSuccess, 'Success');
            ShowTPMIntimation(DeleteFailed, 'Failed');
            CheckSession();
        }
        function jqTasks_AfterRowInserted(response, formData, formid) {
            var AddSuccess = response.getResponseHeader("AddSuccess");          
            var AddFailed = response.getResponseHeader("AddFailed");
            ShowTPMIntimation(AddSuccess, 'Success');
            ShowTPMIntimation(AddFailed, 'Failed');
            CheckSession();
        }
        function ResetMemberTask(rowId, selected) {

            var rowIds = $("#jqTeamMember").getDataIDs();
            $.each(rowIds, function (index, rowId) {
                $("#jqTeamMember").collapseSubGridRow(rowId);
            });
        }
    </script>
</body>
</html>

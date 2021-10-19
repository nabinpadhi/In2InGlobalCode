<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="projects.aspx.cs" Inherits="InGlobal.presentation.User.tpm.projects" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Trirand" Namespace="Trirand.Web.UI.WebControls" Assembly="Trirand.Web" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITTPM - IT Project Management</title>
    <%--<link href="../../styles/sratstyle.css" rel="Stylesheet" type="text/css" />--%>
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
        <table width="100%;">
            <tr>
                <td style="width: 33%">
                </td>
                <td style="width: 34%">
                    <h2>
                        Project Management</h2>
                </td>
                <td style="width: 33%; text-align: right;">
                <a style="position: absolute; top: 0px; right: 0px;" href="#" class="LogoutLink"
        onclick="JavaScript:window.parent.Logmeout();">&nbsp;&nbsp;Logout&nbsp;&nbsp;
    </a>
                </td>
            </tr>
        </table>
        <br />
        <center>
            <table style="width: 100%">
                <tr>
                    <td style="width:75%;vertical-align:top;">
                        <Trirand:JQGrid ID="jqGridTPMProjects" runat="server" AutoWidth="true" MultiSelect="true"
                            MultiSelectMode="SelectOnCheckBoxClickOnly" Height="100%" OnDataRequesting="jqGridTPMProjects_DataRequesting"
                            OnRowAdding="jqGridTPMProjects_RowAdding" OnCellBinding="jqGridTPMProjects_CellBinding" 
                            OnRowDeleting="jqGridTPMProjects_RowDeleting"  OnRowEditing="jqGridTPMProjects_RowEditing">
                            <Columns>
                                <Trirand:JQGridColumn DataField="ID" Editable="false" PrimaryKey="true" Visible="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="Name" HeaderText="Project Name" Editable="true"
                                    TextAlign="Left" Width="20">
                                    <EditClientSideValidators>
                                        <Trirand:RequiredValidator />
                                    </EditClientSideValidators>
                                    <SearchOptions><Trirand:JQGridColumnSearchOption SearchOperation="Contains" /></SearchOptions>
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="Team Size" DataField="TeamSize" EditType="TextBox"
                                    Editable="true" Width="10">
                                    <EditClientSideValidators>
                                        <Trirand:RequiredValidator />
                                        <Trirand:IntegerValidator />
                                    </EditClientSideValidators>
                                    <%-- <Formatter>
                                                                    <Trirand:CheckBoxFormatter Enabled="true" />
                                                                </Formatter>  --%>
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="Client" DataField="ClientName" EditType="TextBox"
                                    Editable="true" Width="10">
                                    <EditClientSideValidators>
                                        <Trirand:RequiredValidator />                                        
                                    </EditClientSideValidators>
                                    <SearchOptions><Trirand:JQGridColumnSearchOption SearchOperation="Contains" /></SearchOptions>
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:MM/dd/yyyy}"
                                    EditType="DatePicker" EditorControlID="startDate" Editable="true" Width="10">
                                    <EditClientSideValidators>
                                    <Trirand:DateValidator  />
                                        <Trirand:RequiredValidator />
                                    </EditClientSideValidators>
                                    <%-- <Formatter>
                                                                    <Trirand:CheckBoxFormatter Enabled="true" />
                                                                </Formatter>  --%>
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="End Date" DataField="EndDate" DataFormatString="{0:MM/dd/yyyy}"
                                    EditType="DatePicker" Editable="true" EditorControlID="endDate" Width="10">                                    
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="Target Date" DataField="TargetEndDate" EditType="DatePicker"
                                    Editable="true" EditorControlID="targetEndDate" DataFormatString="{0:MM/dd/yyyy}"
                                    Width="10">
                                    <EditClientSideValidators>
                                        <Trirand:RequiredValidator />
                                        <Trirand:DateValidator />
                                        <Trirand:DateValidator />
                                    </EditClientSideValidators>
                                    <%-- <Formatter>
                                                                    <Trirand:CheckBoxFormatter Enabled="true" />
                                                                </Formatter>  --%>
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="Status" DataField="status" Editable="false"  Width="5">  
                                 
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn  Visible="false" DataField="DocumentsUploaded"> </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="Documents" Sortable="false" TextAlign="Center"  Editable="false" Width="10">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn HeaderText="Commands" Sortable="false" Editable="false" Width="12">
                                </Trirand:JQGridColumn>
                                
                               
                                
                            </Columns>
                            <ExportSettings ExportDataRange="All" />
                            <ClientSideEvents SubGridRowExpanded="ShowTeamMember" BeforeSubmitCell="jqGridTPMProjects_BeforeSumbitCell" AfterEditCell="jqGridTPMProjects_AfterEditCell" LoadComplete="jqGridTPMProjects_LoadComplete"
                                AfterSubmitCell="jqGridTPMProjects_AfterSubmitCell" LoadDataError="jqGridTPMProjects_LoadDataError" AfterDeleteDialogRowDeleted="jqGridTPMProjects_RowDeleted" 
                                BeforeEditCell="jqGridTPMProjects_BeforeEdit" SubGridBeforeRowExpand="ResetTeamMember" />
                            <%-- AfterDeleteDialogRowDeleted="RowDeleted"
                            LoadDataError="jqRAScoping_LoadDataError" AfterSaveCell="OnAfterSaveCell" AfterSubmitCell="jqRAScoping_AfterSubmitCell"
                            AfterEditCell="OnAfterEditCell"  BeforeEditCell="jqGridTPMProjects_BeforeEdit"--%>
                            <ToolBarSettings ShowDeleteButton="true" ShowEditButton="false"  ShowInlineCancelButton="true">
                            </ToolBarSettings>
                            <AddDialogSettings Width="400" Height="400" ReloadAfterSubmit="true" SubmitText="Add Project"
                                ClearAfterAdding="true" Caption="Add Project" />
                            <HierarchySettings HierarchyMode="Parent" />
                            <EditInlineCellSettings Enabled="true" />
                            <PagerSettings NoRowsMessage="No project added."  PageSize="5" PageSizeOptions="{}" />
                            <DeleteDialogSettings Modal="true" DeleteMessage="<br><center><span style='color:red'>Are you sure you want to delete the selected Project(s)?</span></center><br>"
                                Width="400" />
                            <EditDialogSettings  CancelText="Cancel the Editing" Caption="Edit caption goes here"
                                SubmitText="Go edit that!" TopOffset="50" LeftOffset="50" />
                                <SearchToolBarSettings SearchToolBarAction="SearchOnEnter" />   
                                <AppearanceSettings  ShowRowNumbers="false" Caption="TPM - Projects" />
                                
                        </Trirand:JQGrid>
                        <Trirand:JQGrid ID="jqTeamMember" OnDataRequesting="jqTeamMember_DataRequesting"
                            runat="server" MultiSelect="true" OnCellBinding="jqTeamMember_CellBinding"  MultiSelectMode="SelectOnCheckBoxClickOnly"
                            Height="100%"  Width="100%" OnRowAdding="jqTeamMember_RowAdding" OnRowDeleting="jqTeamMember_RowDeleting" 
                            OnRowEditing="jqTeamMember_RowEditing">
                            <Columns>
                                <Trirand:JQGridColumn DataField="UserID" HeaderText="" PrimaryKey="true" Visible="false"
                                    Editable="false">
                                </Trirand:JQGridColumn>
                                <Trirand:JQGridColumn DataField="Name" Width="180"  HeaderText="Member Name " Editable="true">
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
                                <Trirand:JQGridColumn DataField="Role" Width="132" ShowToolTip="True"  HeaderText="Role"
                                    Editable="true" EditType="DropDown" EditorControlID="ddlRole" TextAlign="Left">
                                    <EditClientSideValidators>
                                        <Trirand:RequiredValidator />
                                    </EditClientSideValidators>                                    
                                </Trirand:JQGridColumn>
                                
                                <Trirand:JQGridColumn DataField="ProjectID" Editable="false" Visible="false">                                   
                                </Trirand:JQGridColumn>
                            </Columns>
                            <ToolBarSettings ShowDeleteButton="true" ShowInlineAddButton="true" ShowAddButton="false">
                            </ToolBarSettings>
                            <HierarchySettings HierarchyMode="Child" />
                            
                            <ClientSideEvents LoadDataError="jqTeamMember_LoadDataError" 
                            AfterDeleteDialogRowDeleted="jqTeamMember_RowDeleted"  LoadComplete="jqTeamMember_LoadComplete" />
                            <EditInlineCellSettings Enabled="true" />
                            <PagerSettings PageSize="150" PageSizeOptions="{}" />
                            <DeleteDialogSettings Modal="true"  DeleteMessage="<br><center>Are you sure you want to exclude selected member(s) ?</center><br>"
                                Width="400" />
                            <EditDialogSettings CancelText="Cancel the Editing" Caption="Edit the Dialog" SubmitText="Go edit that!"
                                TopOffset="50" LeftOffset="50" />
                                <AppearanceSettings ShowRowNumbers="false" Caption="Project - Team Members" />
                        </Trirand:JQGrid>
                       
                        <Trirand:JQDatePicker ID="endDate" runat="server" MinDate="01/01/2000" DateFormat="mm/dd/yyyy" AltFormat="mm/dd/yyyy" DisplayMode="ControlEditor" ShowOn="Focus" />
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
                    </td>
                    <td style="width:25%;background-color:blue;height:400px;">&nbsp;</td>
                </tr>
                
            </table>
            
        </center>
    </div>
    <input type="hidden" id="hdnSessionState" runat="server" value="" />
    <a id="lSO" href="#" onclick="JavaScript:window.parent.NoSession();">_</a>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#jqGridTPMProjects_iladd').click(function () {
                if ($('.edit-cell.ui-state-highlight').html() != null) {

                    $('.edit-cell.ui-state-highlight').attr('tabindex', '-1');
                    $('.edit-cell.ui-state-highlight').html($('.edit-cell.ui-state-highlight').attr("title"));

                    $('.ui-widget-content.jqgrow.ui-row-ltr.selected-row.ui-state-hover').attr("class", "ui-widget-content jqgrow ui-row-ltr");
                    //$('#jqGridTPMProjects').jqGrid().trigger("reloadGrid");

                }
            });

        });

        var expandedTeamMemberRIDs;
        var expandedMemberTaskRIDs;
        $(function () {
            if (window.parent.IsChildPage() == false) {
                window.location = "../TPMLanding.aspx";
            }
        });
        function IsChildPage() {
            return false;
        }
        var oldValue;
        function Logout() {
            window.parent.location = "../../Index.aspx?a=lo";
        }
        function GetTodayDate() {
            var date = new Date();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            if (month < 10) {
                month = "0" + month;
            }
            if (day < 10) {
                day = "0" + day;
            }
            var todayDate = month + "/" + day + "/" + date.getFullYear();

            return todayDate;

        }

        function ShowTeamMember(subgrid_id, row_id) {
            
            showSubGrid_jqTeamMember(subgrid_id, row_id, "", "jqTeamMember");
                        
            $('#' + subgrid_id + '_tjqTeamMember_ilcancel').attr('class', 'ui-pg-button ui-corner-all ui-state-disabled');
            $('#' + subgrid_id + '_tjqTeamMember_ilsave').attr('class', 'ui-pg-button ui-corner-all ui-state-disabled');
            
            $('.ui-jqgrid-bdiv').css('width', '100%');
            $('.ui-state-default.ui-jqgrid-hdiv').css('width', '99%');
            $('.ui-jqgrid.ui-widget.ui-widget-content.ui-corner-all').css('width', '99%');
            $('.ui-jqgrid-view').css('width', '99%');
            $('.ui-icon.ui-icon-pencil').hide();
            var expandedTMRIDs = new Array();

            expandedTMRIDs.push(row_id);
            expandedTeamMemberRIDs = expandedTMRIDs;
            
        }
       
        /******************jqGridTPMProjects*****Events*********************/
        function jqGridTPMProjects_LoadComplete() {            
            CheckSession();
            $('.ui-icon.ui-icon-pencil').hide();
            $('#jqGridTPMProjects_ilcancel').css('padding-left', '7px');
            $('#jqGridTPMProjects_ilcancel').attr('class', 'ui-pg-button ui-corner-all ui-state-disabled');
            $('#jqGridTPMProjects_ilsave').attr('class', 'ui-pg-button ui-corner-all ui-state-disabled');

            if (expandedTeamMemberRIDs != null) {
                jQuery.each(expandedTeamMemberRIDs, function (index, rowId) {
                    $('#jqGridTPMProjects').expandSubGridRow(rowId);
                });
            }           
            expandedTeamMemberRIDs = null;
           
            ConfigProjectDocument();
           
        }
        function jqGridTPMProjects_AfterSubmitCell(serverresponse, rowid, cellname, value, iRow, iCol) {
            CheckSession();
            var ErrMsg = serverresponse.getResponseHeader("ErrorMsg");

            if (ErrMsg != null) {

                if (ErrMsg,indexOf("NoSession:") >-1) {

                    $('#lSO').trigger("click");
                }
                else if (ErrMsg== "Exception") {
                    window.parent.ShowExceptionWindow();
                    return [false, ""];
                }
                if (ErrMsg == "Success") {
                    window.parent.CallSuccess();
                    return [false, ""];
                }
                else {
                    ShowTPMIntimation(ErrMsg, 'Error');
                    $('#jqGridTPMProjects').jqGrid().trigger("reloadGrid");
                        return [false, ""];
                    }
                    
            }
               
            return [true, ""];

        }

        function jqGridTPMProjects_LoadDataError(xhr, status, error) {
            var ErrMsg = error.getResponseHeader("ErrorMsg");
            ShowTPMIntimation(ErrMsg, 'Error');
            CheckSession();
        }
        

        function jqGridTPMProjects_RowDeleted(response, formData, formid) {
            var DeleteSuccess = response.getResponseHeader("DeleteSuccess");
            ShowTPMIntimation(DeleteSuccess, 'Success');
            var DeleteFailed = response.getResponseHeader("DeleteFailed");
            ShowTPMIntimation(DeleteFailed, 'Failed');
            CheckSession();
        }
        function jqGridTPMProjects_BeforeEdit(rowid, cellname, value, iRow, iCol) {
            //alert("rowid-" + rowid +",cellname-" + cellname+",value," + value +", iRow," + iRow +",iCol-" +iCol);

        }
        function jqGridTPMProjects_AfterEditCell(rowid, cellname, value, iRow, iCol) {
            CheckSession();
            /* var inputControl = jQuery('#' + (iRow) + '_' + cellname);
            window.setTimeout(function () {
            inputControl.focus();
            }, 10);
            //$('#jqGridRAScoping_ilcancel').removeClass('ui-state-disabled');
            //window.parent.SetFrameHeight();*/
        }
        function jqGridTPMProjects_BeforeSumbitCell(rowId, name, value, iRow, iCol) {
            if (name == 'StartDate' || name == 'EndDate' || name == 'TargetEndDate') {
                var _startdate, _enddate, _targetdate = new Date();


                _startdate = $('#jqGridTPMProjects').getCell(rowId, "StartDate");
                _enddate = $('#jqGridTPMProjects').getCell(rowId, "EndDate");
                _targetdate = $('#jqGridTPMProjects').getCell(rowId, "TargetEndDate");

                var _cellValue = '<div style=\'width:100%;height:100%;background-color:@bgVal;\'>&nbsp;</div>';

                if (_startdate > GetTodayDate() || _enddate == "") {

                    _cellValue = _cellValue.replace("@bgVal", "Orange");
                }

                if (_enddate > _targetdate) {
                    _cellValue = _cellValue.replace("@bgVal", "Red");
                }
                if (_enddate <= _targetdate) {
                    _cellValue = _cellValue.replace("@bgVal", "Green");
                }
                $('#jqGridTPMProjects').setCell(rowId, "status", _cellValue);
            }
        }
        /************End of******jqGridTPMProjects*****Events*********************/
        
        /******************jqTeamMember*****Events*********************/
        function jqTeamMember_RowDeleted(response, formData, formid) {
            var DeleteSuccess = response.getResponseHeader("DeleteSuccess");
            ShowTPMIntimation(DeleteSuccess, 'Success');
            var DeleteFailed = response.getResponseHeader("DeleteFailed");
            ShowTPMIntimation(DeleteFailed, 'Failed');
            CheckSession();
            
        }
        function jqTeamMember_LoadComplete() {
        
            CheckSession();
            $('.ui-icon.ui-icon-pencil').hide();            
            expandedMemberTaskRIDs = null;
            if (currentActiveCell != null) {

                $(currentActiveCell).parent().trigger('click');
                $(currentActiveCell).parent().focus();
            }
        }
        function jqTeamMember_AfterSubmitCell(serverresponse, rowid, cellname, value, iRow, iCol) {

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
                    return [false, ""];
                }

            }

            return [true, ""];

        }
        
        function jqTeamMember_LoadDataError(xhr, status, error) {

            CheckSession();
            var ErrMsg = error.getResponseHeader("ErrorMsg");
            ShowTPMIntimation(ErrMsg, 'Error');
        }
        function ConfigProjectDocument()
        {
            $('.UploadDocument,.DocumentDownload').click(function () {

                var skinName = "winter";
                var css3Effects = "rollOut";
                var popCaption = 'Project\'s Document(s) - Download';
                var href = "UploadDocument.aspx?t=d";
                if ($(this).attr('class') == "UploadDocument")
                {
                   
                    css3Effects = "rollIn";
                    var href = "UploadDocument.aspx?t=u";
                    popCaption = 'Project\'s Document(s) - Upload';
                }
               
                var effect = "fade";
                
                var width = 450;
                var height = 200;
                
                popLogin = $("body").speedoPopup(
                {
                    href: href,
                    height: height,
                    width: width,
                    theme: skinName,
                    unload: true,
                    draggable: true,
                    close: true,
                    autoClose: false,
                    effectIn: effect,
                    effectOut: effect,
                    css3Effects: css3Effects,
                    overlayClose:false
                });
                AddTitle(popCaption, '450px');
                return false;
            });
        }
        function AddTitle(caption, width) {

            var rawWidth = width;
            rawWidth = rawWidth.replace('px', '');
            var left = ((rawWidth / 2) - 10) + 'px';
            var Popcaption = "<label style='font-weight:bold;margin-left:" + left + ";margin-top:5px'>";
            var wincap = $(Popcaption).text(caption);
            $('.speedo-popup-drag-area').append(wincap);
        }
        function ResetTeamMember(rowId, selected) {

            var rowIds = $("#jqGridTPMProjects").getDataIDs();
            $.each(rowIds, function (index, rowId) {
                $("#jqGridTPMProjects").collapseSubGridRow(rowId);
            });
        }
    </script>
</body>
</html>

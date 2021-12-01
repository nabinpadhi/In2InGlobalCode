﻿<%@ Page Language="C#" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="FileManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.FileManagement" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="vendor/jquery/jquery.min.js"></script>
    <script type="text/javascript">
</script>
    <link href="//netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.no-icons.min.css" rel="stylesheet">
    <link href="//netdna.bootstrapcdn.com/font-awesome/3.2.1/css/font-awesome.css" rel="stylesheet">
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />   
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        body {
            background-color: azure;
        }
        .GridViewImageAlignment{
            text-align:center;
        }
        .file_table {
            width: 100%;
        }

        .file_table_header {
            border-bottom: 1px solid #ffb215;
            padding: 8px;
        }

            .file_table_header th {
                background-color: #4472c4;
                color: white;
                border-bottom: 1px solid black;
            }

        .file_table tr td {
            border-bottom: 1px solid #ccc;
        }

        ul {
            list-style: none;
            padding: 0;
        }

        li {
            padding-left: 1.3em;
        }

        li:before {
            content: "\f00c"; /* FontAwesome Unicode */
            font-family: FontAwesome;
            display: inline-block;
            margin-left: -1.3em; /* same as padding-left set on li */
            width: 1.3em; /* same as padding-left set on li */
        }          
    </style> 
     <script lang="JavaScript">
         function __doPostBack(eventTarget, eventArgument) {
             documenT.Form1.__EVENTTARGET.value = eventTarget;
             document.Form1.__EVENTARGUMENT.value = eventArgument;
             document.Form1.submit();
         }
</script>
</head>
<body style="background-color:azure;">
   
    <form id="form1" runat="server">
        <center>
            <div id="fmPageDiv" style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 20px;display:block;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height:40px;padding-top:10px;"><span class="menu_frame_title">File Management</span></div>
                    <asp:ScriptManager ID="scriptmanager1" runat="server">
                </asp:ScriptManager>                
                <asp:UpdatePanel ID="pdnlFileMgnt" runat="server">
                    <Triggers><asp:PostBackTrigger ControlID="btnDownload" /></Triggers>
                    <Triggers><asp:PostBackTrigger ControlID="btnUploader" /></Triggers>
                    <ContentTemplate> 
                         <div style="border-bottom:0 solid gray;display:flex;padding:2px;width:auto;">
                            <div id="btnProjectMgnt" onclick="ShowProjectMgnt();" class="PanelTab">Project Management</div>
                            <div id="btnFileMgnt" onclick="ShowFileMgnt();" class="PanelTab">File Management</div>                          
                        </div>
                        <input type="hidden" value="" id="__EVENTARGUMENT" name="__EVENTARGUMENT">
                        <input type="hidden" value="" id="__EVENTTARGET" name="__EVENTTARGET">
                        <div title="Project Management" class="projectmgnt" style="background-color: azure;">
                               <table style="width: 100%; background-color: azure;">
                                <tr>
                                    <td>
                                        <center>
                                            <div style="width: 60%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                                <table>
                                                    <tr>
                                                        <td style="width:50%;">
                                                            <table style="width:100%">
                                                                <tr>                                                                    
                                                                    <td style="width:40%">
                                                                        Project Name(<span style="color: red">*</span>)<br />
                                                                        <span fieldtype="readonly" value="" runat="server" id="spnProjectName" />
                                                                        <asp:HiddenField ID="hdnPName" runat="server" Value="" />
                                                                         <asp:HiddenField ID="hdnProjectToEdit" runat="server" Value="" />
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td>
                                                                        Created By(<span style="color: red">*</span>)                                                                        
                                                                        <span fieldtype="readonly" value="" runat="server" id="spnCreatedBy" />
                                                                    </td>
                                                                </tr>                                                                                                         
                                                            </table>
                                                        </td>
                                                        <td  style="width:50%;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Description(<span style="color: red">*</span>)
                                                                        <textarea rows="5" id="txtDescription" class="txtDescription" name="txtDescription" style="resize:none; width: 97%; height: 70px;" runat="server"></textarea>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="margin-top: 15px;">
                                                                <center>
                                                                    <asp:Button  ID="btnCreateProject" runat="server" OnClientClick="return ValidateProject();" OnClick="btnCreateProject_Click"  CssClass="button" Text="Create" />
                                                                    <input type="button" class="button" style="margin-left: 10px;" value="Cancel" onclick="ClearProject();" />
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
                                            <div style="width: 70%; border: 1px solid black; border-radius: 5px; margin-top:5px; margin-bottom: 10px;">                                    
                                                <asp:GridView DataKeyNames="project_id" ID="grdProject" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                    AllowPaging="True" AllowSorting="true" OnSorting="grdProject_Sorting" OnRowDataBound="grdProject_RowDataBound" OnRowDeleting="grdProject_RowDeleting" 
                                                    OnPageIndexChanging="grdProject_PageIndexChanging"  AutoGenerateColumns="false" PageSize="4">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>                                                      
                                                         <asp:TemplateField HeaderStyle-Width="150px"  HeaderText="Project Name" SortExpression="ProjectName">                                                            
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("project_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Created By" SortExpression="CreatedBy">                                                            
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Bind("created_by") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                          
                                                        <asp:BoundField ItemStyle-Wrap="true" HeaderText="Description"  DataField="description" />  
                                                       <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                                                      <ItemTemplate >                                                      
                                                          <asp:LinkButton href="#" runat="server" id="lnkDel" >Delete</asp:LinkButton>   <asp:LinkButton href="#" runat="server" id="lnkEdit" >Edit</asp:LinkButton>
                                                      </ItemTemplate>                                               
                                                    </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </center>
                                    </td>
                                </tr>
                        </table>
                        </div>
                        <div title="File Management" class="filemgnt" style="background-color: azure;">
                            <table style="width: 100%; background-color: azure;">
                                <tr>
                                    <td style="width: 70%;">
                                        <table style="width: 100%; margin-top: 25px;">                               
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <div style="border: 1px solid black; margin-left: auto; margin-right: auto; border-radius: 5px">
                                                        <table style="width: 100%; margin-top: auto; margin-left: auto; margin-right: auto;">
                                                            <tr>
                                                                <td style="width: 60%; vertical-align:top;">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 50%;text-align:left;padding-left:25px;">
                                                                                 <b>Select Project</b>
                                                                                <div style="width:100px;text-align:left;">
                                                                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedProject_SelectedIndexChanged"  DataValueField="ProjectName" DataTextField="ProjectName"  ID="ddlAssignedProject" AppendDataBoundItems="true" runat="server">
                                                                                        <asp:ListItem Text="--Select a Project--"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: 5%"></td>
                                                                            <td style="width: 45%;margin-left:0px;text-align:left;">
                                                                                <b>Select Template</b>
                                                                                <div style="text-align:left;">
                                                                                    <asp:DropDownList OnSelectedIndexChanged="LoadInstruction" Enabled="false" DataTextField="TemplateName" AutoPostBack="true" DataValueField="Path"  ID="ddlTemplate" AppendDataBoundItems="true" runat="server">
                                                                                        <asp:ListItem Text="--Select a Template--"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td></td>
                                                                            <td style="text-align: right;">
                                                                                <asp:Button CssClass="button" Enabled="false" OnClientClick="return ValidateDownload();" OnClick="btnDownload_Click" Text="Download" ID="btnDownload" runat="server"></asp:Button></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">                                                                    
                                                                                <center>
                                                                                    <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                                                                        <asp:GridView DataKeyNames="ID" ID="grdUploadedFiles" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                                                            AllowPaging="True" RowStyle-Wrap="false" HeaderStyle-Wrap="false" EmptyDataText="No file found uploaded by you." OnPageIndexChanging="grdUploadedFiles_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                                                                            <PagerStyle CssClass="pagination-ys" />
                                                                                            <Columns>
                                                                                                    <asp:TemplateField ItemStyle-Width="150px" HeaderStyle-Width="150px" HeaderText="File Name">
                                                                                                        <ItemTemplate>
                                                                                                            <a href='#' onclick="OpenCSV('<%# DataBinder.Eval(Container.DataItem, "FileName") %>');">
                                                                                                                <%# DataBinder.Eval(Container.DataItem, "FileName")%>
                                                                                                            </a>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>                                                                                                
                                                                                                <asp:BoundField ItemStyle-Width="150px" HeaderStyle-Width="150px" HeaderText="Project Name" DataField="ProjectName" />
                                                                                                <asp:BoundField ItemStyle-Width="150px" HeaderStyle-Width="150px" HeaderText="Uploaded By" DataField="UploadedBy" />
                                                                                                <asp:BoundField ItemStyle-Width="150px" HeaderStyle-Width="150px" HeaderText="Uploaded On" DataField="Date" />
                                                                                                <asp:ImageField ItemStyle-Width="50px" HeaderStyle-Width="50px" ItemStyle-CssClass ="GridViewImageAlignment" HeaderText="Status" ControlStyle-Height="20px" ControlStyle-Width="20px" DataImageUrlField="UploadedStatus"></asp:ImageField>                                                                                             
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </center>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left;">
                                                                                <asp:FileUpload ID="fileUploader" accept=".csv" Enabled="false" runat="server" />                                                                                                                                                    
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnUploader" Enabled="false" class="button" OnClientClick="return VerifyFile();"  runat="server" Text="Upload" OnClick="btnUploader_Click" />
                                                                            </td>
                                                                
                                                                            <td>
                                                                            </td>

                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 35%;">
                                                                    <div style="width: 95%; text-align: left; margin-left: 15px; height: 220px; overflow-y: auto;">
                                                                        <b>Template Instruction</b><br />
                                                                        <p></p>
                                                                        <ul runat="server" style="width:80%;word-wrap:break-word;" id="tplInstruction">                                                              

                                                                        </ul>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 30%; vertical-align: top;">
                                        <div style="margin-top: 10px; margin-left: 20px; border-left: 1px solid gray; height: 300px;">
                                            <span style="margin-left: 10px;"><b>Search Template </b></span>
                                            <table style="width: 100%; margin-left: 5px;margin-right: 5px;">
                                                <tr id="usrEmailTR" runat="server">
                                                    <td style="width: 35%">
                                                        <b><u>Email ID</u> : </b>
                                                    </td>
                                                    <td style="width: 65%;">
                                                        <asp:TextBox Text="" ID="usrEmailId" AutoPostBack="true" OnTextChanged="usrEmailId_TextChanged" runat="server" style="width: 80%" ></asp:TextBox>                                            
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b><u>Project ID</u> : </b>
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <asp:DropDownList OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" ID="ddlProjects" Width="85%" runat="server" DataTextField="ProjectName">
                                                                    <asp:ListItem Text="--Select a Project--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div style="width: 100%">
                                                            <table id="tblTemplateDetail" runat="server" style="width: 100%;">
                                                    
                                                                <tr>
                                                                    <td><b><u>Template Details:</u></b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="margin-left: auto; margin-top: auto;">
                                                                        <asp:GridView ID="grdTemplate" EmptyDataText="No Template Found..." Visible="false" runat="server" Width="95%" HeaderStyle-CssClass="pagination-ys"
                                                                            AllowPaging="True" OnPageIndexChanging="grdTemplate_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                                                            <PagerStyle CssClass="pagination-ys" />
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Template Name" DataField="TemplateName" />
                                                                                <asp:BoundField HeaderText="DateAdded" DataField="DateAdded" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
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
     <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/ErrorMessage.js?"), DateTime.Now.Ticks) %>" type="text/javascript" lang="javascript"></script>
     <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/Validation.js?"), DateTime.Now.Ticks) %>" type="text/javascript" lang="javascript"></script>
     <script src="js/fastclick.js" type="text/javascript" lang="javascript"></script>
    <script src="js/prism.js" type="text/javascript" lang="javascript"></script>
     <link rel="stylesheet" href="css/jquery-ui.css">    
    <script src="js/jquery.min.js"></script>    
    <script src="js/jquery.easyui.min.js"></script>   
    
 <script>
    
    $(document).ready(function () {
       // $('select:not(.ignore)').niceSelect();
        FastClick.attach(document.body);
        $("#projectids").change(function () {
            $('#projectid').val($("#projectids").val());
        }); 
        ShowProjectMgnt();
        ClearProject();
    });
     function In2InGlobalConfirm(id) {

         $.messager.confirm({
             title: 'In2In Global Confirmation',
             msg: 'Are you sure you want to delete this?',
             ok: 'Yes',
             cancel: 'No',
             fn: function (r) {
                 if (r) {
                     DeleteProject(id);
                 }
             }
         });
     }
     function DeleteProject(id) {

         var _target = 'grdProject';
         $("#__EVENTARGUMENT").val(id);
         $("#__EVENTTARGET").val(_target);
         __doPostBack(_target, id);

     }

     function OpenCSV(fn) {
         window.parent.ShowDiv(fn);
     }
     function ClearProject() {

         $('#txtDescription').val('');
         $('#spnProjectName').text($('#hdnPName').val())
         $('#btnCreateProject').val('Create');
     }
     function ValidateProject() {

         Error_Message = "";
         Error_Count = 1;

         CheckNull($('#txtDescription').val(), in2in25);
         if (Error_Message != "") {
             ShowError(Error_Message, 80);
             return false;
         }
         else {
             return true;
         }
     }
     function PullDataToEdit(projectname, createdby, description) {


         $('#spnProjectName').text(projectname);
         $('#hdnProjectToEdit').val(projectname);
         $('#txtDescription').val(description);
         $('#txtCreatedBy').val(createdby);
         $('#btnCreateProject').val('Update');

     }
     function AddProject() {
         var return_status = function () {
             var tmp = null;
             var projectname = $('#txtProjectName').val();
             var createdBy = $('#txtcreadtedBy').val();
             var description = $('#txtDescription').val();
             var dataValue = "{ ProjectName:'" + projectname + "', CreatedBy:'" + createdBy + "',Description:'" + description + "'}";
             $.ajax({
                 'async': false,
                 'type': "POST",
                 'global': false,
                 'dataType': 'json',
                 contentType: 'application/json; charset=utf-8',
                 'url': "ProjectManagement.aspx/AddNewProject",
                 'data': dataValue,
                 data: "{ ProjectName:'" + projectname + "', CreatedBy:'" + createdBy + "',Description:'" + description + "'}",
                 success: function (data) {
                     tmp = data.d;
                 }
             });
             return tmp;
         }();

         if (return_status == "Success") {

             toastr.success('New Project created', 'Success', { timeOut: 1000, progressBar: true, onHidden: function () { window.location.href = BASE_URL; } });
         }
     }
     function ShowProjectMgnt() {
         $('.projectmgnt').show();
         $('.filemgnt').hide();
         
         $('#btnProjectMgnt').css("background-color", "azure");
         $('#btnProjectMgnt').css("color", "blue");
         $('#btnFileMgnt').css("background-color", "#2c3c59");
         $('#btnFileMgnt').css("color", "#fff");
     }
     function ShowFileMgnt() {
         $('.projectmgnt').hide();
         $('.filemgnt').show();

         $('#btnFileMgnt').css("background-color", "azure");
         $('#btnFileMgnt').css("color", "blue");
         $('#btnProjectMgnt').css("background-color", "#2c3c59");
         $('#btnProjectMgnt').css("color", "#fff");
        
     }
     function ValidateDownload() {
         Error_Message = "";
         Error_Count = 1;

         var value = document.getElementById("<%=ddlTemplate.ClientID%>");         
         CheckNullDropdown(value.selectedIndex, in2in15);
         
         if (Error_Message != "") {
             ShowError(Error_Message, 50);
             return false;
         }
         else {

             return true;
         }
     }  
     function VerifyFile() {

         Error_Message = "";
         Error_Count = 1;
         
         var fileName = $('#fileUploader').val();
         var idxDot = fileName.lastIndexOf(".") + 1;
         var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();

         if (CheckNull(fileName, in2in23)) {
             CheckFileExtension(extFile, "csv", in2in24);
         }
         

         if (Error_Message != "") {
             ShowError(Error_Message, 50);
             return false;
         }
         else {

             return true;
         }
     }
         
     function ShowServerMessage(servermessage) {

         if (servermessage != "") {
             $.messager.show({
                 title: 'In2In Global',
                 msg: servermessage,
                 showType: 'slide',
                 style: {
                     right: '',
                     top: '',
                     bottom: -document.body.scrollTop - document.documentElement.scrollTop
                 }
             });
         }

     }
     function ShowHidden() { }

        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-64633646-1', 'auto');
        ga('send', 'pageview');        
    </script>
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
    </style>
</body>
</html>

<%@ Page Language="C#" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="FileManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.FileManagement" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="vendor/jquery/jquery.min.js"></script>   
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />  
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" /> 
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/Grid.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />      
</head>
<body style="background-color:azure;">
    <form id="form1" runat="server">
        <center>
            <div id="fmPageDiv" style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 5px;display:block;">
                <div class="pagination-ys" style="border:0px solid black;border-bottom:1px solid black; border-radius: 5px; height:40px;padding-top:10px;"><span class="menu_frame_title">File Management</span></div>
                    <asp:ScriptManager ID="scriptmanager1" runat="server">
                </asp:ScriptManager>                
                <asp:UpdatePanel ID="pdnlFileMgnt" runat="server">
                     <Triggers><asp:PostBackTrigger ControlID="btnDownload" /></Triggers>                    
                    <ContentTemplate> 
                         <div style="border-bottom:0 solid gray;display:flex;padding:2px;width:auto;">
                            <div id="btnProjectMgnt" onclick="ShowProjectMgnt();" class="PanelTab" style="background-color:azure;color:blue;">Project Management</div>
                            <div id="btnFileMgnt" onclick="ShowFileMgnt();" class="PanelTab">File Management</div>                          
                        </div>
                        
                        <div title="Project Management" class="projectmgnt" style="background-color: azure;color:blue;">
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
                                                                        <asp:HiddenField ID="hdnPID" runat="server" Value="" />
                                                                         <asp:HiddenField ID="hdnProjectToEdit" runat="server" Value="" />  
                                                                        <asp:HiddenField ID="hdnPNVS" runat="server" Value="" />
                                                                        <asp:Button style="display:none" Text="Delete" OnClientClick="return true;" OnClick="hdnDelBtn_Click" ID="hdnDelBtn" runat="server"></asp:Button>
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
                                                            <div style="margin-top: 5px;">
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
                                            <div style="width: 70%; border: 1px solid black; border-radius: 5px; margin-top:5px; margin-bottom:10px;"> 
                                                <div class="AspNet-GridView">
                                                <asp:GridView DataKeyNames="project_id" ID="grdProject" runat="server" Width="100%" AllowPaging="True" OnRowDataBound="grdProject_RowDataBound"
                                                    OnPageIndexChanging="grdProject_PageIndexChanging"  AutoGenerateColumns="false" PageSize="4"
                                                    HeaderStyle-CssClass="AspNet-GridView" EmptyDataText="No file has been uploaded." >
                                                     <PagerStyle HorizontalAlign = "Center" CssClass="GridPager" />
                                                     <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                                    <Columns>                                                      
                                                         <asp:TemplateField HeaderStyle-Width="150px"  HeaderText="Project Name" SortExpression="project_name">                                                            
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("project_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Created By" SortExpression="created_by">                                                            
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Bind("created_by") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                          
                                                        <asp:BoundField ItemStyle-Wrap="true" HeaderText="Description"  DataField="description" />  
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
                                                </asp:GridView>
                                                </div>
                                            </div>
                                        </center>
                                    </td>
                                </tr>
                        </table>
                        </div>
                        <div title="File Management" class="filemgnt" style="background-color: azure;display:none;">
                            <table style="width: 100%; background-color: azure;">
                                <tr>
                                    <td style="width: 70%;">                                                                
                                        <table style="width: 100%;">                               
                                            <tr>
                                                <td style="text-align: center;">
                                                    <div style="border: 1px solid black; margin-left: auto; margin-right: auto; border-radius: 5px">
                                                        <table style="width: 100%; margin-top: auto; margin-left: auto; margin-right: auto;">
                                                            <tr>
                                                                <td style="width: 60%; vertical-align:top;">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 25%;text-align:left;padding-left:25px;">
                                                                                 <b>Select Project</b>
                                                                                <div style="width:100px;text-align:left;">
                                                                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedProject_SelectedIndexChanged" EnableViewState="true"  DataValueField="ProjectName" DataTextField="ProjectName"  ID="ddlAssignedProject" AppendDataBoundItems="true" runat="server">
                                                                                        <asp:ListItem Text="--Select a Project--"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td> 
                                                                            
                                                                            <td style="width: 30%;margin-left:0px;text-align:left;">
                                                                                <b>Select Template</b>
                                                                                <div style="text-align:left;">
                                                                                    <asp:DropDownList OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" EnableViewState="true" Enabled="false" DataTextField="file_name" AutoPostBack="true" DataValueField="file_name"  ID="ddlTemplate" AppendDataBoundItems="true" runat="server">
                                                                                        <asp:ListItem Text="--Select a Template--"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                            <td style="width:40%;vertical-align:top;padding-top:20px;">
                                                                                <asp:Button CssClass="button" Enabled="false" OnClientClick="return ValidateDownload();" OnClick="btnDownload_Click" Text="Download" ID="btnDownload" runat="server" style="width:70px;padding-left:5px;"></asp:Button>
                                                                            </td>
                                                                        </tr>                                                                       
                                                                        <tr>
                                                                            <td colspan="3">                                                                    
                                                                                <center>
                                                                                    <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                                                                        <div class="AspNet-GridView">
                                                                                        <asp:GridView DataKeyNames="project_id" ID="grdUploadedFiles" runat="server" Width="100%" HeaderStyle-CssClass="AspNet-GridView"
                                                                                            AllowPaging="True" RowStyle-Wrap="false" HeaderStyle-Wrap="false" EmptyDataText="No file found uploaded by you." 
                                                                                            OnPageIndexChanging="grdUploadedFiles_PageIndexChanging" OnRowDataBound="grdUploadedFiles_RowDataBound" AutoGenerateColumns="false" PageSize="6">
                                                                                            <PagerStyle HorizontalAlign = "Center" CssClass="GridPager" />
                                                                                            <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                                                                            <Columns>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="specify" ItemStyle-CssClass="specify" ItemStyle-Width="150px" HeaderStyle-Width="152px" HeaderText="File Name">
                                                                                                        <ItemTemplate>
                                                                                                            <a href='#' title=" <%# DataBinder.Eval(Container.DataItem, "template_file_name")%>" onclick="OpenCSV('<%# DataBinder.Eval(Container.DataItem, "template_file_name") %>');">
                                                                                                                <%# DataBinder.Eval(Container.DataItem, "template_file_name")%>
                                                                                                            </a>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>                                                                                                
                                                                                                <asp:BoundField ItemStyle-Width="150px" HeaderStyle-Width="150px" HeaderText="Project Name" DataField="project_name" />
                                                                                                <asp:BoundField ItemStyle-Width="150px" HeaderStyle-Width="150px" HeaderText="Uploaded By" DataField="uploaded_by" />
                                                                                                <asp:BoundField ItemStyle-Width="150px" HeaderStyle-Width="150px" HeaderText="Uploaded On" DataField="uploaded_on" />
                                                                                                <asp:ImageField ItemStyle-Width="50px" HeaderStyle-Width="50px" ItemStyle-CssClass ="GridViewImageAlignment" HeaderText="Status" ControlStyle-Height="20px" ControlStyle-Width="20px" DataImageUrlField="uploadstatus"></asp:ImageField>                                                                                             
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                            </div>
                                                                                    </div>
                                                                                </center>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left;">
                                                                                <asp:FileUpload ID="fileUploader" accept=".csv" Enabled="false" runat="server" />                                                                                                                                                    
                                                                            </td>
                                                                            <td>
                                                                                 <asp:Button ID="btnUpload" OnClientClick="javascript:StartUploading();return false;"  style="width:70px;padding-left:15px;" class="button" Enabled="false" runat="server" Text="Upload" />
                                                                            </td>
                                                                
                                                                            <td>
                                                                                <asp:ImageButton runat="server" ID="btnRefresh" ToolTip="Refresh" ImageUrl="~/admin/img/refresh.jpg" Width="30px" BorderStyle="None" OnClick="btnRefresh_Click" />
                                                                            </td>

                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 35%;vertical-align:text-top;">
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
                                        <div style="margin-top: 10px; margin-left: 20px;margin-bottom:20px;border-radius:5px; border: 1px solid gray; height: 280px;">
                                            <span style="margin-left: 10px;"><b>Search Template </b></span>
                                            <table style="width: 100%; margin-left: 5px;margin-right: 5px;">
                                                <tr id="usrEmailTR" runat="server">
                                                    <td style="width: 35%">
                                                        <b><u>Email ID</u> : </b>
                                                    </td>
                                                    <td style="width: 65%;">
                                                          <div>
                                                            <asp:DropDownList OnSelectedIndexChanged="ddlusrEmailId_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" ID="ddlUsrEmailId" Width="85%" runat="server" DataTextField="UserEmail">
                                                                    <asp:ListItem Text="--Select a Email--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>                                                                                                  
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
                                                                    <td >
                                                                        <div class="AspNet-GridView">
                                                                            <asp:GridView ID="grdTemplate" EmptyDataText="No Template Found..."  runat="server" Width="100%" HeaderStyle-CssClass="AspNet-GridView"
                                                                                AllowPaging="True" OnPageIndexChanging="grdTemplate_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                                                               <PagerStyle HorizontalAlign = "Center" CssClass="GridPager" />
                                                                                <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderStyle-CssClass="specifySearchCol" ItemStyle-CssClass="specifySearch" ItemStyle-Width="160px" HeaderStyle-Width="165px"  HeaderText="Template Name" DataField="template_file_name" />
                                                                                    <asp:BoundField ItemStyle-Width="200px" HeaderStyle-Width="200px" HeaderText="On Date" DataField="uploaded_on" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            </div>
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
     function In2InGlobalConfirm(pName, pID) {

         $.messager.confirm({
             title: 'In2In Global Confirmation',
             msg: 'Are you sure you want to delete this?',
             ok: 'Yes',
             cancel: 'No',
             fn: function (r) {

                 if (r) {
                     $('#hdnPName').val(pName);
                     $('#hdnPID').val(pID);
                     $('#hdnDelBtn').trigger('click');

                 }
                 else {
                     $('#hdnPName').val('');
                     $('#hdnPID').val('');

                 }
             }
         });
     }   
     function OpenCSV(fn) {
         window.parent.ShowDiv(fn);
     }
     function ClearProject() {
         $('#spnProjectName').text($('#hdnPNVS').val());
         $('#txtDescription').val('');         
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

         description = description.replaceAll("<br>", "\n");
         $('#spnProjectName').text(projectname);
         $('#hdnProjectToEdit').val(projectname);
         $('#txtDescription').val(description);
         $('#txtCreatedBy').val(createdby);
         $('#btnCreateProject').val('Update');
         
         return false;
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
             ShowError(Error_Message, 80);
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
         $('.messager-body.panel-body.panel-body-noborder.window-body').css('height', '32px');
         $('.messager-body.panel-body.panel-body-noborder.window-body').css('width', '275px');
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

     function StartUploading() {

         if (VerifyFile()) {

             var fileUpload = $("#fileUploader").get(0);
             var files = fileUpload.files;

             var data = new FormData();
             data.append("targetfolder", "./uploadedFiles/");
             data.append("UploadedBy", $('#spnCreatedBy').text());
             data.append("ForScreen", "FileManagement");

             for (var i = 0; i < files.length; i++) {
                 data.append(files[i].name, files[i]);
             }

             $.ajax({
                 url: "FileUploadHandler.ashx",
                 type: "POST",
                 data: data,
                 contentType: false,
                 processData: false,
                 success: function (result) {

                     if (result != '') {

                       
                         ShowServerMessage("File(s) Uploaded Successfully.");
                         $("#fileUploader").val('');
                     }

                 },
                 error: function (err) {
                     ShowServerMessage(err.statusText);
                 },
                 complete: function (data) {

                     $ddlAssignedProject.attr('selectedIndex', 0);
                     ShowCreateTemplate();

                 }
             });

         }

     }
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
          .specify {
        overflow: hidden;
        text-overflow: ellipsis;
        max-height: 20px;
        height: 20px;
        word-break: break-all;
        word-wrap: break-word;
        display:inline-block;     
         white-space: nowrap;
    }
            .specifySearch {
        overflow: hidden;
        text-overflow: ellipsis;
        max-height: 20px;
        height: 20px;
        word-break: break-all;
        word-wrap: break-word;
        display:inline-block;
        margin-bottom: 2px;  
        margin-left:3px;
         white-space: nowrap;
    }
            .specifySearchCol {
        overflow: hidden;
        text-overflow: ellipsis;
        max-height: 20px;
        height: 20px;
        word-break: break-all;
        word-wrap: break-word;
        display:inline-block;        
         white-space: nowrap;
    }
       .messager-body.panel-body.panel-body-noborder.window-body{
            width:278px;
            height:32px;
        }
    </style>
</body>
</html>

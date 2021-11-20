<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.FileManagement" %>

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

   
  
</head>
<body style="background-color:azure;">
   
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 20px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height:40px;padding-top:10px;"><span class="menu_frame_title">File Management</span></div>
                    <asp:ScriptManager ID="scriptmanager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="pdnlFileMgnt" runat="server">
                    <Triggers><asp:PostBackTrigger ControlID="btnDownload" /></Triggers>
                    <ContentTemplate> 
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
                                                                                    <asp:DropDownList OnSelectedIndexChanged="LoadInstruction" DataTextField="TemplateName" AutoPostBack="true" DataValueField="Path"  ID="ddlTemplate" AppendDataBoundItems="true" runat="server">
                                                                                        <asp:ListItem Text="--Select a Template--"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td></td>
                                                                            <td style="text-align: right;">
                                                                                <asp:Button CssClass="button" OnClientClick="return ValidateDownload();" OnClick="btnDownload_Click" Text="Download" ID="btnDownload" runat="server"></asp:Button></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">                                                                    
                                                                                <center>
                                                                                    <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                                                                        <asp:GridView DataKeyNames="ID" ID="grdUploadedFiles" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                                                            AllowPaging="True" EmptyDataText="No file found uploaded by you." OnPageIndexChanging="grdUploadedFiles_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                                                                            <PagerStyle CssClass="pagination-ys" />
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="File Name" DataField="FileName" />
                                                                                                <asp:BoundField HeaderText="Project Name" DataField="ProjectName" />
                                                                                                <asp:BoundField HeaderText="Uploaded By" DataField="UploadedBy" />
                                                                                                <asp:BoundField HeaderText="Uploaded On" DataField="Date" />
                                                                                                <asp:ImageField ItemStyle-CssClass ="GridViewImageAlignment" HeaderText="Uploaded Status" ControlStyle-Height="20px" ControlStyle-Width="20px" DataImageUrlField="UploadedStatus"></asp:ImageField>                                                                                             
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        
            </div>         
        </center>
    </form>
    
      <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" lang="javascript"></script>
     <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/ErrorMessage.js?"), DateTime.Now.Ticks) %>" type="text/javascript" lang="javascript"></script>
     <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/Validation.js?"), DateTime.Now.Ticks) %>" type="text/javascript" lang="javascript"></script>
    
 <script>
    
    $(document).ready(function () {
       // $('select:not(.ignore)').niceSelect();
        //FastClick.attach(document.body);
        $("#projectids").change(function () {
            $('#projectid').val($("#projectids").val());
        });
       
    });               
    
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
</body>
</html>

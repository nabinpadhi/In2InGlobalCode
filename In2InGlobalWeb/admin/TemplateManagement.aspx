<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="TemplateManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.TemplateManagement" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
     <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/Validation.js?"), DateTime.Now.Ticks) %>" type="text/javascript" lang="javascript"></script>
      <link rel="stylesheet" href="//code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css" />
    <script lang="JavaScript">
        function __doPostBack(eventTarget, eventArgument) {
            documenT.Form1.__EVENTTARGET.value = eventTarget;
            document.Form1.__EVENTARGUMENT.value = eventArgument;
            document.Form1.submit();
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 10px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Template Management</span></div>
                <asp:ScriptManager ID="Templatescriptmanager" runat="server">                    
                </asp:ScriptManager>
                <asp:UpdateProgress ID="UpdatePnlTemplate" runat="server" AssociatedUpdatePanelID="pdnlTemplate">
                <ProgressTemplate>
                        <img src="img/uploading.gif" alt="Uploading..." />
                </ProgressTemplate>
            </asp:UpdateProgress>
                <asp:UpdatePanel  ID="pdnlTemplate" runat="server">   
                    <Triggers><asp:PostBackTrigger ControlID="btnUploader" /></Triggers>
                    <ContentTemplate>
                          <input type="hidden" value="" id="__EVENTARGUMENT" name="__EVENTARGUMENT">
                        <input type="hidden" value="" id="__EVENTTARGET" name="__EVENTTARGET">
                        
                        <div name="pnlTemplate" id="pnlTemplate" style="width:auto;height:auto;min-height:350px;color:black">
                        <div style="border-bottom:0 solid gray;display:flex;padding:2px;width:auto;">
                            <div id="btnUploadMasterTemplate" onclick="ShowUploadMasterTemplate();" class="PanelTab"> Upload Master Template </div>
                            <div id="btnCreateTemplate" onclick="ShowCreateTemplate();" class="PanelTab"> Create Template </div>
                    <%--        <div style="margin-left:4px;" onclick="ShowAssignTemplate();" class="PanelTab" id="btnAssignTemplate">Assign Template</div>   --%>                         
                        </div>
                        <div title="Upload Master Template" class="upldmt" style="background-color: azure;padding:100px">
                             <table style="width: 100%; background-color: azure;">
                                <tr>                                   
                                    <td style="width:85%;">                                      
                                        <center>                                           
                                            <div style="width: 60%;border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                                <table style="width:100%;">
                                                    <tr><td style="width:25%">Template Files</td>
                                                        <td style="width:5%">:</td>
                                                        <td style="width:70%"><asp:FileUpload accept=".csv" ID="templateFileUpload" runat="server" /></td>
                                                        <td>
                                                            <asp:Button ID="btnUploader" class="button" OnClientClick="return VerifyFile();" OnClick="btnUploader_Click"  runat="server" Text="Upload" />
                                                            <asp:Button ID="hdnFake" Text="" EnableViewState="true" runat="server" Visible="false" OnClick="hdnFake_Click" />
                                                        </td>
                                                    </tr>
                                                </table>                                                                                    
                                            </div>
                                            </center>
                                        </td>
                                    </tr>
                                 </table>
                            </div>
                        <div title="Create Template" class="crtpnl" style="background-color: azure;padding:10px;display:none;">
                             <table style="width: 100%; background-color: azure;">
                                <tr>                                   
                                    <td style="width:85%;">                                      
                                        <center>                                           
                                            <div style="width: 60%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                                <table>
                                                    <tr>
                                                        <td style="width:50%;">
                                                            <table style="width:100%">
                                                                <tr>
                                                                    
                                                                    <td style="width:40%">
                                                                        Template Name(<span style="color: red">*</span>)<br />
                                                                        <asp:DropDownList ID="ddlMasterTemplate" Width="92%" AppendDataBoundItems="true" runat="server" DataTextField="TemplateName">
                                                                            <asp:ListItem Text="--Select a Template--" ></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="hdnTID" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td>
                                                                        Created By(<span style="color: red">*</span>)                                                                        
                                                                        <span fieldtype="readonly" value="" runat="server" id="txtcreatedB" />
                                                                    </td>
                                                                </tr>                                                                                                         
                                                            </table>
                                                        </td>
                                                        <td  style="width:50%;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Instruction(<span style="color: red">*</span>)
                                                                        <textarea rows="5" id="txtInstruction" class="txtInstruction" name="txtInstruction" style="resize:none; width: 97%; height: 70px;" runat="server"></textarea>
                                                                        <asp:HiddenField ID="hdnMTName" Value="" runat="server" />
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="margin-top: 15px;">
                                                                <center>
                                                                    <asp:Button  ID="btnCreate" runat="server" OnClientClick="return ValidateMasterTemplate();" OnClick="btnCreate_Click"  CssClass="button" Text="Create" />
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
                                    <td>
                                        <center>
                                            <div class="confirmDialog" style="text-align:center;color:black;display:none;position:center;padding-top:30px;">
                                                Are you sure you want to delete this record ?
                                            </div>
                                            <div style="width:60%; border: 1px solid black; border-radius: 5px; margin-top: 10px; margin-bottom: 20px;">                                    
                                                <asp:GridView DataKeyNames="template_id" ID="grdMasterTemplate" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                    AllowPaging="True" OnRowDataBound="grdMasterTemplate_RowDataBound" OnRowDeleting="grdMasterTemplate_RowDeleting" OnPageIndexChanging="grdMasterTemplate_PageIndexChanging"  AutoGenerateColumns="false" PageSize="4">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Template Name" ItemStyle-Width="25%" DataField="template_name" />
                                                        <asp:BoundField HeaderText="Created By" ItemStyle-Width="25%" DataField="created_by" />                                                                                                              
                                                        <asp:BoundField HeaderText ="Instruction" ItemStyle-Width="35%" DataField="instruction"/>   
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
                        <div title="Assign Template" class="Asgnpnl" style="background-color: azure;padding:10px;display:none;">
                             <table style="width: 100%; background-color: azure;">
                                <tr>
                                 <td>
                                    <center>
                                        <div style="width: 60%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                         <table style="width:100%">
                                                        <tr>
                                                            <td style="width:40%;text-align:right;">Project Name(<span style="color: red">*</span>)</td>
                                                            <td style="width:60%;padding-right:20%;">
                                                                <asp:DropDownList ID="ddlProjects" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AppendDataBoundItems="true" style="width:80%;" runat="server" DataTextField="ProjectName">
                                                                    <asp:ListItem Text="--Select a Project--" ></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr> 
                                                         <tr>
                                                            <td style="text-align:right;">Email Id(<span style="color: red">*</span>)</td>
                                                            <td style="padding-right:20%;">                                                                
                                                                <asp:DropDownList ID="ddlUserEmail" style="width:77%;" AppendDataBoundItems="true" runat="server" DataTextField="Email">
                                                                    <asp:ListItem Text="--Select an Email--" ></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                          
                                             <tr>
                                                            <td style="text-align:right;">Template Name(<span style="color: red">*</span>)</td>
                                                            <td style="padding-right:20%;">
                                                                <asp:DropDownList ID="ddlTemplates" style="width:auto;" AppendDataBoundItems="true" runat="server" DataTextField="TemplateName">
                                                                    <asp:ListItem Text="--Select a Template--" ></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="2">
                                                                <div style="margin-top: 15px;">
                                                                    <center>
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Assign" OnClientClick="return ValidateTemplate();" OnClick="btnSave_Click" />
                                                                        <input type="button" class="button" style="margin-left: 10px;" value="Cancel" onclick="ClearAll();" />
                                                                    </center>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="2">
                                                                <center>
                                                                    <div style="width: 97%; border: 1px solid black; border-radius: 5px; margin-top: 10px; margin-bottom: 20px;">                                    
                                                                        <asp:GridView DataKeyNames="ID" ID="grdTemplate" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                                            AllowPaging="True" OnRowDataBound="grdTemplate_RowDataBound" OnRowDeleting="grdTemplate_RowDeleting" OnPageIndexChanging="grdTemplate_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                                                            <PagerStyle CssClass="pagination-ys" />
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Template Name" DataField="TemplateName" />
                                                                                <asp:BoundField HeaderText="Assigned On" DataField="DateAdded" />
                                                                                <asp:BoundField HeaderText="Project Name" DataField="ProjectName" />
                                                                                <asp:BoundField HeaderText="User Email" DataField="Email" />
                                                                                <asp:CommandField ItemStyle-HorizontalAlign="Center" HeaderText="Action" ShowDeleteButton="true" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </center>
                                                            </td>
                                                        </tr>
                                                </table>                                        
                                              </div>
                                    </center>
                                 </td>
                                </tr></table>
                      
                            </div>
                       
                    </div> 
                        
                     </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--<center>
                                <div>
                                    Are you sure you want to delete this Master Template ?
                                    <p><input type="button" value="Yes" id="btnYes" name="btnYes" onclick="javascript: return true;" />&nbsp; </p>
                                    <p><input type="button" value="No" id="btnNo" name="btnNo" onclick="javascript: return false;" />&nbsp; </p>
                                </div>
                            </center>--%>
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
    <script type="text/javascript">
        $(function () {
            $('.confirmDialog').hide();
        });
      var recentnl = "btnCreateTemplate";     
      (function ($) {
          $.fn.invisible = function () {
              return this.each(function () {
                  $(this).css("display", "none");
              });
          };
          $.fn.visible = function () {
              return this.each(function () {
                  $(this).css("display", "block");
              });
          };
      }(jQuery));

      $(document).ready(function () {
            
          alert($.messager);

          FastClick.attach(document.body);
          ShowUploadMasterTemplate();
          ClearAll();
         
           
      }); 
      function PullDataToEdit(id,templatename,instruction) {
                    
          $('#hdnTID').val(id);
          $('#txtInstruction').val(instruction);
          document.getElementById("hdnMTName").value = templatename;
          $('#btnCreate').val('Save');         
          $('#ddlMasterTemplate').val(id);
      }
        function ClearAll() {
            
            
            document.getElementById("hdnMTName").value = "";           
            $("#txtInstruction").val('');            
            $('#btnCreate').val('Create');
            $('#txtMasterTemplateName').invisible();
            $('#ddlMasterTemplate').visible();
            $('#ddlMasterTemplate').prop('selectedIndex', 0);

            
      }
        function IIn2InGlobalConfirm(id) {

            $(".confirmDialog").dialog({
                resizable: false,
                height: "auto",
                title: "In2In Global Confirmation",
                width: 400,
                height: 170,
                modal: true,
                buttons: {
                    "Yes": function () {
                        $(this).dialog("close");
                        DeleteTemplate(id);
                    },
                    "No": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
        //$.messager.confirm(
        function In2InGlobalConfirm(id) {
            $.messager.confirm('In2In Global Confirmation', 'Are you sure you want to delete this?', function (r) {
                if (r) {
                    DeleteTemplate(id);;
                }
            });
        }
        function DeleteTemplate(id) {

            var _target = 'grdMasterTemplate';
            $("#__EVENTARGUMENT").val(id);
            $("#__EVENTTARGET").val(_target);
            __doPostBack(_target, id);

        }
      function ValidateMasterTemplate() {
          var winH = 80;
          Error_Message = "";
          Error_Count = 1;  
          if ($('#btnCreate').val() == 'Create') {
              CheckNullDropdown($("select[name='ddlMasterTemplate'] option:selected").index(), in2in15);
          }
          CheckNull($("#txtInstruction").val(), in2in18);          

          if (Error_Message != "") {
              ShowError(Error_Message, winH);
              return false;
          }
          else {
              return true;
          }
      }
        function ValidateTemplate() {

            Error_Message = "";
            Error_Count = 1;            
            CheckNullDropdown($("select[name='ddlTemplates'] option:selected").index(), in2in15);            
            CheckNullDropdown($("select[name='ddlProjects'] option:selected").index(), in2in17);
            CheckNullDropdown($("select[name='ddlUserEmail'] option:selected").index(), in2in22);
            if (Error_Message != "") {
                ShowError(Error_Message, 80);
                return false;
            }
            else {
                return true;
            }
        }

      $(".txtInstruction").focus(function () {
          if (document.getElementById('txtInstruction').value === '') {
              document.getElementById('txtInstruction').value += '• ';
          }
      });
      $(".txtInstruction").keyup(function (event) {
          var keycode = (event.keyCode ? event.keyCode : event.which);
          if (keycode == '13') {
              document.getElementById('txtInstruction').value += '• ';
          }
          var txtval = document.getElementById('txtInstruction').value;
          if (txtval.substr(txtval.length - 1) == '\n') {
              document.getElementById('txtInstruction').value = txtval.substring(0, txtval.length - 1);
          }
      });

      function ShowHidden() { } 

      function ShowServerMessage(servermessage) {          
          
          $("#txtInstruction").val('');
          $("#txtDescription").val('');

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
     
      function ShowAssignTemplate() {

          $('.Asgnpnl').visible();
          $('.crtpnl').invisible();
          $('.crtppnl').invisible();
          $('.upldmt').invisible();
          $('#btnUploadMasterTemplate').css("background-color", "#2c3c59");
          $('#btnUploadMasterTemplate').css("color", "#fff");
          $('#btnAssignTemplate').css("background-color", "azure");
          $('#btnAssignTemplate').css("color", "blue");
          $('#btnCreateTemplate').css("background-color", "#2c3c59");
          $('#btnCreateTemplate').css("color", "white");
         
          ClearAll();
      }
      function ShowCreateTemplate() {

          //$('.Asgnpnl').invisible();
          $('.crtppnl').invisible();
          $('.crtpnl').visible();
          $('.upldmt').invisible();
          $('#btnUploadMasterTemplate').css("background-color", "#2c3c59");
          $('#btnUploadMasterTemplate').css("color", "#fff");
          $('#btnCreateTemplate').css("background-color", "azure");
          $('#btnCreateTemplate').css("color", "blue");
         // $('#btnAssignTemplate').css("background-color", "#2c3c59");
         // $('#btnAssignTemplate').css("color", "#fff");          
          
          
          ClearAll();
      }
      
     
      function ShowUploadMasterTemplate()
      {          
          $('.Asgnpnl').invisible();
          $('.crtpnl').invisible();
          $('.crtppnl').invisible();
          $('.upldmt').visible();
          $('#btnUploadMasterTemplate').css("background-color", "azure");
          $('#btnUploadMasterTemplate').css("color", "blue");

          $('#btnCreateTemplate').css("background-color", "#2c3c59");
          $('#btnCreateTemplate').css("color", "#fff");
          $('#btnAssignTemplate').css("background-color", "#2c3c59");
          $('#btnAssignTemplate').css("color", "#fff");
          
      }
      function sleep(milliseconds) {
          var start = new Date().getTime();
          for (var i = 0; i < 1e7; i++) {
              if ((new Date().getTime() - start) > milliseconds) {
                  break;
              }
          }
      }
      function VerifyFile() {

          var winH = 80
          Error_Message = "";
          Error_Count = 1;

          var fileName = $('#templateFileUpload').val();
          var idxDot = fileName.lastIndexOf(".") + 1;
          var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();

          if (CheckNull(fileName, in2in23)) {
              CheckFileExtension(extFile, "csv", in2in24);
              CheckMasterTemplate(fileName, in2in26, winH)
          }



          if (Error_Message != "") {
              ShowError(Error_Message, 80);
              return false;
          }
          else {

              return true;
          }
      }     
     
    </script>
    <style type="text/css">
        body {
            background-color: azure;
        }
        .panel-body {
               color:black;
        }
        .ui-dialog-titlebar
        {
            color: white;
            background-color: #8f0108;
            border: 1px solid #dddddd;    
            text-indent: 5px;    
            border-radius: 5px;
        }
        .ui-dialog-buttonpane {

            background-color:lightGray;                
        }
        .ui-dialog{
            border: 1px solid blue;
            border-radius:5px;
        }
        .ui-button{
            border: 1px solid blue;
            border-radius:5px;
        }
        .ui-button:hover{
            border: 1px solid blue;
            border-radius:5px;
            font-weight:bold;
        }
    </style>
</body>
</html>

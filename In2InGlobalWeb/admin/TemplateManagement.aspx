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
   
</head>
<body onload="ShowFullPostBackMessage();">
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
                        <div name="pnlTemplate" id="pnlTemplate" style="width:auto;height:auto;min-height:350px;color:black">
                        <div style="border-bottom:0 solid gray;display:flex;padding:2px;width:auto;">
                            <div id="btnUploadMasterTemplate" onclick="ShowUploadMasterTemplate();" class="PanelTab"> Upload Master Template </div>
                            <div id="btnCreateTemplate" onclick="ShowCreateTemplate();" class="PanelTab"> Create Template </div>
                            <div style="margin-left:4px;" onclick="ShowAssignTemplate();" class="PanelTab" id="btnAssignTemplate">Assign Template</div>
                            <div style="margin-left:4px;" onclick="ShowCreateProject();" class="PanelTab" id="btnCreateProject">Create Project</div>
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
                                                                        <asp:TextBox ID="txtMasterTemplateName" runat="server" ReadOnly="true" Text="" style="display:none"></asp:TextBox>
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
                                            <div style="width:60%; border: 1px solid black; border-radius: 5px; margin-top: 10px; margin-bottom: 20px;">                                    
                                                <asp:GridView DataKeyNames="ID" ID="grdMasterTemplate" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                    AllowPaging="True" OnRowDataBound="grdMasterTemplate_RowDataBound" OnRowDeleting="grdMasterTemplate_RowDeleting" OnPageIndexChanging="grdMasterTemplate_PageIndexChanging"  AutoGenerateColumns="false" PageSize="4">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Template Name" ItemStyle-Width="25%" DataField="TemplateName" />
                                                        <asp:BoundField HeaderText="Created By" ItemStyle-Width="25%" DataField="CreatedBy" />                                                                                                              
                                                        <asp:BoundField HeaderText ="Instruction" ItemStyle-Width="35%" DataField="Instruction"/>   
                                                        <asp:CommandField ItemStyle-HorizontalAlign="Center" HeaderText="Action" ShowEditButton="true" ShowDeleteButton="true" />                                                                                                             
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
                        <div title="Create Project" class="crtppnl" style="background-color: azure;padding:10px">
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
                                                                    <asp:Button  ID="btnCreatePro" runat="server" OnClick="btnCreatePro_Click"  CssClass="button" Text="Create" />
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
                                            <div style="width: 70%; border: 1px solid black; border-radius: 5px; margin-top: 10px; margin-bottom: 20px;">                                    
                                                <asp:GridView ID="grdProject" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                    AllowPaging="True" AllowSorting="true" OnSorting="grdProject_Sorting" OnRowDeleting="grdProject_RowDeleting" 
                                                    OnPageIndexChanging="grdProject_PageIndexChanging"  AutoGenerateColumns="false" PageSize="4">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>                                                      
                                                         <asp:TemplateField HeaderStyle-Width="150px"  HeaderText="Project Name" SortExpression="ProjectName">                                                            
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Created By" SortExpression="CreatedBy">                                                            
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                          
                                                        <asp:BoundField ItemStyle-Wrap="true" HeaderText="Description"  DataField="Description" />                                                                                                              
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </center>
                                    </td>
                                </tr>
                        </table>
                        </div>
                    </div> 
                        <div id="deletedialog" name="deletedialog">
                            
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
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>
  <script>   
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
            //$('select:not(.ignore)').niceSelect();
            FastClick.attach(document.body);
            //ClearAll();
            ShowCreateTemplate();
            ShowUploadMasterTemplate();
           
      }); 
      function PullDataToEdit(id,templatename,instruction) {
          
          $('#txtMasterTemplateName').val(templatename);
          $('#txtInstruction').val(instruction);
          document.getElementById("hdnMTName").value = templatename;
          $('#btnCreate').val('Save');
          $('#txtMasterTemplateName').visible();
          $('#ddlMasterTemplate').invisible();
      }
        function ClearAll() {
            
            $('#ddlTemplates').prop('selectedIndex', 0);            
            $('#ddlProjects').prop('selectedIndex', 0);
            document.getElementById("hdnMTName").value = "";
            $('#ddlUserEmail').prop('selectedIndex', 0);
            $("#txtInstruction").val('');            
            $('#btnCreate').val('Create');
            $('#txtMasterTemplateName').invisible();
            $('#ddlMasterTemplate').visible();
            $('#ddlMasterTemplate').prop('selectedIndex', 0);

            
      }
      function ClearProject() {

          $('#txtDescription').val('');
      }
      function ValidateMasterTemplate() {

          Error_Message = "";
          Error_Count = 1;  
          if ($('#btnCreate').val() == 'Create') {
              CheckNullDropdown($("select[name='ddlMasterTemplate'] option:selected").index(), in2in15);
          }
          CheckNull($("#txtInstruction").val(), in2in18);          

          if (Error_Message != "") {
              ShowError(Error_Message, 80);
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
          $('#btnCreateProject').css("background-color", "#2c3c59");
          $('#btnCreateProject').css("color", "white");
          ClearAll();
      }
      function ShowCreateTemplate() {

          $('.Asgnpnl').invisible();
          $('.crtppnl').invisible();
          $('.crtpnl').visible();
          $('.upldmt').invisible();
          $('#btnUploadMasterTemplate').css("background-color", "#2c3c59");
          $('#btnUploadMasterTemplate').css("color", "#fff");
          $('#btnCreateTemplate').css("background-color", "azure");
          $('#btnCreateTemplate').css("color", "blue");
          $('#btnAssignTemplate').css("background-color", "#2c3c59");
          $('#btnAssignTemplate').css("color", "#fff");
          $('#btnCreateProject').css("background-color", "#2c3c59");
          $('#btnCreateProject').css("color", "white");
          
          
          ClearAll();
      }
      
      function ShowCreateProject() {

          $('.Asgnpnl').invisible();
          $('.crtpnl').invisible();
          $('.crtppnl').visible();
          $('.upldmt').invisible();
          $('#btnUploadMasterTemplate').css("background-color", "#2c3c59");
          $('#btnUploadMasterTemplate').css("color", "#fff");
          $('#btnCreateTemplate').css("background-color", "#2c3c59");
          $('#btnCreateTemplate').css("color", "#fff");
          $('#btnAssignTemplate').css("background-color", "#2c3c59");
          $('#btnAssignTemplate').css("color", "#fff");
          $('#btnCreateProject').css("background-color", "azure");
          $('#btnCreateProject').css("color", "blue");

          ClearProject();
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
          $('#btnCreateProject').css("background-color", "#2c3c59");
          $('#btnCreateProject').css("color", "#fff");
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

          Error_Message = "";
          Error_Count = 1;

          var fileName = $('#templateFileUpload').val();
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
      function ShowFullPostBackMessage() {
         // var sessionvalue = '<%=Session["servermessage"]%>';
         /* if (sessionvalue != null) {
              alert(sessionvalue)
          }
          if ($('#hdnFake').text() != "") {
              __doPostBack('hdnFake', 'OnClick');
          }*/
      }
      $('#deletedialog').hide();
      function ConfirmDelete(item) {
          $('#deletedialog').confirm({
              title: 'Delete Confirmation',
              content: '!',
              buttons: {
                  confirm: function () {
                      return true;
                  },
                  cancel: function () {
                      return false;
                  }
              }
          });
          
      };
    </script>
    <style type="text/css">
        body {
            background-color: azure;
        }
        .panel-body {
               color:black;
        }
    </style>
</body>
</html>

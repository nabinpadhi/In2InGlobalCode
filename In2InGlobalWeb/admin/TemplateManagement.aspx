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
    <script src="../scripts/Validation.js" type="text/javascript" lang="javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css" />
    <link href="css/Grid.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Template Management</span></div>
                <asp:ScriptManager ID="Templatescriptmanager" runat="server">                    
                </asp:ScriptManager>              
                <asp:UpdatePanel  ID="pdnlTemplate" runat="server">                    
                    <ContentTemplate>
                           <asp:Button style="display:none" Text="Delete" OnClientClick="return true;" OnClick="btnFUCalbk_Click" ID="btnFUCalbk" runat="server" />
                            <asp:HiddenField ID="hdnFUCalBkMsg" Value="" runat="server" />

                        <div id="pnlTemplate" style="width:auto;height:auto;min-height:345px;color:black">
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
                                                        <td style="width:70%"><asp:FileUpload  AllowMultiple="true"  accept=".csv" ID="tmpltFU" runat="server" /></td>
                                                        <td>
                                                            <asp:Button ID="btnUpload" class="button" runat="server" Text="Upload" />
                                                           
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
                                            <div style="width: 60%; border: 1px solid black; border-radius: 5px; margin-top: 0px;">
                                                <table>
                                                    <tr>
                                                        <td style="width:50%;">
                                                            <table style="width:100%">
                                                                <tr>
                                                                    
                                                                    <td style="width:40%">
                                                                        Template Name(<span style="color: red">*</span>)<br />
                                                                        <asp:DropDownList ID="ddlMasterTemplate" Width="92%" onchange="UpdateHdnTID();" AppendDataBoundItems="true" runat="server" DataValueField="template_id" DataTextField="file_name">
                                                                            <asp:ListItem Text="--Select a Template--" ></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="hdnTID" Value="" runat="server" />                                                                        
                                                                        <asp:HiddenField ID="hdnTName" runat="server" Value="" />        
                                                                        <asp:Button runat="server" ID="hdnDelBtn" Text="" style="display:none;" OnClientClick="return true;" OnClick="hdnDelBtn_Click" />
                                                         
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
                                                            <center>
                                                            <div style="margin-top: 0px;">
                                                                    <asp:Button  ID="btnCreate" runat="server" OnClientClick="return ValidateMasterTemplate();" OnClick="btnCreate_Click"  CssClass="button" Text="Create" />
                                                                    <input type="button" class="button" style="margin-left: 10px;" value="Cancel" onclick="ClearAll();" />                                                                                                                                  
                                                            </div>
                                                            </center>
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
                                            <div style="width:60%; border: 1px solid black; border-radius: 5px; margin-top: 5px; margin-bottom: 5px;"> 
                                                 <div class="AspNet-GridView">
                                                <asp:GridView DataKeyNames="template_id" ID="grdMasterTemplate" runat="server" Width="100%" 
                                                    HeaderStyle-CssClass="AspNet-GridView" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText ="No Template has been created."
                                                    AllowPaging="True" OnRowDataBound="grdMasterTemplate_RowDataBound" OnRowDeleting="grdMasterTemplate_RowDeleting" 
                                                    OnPageIndexChanging="grdMasterTemplate_PageIndexChanging"  AutoGenerateColumns="false" PageSize="4">
                                                    <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Template Name" ItemStyle-Width="25%" DataField="template_name" />
                                                        <asp:BoundField HeaderText="Created By" ItemStyle-Width="25%" DataField="created_by" />                                                                                                              
                                                        <asp:BoundField HeaderText ="Instruction" ItemStyle-Width="35%" DataField="instruction"/>   
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
                                                                        <div>
                                                                        <asp:GridView DataKeyNames="template_id" ID="grdTemplate" runat="server" Width="100%" AllowPaging="True" 
                                                                            OnRowDataBound="grdTemplate_RowDataBound" OnRowDeleting="grdTemplate_RowDeleting" 
                                                                            OnPageIndexChanging="grdTemplate_PageIndexChanging" AutoGenerateColumns="false" PageSize="4" 
                                                                            HeaderStyle-CssClass="AspNet-GridView" EmptyDataText="No file has been uploaded." 
                                                                            SortedAscendingCellStyle-CssClass="SortedAscendingHeaderStyle" SortedDescendingCellStyle-CssClass="SortedDescendingHeaderStyle">
                                                                             <PagerStyle HorizontalAlign = "Center" CssClass="GridPager" />
                                                                             <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Template Name" DataField="TemplateName" />
                                                                                <asp:BoundField HeaderText="Assigned On" DataField="DateAdded" />
                                                                                <asp:BoundField HeaderText="Project Name" DataField="ProjectName" />
                                                                                <asp:BoundField HeaderText="User Email" DataField="Email" />
                                                                                <asp:CommandField ItemStyle-HorizontalAlign="Center" HeaderText="Action" ShowDeleteButton="true" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                            </div>
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
    <style type="text/css">
        body {
            background-color: azure;
        }

        .panel-title {
            color: greenyellow;
            background-color: #8f0108;
            border: 0px solid #dddddd;
            text-indent: 5px;
            border-radius: 5px;
        }

        .window-body.panel-body {
            color: silver;
            text-align: left;
        }

        .l-btn-text {
            color: yellow;
        }
    </style>
    <script type="text/javascript">
        var uploadingFileName = "";
       
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

            FastClick.attach(document.body);
            ShowUploadMasterTemplate();
            ClearAll();
            $('input[type="file"]').change(function (e) {
                uploadingFileName = e.target.files[0].name;

            });


        });
        function PullDataToEdit(template_id, templatename, instruction, master_template_id) {

            $('#hdnTID').val(template_id);
            if (instruction.indexOf('•') == 0) {

               var linecnt = instruction.match(/\•/g).length;              
                for (let i = 0; i < linecnt; i++) {

                    instruction = instruction.replace('_#_', '\n');
                }
            }
            $('#txtInstruction').val(instruction);
            document.getElementById("hdnMTName").value = templatename;
            $('#btnCreate').val('Update');
            $('#ddlMasterTemplate').val(master_template_id);
            $('#ddlMasterTemplate').prop('disabled', 'disabled');
        }
        function ClearAll() {


            document.getElementById("hdnMTName").value = "";
            $('#hdnTID').val('');
            $("#txtInstruction").val('');
            $('#btnCreate').val('Create');
            $('#txtMasterTemplateName').invisible();
            $('#ddlMasterTemplate').removeAttr("disabled");           
            $('#ddlMasterTemplate').prop('selectedIndex', 0);


        }
        function UpdateHdnTID() {
            if ($('#btnCreate').val() == "Save") {
                $('#hdnTID').val('');
            }
        }
        //$.messager.confirm(
        function In2InGlobalConfirm(id) {

            $.messager.confirm({
                title: 'In2In Global Confirmation',
                msg: 'Are you sure you want to delete this?',
                ok: 'Yes',
                cancel: 'No',
                fn: function (r) {
                    if (r) {
                        $('#hdnTID').val(id);
                        $('#hdnDelBtn').trigger("click");
                    }
                }
            });
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

            $('.messager-body.panel-body.panel-body-noborder.window-body').css('height', '32px');
            $('.messager-body.panel-body.panel-body-noborder.window-body').css('width', '275px');
        }

        function ShowAssignTemplate() {

            $('.Asgnpnl').visible();
            $('.crtpnl').invisible();
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


        function ShowUploadMasterTemplate() {
            //$('.Asgnpnl').invisible();
            $('.crtpnl').invisible();
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
            $('input[type="file"]').change(function (e) {
                uploadingFileName = e.target.files[0].name;

            });
            
            var winH = 50
            Error_Message = "";
            Error_Count = 1;


            var idxDot = uploadingFileName.lastIndexOf(".") + 1;
            var extFile = uploadingFileName.substr(idxDot, uploadingFileName.length).toLowerCase();

            if (CheckNull(uploadingFileName, in2in23)) {
                if (CheckFileExtension(extFile, "csv", in2in24)) {

                    CheckMasterTemplate(uploadingFileName, in2in26, winH)
                }
            }

            if (Error_Count == 2) {
                Error_Message = Error_Message.replace("1 .", "");                
            }
           
            if (Error_Message != "")
            {
               
                uploadingFileName = "";
                if (Error_Message.length < 200)
                {
                    ShowError(Error_Message, 32);
                }
                else
                {
                    ShowError(Error_Message, 70);
                }
                uploadingFileName = "";
                return false;
            }
            else {

                return true;
            }
        }
       
        $("#btnUpload").click(function (evt)
        {
            if (VerifyFile())
            {
                
                var fileUpload = $("#tmpltFU").get(0);
                var files = fileUpload.files;

                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                //moved  below parameters to session variables.
               /* data.append("targetfolder", "./mastertemplate/");
                data.append("uploadedby", $('#txtcreatedb').text());
                data.append("forscreen", "templatemanagement");*/
               
               
                $.ajax({
                    url: "FileUploadHandler.ashx",
                    type: "POST",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (result) {

                        if (result != '') {

                            RefreshTemplateNames(result);
                            ShowServerMessage("Master Template Uploaded Successfully.");                          
                            $("#tmpltFU").val('');
                            uploadingFileName = "";

                        }

                    },
                    error: function (err) {
                        ShowServerMessage(err.statusText);
                    },
                    complete: function (data) {
                        
                        ShowCreateTemplate();

                    }
                });
                
            }
          
            evt.preventDefault();
        }); 
        function RefreshTemplateNames(templates) {

            if (templates.length > 0) {
                var myTemplates = $.parseJSON(templates);
                $('#ddlMasterTemplate').html('');
                var myDdl = document.getElementById('ddlMasterTemplate');    
                myDdl.innerHTML = "<option selected='selected' value='0'>--Select a Template--</option>";
                for (var i = 0; i < myTemplates.length; i++) {
                  
                    myDdl.innerHTML = myDdl.innerHTML + "<option value='" + myTemplates[i]['template_id'] + "'>" + myTemplates[i]['file_name'] + "</option>";
                  
                }
                $('#ddlMasterTemplate').val('first').change();
                $("#ddlMasterTemplate").prop('selectedIndex', 0);
            }


        }
        
    </script>
    <style type="text/css">
        body {
            background-color: azure;
        }

        .window-body.panel-body {
            color: silver;
            padding-top: 10px;
            text-align: left;
            vertical-align: middle;
        }

        .panel-title {
            color: greenyellow;
            background-color: #8f0108;
            border: 0px solid #dddddd;
            text-indent: 5px;
            border-radius: 5px;
        }

        .l-btn-text {
            color: yellow;
        }

        .specify {
            overflow: hidden;
            text-overflow: ellipsis;
            max-height: 20px;
            height: 20px;
            word-break: break-all;
            word-wrap: break-word;
            display: block;
            border: none;
        }

        .messager-body.panel-body.panel-body-noborder.window-body {
            width: 278px;
            height: 32px;
        }
    </style>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.ProjectManagement" %>

<!DOCTYPE html>


<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="css/gridview.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Project Management</span></div>
               <%-- <asp:ScriptManager ID="scriptmanager1" runat="server" >
                </asp:ScriptManager>
                <asp:UpdatePanel ID="pdnlCompany" runat="server">
                    <asp:ContentTemplate>
                         </asp:ContentTemplate>
                </asp:UpdatePanel>--%>
                <asp:ScriptManager ID="projectscriptmanager" runat="server">                    
                </asp:ScriptManager>
                <asp:UpdateProgress ID="UpdatePnlProject" runat="server" AssociatedUpdatePanelID="pdnlProject">
                <ProgressTemplate>
                        <img src="img/uploading.gif" alt="Uploading..." />
                </ProgressTemplate>
            </asp:UpdateProgress>
                <asp:UpdatePanel  ID="pdnlProject" runat="server">   
                   
                    <ContentTemplate>
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
                                            <div style="width: 70%; border: 1px solid black; border-radius: 5px; margin-top: 10px; margin-bottom: 20px;">                                    
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
                                                        <asp:CommandField ItemStyle-HorizontalAlign="Center" HeaderText="Action" ShowEditButton="true" ShowDeleteButton="true" />                                                                                                             
                                                    </Columns>
                                                </asp:GridView>
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


    <script type="text/javascript">

        var BASE_URL = "ProjectManagement.aspx";
        $(document).ready(function () {
            ClearProject();
        });
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
    </script>
    <style type="text/css">
        body {
            background-color: azure;
        }
    </style>
</body>
</html>

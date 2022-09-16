<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.UserManagement" %>

<!DOCTYPE html>


<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <link href='https://fonts.googleapis.com/css?family=Work+Sans:300,400,600&Inconsolata:400,700' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />    
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
     <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />  
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" /> 
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/Grid.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />   
   
</head>
<body>

    <form id="form1" runat="server">
       
        <center>
            <div style="width: 100%;height:435px;"  class="MainPageFrameDiv">
                <div class="pagination-ys"><span class="menu_frame_title">User Management</span></div>
                <asp:ScriptManager ID="scriptmanager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="pdnlCompany" runat="server">
                    <ContentTemplate> 
                        <div style="width:100%" id="userDiv">                       
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 90%;">
                                    <center>
                                        <div class="formDiv" style="width: 80%; border: 0px solid #d3d3d3;border-radius: 5px; margin-top: 10px;">
                                            <table style="width: 98%;padding-top:10px;padding-left:30px">
                                                <tr>
                                                    <td>First Name(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="text" id="txtFName" class="validate" data-validate-msg="First Name cannot be blank." runat="server" value="" /></td>
                                                    <td>Last Name(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="text" id="txtLName" runat="server" value="" /></td>
                                                </tr>
                                                <tr>
                                                    <td>Company Name(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCompanyName" runat="server" AppendDataBoundItems="true" DataTextField="CompanyName">
                                                            <asp:ListItem Text="-- Select a Company --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>Role Name(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlRoleName" AppendDataBoundItems="true"  runat="server" DataTextField="RoleName">
                                                            <asp:ListItem Text="-- Select a Role --"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Email ID(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="text" id="txtEmail" runat="server" value="" />
                                                        <asp:HiddenField ID="hdnUserEmail" runat="server" Value="" />                                                        
                                                        <asp:Button runat="server" ID="hdnDelBtn" style="display:none;" OnClientClick="return true;" OnClick="hdnDelBtn_Click" />
                                                    </td>
                                                    <td>Password(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="password" id="txtPassword" autocomplete="off" runat="server" value="" /></td>
                                                </tr>
                                                <tr> 
                                                    <td>Activity Name</td>
                                                    <td>
                                                        <asp:DropDownList  AppendDataBoundItems="true" ID="ddlActivityAccess" runat="server" DataTextField="ActivityAccess">                                                            
                                                                                                              
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td colspan="3">
                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                         <div style="margin-top: 10px;">
                                                            <center>
                                                                <asp:Button runat="server" id="btnSave" CssClass="button" Text="Save" OnClientClick="return ValidateUser();" OnClick="AddNewUser" />
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
                                <td style="padding-bottom:50px;padding-top:5px">                                   
                                    <center>
                                       
                                        <div class="AspNet-GridView" style="width: 70%;border: 1px solid #d3d3d3;border-radius: 5px;">
                                       <asp:GridView ID="grdUsers" runat="server" OnRowEditing="grdUsers_RowEditing"
                                            OnPageIndexChanging="grdUsers_PageIndexChanging" Width="100%" HeaderStyle-CssClass="AspNet-GridView" 
                                            AllowPaging="True" DataKeyNames="user_email" PageSize="4"
                                            OnRowDataBound="grdUsers_RowDataBound"  AutoGenerateColumns="false">
                                             <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                            <columns>
                                                <asp:BoundField DataField="first_name" ControlStyle-Width="94%" HeaderText="First Name" />
                                                <asp:BoundField DataField="last_name" ControlStyle-Width="94%" HeaderText="Last Name" />
                                                <asp:BoundField DataField="company_name" ControlStyle-Width="94%" HeaderText="Company" />
                                                <asp:BoundField DataField="user_email" ReadOnly="true" ControlStyle-Width="94%" HeaderText="Email ID" />
                                                <asp:BoundField DataField="role_name" ReadOnly="true" ControlStyle-Width="94%" HeaderText="Role" />
                                                <asp:BoundField DataField="activity_name" ControlStyle-Width="94%" HeaderText="Activity Name" />                                                
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
                                            </columns>
                                             <PagerStyle HorizontalAlign = "Center" CssClass="GridPager" />
                                        </asp:GridView>
                                            </div>
                                      
                                    </center>
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" runat="server" id="hdnServerMessage" />
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
    
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        $(document).ready(function () {
            //$('select:not(.ignore)').niceSelect();
            FastClick.attach(document.body);
            ClearAll(); 
            window.parent.$('#navOverlayImg').hide();
            window.parent.$('#navOverlay').hide();
        });
        function InitializeRequest(sender, args) {
            window.parent.$('#navOverlayImg').css("top", 176);
            window.parent.$('#navOverlayImg').css("left", 820);
            window.parent.$('#navOverlayImg').show();
            window.parent.$('#navOverlay').show();

        }

        function EndRequest(sender, args) {
            window.parent.$('#navOverlayImg').hide();
            window.parent.$('#navOverlay').hide();
        }
        function In2InGlobalConfirm(email) {

            $.messager.confirm({
                title: 'In2In Global Confirmation',
                msg: 'Are you sure you want to delete this User?',
                ok: 'Yes',
                cancel: 'No',
                fn: function (r) {

                    if (r) {
                        $('#hdnUserEmail').val(email);
                        
                        $('#hdnDelBtn').trigger('click');
                    }
                    else {
                        $('#hdnUserEmail').val('');                        

                    }
                }
            });
        }   
       
        function PullDataToEdit(fname, lname,company,email,role, activity, phone) {
                        
            $('#txtFName').val(fname);
            $('#hdnUserEmail').val(email);
            $('#txtLName').val(lname);  
            $('#txtEmail').val(email);                         
            $("#ddlRoleName").val(role);
            $("#ddlCompanyName").val(company);           
            $("#ddlActivityAccess").val(activity);
            $("#txtPassword").val("******");
            $('input[type="password"]').prop("readonly", true);
            $('input[type="password"]').attr("fieldtype", "readonly");
            $('#txtEmail').prop("readonly", true);
            $('#txtEmail').attr("fieldtype", "readonly");

            $("#txtPassword").css("background-color", "lightgray")
            
            ModifyActivity(role);
            $('#btnSave').val('Update');

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

        function ValidateUser() {

            Error_Message = "";
            Error_Count = 1;

            CheckNull($("#txtFName").val(), in2in10);
            CheckNull($("#txtLName").val(), in2in11);
            CheckNullDropdown($("select[name='ddlCompanyName'] option:selected").index(), in2in21);
            CheckNullDropdown($("select[name='ddlRoleName'] option:selected").index(), in2in12);            
         
            CheckNull($("#txtEmail").val(), in2in6)

            if ($("#txtEmail").val() != "") {
                if (isValidEmailAddress($('#txtEmail').val()) == false) {

                    Error_Message = Error_Message + Error_Count + " . " + InvalidEmail_err_msg + "<br>";
                    Error_Count = Error_Count + 1;
                }

            }
            CheckNull($('input[type="password"]').val(), in2in13);
            if (Error_Message != "") {
                ShowError(Error_Message, 120);
                return false;
            }
            else {

                return true;
            }
        }
        function ClearAll() {

            $('#txtFName').val('');
            $('#txtLName').val('');
            $('#txtEmail').val('');
            
            $('#ddlRoleName').prop('selectedIndex', 0);
            $('#ddlCompanyName').prop('selectedIndex', 0);
            $('#ddlActivityAccess').prop('selectedIndex', 0);           
            $('#ddlActivityAccess').prop("disabled", false);
            $('#btnSave').val('Save');
            $('#hdnUserEmail').val('');
            $("#txtPassword").val('');
            $('input[type="password"]').prop("readonly", false);
            $("#txtPassword").css("background-color", "white")            
        }
       function ModifyActivity(selectedRole) {
           
            if (selectedRole == "1") {

                $('#ddlActivityAccess').val('1');
                $('#ddlActivityAccess').prop("disabled", true);
            }
            else {

                $('#ddlActivityAccess').prop("disabled", false);
                $('#ddlActivityAccess').prop('selectedIndex', 0);

            }
        }
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-64633646-1', 'auto');
        ga('send', 'pageview');

        function ShowHidden() { }
    </script>
    <style type="text/css">
       
        .window-body.panel-body {
               color:silver;              
               padding-top:30px;
               text-align:left;
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

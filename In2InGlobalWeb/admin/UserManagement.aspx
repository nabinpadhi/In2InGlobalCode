<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.UserManagement" %>

<!DOCTYPE html>


<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
</script>

    <link href='https://fonts.googleapis.com/css?family=Work+Sans:300,400,600&Inconsolata:400,700' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />   
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
  
   
</head>
<body>
    
    <form id="form1" runat="server">         
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 20px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">User Management</span></div>
                <asp:ScriptManager ID="scriptmanager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="pdnlCompany" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; background-color: azure;">
                            <tr>
                                <td style="width: 80%;">
                                    <center>
                                        <div style="width: 80%; border: 1px solid black; border-radius: 5px; margin-top: 30px;">
                                            <table style="width: 80%;">
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
                                                        <asp:DropDownList ID="ddlCompanyName" Width="80%" runat="server" DataTextField="CompanyName">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>Role Name(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlRoleName" Width="100%" runat="server" DataTextField="RoleName"></asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Email ID(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="text" id="txtEmail" runat="server" value="" /></td>
                                                    <td>Password(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="password" id="txtPassword" autocomplete="off" runat="server" value="" /></td>
                                                </tr>
                                                <tr>                                                    
                                                    <td colspan="4">
                                                        <div style="margin-top: 10px;">
                                                            <center>
                                                                <asp:Button runat="server" CssClass="button" Text="Save" OnClientClick="return ValidateUser();" OnClick="AddNewUser" />
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

                                        <asp:GridView ID="grdUsers" runat="server" OnRowEditing="grdUsers_RowEditing" OnRowDeleting="grdUsers_RowDeleting"
                                            OnPageIndexChanging="grdUsers_PageIndexChanging" Width="80%" HeaderStyle-CssClass="pagination-ys"
                                            AllowPaging="True" DataKeyNames="Email" PageSize="4" OnRowUpdating="grdUsers_RowUpdating"
                                            OnRowCancelingEdit="grdUsers_RowCancelingEdit" AutoGenerateColumns="false">
                                            <columns>
                                                <asp:BoundField DataField="FirstName" ControlStyle-Width="94%" HeaderText="First Name" />
                                                <asp:BoundField DataField="LastName" ControlStyle-Width="94%" HeaderText="Last Name" />
                                                <asp:BoundField DataField="Company" ControlStyle-Width="94%" HeaderText="Company" />
                                                <asp:BoundField DataField="Email" ReadOnly="true" ControlStyle-Width="94%" HeaderText="Email ID" />
                                                <asp:BoundField DataField="Role" ReadOnly="true" ControlStyle-Width="94%" HeaderText="Role" />
                                                <asp:CommandField ShowEditButton="true" />
                                                <asp:CommandField ShowDeleteButton="true" />
                                            </columns>
                                            <pagerstyle cssclass="pagination-ys" />
                                        </asp:GridView>
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>                   
                </asp:UpdatePanel>
                 <br />
                 <br />
                 <br />
            </div>
        </center>
    </form> 
    <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" language="javascript"></script>
    <script src="../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" language="javascript"></script>
     <script src="../scripts/ErrorMessage.js" type="text/javascript" language="javascript"></script>

    <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/Validation.js?"), DateTime.Now.Ticks) %>" type="text/javascript" language="javascript"></script>
    <script src="js/jquery.nice-select.min.js" type="text/javascript" language="javascript"></script>
    <script src="js/fastclick.js" type="text/javascript" language="javascript"></script>
    <script src="js/prism.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('select:not(.ignore)').niceSelect();
            FastClick.attach(document.body);
            ClearAll();
        });
        function ValidateUser() {

            Error_Message = "";
            Error_Count = 1;

            CheckNull($("#txtFName").val(), in2in10);
            CheckNull($("#txtLName").val(), in2in11);
            CheckNullDropdown($("select[name='ddlCompanyName'] option:selected").index(), in2in5);
            CheckNullDropdown($("select[name='ddlRoleName'] option:selected").index(), in2in12);
            CheckNull($("#txtEmail").val(), in2in6);
            CheckNull($('input[type="password"]').val(), in2in13);           
            if (Error_Message != "") {
                ShowError(Error_Message,80);
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
            ResetDropDowns();
            $('input[type="password"]').val('');
        }
        function ResetDropDowns() {
            var companyddl = $('.nice-select')[0];
            var roleddl = $('.nice-select')[1];
            $(companyddl).find('.current').remove();
            $(companyddl).append("<span class='current'>--Select a Company--</span>");
            $(roleddl).find('.current').remove();
            $(roleddl).append("<span class='current'>--Select a Role--</span>");
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
        body {
            background-color: azure;
        }
    </style>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.TemplateManagement" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 20px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Template Management</span></div>
                <asp:ScriptManager ID="scriptmanager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="pdnlCompany" runat="server">
                    <ContentTemplate>
                <table style="width: 100%; background-color: azure;">
                    <tr>
                        <td>
                            <center>
                                <div style="width: 60%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Template Name</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTemplates" Width="100%" runat="server" DataTextField="TemplateName"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>Email Id</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUsers" Width="100%" runat="server" DataTextField="Email"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Project Name</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProjects" Width="100%" runat="server" DataTextField="ProjectName"></asp:DropDownList>
                                                        </td>
                                                    </tr>                                                   
                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Instruction</td>
                                                        <td>
                                                            <textarea id="txtInstruction" style="width: 97%; height: 70px;" runat="server"></textarea>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="margin-top: 15px;">
                                                    <center>
                                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return ValidateTemplate();" OnClick="btnSave_Click" />
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
                                    <asp:GridView DataKeyNames="ID" ID="grdTemplate" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                        AllowPaging="True" OnRowDeleting="grdTemplate_RowDeleting" OnPageIndexChanging="grdTemplate_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Template Name" DataField="TemplateName" />
                                            <asp:BoundField HeaderText="Date Added" DataField="DateAdded" />
                                            <asp:BoundField HeaderText="Project Name" DataField="ProjectName" />
                                            <asp:BoundField HeaderText="User Email" DataField="Email" />
                                            <asp:CommandField ShowDeleteButton="true" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </center>
                        </td>
                    </tr>
            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
    <script>
        $(document).ready(function () {
            $('select:not(.ignore)').niceSelect();
            FastClick.attach(document.body);
            ClearAll();
        });        
        function ClearAll() {
            
            var templateddl = $('.nice-select')[0];
            var userddl = $('.nice-select')[1];
            var projectddl = $('.nice-select')[2];

            $(templateddl).find('.current').remove();
            $(templateddl).append("<span class='current'>--Select a Template--</span>");
            $(userddl).find('.current').remove();
            $(userddl).append("<span class='current'>--Select an User--</span>");
            $(projectddl).find('.current').remove();
            $(projectddl).append("<span class='current'>--Select a Project--</span>");
            $('#ddlTemplates').prop('selectedIndex', 0);
            $('#ddlUsers').prop('selectedIndex', 0);
            $('#ddlProjects').prop('selectedIndex', 0);
            $("#txtInstruction").val('');
        }
        function ValidateTemplate() {

            Error_Message = "";
            Error_Count = 1;            
            CheckNullDropdown($("select[name='ddlTemplates'] option:selected").index(), in2in15);
            CheckNullDropdown($("select[name='ddlUsers'] option:selected").index(), in2in16);
            CheckNullDropdown($("select[name='ddlProjects'] option:selected").index(), in2in17);
            CheckNull($("#txtInstruction").val(), in2in18);
            if (Error_Message != "") {
                ShowError(Error_Message, 80);
                return false;
            }
            else {
                return true;
            }
        }
        function ShowHidden() {

        }
    </script>
    <script>
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
    </style>
</body>
</html>

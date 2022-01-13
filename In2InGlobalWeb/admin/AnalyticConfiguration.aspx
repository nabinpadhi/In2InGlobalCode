﻿<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AnalyticConfiguration.aspx.cs" Inherits="In2InGlobal.presentation.admin.AnalyticConfiguration" %>

<!DOCTYPE html>


<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <link href="css/gridview.css" rel="stylesheet" type="text/css" />

    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/Grid.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <script lang="JavaScript">

</script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; height: 435px; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Analytic Configuration</span></div>
                <asp:ScriptManager ID="analyticconfigurationscriptmanager" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="pdnlanalyticconfiguration" runat="server">
                    <ContentTemplate>
                        <div style="border-bottom: 0 solid gray; display: flex; padding: 2px; width: auto;">
                            <div id="btnAnaConf" onclick="ShowAnaConf();" class="PanelTab" style="background-color: azure; color: blue;">Analytics Configuration</div>
                            <div id="btnProConf" onclick="ShowProConf();" class="PanelTab">Process Configuration</div>
                        </div>

                        <div title="Analytics Configuration" class="anaconf" style="background-color: azure; color: blue;">
                            <div style="width: 100%" id="analyticconfigurationDiv">

                                <table style="width: 90%; background-color: azure;">
                                    <tr>
                                        <td>
                                            <center>
                                                <div style="width: 80%; border: 1px solid black; border-radius: 5px; margin-top: 10px;">
                                                    <table style="width: 80%; padding-top: 10px;">
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                <span style="font-size: small; font-weight: bold;">Select Company(<span style="color: red">*</span>)</span>
                                                                <asp:DropDownList ID="ddlCompany" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" Width="94%" AppendDataBoundItems="true" DataValueField="com_id" DataTextField="company_name">
                                                                    <asp:ListItem>--Select a Company</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <input type="text" readonly="true" value="" style="width: 96%; display: none; background-color: #d6d2d2;" fieldtype="readonly" runat="server" id="txtCompanyName" name="txtCompanyName" class="txtCompanyName" />
                                                            </td>
                                                            <td style="width: 35%;">
                                                                <span style="font-size: small; font-weight: bold;">Select User(<span style="color: red">*</span>)</span>
                                                                <asp:DropDownList ID="ddlUser" AutoPostBack="true" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" runat="server" Width="94%" AppendDataBoundItems="true" DataValueField="user_id" DataTextField="user_email">
                                                                    <asp:ListItem>--Select a User--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <input type="text" readonly="true" value="" style="width: 96%; display: none; background-color: #d6d2d2;" fieldtype="readonly" runat="server" id="txtUser" name="txtUser" class="txtUser" />
                                                            </td>
                                                            <td style="width: 35%;">
                                                                <span style="font-size: small; font-weight: bold;">Select Project(<span style="color: red">*</span>)</span>
                                                                <asp:DropDownList ID="ddlProject" runat="server" Width="94%" AppendDataBoundItems="true" DataValueField="user_id" DataTextField="user_email">
                                                                    <asp:ListItem>--Select a Project--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <input type="text" readonly="true" value="" style="width: 96%; display: none; background-color: #d6d2d2;" fieldtype="readonly" runat="server" id="txtProject" name="txtProject" class="txtProject" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 18%;"><span style="font-size: small; font-weight: bold;">Dashboard Link</span>(<span style="color: red">*</span>)</td>
                                                                        <td style="width: 82%;">
                                                                            <input type="text" value="" style="width: 96%" runat="server" id="txtlink" name="txtlink" class="txtlink" /></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <div style="margin-top: 30px;">
                                                                    <center>
                                                                        <asp:Button runat="server" ID="btnSave" CssClass="button" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateDashboardConfiguration();" />
                                                                        <input type="button" class="button" style="margin-left: 10px;" value="Cancel" onclick="ClearAll();" />
                                                                        <asp:Button runat="server" ID="hdnDelBtn" Text="" style="display: none;" OnClientClick="return true;" OnClick="hdnDelBtn_Click" />
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
                                        <td style="width: 80%;">
                                            <center>
                                                <%--<div style="width: 72%; height: 90%; border: 1px solid black; border-radius: 5px; margin-top: 10px; margin-bottom: 20px;"> </div>--%>
                                                <div>
                                                    <asp:HiddenField ID="hdnDBID" Value="" runat="server" />
                                                    <asp:GridView runat="server" ID="grdAnalyticsLink" Width="72%" OnPageIndexChanging="grdAnalyticsLink_PageIndexChanging"
                                                        HeaderStyle-CssClass="AspNet-GridView" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No Configuration data found"
                                                        AllowPaging="True" OnRowDataBound="grdAnalyticsLink_RowDataBound" DataKeyNames="id" PageSize="4" AutoGenerateColumns="false">
                                                        <AlternatingRowStyle CssClass="AspNet-GridView-Alternate" />
                                                        <Columns>
                                                            <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridColumnHeader">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompanyid" runat="server" Text='<%#Eval("company_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridColumnHeader">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserid" runat="server" Text='<%#Eval("user_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridColumnHeader">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProjectid" runat="server" Text='<%#Eval("project_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="company_name" HeaderText="Company Name" />
                                                            <asp:BoundField DataField="user_email" HeaderText="User Email" />
                                                            <asp:BoundField DataField="project_name" HeaderText="Project Name" />
                                                            <asp:BoundField HeaderStyle-CssClass="specifyCol" ItemStyle-CssClass="specify" DataField="dashboard_url" HeaderText="Dashboard Link" />
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="EditButton" CssClass="GridEditButton" runat="server" Text="" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="DeleteButton" CssClass="GridDeleteButton" runat="server" Text="" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                    </asp:GridView>
                                                </div>

                                            </center>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div title="Process Configuration" class="proconf" style="background-color: azure; color: blue;display:none;">
                            <table style="width: 90%; background-color: azure;">
                                <tr>
                                    <td>
                                        <center>
                                            <div style="width: 80%; border: 1px solid black; border-radius: 5px; margin-top: 10px;">
                                                <table style="width: 80%; padding-top: 10px;">
                                                    <tr>
                                                        <td style="width: 30%;">
                                                            <span style="font-size: small; font-weight: bold;">Select Company(<span style="color: red">*</span>)</span>
                                                            <asp:DropDownList ID="ddlCompanyProcess" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCompanyProcess_SelectedIndexChanged" Width="94%" AppendDataBoundItems="true" DataValueField="com_id" DataTextField="company_name">
                                                                <asp:ListItem>--Select a Company</asp:ListItem>
                                                            </asp:DropDownList>
                                                            
                                                        </td>
                                                        <td style="width: 35%;">
                                                            <span style="font-size: small; font-weight: bold;">Select User(<span style="color: red">*</span>)</span>
                                                            <asp:DropDownList ID="ddlUserProcess" AutoPostBack="true" OnSelectedIndexChanged="ddlUserProcess_SelectedIndexChanged" runat="server" Width="94%" AppendDataBoundItems="true" DataValueField="user_id" DataTextField="user_email">
                                                                <asp:ListItem>--Select a User--</asp:ListItem>
                                                            </asp:DropDownList>
                                                           
                                                        </td>
                                                        <td style="width: 35%;">
                                                            <span style="font-size: small; font-weight: bold;">Select Project(<span style="color: red">*</span>)</span>
                                                            <asp:DropDownList ID="ddlProjectProcess" runat="server" Width="94%" AppendDataBoundItems="true" DataValueField="project_id" DataTextField="project_name">
                                                                <asp:ListItem>--Select a Project--</asp:ListItem>
                                                            </asp:DropDownList>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <div style="margin-top: 30px;">
                                                               
                                                                    <asp:Button  runat="server" ID="btnUpdateProcess" CssClass="button" Text="Update" tooltip="Update Process" OnClick="btnUpdateProcess_Click" OnClientClick="return ValidateProcessConfiguration();" />
                                                                    <input type="button" class="button" style="margin-left: 10px;" value="Cancel" onclick="ClearAllProcess();" />
                                                                   
                                                               
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

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
    <script src="js/fastclick.js" type="text/javascript" lang="javascript"></script>
    <script src="js/prism.js" type="text/javascript" lang="javascript"></script>

    <link rel="stylesheet" href="css/jquery-ui.css">
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ShowAnaConf();
            ClearAll();
            $('.aspNetDisabled').css('color', 'darkgray');
            $('.aspNetDisabled:hover').css('text-decoration', 'none');
        });
        function ClearAll() {

            $('#ddlCompany').removeAttr("disabled");
            $('#ddlCompany').prop('selectedIndex', 0);
            $('#ddlUser').prop('selectedIndex', 0);
            $('#ddlProject').prop('selectedIndex', 0);
            $('#txtlink').val('');
            $('#btnSave').val('Save');
            $('#ddlUser').prop('disabled', 'disabled');
            $('#ddlProject').prop('disabled', 'disabled');
            $('#txtUser').val('');
            $('#txtUser').hide();
            $('#ddlUser').show();
            $('#txtProject').val('');
            $('#txtProject').hide();
            $('#ddlProject').show();
            $('#txtCompanyName').val('');
            $('#txtCompanyName').hide();
            $('#ddlCompany').show();

        }
        function ValidateDashboardConfiguration() {

            Error_Message = "";
            Error_Count = 1;
            if ($('#btnSave').val() != 'Update') {
                if (CheckNullDropdown($("select[name='ddlCompany'] option:selected").index(), in2in21)) {
                    if (CheckNullDropdown($("select[name='ddlUser'] option:selected").index(), in2in16)) {

                        CheckNullDropdown($("select[name='ddlProject'] option:selected").index(), in2in17);
                    }

                }
            }
            CheckNull($('#txtlink').val(), in2in27);
            if (Error_Message != "") {
                ShowError(Error_Message, 80);
                return false;
            }
            else {
                return true;
            }
        }
        function ValidateProcessConfiguration() {

            Error_Message = "";
            Error_Count = 1;

            if (CheckNullDropdown($("select[name='ddlCompanyProcess'] option:selected").index(), in2in21)) {
                if (CheckNullDropdown($("select[name='ddlUserProcess'] option:selected").index(), in2in16)) {

                    CheckNullDropdown($("select[name='ddlProjectProcess'] option:selected").index(), in2in17);
                }

            }


            if (Error_Message != "") {
                ShowError(Error_Message, 80);
                return false;
            }
            else {
                return true;
            }

        }
        function PullDataToEdit(link, DBID, user, companyname, projectname) {


            $('#ddlUser').prop('disabled', 'disabled');
            $('#ddlUser').hide();
            $('#txtUser').val(user);
            $('#txtUser').show();
            $('#ddlProject').prop('disabled', 'disabled');
            $('#ddlProject').hide();
            $('#txtProject').val(projectname);
            $('#txtProject').show();
            $('#ddlCompany').prop('disabled', 'disabled');
            $('#ddlCompany').hide();
            $('#txtCompanyName').val(companyname);
            $('#txtCompanyName').show();
            $('#hdnDBID').val(DBID);
            $('#txtlink').val(link);
            $('#btnSave').val('Update');

        }

        function In2InGlobalConfirm(id) {

            $.messager.confirm({
                title: 'In2In Global Confirmation',
                msg: 'Are you sure you want to delete this?',
                ok: 'Yes',
                cancel: 'No',
                fn: function (r) {
                    if (r) {
                        $('#hdnDBID').val(id);
                        $('#hdnDelBtn').trigger("click");
                    }
                }
            });
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
        function ShowAnaConf() {
            $('.anaconf').show();
            $('.proconf').hide();

            $('#btnAnaConf').css("background-color", "azure");
            $('#btnAnaConf').css("color", "blue");
            $('#btnProConf').css("background-color", "#2c3c59");
            $('#btnProConf').css("color", "#fff");
        }
        function ShowProConf() {
            $('.anaconf').hide();
            $('.proconf').show();

            $('#btnProConf').css("background-color", "azure");
            $('#btnProConf').css("color", "blue");
            $('#btnAnaConf').css("background-color", "#2c3c59");
            $('#btnAnaConf').css("color", "#fff");

        }
        function ShowHidden() {

        }
    </script>
    <style type="text/css">
        body {
            background-color: azure;
        }

        .window-body.panel-body {
            color: silver;
            padding-top: 30px;
            text-align: left;
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
            display: inline-block;
            white-space: nowrap;
            Width: 386px;
        }

        .specifyCol {
            overflow: hidden;
            text-overflow: ellipsis;
            height: 20px;
            word-break: break-all;
            word-wrap: break-word;
            display: flex;
            white-space: nowrap;
            Width: 388px;
            text-align: center;
            align-items: center;
        }
    </style>
</body>
</html>

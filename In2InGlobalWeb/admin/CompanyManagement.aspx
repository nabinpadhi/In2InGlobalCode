<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.CompanyManagement" %>

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
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 20px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Company Management</span></div>
               <%-- <asp:ScriptManager ID="scriptmanager1" runat="server" >
                </asp:ScriptManager>
                <asp:UpdatePanel ID="pdnlCompany" runat="server">
                    <asp:ContentTemplate>
                         </asp:ContentTemplate>
                </asp:UpdatePanel>--%>
                        <table style="width: 100%; background-color: azure;">
                            <tr>
                                <td>
                                    <center>
                                        <div style="width: 50%; border: 1px solid black; border-radius: 5px; margin-top: 10px;">
                                            <table>
                                                <tr>
                                                    <td>Company Name(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="text" id="txtCompanyName" runat="server" value="" /></td>
                                                    <td>LOB</td>
                                                    <td>
                                                        <input type="text" id="txtLOB" runat="server" value="" /></td>
                                                </tr>
                                                <tr>
                                                    <td>Phone Number</td>
                                                    <td>
                                                        <input type="text" id="txtPhoneNo" runat="server" />

                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <div style="margin-top: 30px;">
                                                            <center>
                                                                <asp:Button runat="server" ID="btnSave" CssClass="button" Text="Save" OnClientClick="return ValidateCompany();" OnClick="btnSave_Click" />
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
                                        <div style="width: 60%; height: 90%; border: 1px solid black; border-radius: 5px; margin-top: 30px; margin-bottom: 20px;">                                           
                                             <asp:GridView ID="grdCompany" Width="100%" runat="server" OnRowEditing="grdCompany_RowEditing" OnRowDeleting="grdCompany_RowDeleting"
                                                OnPageIndexChanging="grdCompany_PageIndexChanging" HeaderStyle-CssClass="pagination-ys" AllowPaging="True"
                                                DataKeyNames="company_id" PageSize="4" OnRowUpdating="grdCompany_RowUpdating"
                                                OnRowCancelingEdit="grdCompany_RowCancelingEdit" OnRowDataBound="grdCompany_RowDataBound" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="company_id" ControlStyle-Width="94%" HeaderText="CompanyID" Visible="false" />
                                                    <asp:BoundField DataField="company_name" ControlStyle-Width="94%" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="lob" ControlStyle-Width="94%" HeaderText="LOB" />
                                                    <asp:BoundField DataField="company_phone" ControlStyle-Width="94%" HeaderText="Phone No" />
                                                    <asp:CommandField ShowEditButton="true" />
                                                    <asp:CommandField ShowDeleteButton="true" />
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys" />
                                            </asp:GridView>
                                        </div>
                                    </center>
                                </td>
                            </tr>
                        </table>
                   
            </div>
        </center>
    </form>
    <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../NewJEasyUI/jquery.easyui.min.js" type="text/javascript" lang="javascript"></script>
    <script src="../scripts/ErrorMessage.js" type="text/javascript" lang="javascript"></script>
    <script src="<%= String.Format("{0}dt={1}",ResolveUrl("../scripts/Validation.js?"), DateTime.Now.Ticks) %>" type="text/javascript" lang="javascript"></script>


    <script type="text/javascript">

        var BASE_URL = "CompanyManagement.aspx";
        $(document).ready(function () {
            ClearAll();
        });
        function ClearAll() {

            $('#txtCompanyName').val('');
            $('#txtEmail').val('');
            $('#txtPhoneNo').val('');

        }
        function ValidateCompany() {

            Error_Message = "";
            Error_Count = 1;

            CheckNull($('#txtCompanyName').val(), in2in5);
           
            if ($('#txtPhoneNo').val() != "") {
                ValidatePhoneNumber($('#txtPhoneNo').val(), in2in20);
            }                       
            if (Error_Message != "") {
                ShowError(Error_Message, 80);
                return false;
            }
            else {
                return true;
            }
        }

        function AddCompany() {
            var return_status = function () {
                var tmp = null;
                var lob = $('#txtLOB').val();
                var companyname = $('#txtCompanyName').val();
                var phoneno = $('#txtPhoneNo').val();
                var dataValue = "{ companyname:'" + companyname + "', lob:'" + lob + "',phoneno:'" + phoneno + "'}";
                $.ajax({
                    'async': false,
                    'type': "POST",
                    'global': false,
                    'dataType': 'json',
                    contentType: 'application/json; charset=utf-8',
                    'url': "CompanyManagement.aspx/AddNewCompany",
                    'data': dataValue,
                    data: "{ companyname:'" + $('#txtCompanyName').val() + "', lob:'" + $('#txtLOB').val() + "',phoneno:'" + $('#txtPhoneNo').val() + "'}",
                    success: function (data) {
                        tmp = data.d;
                    }
                });
                return tmp;
            }();

            if (return_status == "Success") {

                toastr.success('New company created', 'Success', { timeOut: 1000, progressBar: true, onHidden: function () { window.location.href = BASE_URL; } });
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


        }
        function ShowHidden() {

        }
    </script>
    <style type="text/css">
        body {
            background-color: azure;
        }
    </style>
</body>
</html>

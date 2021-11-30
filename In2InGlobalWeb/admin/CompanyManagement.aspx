<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CompanyManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.CompanyManagement" %>

<!DOCTYPE html>


<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="css/gridview.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css" />
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
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
            <div style="width: 100%;height:450px; border: 1px solid black; border-radius: 5px; margin-top: 20px;">
                <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height: 40px; padding-top: 10px;"><span class="menu_frame_title">Company Management</span></div>
                <asp:ScriptManager ID="companyscriptmanager" runat="server">                    
                </asp:ScriptManager>               
                <asp:UpdatePanel  ID="pdnlCompany" runat="server">                       
                    <ContentTemplate> 
                       <div style="width:100%" id="companyDiv">
                       <input type="hidden" value="" id="__EVENTARGUMENT" name="__EVENTARGUMENT">
                        <input type="hidden" value="" id="__EVENTTARGET" name="__EVENTTARGET">
                        
                        <table style="width: 70%; background-color: azure;">
                            <tr>
                                <td>
                                    <center>
                                        <div style="width: 50%; border: 1px solid black; border-radius: 5px; margin-top: 10px;">
                                            <table style="padding-top:10px;">
                                                <tr>
                                                    <td>Company Name(<span style="color: red">*</span>)</td>
                                                    <td>
                                                        <input type="text" id="txtCompanyName" runat="server" value="" />
                                                        <asp:HiddenField ID="hdnCompanyID" runat="server" Value="" />
                                                    </td>
                                                   
                                                   
                                                </tr>     
                                                 <tr>                                                   
                                                    <td>LOB</td>
                                                    <td>
                                                       <asp:DropDownList ID="ddlLOB" runat="server" AppendDataBoundItems="true" DataValueField="lob_id" DataTextField="lob_name">
                                                           <asp:ListItem>--Select a LOB--</asp:ListItem>
                                                       </asp:DropDownList>
                                                    </td>
                                                </tr>     
                                                <tr>
                                                    <td colspan="2">
                                                        <div style="margin-top: 30px;">
                                                            <center>
                                                                <asp:Button runat="server" ID="btnSave" CssClass="button" Text="Save" OnClientClick="return ValidateCompany();" OnClick="btnSave_Click"/>
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
                                      <div class="confirmDialog" style="text-align:center;color:black;display:none;position:center;padding-top:30px;">
                                        Are you sure you want to delete this record ?
                                    </div>
                                    <center>
                                        <div style="width: 50%; height: 90%; border: 1px solid black; border-radius: 5px; margin-top: 30px; margin-bottom: 20px;">                                           
                                             <asp:GridView ID="grdCompany" Width="100%" runat="server" OnRowEditing="grdCompany_RowEditing" OnRowDeleting="grdCompany_RowDeleting"
                                                OnPageIndexChanging="grdCompany_PageIndexChanging" HeaderStyle-CssClass="pagination-ys" AllowPaging="True"
                                                DataKeyNames="company_id" PageSize="10"  OnRowUpdating="grdCompany_RowUpdating"
                                                OnRowCancelingEdit="grdCompany_RowCancelingEdit" OnRowDataBound="grdCompany_RowDataBound" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="company_id" ControlStyle-Width="94%" HeaderText="CompanyID" Visible="false" />                                                    
                                                    <asp:BoundField DataField="company_name" ControlStyle-Width="94%" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="lob" ControlStyle-Width="94%" HeaderText="LOB" />                             
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                                                  <ItemTemplate >                                                      
                                                      <asp:LinkButton href="#" runat="server" id="lnkDel" >Delete</asp:LinkButton>   <asp:LinkButton href="#" runat="server" id="lnkEdit" >Edit</asp:LinkButton>
                                                  </ItemTemplate>                                               
                                                </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys" />
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
    <script src="js/fastclick.js" type="text/javascript" lang="javascript"></script>
    <script src="js/prism.js" type="text/javascript" lang="javascript"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css">    
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>    
    <script src="https://code.jquery.com/ui/1.13.0/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.confirmDialog').hide();            
        });
        var BASE_URL = "CompanyManagement.aspx";
        $(document).ready(function () {
            FastClick.attach(document.body);
            ClearAll();
        });
        function ClearAll() {

            $('#txtCompanyName').val('');
            $('#ddlLOB').prop('selectedIndex', 0);  
            $('#btnSave').val('Save');
       
        }
        function ValidateCompany() {

            Error_Message = "";
            Error_Count = 1;

            CheckNull($('#txtCompanyName').val(), in2in5);
                              
            if (Error_Message != "") {
                ShowError(Error_Message, 80);
                return false;
            }
            else {                
                    return true;
            }
        }
       
        function In2InGlobalConfirm(id) {

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
                        DeleteCompany(id);                       
                    },
                    "No": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
        function DeleteCompany(id) {

            var _target = 'grdCompany';
            $("#__EVENTARGUMENT").val(id);
            $("#__EVENTTARGET").val(_target);
            __doPostBack(_target, id);

        }
        function PullDataToEdit(cid,cname, lob) {

            $('#txtCompanyName').val(cname);            
            $("#ddlLOB").val(lob); 
            $('#hdnCompanyID').val(cid);
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
        function ShowHidden() {

        }
    </script>
    <style type="text/css">
        body {
            background-color: azure;
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.FileManagement" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="vendor/jquery/jquery.min.js"></script>
    <script type="text/javascript">
</script>
    <link href='https://fonts.googleapis.com/css?family=Work+Sans:300,400,600&Inconsolata:400,700' rel='stylesheet' type='text/css' />

    
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/gridview.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />
    <link href="<%= String.Format("{0}dt={1}",ResolveUrl("css/style.css?"), DateTime.Now.Ticks) %>" rel="stylesheet" type="text/css" />

    <style type="text/css">
        body {
            background-color: azure;
        }
        .GridViewImageAlignment{
            text-align:center;
        }
        .file_table {
            width: 100%;
        }

        .file_table_header {
            border-bottom: 1px solid #ffb215;
            padding: 8px;
        }

            .file_table_header th {
                background-color: #4472c4;
                color: white;
                border-bottom: 1px solid black;
            }

        .file_table tr td {
            border-bottom: 1px solid #ccc;
        }

        ul {
            list-style: none;
            padding: 0;
        }

        li {
            padding-left: 1.3em;
        }

            li:before {
                content: "\f00c"; /* FontAwesome Unicode */
                font-family: FontAwesome;
                display: inline-block;
                margin-left: -1.3em; /* same as padding-left set on li */
                width: 1.3em; /* same as padding-left set on li */
            }          
    </style> 


</head>
<body style="background-color:azure;">
   
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 20px;">
             <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px; height:40px;padding-top:10px;"><span class="menu_frame_title">File Management</span></div>
                <table style="width: 100%; background-color: azure;">
                    <tr>
                        <td style="width: 75%;">
                            <table style="width: 100%; margin-top: 25px;">
                                <tr>
                                    <td style="width: 30%;">Project ID :</td>
                                    <td style="width: 70%;">
                                        <input readonly="true" value="P001" type="text" id="projectid" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <div style="border: 1px solid black; margin-left: auto; margin-right: auto; border-radius: 5px">
                                            <table style="width: 100%; margin-top: auto; margin-left: auto; margin-right: auto;">
                                                <tr>
                                                    <td style="width: 60%;">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 70%; text-align: right;">Select Template</td>
                                                                <td style="width: 10%">: </td>
                                                                <td style="width: 20%">
                                                                    <div>
                                                                        <asp:DropDownList ID="ddlTemplate" Width="100%" runat="server" DataTextField="TemplateName"></asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td style="text-align: right;">
                                                                    <button type="button" class="button" onclick="downloadFile();" value="Download" id="btnDownload" runat="server">Download</button></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">                                                                    
                                                                    <center>
                                                                        <div style="width: 100%; border: 1px solid black; border-radius: 5px; margin-top: 5px;">
                                                                            <asp:GridView ID="grdUploadedFiles" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                                                AllowPaging="True" OnPageIndexChanging="grdUploadedFiles_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                                                                <PagerStyle CssClass="pagination-ys" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="File Name" DataField="FileName" />
                                                                                    <asp:BoundField HeaderText="Date" DataField="Date" />
                                                                                    <asp:ImageField ItemStyle-CssClass ="GridViewImageAlignment" HeaderText="Uploaded Status" ControlStyle-Height="25px" ControlStyle-Width="25px" DataImageUrlField="Uploaded Status"></asp:ImageField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </center>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <asp:FileUpload ID="fileUploader" runat="server" />                                                                    

                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnUploader" class="button"  runat="server" Text="Upload" OnClick="btnUploader_Click" />
                                                                </td>
                                                                
                                                                <td>

                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 40%;">
                                                        <div style="width: 90%; text-align: left; margin-left: 20px; height: 220px; overflow-y: auto; margin-top: 0px;">
                                                            <b>Template Instruction</b><br />
                                                            <p></p>
                                                            <ul>
                                                                <li>Step 1. Name the process or task</li>
                                                                <li>Step 2. Define the scope of work</li>
                                                                <li>Step 3. Explain the inputs and outputs</li>
                                                                <li>Step 4. Write down each step</li>
                                                                <li>Step 5. Order the steps</li>

                                                            </ul>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 25%; vertical-align: top;">
                            <div style="margin-top: 50px; margin-left: 20px; border-left: 1px solid gray; height: 300px;">
                                <span style="margin-left: 10px;"><b>Search Project </b></span>
                                <table style="width: 100%; margin-left: 5px;margin-right: 5px;">
                                    <tr id="usrEmailTR" runat="server">
                                        <td style="width: 40%">
                                            <b><u>Email ID</u> : </b>
                                        </td>
                                        <td style="width: 60%;">
                                            <input type="text" value="" id="usrEmailId" runat="server" style="width: 80%;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><u>Project ID</u> : </b>
                                        </td>
                                        <td>
                                            <div>
                                                <asp:DropDownList OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AutoPostBack="true" ID="ddlProjects" Width="100%" runat="server" DataTextField="ProjectName"></asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="width: 100%">
                                                <table id="tblTemplateDetail" runat="server" style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <hr style="width: 80%; margin-left: 0px;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b><u>Template Details:</u></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="margin-left: auto; margin-top: auto;">
                                                            <asp:GridView ID="grdTemplate" runat="server" Width="100%" HeaderStyle-CssClass="pagination-ys"
                                                                AllowPaging="True" OnPageIndexChanging="grdTemplate_PageIndexChanging" AutoGenerateColumns="false" PageSize="4">
                                                                <PagerStyle CssClass="pagination-ys" />
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Template Name" DataField="TemplateName" />
                                                                    <asp:BoundField HeaderText="DateAdded" DataField="DateAdded" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>            
        </center>
    </form>
    <script src="js/jquery.js"></script>
    <script src="js/jquery.nice-select.min.js"></script>
    <script src="js/fastclick.js"></script>
    <script src="js/prism.js"></script>

    <script>
        $(document).ready(function () {
            $('select:not(.ignore)').niceSelect();
            FastClick.attach(document.body);
            $("#projectids").change(function () {
                $('#projectid').val($("#projectids").val());
            });

        });
        function downloadFile() {

            alert("Your Template file will be downloaded.")
        }
        function uploadFile() {

            alert("Your selected file will be uploaded.")
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
</body>
</html>

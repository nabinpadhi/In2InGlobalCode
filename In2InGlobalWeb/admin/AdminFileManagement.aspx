<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFileManagement.aspx.cs" Inherits="In2InGlobal.presentation.admin.AdminFileManagement" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="vendor/jquery/jquery.min.js"></script>
    <script type="text/javascript">
</script>
    <link href='https://fonts.googleapis.com/css?family=Work+Sans:300,400,600&Inconsolata:400,700' rel='stylesheet' type='text/css' />

    <link rel="stylesheet" href="css/style.css" />
    <style type="text/css">
        .file_table {
            width: 100%;
        }

        .file_table_header {
            border-bottom: 1px solid #ffb215;
            padding: 8px;
            background-color: #dedede;
        }

            .file_table_header th {
                border-bottom: 1px solid black;
            }

        .file_table tr td {
            border-bottom: 1px solid #ccc;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="margin-top: 50px; border: 1px; border-radius: 5px; border-color: blue;">
                <table style="width: 90%; background-color: aliceblue;">
                    <tr>
                        <td style="width: 70%;">
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
                                                    <td style="width: 80%;">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 70%; text-align: right;">Select Template</td>
                                                                <td style="width: 10%">: </td>
                                                                <td style="width: 20%">
                                                                    <select style="width: 100%;" name="templates" id="templates" runat="server">
                                                                        <option value="Template1">Template 1</option>
                                                                        <option value="Template2">Template 2</option>
                                                                        <option value="Template3">Template 3</option>
                                                                        <option value="Template4">Template 4</option>
                                                                    </select>
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
                                                                    <table style="width: 100%; border: 1px solid black; text-align: justify; margin-left: auto; margin-right: auto;" class="file_table">
                                                                        <tr class="file_table_header">
                                                                            <th style="width: 34%; background-color: blue; color: white;">File Name</th>
                                                                            <th style="width: 43%; background-color: blue; color: white;">Date</th>
                                                                            <th style="width: 23%; background-color: blue; color: white;">Upload Status</th>
                                                                        </tr>
                                                                        <tr style="border: 1px solid black;">
                                                                            <td>Internal-expenditure.pdf</td>
                                                                            <td>22nd October 2021</td>
                                                                            <td style="text-align: center;">
                                                                                <img width="20%;" src="img/success-mark.png" /></td>
                                                                        </tr>
                                                                        <tr style="border-bottom: 1px solid black;">
                                                                            <td>Machine_expenses.pdf</td>
                                                                            <td>22nd September 2021</td>
                                                                            <td style="text-align: center;">
                                                                                <img width="20%;" src="img/success-mark.png" /></td>
                                                                        </tr>
                                                                        <tr style="border-bottom: 1px solid black;">
                                                                            <td>Total-Fooding.pdf</td>
                                                                            <td>22nd October 2021</td>
                                                                            <td style="text-align: center;">
                                                                                <img width="20%;" src="img/success-mark.png" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <button type="button" class="button" onclick="uploadFile();" value="Upload" id="btnUpload" runat="server">Upload</button></td>
                                                                <td></td>
                                                                <td></td>

                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20%;">
                                                        <div style="width: 100%; text-align: left; height: 300px; margin-top: 0px;">
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
                        <td style="width: 30%; vertical-align: top;">
                            <div style="margin-top: 50px; border-left: 1px solid gray; height: 300px;">
                                <table>
                                    <tr>
                                        <td colspan="2"><b>Search Project </b></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><u>Email ID</u> : </b>
                                        </td>
                                        <td>
                                            <input type="text" value="" id="usrEmailId" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><u>Project ID</u> : </b>
                                        </td>
                                        <td>
                                            <select name="projectids" id="projectids" runat="server">
                                                <option value="P001">P001</option>
                                                <option value="P002">P002</option>
                                                <option value="P003">P003</option>
                                                <option value="P004">P004</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="width:100%">
                                                <table style="width:100%;">
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
                                                            <table style="width: 100%; font-size: smaller; display: table;">
                                                                <tr style="display: table-row; border: 1px solid gray;">
                                                                    <td style="display: table-cell; background-color: black; color: white; font-weight: bold; border: 1px solid gray;">Template Details</td>
                                                                    <td style="border: 1px solid gray; display: table-cell; background-color: black; color: white; font-weight: bold;">Date Added</td>
                                                                </tr>
                                                                <tr style="display: table-row; border: 1px solid gray;">
                                                                    <td style="display: table-cell; border: 1px solid gray;">Template 1</td>
                                                                    <td style="border: 1px solid gray; display: table-cell;">09/06/2021</td>
                                                                </tr>
                                                                <tr style="display: table-row; border: 1px solid gray;">
                                                                    <td style="display: table-cell; border: 1px solid gray;">Template 2</td>
                                                                    <td style="border: 1px solid gray; display: table-cell;">19/06/2021</td>
                                                                </tr>
                                                                <tr style="display: table-row; border: 1px solid gray;">
                                                                    <td style="display: table-cell; border: 1px solid gray;">Template 006</td>
                                                                    <td style="border: 1px solid gray; display: table-cell;">09/08/2021</td>
                                                                </tr>
                                                            </table>
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="In2InGlobal.presentation.admin.MyProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script>document.getElementsByTagName("html")[0].className += " js";</script>
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
           <center>
         <div style="width: 70%; border: 1px solid black; border-radius: 5px; margin-top: 30px;">
             <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px;height:40px;padding-top:10px;"><span class="menu_frame_title">My Profile</span></div>
          <div style="position:relative;padding:50px 30px 30px 30px;">
          <table style="width:90%;">
              <tr style="padding:10px">
                  <td style="padding:10px">User Name  :</td>                  
                  <td><input type="text" readonly="true" value="Sujay Mondal" runat="server" id="username" /></td>
                  <td style="padding:10px">Company Name  :</td>
                  <td><input type="text" readonly="true" value="In2In Global" runat="server" id="companyname" /></td>                 
              </tr>
               <tr>
                  <td style="padding:10px">Email Id  :</td>
                  <td><input type="text" readonly="true" value="sujaymondal@gmail.com" runat="server" id="email" /></td>
                  <td style="padding:10px">Activity Access  :</td>
                  <td><input type="text" readonly="true" value="Administrator" runat="server" id="activityaccess" /></td>                 
              </tr>
               <tr>
                  <td style="padding:10px">Role  :</td>
                  <td><input type="text" readonly="true" value="Management" runat="server" id="role" /></td>
                  <td style="padding:10px">Status  :</td>                    
                  <td><input type="text" readonly="true" id="status" runat="server" value="Active" /></td>                 
              </tr>
          </table>
              </div>
             </div>
      </center>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="In2InGlobal.presentation.admin.MyProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script>document.getElementsByTagName("html")[0].className += " js";</script>
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />  
     <style type="text/css">
         body {
            background-color: azure;
        }
        </style>
</head>
<body style="background-color:azure;">
    <center><h4><i>My Profile</i></h4></center>
    <form status="1" id="form1" runat="server">      
      <center>
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
      </center>
    </form>
</body>
</html>

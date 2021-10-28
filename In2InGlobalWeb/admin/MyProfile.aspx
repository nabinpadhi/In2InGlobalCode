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
    <link rel="stylesheet" href="css/gridview.css" />
</head>
<body style="background-color:azure;">
    
    <form status="1" id="form1" runat="server">      
      <center>
           <center>
         <div style="width: 70%; border: 1px solid black; border-radius: 5px; margin-top: 30px;">
             <div class="pagination-ys" style="border: 1px solid black; border-radius: 5px;height:40px;padding-top:10px;">My Profile</div>
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

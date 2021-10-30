using System;

namespace In2InGlobal.presentation.admin
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string target = Request.QueryString.Get("target");
            if (target == "NormalUser")
            {
                username.Value = "Ganeswar Sahoo";
                companyname.Value = "Freelancer";
                email.Value = "ganeswarsahoo@gmail.com";
                activityaccess.Value = "Normal User";
                role.Value = "Customer";
                status.Value = "Active";
                txtUserName.Value = "ganesh";
            }
            else
            {
                username.Value = "Sujay Mondal";
                companyname.Value = "In2In Global";
                email.Value = "sujaymondal@gmail.com";
                activityaccess.Value = "Administrator";
                role.Value = "Management";
                status.Value = "Active";
                txtUserName.Value = "admin";
            }
        }
    }
}
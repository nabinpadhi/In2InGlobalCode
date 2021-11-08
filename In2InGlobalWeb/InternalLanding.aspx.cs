using System;

namespace In2InGlobal.presentation.admin
{
    public partial class InternalLanding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoggedInUsrRole = Session["UserRole"].ToString();

            if (LoggedInUsrRole == "User")
            {  
                usrMngmnt.Visible = false;
                comMngmnt.Visible = false;
                tmpltMngmnt.Visible = false;
                ancAnalytics.Attributes.Add("onclick", "javascript:OpenPage('https://analytics.zoho.in/open-view/210664000000009321');");
            }
            else if(LoggedInUsrRole == "Admin")
            {
                usrMngmnt.Visible = true;
                comMngmnt.Visible = true;
                tmpltMngmnt.Visible = true;
                ancAnalytics.Attributes.Add("onclick", "javascript:OpenPage('https://analytics.zoho.in/workspace/210664000000004003');");
            }
            ancFileMan.Attributes.Add("onclick", "javascript:OpenPage('admin/FileManagement.aspx');");
            usrMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/UserManagement.aspx');");
            comMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/CompanyManagement.aspx');");
            tmpltMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/TemplateManagement.aspx');");
        }
    }
}
using System;

namespace In2InGlobal.presentation.admin
{
    public partial class InternalLanding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string target = Request.QueryString.Get("target");
            if (target == "NormalUser")
            {
                spnUserName.InnerText = "Ganeswar Sahoo";
                ancFileMan.Attributes.Add("onclick", "javascript:OpenPage('admin/FileManagement.aspx?t=n');");
                usrMngmnt.Visible = false;
                comMngmnt.Visible = false;
                tmpltMngmnt.Visible = false;
                ancAnalytics.Attributes.Add("onclick", "javascript:OpenPage('https://analytics.zoho.in/open-view/210664000000009321');");
            }
            else
            {
                spnUserName.InnerText = "Sujay Mondal";
                ancFileMan.Attributes.Add("onclick", "javascript:OpenPage('admin/FileManagement.aspx?t=a');");
                usrMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/UserManagement.aspx?t=a');");
                comMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/CompanyManagement.aspx?t=a');");
                tmpltMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/TemplateManagement.aspx?t=a');");
                ancAnalytics.Attributes.Add("onclick", "javascript:OpenPage('https://analytics.zoho.in/workspace/210664000000004003');");
            }
        }
    }
}
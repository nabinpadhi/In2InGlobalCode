using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class InternalLanding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string target = Request.QueryString.Get("target");
            if(target=="NormalUser")
            {
                spnUserName.InnerText = "Welcome Normal User";
                ancFileMan.Attributes.Add("onclick", "javascript:OpenPage('admin/UserFileManagement.aspx');");
                ancAnalytics.Attributes.Add("onclick", "javascript:OpenPage('admin/UserAnalytics.aspx');");
            }
            else
            {
                spnUserName.InnerText = "Welcome Admin User";
                ancFileMan.Attributes.Add("onclick","javascript:OpenPage('admin/AdminFileManagement.aspx');");
                ancAnalytics.Attributes.Add("onclick", "javascript:OpenPage('admin/AdminAnalytics.aspx');");
            }
        }
    }
}
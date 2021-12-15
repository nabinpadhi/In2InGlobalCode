﻿using In2InGlobalBL;
using System;
using System.Data;
using System.Text;

namespace In2InGlobal.presentation.admin
{
    public partial class InternalLanding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] != null)
            {
                string LoggedInUsrRole = Session["UserRole"].ToString();

                if (LoggedInUsrRole == "User")
                {
                    usrMngmnt.Visible = false;
                    comMngmnt.Visible = false;
                    tmpltMngmnt.Visible = false;
                    divConfiguration.Visible = false;
                    ancAnalytics.Attributes.Add("onclick", "javascript:return false;");
                }
                else if (LoggedInUsrRole == "Admin")
                {
                    usrMngmnt.Visible = true;
                    comMngmnt.Visible = true;
                    tmpltMngmnt.Visible = true;
                    divConfiguration.Visible = true;
                    ancAnalytics.Attributes.Add("onclick", "javascript:OpenPage('https://analytics.zoho.in/workspace/210664000000004003');");
                    ancConfiguration.Attributes.Add("onclick", "javascript:OpenPage('admin/AnalyticConfiguration.aspx');");
                }
                BuildAnalyticsProjectList(LoggedInUsrRole);
                ancFileMan.Attributes.Add("onclick", "javascript:OpenPage('admin/FileManagement.aspx');");
                usrMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/UserManagement.aspx');");
                comMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/CompanyManagement.aspx');");
                tmpltMngmnt.Attributes.Add("onclick", "javascript:OpenPage('admin/TemplateManagement.aspx');");
                projtMngmt.Attributes.Add("onclick", "javascript:OpenPage('admin/ProjectManagement.aspx');");
                
                
            }
            else
            { Response.Redirect("login.aspx"); }
        }

        private void BuildAnalyticsProjectList(string LoggedInUsrRole)
        {
            DataTable dtUserProjectList = GetUserAssignedProjects();
            StringBuilder analyticsProjectLinkLiString = new StringBuilder();
            if (dtUserProjectList.Rows.Count == 0 && LoggedInUsrRole == "User")
            {
                liAnalytics.Visible = false;
              
            }
            else {
                liAnalytics.Visible = true;
            }

            if(LoggedInUsrRole == "User")
            {
                foreach (DataRow dr in dtUserProjectList.Rows)
                {
                    //string ancLink = " onclick = \"OpenPage('https://analytics.zoho.in/open-view/210664000000009321/60a0a9d9d2953abc7eab81953595f3ac');\"";
                    string ancLink = " onclick = \"OpenPage('" + dr["dashboard_url"].ToString() + "');\"";

                    string liForProject = " <li class='cd-side__item'><a href = '#'  id = '" + dr["project_name"].ToString() + "'" + ancLink + "' runat = 'server' >" + dr["project_name"].ToString() + "</a></li>";

                    analyticsProjectLinkLiString.AppendLine(liForProject);
                }
            }

            AnalyticsProjectList.InnerHtml = analyticsProjectLinkLiString.ToString();
        }

        private DataTable GetUserAssignedProjects()
        {
            DataSet dsUserAssignedProject = new DataSet();
            try
            {
                string userEmail = Session["UserEmail"].ToString();
                string userRole = Session["UserRole"].ToString();

                ProjectMasterBL projectBL = new ProjectMasterBL();
                dsUserAssignedProject = projectBL.getProjectNameForDashboard(userRole, userEmail);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
            }
            return dsUserAssignedProject.Tables[0];
        }
    }
    
}
using In2InGlobal.businesslogic;
using In2InGlobalBL;
using In2InGlobalBusinessEL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class AnalyticConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] != null)
            {
                BindCompany();
                BindDashboardGrid();
            }
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);


        }

        private void BindCompany()
        {
            DataSet dsCompany = new DataSet();
            CompanyMasterBL projectBL = new CompanyMasterBL();
            dsCompany = projectBL.getCompanyName();
            ddlCompany.DataTextField = "company_name";
            ddlCompany.DataValueField = "company_id";
            ddlCompany.DataSource = dsCompany.Tables[0];
            ddlCompany.DataBind();
        }

        private void BindUsers(long companyID)
        {
            try
            {
                DataSet dsUsers = new DataSet();
                UserMasterBL userBL = new UserMasterBL();
                dsUsers = userBL.GetUsers(companyID);
                ddlUser.DataTextField = "user_email";
                ddlUser.DataValueField = "user_id";
                ddlUser.DataSource = dsUsers.Tables[0];
                ddlUser.DataBind();
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        private void BindProjects(string userEmail)
        {
            try
            {
                
                DataSet dsUsersPrject = new DataSet();
                ProjectMasterBL projectBL = new ProjectMasterBL();
                dsUsersPrject = projectBL.getAssignedProject("", userEmail);
                ddlProject.DataTextField = "project_name";
                ddlProject.DataValueField = "project_id";
                ddlProject.DataSource = dsUsersPrject.Tables[0];
                ddlProject.DataBind();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void BindDashboardGrid()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/AnalyticDashboard.json"));
            DataTable tblProject = JsonConvert.DeserializeObject<DataTable>(json);//.Select("user_email='"+Session["UserEmail"].ToString()+"'").CopyToDataTable(); 
            grdAnalyticsLink.DataSource = tblProject;
            grdAnalyticsLink.DataBind();
        }
        protected void grdAnalyticsLink_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdAnalyticsLink.PageIndex = e.NewPageIndex;
            BindDashboardGrid();
            grdAnalyticsLink.DataBind();
            
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUsers(Convert.ToInt64(ddlCompany.SelectedValue));
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjects(ddlUser.SelectedItem.Text);
        }
    }
    
}
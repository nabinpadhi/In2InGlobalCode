using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class TemplateManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string target = Request.QueryString.Get("t");
                if (target != "a")
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {
                    BindProjects();
                    BindTemplate();
                    // BindRoles();
                }
            }
            
        }
        private void BindTemplate()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Template.json");
            grdTemplate.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdTemplate.DataBind();
        }
        private void BindProjects()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Projects.json");
            ddlProjects.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlProjects.DataBind();
        }
        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindTemplate();
            grdTemplate.PageIndex = e.NewPageIndex;
            grdTemplate.DataBind();

        }
    }
}
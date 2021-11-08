using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class FileManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                {
                BindFileGrid();
                LoadTemplates();
                BindProjects();
                string usrRole = Session["UserRole"].ToString();
                if (usrRole == "Admin")
                {
                    usrEmailTR.Visible = true;
                    tblTemplateDetail.Visible = true;
                    BindTemplateGrid(ddlProjects.SelectedValue);


                }
                else
                {
                    usrEmailTR.Visible = false;
                    tblTemplateDetail.Visible = false;
                }
            }
        }

        private void BindProjects()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Projects.json");
            ddlProjects.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlProjects.DataBind();
        }

        private void LoadTemplates()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Template.json");
            ddlTemplate.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlTemplate.DataTextField = "TemplateName";
            ddlTemplate.DataValueField = "TemplateName";
            ddlTemplate.DataBind();
        }

        private void BindFileGrid()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/UploadedFiles.json");
            grdUploadedFiles.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdUploadedFiles.DataBind();
        }
        private void BindTemplateGrid(string _pid)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Template.json");
            DataTable tblTemplate = JsonConvert.DeserializeObject<DataTable>(json);
            if (_pid != "")
            {
                _pid = "ProjectName = '" + _pid +"'";
                if (tblTemplate.Select(_pid).Length > 0)
                {
                    tblTemplate = tblTemplate.Select(_pid).CopyToDataTable();
                    grdTemplate.DataSource = tblTemplate;
                    grdTemplate.DataBind();

                }
                else { grdTemplate.DataSource = null; }
            }

        }
        protected void grdUploadedFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindFileGrid();
            grdUploadedFiles.PageIndex = e.NewPageIndex;
            grdUploadedFiles.DataBind();

        }
        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindTemplateGrid("");
            grdTemplate.PageIndex = e.NewPageIndex;
            grdTemplate.DataBind();

        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTemplateGrid(ddlProjects.SelectedValue);
        }
    }
}
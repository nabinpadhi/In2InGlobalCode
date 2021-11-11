using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
                string usrRole = Session["UserRole"].ToString();
                if (usrRole == "Admin")
                {

                    BindProjects();
                    BindTemplate();
                    BindTemplateName();
                    BindUserNames();
                }
                else
                {   
                    Response.Redirect("Login.aspx");
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
        private void BindUserNames()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            ddlUsers.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlUsers.DataBind();
        }
        private void BindTemplateName()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/TemplateRawFiles.json");
            ddlTemplates.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlTemplates.DataBind();
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

        protected void grdTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grdTemplate.DataKeys[e.RowIndex].Value.ToString();
            DeleteTemplate(ID);
            BindTemplate();
        }

        private void DeleteTemplate(string iD)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Template.json"));
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (usrTable.Select("ID ='" + iD + "'").Length > 0)
            {
                usrTable.Select("ID ='" + iD + "'")[0].Delete();
                usrTable.AcceptChanges();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/Template.json"), output);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Template.json"));
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);

            int _templateID = usrTable.Rows.Count + 1;
            string today = DateTime.Now.ToShortDateString();

            DataRow dr = usrTable.Rows.Add(_templateID, ddlTemplates.Text, today,ddlProjects.Text, ddlUsers.Text);
            usrTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/Template.json"), output);            
        }
    }
}
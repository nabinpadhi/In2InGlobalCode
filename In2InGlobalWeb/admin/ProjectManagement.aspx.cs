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
    public partial class ProjectManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usrRole = Session["UserRole"].ToString();               
                spnCreatedBy.InnerText = Session["UserEmail"].ToString();
                spnProjectName.InnerText = GenerateProjectName();
                hdnPName.Value = spnProjectName.InnerText;
                BindProjectGrid();
            }

        }
        private string GenerateProjectName()
        {
            string projson = (new WebClient()).DownloadString(HttpContext.Current.Server.MapPath("json-data/Projects.json"));
            DataTable ProjectTable = JsonConvert.DeserializeObject<DataTable>(projson);

            int _ProjectID = ProjectTable.Rows.Count + 1;
            string ProjectName = "PRO-" + $"{_ProjectID:0000}";
           
            return ProjectName;
        }

       
        private void BindProjectGrid()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Projects.json"));
            DataTable tblProject = JsonConvert.DeserializeObject<DataTable>(json);
            ViewState["dirProject"] = tblProject;
            grdProject.DataSource = tblProject;
            grdProject.DataBind();
        }
        protected void btnCreateProject_Click(object sender, EventArgs e)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Projects.json"));
            DataTable ProjectTable = JsonConvert.DeserializeObject<DataTable>(json);
            string _message = "Project Created Successfully.)";
            if (hdnProjectToEdit.Value =="")
            {
                if (ProjectTable.Rows.Count == 0)
                {
                    ProjectTable.Columns.Add("ID");
                    ProjectTable.Columns.Add("TemplateName");
                    ProjectTable.Columns.Add("CreatedBy");
                    ProjectTable.Columns.Add("Instruction");
                }
                int _ProjectID = ProjectTable.Rows.Count + 1;
                string ProjectName = "PRO-" + $"{_ProjectID:0000}";
                string createdBy = Session["UserEmail"].ToString();
                string description = txtDescription.Value;

                DataRow dr = ProjectTable.Rows.Add(ProjectName, createdBy, description);
                ProjectTable.AcceptChanges();
                dr.SetModified();
            }
            else
            {
                DataRow drProject = ProjectTable.Select("ProjectName='" + hdnProjectToEdit.Value + "'")[0];
                drProject["Description"] = txtDescription.InnerText;
                ProjectTable.AcceptChanges();
                drProject.SetModified();
                _message = "Project updated Successfully.)";
            }
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(ProjectTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/Projects.json"), output);

            BindProjectGrid();
            grdProject.PageIndex = grdProject.PageCount - 1;
            spnProjectName.InnerText = GenerateProjectName();
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);
        }

        protected void grdProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdProject.PageIndex = e.NewPageIndex;
            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = "ProjectName";
            DataTable dtrslt = (DataTable)ViewState["dirProject"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["cursortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = ViewState["SortExpression"] + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = ViewState["SortExpression"] + " Asc";
                    ViewState["sortdr"] = "Asc";
                }

                grdProject.DataSource = dtrslt;
                grdProject.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowCreateProject();", true);

        }

        protected void grdProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DeleteProject(((System.Web.UI.WebControls.Label)grdProject.Rows[e.RowIndex].Cells[0].Controls[1]).Text);
            BindProjectGrid();
            grdProject.PageIndex = grdProject.PageCount - 1;
            spnProjectName.InnerText = GenerateProjectName();
            string _message = "Project removed successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
        }
        protected void grdProject_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirProject"];
            ViewState["SortExpression"] = e.SortExpression;
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                    ViewState["cursortdr"] = "Asc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                    ViewState["cursortdr"] = "Desc";
                }

                grdProject.DataSource = dtrslt;
                grdProject.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowCreateProject();", true);
        }

        protected void grdProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;

                string ID = grdProject.DataKeys[e.Row.RowIndex].Value.ToString();
                string instuction = e.Row.Cells[2].Text.Replace("\n", "\\#");
                foreach (LinkButton button in e.Row.Cells[3].Controls.OfType<LinkButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                    if (button.CommandName == "Edit")
                    {
                        button.Attributes["onclick"] = "PullDataToEdit('" + ID + "','" + item + "','" + instuction + "');";
                        button.Attributes["href"] = "#";
                    }
                }
            }
        }
        private void DeleteProject(string pName)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Projects.json"));
            DataTable projTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (projTable.Select("ProjectName ='" + pName + "'").Length > 0)
            {
                projTable.Select("ProjectName ='" + pName + "'")[0].Delete();
                projTable.AcceptChanges();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(projTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/Projects.json"), output);
            }
            
            
        }
    }
}
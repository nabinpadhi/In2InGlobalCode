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
                if (Session["UserRole"] != null)
                {
                    string usrRole = Session["UserRole"].ToString();
                    if (usrRole == "Admin")
                    {

                        BindProjects();
                        BindUsers();
                        BindTemplate();
                        BindMasterTemplate();
                        BindMasterTemplateGrid();
                        BindTemplateToAssign();
                        BindProjectGrid();
                        //txtcreatedBy = Session["UserEmail"].ToString();
                        txtcreatedB.InnerText = Session["UserEmail"].ToString();
                        spnCreatedBy.InnerText = Session["UserEmail"].ToString();
                        spnProjectName.InnerText = GenerateProjectName();

                    }
                    else
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }


                }

            }

        private string GenerateProjectName()
        {
            string projson = (new WebClient()).DownloadString(HttpContext.Current.Server.MapPath("json-data/Projects.json"));
            DataTable ProjectTable = JsonConvert.DeserializeObject<DataTable>(projson);

            int _ProjectID = ProjectTable.Rows.Count + 1;
            string ProjectName = "PRO - " + $"{_ProjectID:0000}";
            return ProjectName;
        }

        private void BindMasterTemplateGrid()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/MasterTemplate.json"));
            grdMasterTemplate.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdMasterTemplate.DataBind();
        }
        private void BindTemplate()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Template.json"));
            grdTemplate.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdTemplate.DataBind();
        }
        private void BindProjectGrid()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Projects.json"));
            grdProject.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdProject.DataBind();
        }
        /* Used to load the template name extracting from provided files a folder
           This will lod the template name on create template screen*/
        private void BindMasterTemplate()
        {  
            DataTable dtTemplate = new DataTable();
            dtTemplate.Columns.Add("TemplateName");

            string jsonMasterTemplate = (new WebClient()).DownloadString(Server.MapPath("json-data/MasterTemplate.json"));            
            DataTable dtMasterTemplate = JsonConvert.DeserializeObject<DataTable>(jsonMasterTemplate);

            foreach (string s in System.IO.Directory.GetFiles(Server.MapPath("MasterTemplate")))
            {
                string fileName = System.IO.Path.GetFileName(s);
                DataRow newRow = dtTemplate.NewRow();
                newRow["TemplateName"] = fileName.Substring(0, fileName.Length-4);               

                dtTemplate.Rows.Add(newRow);
            }
            foreach (DataRow dr in dtMasterTemplate.Rows)
            {
                if (dtTemplate.Select("TemplateName='" + dr["TemplateName"] + "'").Length > 0)
                {
                    dtTemplate.Select("TemplateName='" + dr["TemplateName"] + "'")[0].Delete();
                }
            }
            ddlMasterTemplate.DataSource = dtTemplate;
            ddlMasterTemplate.DataBind();
            
        }
        private void BindTemplateToAssign()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string jsonMasterTemplate = (new WebClient()).DownloadString(Server.MapPath("json-data/MasterTemplate.json"));            
            DataTable dtMasterTemplate = JsonConvert.DeserializeObject<DataTable>(jsonMasterTemplate);           
            ddlTemplates.DataSource = dtMasterTemplate;
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
        private void BindUsers()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            ddlUserEmail.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlUserEmail.DataBind();
        }
        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindTemplate();
            grdTemplate.PageIndex = e.NewPageIndex;
            grdTemplate.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowAssignTemplate();", true);

        }

        protected void grdTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grdTemplate.DataKeys[e.RowIndex].Value.ToString();
            DeleteTemplate(ID);
            BindTemplate();
            BindTemplateToAssign();
            string _message = "Template assignment removed.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowAssignTemplate();", _message), true);
        }
        
        protected void grdMasterTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindMasterTemplateGrid();
            grdMasterTemplate.PageIndex = e.NewPageIndex;
            grdMasterTemplate.DataBind();
            //
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowCreateTemplate();", true);

        }

        protected void grdMasterTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grdMasterTemplate.DataKeys[e.RowIndex].Value.ToString();
            DeleteMasterTemplate(ID);
            BindMasterTemplateGrid();
            BindMasterTemplate();
            string _message = "Template removed successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
        }
        protected void grdProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindProjectGrid();
            grdProject.PageIndex = e.NewPageIndex;
            grdProject.DataBind();
            //
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowCreateProject();", true);

        }

        protected void grdProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            string _message = "Template removed successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
        }
        private void DeleteMasterTemplate(string iD)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/MasterTemplate.json"));
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (usrTable.Select("ID ='" + iD + "'").Length > 0)
            {
                usrTable.Select("ID ='" + iD + "'")[0].Delete();
                usrTable.AcceptChanges();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/MasterTemplate.json"), output);
            }
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
            DataRow dr = usrTable.Rows.Add(_templateID, ddlTemplates.Text, today,ddlProjects.Text, ddlUserEmail.SelectedItem.Text);
            usrTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/Template.json"), output);
            string _message = "Selected Template Assigned Successfully)";
            BindTemplate();
            BindTemplateToAssign();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowAssignTemplate();", _message), true);
            
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/MasterTemplate.json"));
            DataTable masterTemplateTable = JsonConvert.DeserializeObject<DataTable>(json);
            if(masterTemplateTable.Rows.Count == 0)
            {
                masterTemplateTable.Columns.Add("ID");
                masterTemplateTable.Columns.Add("TemplateName");
                masterTemplateTable.Columns.Add("CreatedBy");
                masterTemplateTable.Columns.Add("Instruction");
            }
            int _templateID = masterTemplateTable.Rows.Count + 1;
            string createdBy = Session["UserEmail"].ToString();
            string templateName = ddlMasterTemplate.Text;
            string instruction = txtInstruction.Value;

            DataRow dr = masterTemplateTable.Rows.Add(_templateID, templateName, createdBy,instruction);
            masterTemplateTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(masterTemplateTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/MasterTemplate.json"), output);
            BindMasterTemplateGrid();
            BindMasterTemplate();
            string _message = "Template Updated Successfully.)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate(); ", _message), true);
        }
        
            protected void btnCreatePro_Click(object sender, EventArgs e)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Projects.json"));
            DataTable ProjectTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (ProjectTable.Rows.Count == 0)
            {
                ProjectTable.Columns.Add("ID");
                ProjectTable.Columns.Add("TemplateName");
                ProjectTable.Columns.Add("CreatedBy");
                ProjectTable.Columns.Add("Instruction");
            }
            int _ProjectID = ProjectTable.Rows.Count + 1;
            string ProjectName = "PRO - "+ $"{_ProjectID:0000}";
            string createdBy = Session["UserEmail"].ToString();
            string templateName = ddlMasterTemplate.Text;
            string description = txtDescription.Value;

            DataRow dr = ProjectTable.Rows.Add(ProjectName, createdBy, description);
            ProjectTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(ProjectTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/Projects.json"), output);

            BindProjectGrid();
            grdProject.PageIndex = grdProject.PageCount - 1;
            spnProjectName.InnerText = GenerateProjectName();
            string _message = "Project Created Successfully.)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowCreateProject(); ", _message), true);
        }
        /* private void UploadTemplateFile()
         {
             string fileName = "";
             string filePath = Server.MapPath("TemplateFiles");           
             if (templateFileUploader.HasFile)
             {
                 fileName = templateFileUploader.FileName;
                 templateFileUploader.SaveAs(System.IO.Path.Combine(filePath, fileName));

             }

         }*/
    }
}
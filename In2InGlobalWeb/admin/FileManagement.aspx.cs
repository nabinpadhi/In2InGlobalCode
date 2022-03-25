using In2InGlobal.businesslogic;
using In2InGlobal.presentation.Tools;
using In2InGlobalBL;
using In2InGlobalBusinessEL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class FileManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["UserRole"] != null)
                    {
                        BindFileGrid("");
                        LoadTemplates();                      
                        BindAssignedProjects();
                        BindAssignedUser();
                        string usrRole = Session["UserRole"].ToString();
                        spnCreatedBy.InnerText = Session["UserEmail"].ToString();
                        spnProjectName.InnerText = GenerateProjectName();
                        hdnPNVS.Value = spnProjectName.InnerText;
                        BindProjectGrid();
                        HttpContext.Current.Session["SelectedProjectName"] = null;
                        HttpContext.Current.Session["UserEmail"] = Session["UserEmail"].ToString();
                        HttpContext.Current.Session["UserRole"] = Session["UserRole"].ToString();
                        HttpContext.Current.Session["targetfolder"] = "./uploadedFiles/";
                        HttpContext.Current.Session["UploadedBy"] = Session["UserEmail"].ToString();
                        HttpContext.Current.Session["ForScreen"] = "FileManagement";

                        ddlUsrEmailId.Items.FindByText(Session["UserEmail"].ToString()).Selected = true;

                        if (usrRole == "Admin")
                        {
                            usrEmailTR.Visible = true;
                            searchTemplatePanel.Visible = true;
                            BindTemplateGrid("", ddlUsrEmailId.SelectedItem.Text);
                            grdUploadedFiles.Columns[0].ItemStyle.Width = 150;
                            grdUploadedFiles.Columns[0].HeaderStyle.Width = 152;
                        }
                        else
                        {
                            searchTemplatePanel.Visible = false;
                            grdUploadedFiles.Columns[0].ItemStyle.Width = 252;
                            grdUploadedFiles.Columns[0].HeaderStyle.Width = 252;

                        }

                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
                    }
                }               
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

      
        private void BindAssignedProjects()
        {
            try
            {
                string userEmail = Session["UserEmail"].ToString();
                string userRole = Session["UserRole"].ToString();

                DataSet dsUserDetails = new DataSet();
                ProjectMasterBL projectBL = new ProjectMasterBL();
                dsUserDetails = projectBL.getAssignedProject(userRole, userEmail);

                ddlAssignedProject.Items.Clear();
                ddlAssignedProject.Items.Add("--Select a Project--");

                ddlAssignedProject.DataSource = dsUserDetails;
                ddlAssignedProject.DataTextField = "project_name";
                ddlAssignedProject.DataValueField = "project_id";
                ddlAssignedProject.DataBind();

                ddlProjects.Items.Clear();
                ddlProjects.Items.Add("--Select a Project--");

                ddlProjects.DataSource = dsUserDetails;
                ddlProjects.DataTextField = "project_name";
                ddlProjects.DataValueField = "project_id";
                ddlProjects.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }


        private void BindAssignedUser()
        {
            try
            {
                string userEmail = Session["UserEmail"].ToString();
                string userRole = Session["UserRole"].ToString();

                DataSet dsUserDetails = new DataSet();
                ProjectMasterBL projectBL = new ProjectMasterBL();
                dsUserDetails = projectBL.getEmailforAdminAndUser(userRole, userEmail);

                ddlUsrEmailId.DataSource = dsUserDetails;
                ddlUsrEmailId.DataTextField = "user_email";
                ddlUsrEmailId.DataValueField = "user_id";
                ddlUsrEmailId.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }



        private void LoadTemplates()
        {
            DataSet dsloadTemplate = new DataSet();
            TemplateMasterBl templateMasterBL = new TemplateMasterBl();
            dsloadTemplate = templateMasterBL.FMMasterTemplateName();
            try
            {
                if (dsloadTemplate.Tables[0].Rows.Count > 0)
                {
                    Session["FMTemplateName"] = dsloadTemplate.Tables[0];
                    ddlTemplate.Items.Clear();
                    ddlTemplate.DataSource = dsloadTemplate.Tables[0];
                    ddlTemplate.DataBind();
                    ddlTemplate.Items.Insert(0, new ListItem("--Select a Template--"));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }

        private void BindFileGrid(string pid)
        {
            try
            {
                grdUploadedFiles.DataSource = null;
            grdUploadedFiles.DataBind();
            int projectID = 0;
            DataSet dsUserDetails = new DataSet();
            UploadTemplateBL projectBL = new UploadTemplateBL();
            if (pid == string.Empty || pid.ToLower() == "--select a project--")
            {
                projectID = 0;
            }
            else { projectID = Convert.ToInt32(pid); }
            string userEmail = Session["UserEmail"].ToString();
            string userRole = Session["UserRole"].ToString();

            
                dsUserDetails = projectBL.LoadUploadFileTemplateGrid(userRole, userEmail, projectID);
                if (projectID == 0 && dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    grdUploadedFiles.DataSource = dsUserDetails.Tables[0];
                    grdUploadedFiles.DataBind();
                }
                else
                {
                    var projectUploadRows = dsUserDetails.Tables[0].Select("project_id='" + pid + "'");
                    if (projectUploadRows.Length > 0)
                    {
                        grdUploadedFiles.DataSource = projectUploadRows.CopyToDataTable();
                        grdUploadedFiles.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }


        private void BindTemplateGrid(string pid, string _email)
        {
            try { 
            grdTemplate.Visible = true;

            int projectID = 0;
            DataSet dsUserDetails = new DataSet();
            UploadTemplateBL projectBL = new UploadTemplateBL();
            if (pid == "--Select a Project--" || pid == string.Empty)
            {
                projectID = 0;
            }
            else { projectID = Convert.ToInt32(pid); }
            string userRole = Session["UserRole"].ToString();

                dsUserDetails = projectBL.LoadSearchTemplateGrid(userRole, _email, projectID);
                if (pid == string.Empty && dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    grdTemplate.DataSource = dsUserDetails.Tables[0];
                    grdTemplate.DataBind();
                }
                else
                {
                    string filterstr = "project_id = " + projectID;
                    var projectUploadRows = dsUserDetails.Tables[0].Select(filterstr);
                    if (projectUploadRows.Length > 0)
                    {
                        grdTemplate.DataSource = projectUploadRows.CopyToDataTable();
                        grdTemplate.DataBind();
                    }
                    else { grdTemplate.DataSource = null; grdTemplate.DataBind(); }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }
        protected void grdUploadedFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                BindFileGrid(ddlAssignedProject.SelectedValue);
                grdUploadedFiles.PageIndex = e.NewPageIndex;
                grdUploadedFiles.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                BindTemplateGrid("", Session["UserEmail"].ToString());
                grdTemplate.PageIndex = e.NewPageIndex;
                grdTemplate.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindTemplateGrid(ddlProjects.SelectedValue, "");

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }
        private void SaveUploadTemplateInformationInDB(string fileName, string uploadBy, string projectName)
        {
            UploadTemplateEntity templateEntity = new UploadTemplateEntity();
            try
            {
                if (projectName != null && fileName != null)
                {
                    templateEntity.FileName = fileName;
                    templateEntity.ProjectName = projectName;
                    templateEntity.CreatedBy = uploadBy;
                    templateEntity.RoleName = Session["UserRole"].ToString(); ;
                    templateEntity.UserEmail = Session["UserEmail"].ToString(); ;
                    templateEntity.Status = "Success";

                    UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
                    uploadTemplateBl.SaveAssignedTemplate(templateEntity);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }
        private void UpdateUploadTemplateInformationInDB(string fileName, string uploadBy, string projectName)
        {
            UploadTemplateEntity templateEntity = new UploadTemplateEntity();
            try
            {
                if (projectName != null && fileName != null)
                {
                    templateEntity.FileName = fileName;
                    templateEntity.ProjectName = projectName;
                    templateEntity.CreatedBy = uploadBy;
                    templateEntity.RoleName = Session["UserRole"].ToString(); ;
                    templateEntity.UserEmail = Session["UserEmail"].ToString(); ;
                    templateEntity.Status = "Success";

                    UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
                    uploadTemplateBl.UpdateAssignedTemplate(templateEntity);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        protected void ddlusrEmailId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindTemplateGrid("", ddlUsrEmailId.SelectedValue);

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string _message = "";
            try
            {
                System.IO.FileStream fs = null;

                string fileToOpen = Server.MapPath("MasterTemplate") + "\\" + ddlTemplate.SelectedItem.Text + ".csv";
                fs = System.IO.File.Open(fileToOpen, System.IO.FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + ddlTemplate.SelectedItem.Text + ".csv");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                Response.End();
                _message = "File downloaded successfully.";
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
            ScriptManager.RegisterStartupScript(scriptmanager1, scriptmanager1.GetType(), "ShowServerMessage", string.Format("ShowServerMessage('{0}');ShowFileMgnt();", _message), true);
        }


        protected void ddlAssignedProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAssignedProject.SelectedIndex > 0)
            {  
                ddlTemplate.Enabled = true;
                ddlTemplate.SelectedIndex = 0;
            }
            else
            {                
                ddlTemplate.Enabled = false;
                ddlTemplate.SelectedIndex = 0;
                btnUpload.Enabled = false;
                btnDownload.Enabled = false;
                fileUploader.Enabled = false;
            }

            BindFileGrid(ddlAssignedProject.SelectedValue);
            HttpContext.Current.Session["SelectedProjectName"] = ddlAssignedProject.SelectedItem.Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);

        }

        //Project Management code details 




        private void BindProjectGrid()
        {
            try
            {
                string userEmail = Session["UserEmail"].ToString();
                string userRole = Session["UserRole"].ToString();

                DataSet dsUserDetails = new DataSet();
                ProjectMasterBL projectBL = new ProjectMasterBL();
                dsUserDetails = projectBL.getProjectGridDetails(userRole, userEmail);

                grdProject.DataSource = dsUserDetails.Tables[0];
                grdProject.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }

        private string GenerateProjectName()
        {
            int _ProjectID = 0;
            string ProjectName = string.Empty;

            DataSet dsUserDetails = new DataSet();
            ProjectMasterBL projectBL = new ProjectMasterBL();
            ProjectEntity projectEntitiy = new ProjectEntity();
            try
            {
                dsUserDetails = projectBL.getProjectId();

                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    DataRow drMyProfile = dsUserDetails.Tables[0].Rows[0];
                    if (!drMyProfile["project_id"].Equals(System.DBNull.Value))
                    {
                        int count = Convert.ToInt32(drMyProfile["project_id"].ToString());
                        _ProjectID = count + 1;
                        ProjectName = "PRO-" + $"{_ProjectID:0000}";
                    }
                    else
                    {
                        _ProjectID = 1;
                        ProjectName = "PRO-" + $"{_ProjectID:0000}";
                    }
                }
                else
                {
                    _ProjectID = 1;
                    ProjectName = "PRO-" + $"{_ProjectID:0000}";
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

            return ProjectName;
        }


        protected void btnCreateProject_Click(object sender, EventArgs e)
        {

            string _message = string.Empty;
            DataSet dsUserDetails = new DataSet();
            ProjectMasterBL projectBL = new ProjectMasterBL();
            ProjectEntity projectEntitiy = new ProjectEntity();
            try
            {

                projectEntitiy.CreatedBy = Session["UserEmail"].ToString();
                projectEntitiy.Description = txtDescription.Value;
                projectEntitiy.UserRole = Session["UserRole"].ToString();
                projectEntitiy.UserEmail = Session["UserEmail"].ToString();
                if (hdnProjectToEdit.Value == "")
                {
                    projectEntitiy.ProjectName = spnProjectName.InnerText;
                    projectBL.SaveProjectMaster(projectEntitiy);
                    _message = "Project Created Successfully.)";
                }
                else
                {
                    projectEntitiy.ProjectName = hdnProjectToEdit.Value;
                    projectBL.UpdateProjectMaster(projectEntitiy);
                    _message = "Project updated Successfully.)";
                    hdnProjectToEdit.Value = "";
                    spnProjectName.InnerText = GenerateProjectName();
                    btnCreateProject.Text = "Create";
                }


                BindProjectGrid();
                BindAssignedProjects();
                if (grdProject.PageCount > 1)
                    grdProject.PageIndex = grdProject.PageCount - 1;
                spnProjectName.InnerText = GenerateProjectName();
                txtDescription.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt(); ", _message), true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        protected void grdProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdProject.PageIndex = e.NewPageIndex;
                BindProjectGrid();
                grdProject.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowProjectMgnt();", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }


        protected void grdProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string projectid = ((DataTable)grdProject.DataSource).Rows[e.Row.DataItemIndex][0].ToString();
                    string projectname = ((DataTable)grdProject.DataSource).Rows[e.Row.DataItemIndex][1].ToString();
                    string updatedBy = Session["UserEmail"].ToString();
                    DataTable grdDSTable = (DataTable)grdProject.DataSource;

                    string description = e.Row.Cells[2].Text.Replace("\n", "<br>");
                    foreach (Button editButton in e.Row.Cells[3].Controls.OfType<Button>())
                    {
                        editButton.UseSubmitBehavior = false;
                        editButton.Attributes["onclick"] = "return PullDataToEdit('" + projectname + "','" + updatedBy + "','" + description + "');";
                    }
                    foreach (Button delbutton in e.Row.Cells[4].Controls.OfType<Button>())
                    {

                        delbutton.UseSubmitBehavior = false;
                        delbutton.Attributes["onclick"] = "javascript:In2InGlobalConfirm('" + projectname + "','" + projectid + "');return false;";
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }
        private void DeleteProject(string pID)
        {
            ProjectMasterBL projectBL = new ProjectMasterBL();
            ProjectEntity projectEntitiy = new ProjectEntity();
            try
            {
                if (pID != string.Empty)
                {
                    projectEntitiy.ProjectId = Convert.ToInt64(pID);
                    projectBL.DeleteProjectMaster(projectEntitiy);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }

        protected void hdnDelBtn_Click(object sender, EventArgs e)
        {
            DataSet userDs = new DataSet();

            if (hdnPID.Value != "")
            {
                userDs = getTemplateInfoForProjectId(hdnPID.Value);
                if (userDs.Tables[0].Rows.Count > 0)
                {
                    txtDescription.Value = "";
                    string _message = "Project delete failed.Project mapped to a Template.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt(); ", _message), true);
                }
                else
                {
                    DeleteProject(hdnPID.Value);
                    BindProjectGrid();
                    txtDescription.Value = "";
                    string _message = "Project deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt(); ", _message), true);
                }
            }

        }


        protected DataSet getTemplateInfoForProjectId(string pID) 
        {
            ProjectMasterBL projectBL = new ProjectMasterBL();
            ProjectEntity projectEntitiy = new ProjectEntity();
            DataSet userDs = new DataSet();
            try
            {
                if (pID != string.Empty)
                {
                    projectEntitiy.ProjectId = Convert.ToInt64(pID);
                    userDs= projectBL.getTemplateInfoForProjectId(projectEntitiy);
                }
                
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

            return userDs;
        }


        protected void grdUploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string upload_status_Img_Url = ((System.Web.UI.WebControls.Image)e.Row.Cells[4].Controls[0]).ImageUrl;
                if (e.Row.RowState == DataControlRowState.Alternate)
                {
                    upload_status_Img_Url = "./img/" + upload_status_Img_Url + "-alt.png";
                }
                else
                {
                    upload_status_Img_Url = "./img/" + upload_status_Img_Url + ".png";
                }
                ((System.Web.UI.WebControls.Image)e.Row.Cells[4].Controls[0]).ImageUrl = upload_status_Img_Url;


            }
        }


        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            tplInstruction.InnerHtml = "";
            string templateName = ddlTemplate.SelectedItem.Text;
            int _selIndex = ddlTemplate.SelectedIndex;
            Session["TemplateName"] = templateName;
            DataTable tblMasterTemple = (DataTable)Session["FMTemplateName"];
            if (tblMasterTemple.Rows.Count > 0)
            {
                if (tblMasterTemple.Select("file_name='" + templateName + "'").Length > 0)
                {
                    string instruction = tblMasterTemple.Select("file_name='" + templateName + "'")[0]["instruction"].ToString();
                    foreach (string li in instruction.Split('\n'))
                    {
                        tplInstruction.InnerHtml = tplInstruction.InnerHtml + "<li>" + li + "</li>";
                    }
                }
            }
            else
            {
                tplInstruction.InnerHtml = "<li>No Instruction Found.</li>";
            }
            if (ddlTemplate.SelectedIndex > 0)
            {
                btnUpload.Enabled = true;
                btnDownload.Enabled = true;
                fileUploader.Enabled = true;
                

            }
            else
            {
                btnUpload.Enabled = false;
                fileUploader.Enabled = false;                
                btnDownload.Enabled = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            BindFileGrid("");
            ddlAssignedProject.SelectedIndex = 0;            
            fileUploader.Enabled = false;
            ddlTemplate.Enabled = false;
            btnDownload.Enabled = false;
            btnUpload.Enabled = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }
    }
}



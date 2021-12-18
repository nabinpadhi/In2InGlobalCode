﻿using In2InGlobal.businesslogic;
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
            if (!IsPostBack)
            {
                if (Session["UserRole"] != null)
                {
                    BindFileGrid("");
                    LoadTemplates();
                    BindProjects();
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
                        BindTemplateGrid("", ddlUsrEmailId.SelectedItem.Text);
                    }
                    else
                    {
                        usrEmailTR.Visible = false;
                        BindTemplateGrid("", Session["UserEmail"].ToString());
                    }
                    
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
                }
            }
            if(Request.Form["__EVENTTARGET"] == "ddlTemplate")
            {
                ddlTemplate_SelectedIndexChanged(ddlTemplate, null);
            }
        }

        private void BindProjects()
        {
            try
            {
                //string userEmail = Session["UserEmail"].ToString();
                //string userRole = Session["UserRole"].ToString();

                //DataSet dsUserDetails = new DataSet();
                //ProjectMasterBL projectBL = new ProjectMasterBL();
                //dsUserDetails = projectBL.getAssignedProject(userRole, userEmail);

              
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
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
                ex.Message.ToString();
            }

        }

        private void BindFileGrid(string pid)
        {
            grdUploadedFiles.DataSource = null;
            grdUploadedFiles.DataBind();
            int projectID = 0;
            DataSet dsuploadTemplate = new DataSet(); 
            UploadTemplateBL projectBL = new UploadTemplateBL();
            if (pid == string.Empty || pid.ToLower() =="--select a project--") 
            {
                projectID =0;
            }
            else { projectID = Convert.ToInt32(pid); }
            string userEmail = Session["UserEmail"].ToString();
            string userRole = Session["UserRole"].ToString();
            string projectName = ddlAssignedProject.SelectedItem.Text;
            try
            {
                dsuploadTemplate = projectBL.LoadUploadFileTemplateGrid(userRole, userEmail, projectName);
                if (projectID == 0 && dsuploadTemplate.Tables[0].Rows.Count > 0)
                {
                    grdUploadedFiles.DataSource = dsuploadTemplate.Tables[0];
                    grdUploadedFiles.DataBind();
                }
                else
                {
                    var projectUploadRows = dsuploadTemplate.Tables[0].Select("project_name='" + projectName + "'");
                    if (projectUploadRows.Length > 0)
                    {
                        grdUploadedFiles.DataSource = projectUploadRows.CopyToDataTable();
                        grdUploadedFiles.DataBind();
                    }                    
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


        private void BindTemplateGrid(string pid, string _email)
        {
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

            try
            {
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
                    else { grdTemplate.DataSource = null;grdTemplate.DataBind(); }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        protected void grdUploadedFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindFileGrid(ddlAssignedProject.SelectedValue);
            grdUploadedFiles.PageIndex = e.NewPageIndex;
            grdUploadedFiles.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }

        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindTemplateGrid("", Session["UserEmail"].ToString());
            grdTemplate.PageIndex = e.NewPageIndex;
            grdTemplate.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
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
                ex.ToString();
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
                ex.ToString();
            }
        }

        protected void ddlusrEmailId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTemplateGrid("", ddlUsrEmailId.SelectedValue);

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
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
            catch (IOException ex)
            {
                _message = ex.Message;

            }
            ScriptManager.RegisterStartupScript(scriptmanager1, scriptmanager1.GetType(), "ShowServerMessage", string.Format("ShowServerMessage('{0}');ShowFileMgnt();", _message), true);
        }


        protected void ddlAssignedProject_SelectedIndexChanged(object sender, EventArgs e)
        {             
            if (ddlAssignedProject.SelectedIndex > 0)
            {
                btnUpload.Enabled = true;
                fileUploader.Enabled = true;
                ddlTemplate.Enabled = true;
                btnDownload.Enabled = true;
            }
            else
            {
                btnUpload.Enabled = false;
                fileUploader.Enabled = false;
                ddlTemplate.Enabled = false;
                btnDownload.Enabled = false;
            }

            BindFileGrid(ddlAssignedProject.SelectedValue);
            HttpContext.Current.Session["SelectedProjectName"] = ddlAssignedProject.SelectedItem.Text;
           


            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);

        }

        //Project Management code details 




        private void BindProjectGrid()
        {
            string userEmail = Session["UserEmail"].ToString();
            string userRole = Session["UserRole"].ToString();

            DataSet dsUserDetails = new DataSet();
            ProjectMasterBL projectBL = new ProjectMasterBL();
            dsUserDetails = projectBL.getProjectGridDetails(userRole, userEmail);

            grdProject.DataSource = dsUserDetails.Tables[0];
            grdProject.DataBind();
            
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
                ex.ToString();
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
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            BindProjectGrid();
            BindAssignedProjects();
            if (grdProject.PageCount > 1)
                grdProject.PageIndex = grdProject.PageCount - 1;
            spnProjectName.InnerText = GenerateProjectName();
            txtDescription.Value = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt(); ", _message), true);
        }

        protected void grdProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          
                grdProject.PageIndex = e.NewPageIndex;
                BindProjectGrid();
                grdProject.DataBind();
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowProjectMgnt();", true);

        }

    
        protected void grdProject_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ex.ToString();
            }

        }

        protected void hdnDelBtn_Click(object sender, EventArgs e)
        {
            if (hdnPID.Value != "")
            {
                DeleteProject(hdnPID.Value);
            }
            BindProjectGrid();
            txtDescription.Value = "";
            string _message = "Project deleted successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt(); ", _message), true);

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

            //Puting in session due to File upload time this will consider as a Table Name
            HttpContext.Current.Session["TemplateName"] = ddlTemplate.SelectedItem.Text;

            int _selIndex = ddlTemplate.SelectedIndex;

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
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            BindFileGrid("");
            ddlAssignedProject.SelectedIndex = 0;
            //$('#fileUploader').prop('disabled', 'disabled');
            //     $('#fileUploader').addClass('aspNetDisabled');
            //$('#ddlTemplate').prop('disabled', true);
            //$('#btnDownload').prop('disabled', true);
            //$('#btnUpload').prop('disabled', true);
            fileUploader.Enabled = false;
            ddlTemplate.Enabled = false;
            btnDownload.Enabled = false;
            btnUpload.Enabled = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }
    }
}



using In2InGlobal.businesslogic;
using In2InGlobal.presentation.Tools;
using In2InGlobalBL;
using In2InGlobalBusinessEL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class TemplateManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (Session["UserRole"] != null)
                    {
                        txtcreatedB.InnerText = Session["UserEmail"].ToString();
                        string usrRole = Session["UserRole"].ToString();
                        HttpContext.Current.Session["UserEmail"] = Session["UserEmail"].ToString();
                        HttpContext.Current.Session["UserRole"] = Session["UserRole"].ToString();
                        HttpContext.Current.Session["targetfolder"] = "./MasterTemplate/";
                        HttpContext.Current.Session["UploadedBy"] = Session["UserEmail"].ToString();
                        HttpContext.Current.Session["ForScreen"] = "TemplateManagement";

                        if (usrRole == "Admin")
                        {
                            BindMasterTemplate();
                            BindMasterTemplateGrid();
                            if (Session["servermessage"] != null && Session["servermessage"].ToString() != "")
                            {
                                string servermessge = Session["servermessage"].ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowCreateTemplate();ShowUploadMasterTemplate();ShowServerMessage('{0}'); ", servermessge), true);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
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

        /// <summary>
        /// Bind Master Template Grid
        /// </summary>
        private void BindMasterTemplateGrid()
        {
            
            DataSet dsloadTemplate = null;
            TemplateMasterBl templateMasterBL = new TemplateMasterBl();
            try
            {
                dsloadTemplate = templateMasterBL.PopulateTemplateGrid();
                if (dsloadTemplate != null || dsloadTemplate.Tables.Count != 0)
                {
                    
                    grdMasterTemplate.DataSource = dsloadTemplate.Tables[0];
                    grdMasterTemplate.DataBind();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// BindTemplate
        /// </summary>
        private void BindTemplate()
        {
            DataSet dsloadTemplare = new DataSet();
            TemplateMasterBl templateMasterBL = new TemplateMasterBl();
            try
            {
                dsloadTemplare = templateMasterBL.PopulateTemplateGrid();
                grdTemplate.DataSource = dsloadTemplare;
                grdTemplate.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// populate template name on create template screen
        /// </summary>
        private void BindMasterTemplate()
        {           
            DataSet dsloadTemplate = GetMasterTemplates();
            try
            {
                if (dsloadTemplate.Tables[0].Rows.Count > 0)
                {
                    ddlMasterTemplate.Items.Clear();
                    ddlMasterTemplate.DataTextField = "file_name";
                    ddlMasterTemplate.DataValueField = "template_id";
                    ddlMasterTemplate.Items.Insert(0, new ListItem("--Select a Template--"));
                    ddlMasterTemplate.DataSource = dsloadTemplate.Tables[0];
                    ddlMasterTemplate.DataBind();

                }
                
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
           
        }

        private DataSet GetMasterTemplates()
        {
            DataSet dsloadTemplate = new DataSet();
            try { 
            
            TemplateMasterBl templateMasterBL = new TemplateMasterBl();
            dsloadTemplate = templateMasterBL.PopulateUploadMasterTemplateName();
          
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
            return dsloadTemplate;
        }
        
        
        /// <summary>
        /// Bind Template To Assign
        /// </summary>
        private void BindTemplateToAssign()
        {
            try { 
            int projectId = 0;
            if (ddlProjects.SelectedIndex > 0)
            {
                projectId = Convert.ToInt32(ddlProjects.SelectedValue);
            }
            int userId = 0;
            if (ddlUserEmail.SelectedIndex > 0)
            {
                userId = Convert.ToInt32(ddlUserEmail.SelectedValue);
            }

            DataSet dsUserDetails = new DataSet();
            AssignedTemplateBL projectBL = new AssignedTemplateBL();
            
                dsUserDetails = projectBL.PopulateTemplateNameForAssignedProjectAndUser(projectId, userId);
                ddlTemplates.DataSource = dsUserDetails;
                ddlTemplates.DataTextField = "user_name";
                ddlTemplates.DataValueField = "user_id";
                ddlTemplates.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }


        /// <summary>
        /// Bind Users
        /// </summary>
        private void BindUsers()
        {
            try { 
            int projectId = 0;
            if (ddlProjects.SelectedIndex > 0)
            {
                projectId = Convert.ToInt32(ddlProjects.SelectedValue);
            }
            DataSet dsUserDetails = new DataSet();
            AssignedTemplateBL projectBL = new AssignedTemplateBL();
         
                dsUserDetails = projectBL.PopulateAllUserEmailForAssignedProject(projectId);
                ddlUserEmail.DataSource = dsUserDetails;
                ddlUserEmail.DataTextField = "user_name";
                ddlUserEmail.DataValueField = "user_id";
                ddlUserEmail.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// grdTemplate_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                BindTemplate();
                grdTemplate.PageIndex = e.NewPageIndex;
                grdTemplate.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowAssignTemplate();", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// grdTemplate RowDeleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string ID = grdTemplate.DataKeys[e.RowIndex].Value.ToString();
                DeleteTemplate(ID);
                BindTemplate();

                ddlTemplates.Items.Clear();
                ddlTemplates.Items.Insert(0, new ListItem("--Select a Template--"));
                BindTemplateToAssign();
                string _message = "Template assignment removed.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowAssignTemplate();", _message), true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// grdMasterTemplate PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMasterTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                BindMasterTemplateGrid();
                grdMasterTemplate.PageIndex = e.NewPageIndex;
                grdMasterTemplate.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowCreateTemplate();", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }

        /// <summary>
        /// grdMasterTemplate RowDeleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMasterTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string ID = grdMasterTemplate.DataKeys[e.RowIndex].Value.ToString();
                DeleteMasterTemplate(ID);
                BindMasterTemplateGrid();
                BindMasterTemplate();
                string _message = "Template removed successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// Delete Master Template
        /// </summary>
        /// <param name="iD"></param>
        private void DeleteMasterTemplate(string id)
        {
            try
            {
                if (id != null)
                {
                    TemplateMasterEntity templateEntity = new TemplateMasterEntity();
                    templateEntity.TemplateId = Convert.ToInt32(id);
                    TemplateMasterBl templateMasterBl = new TemplateMasterBl();
                    templateMasterBl.DeleteMasterTemplate(templateEntity);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }

        /// <summary>
        /// Delete Template
        /// </summary>
        /// <param name="iD"></param>
        private void DeleteTemplate(string id)
        {
            try
            {
                if (id != null)
                {
                    TemplateMasterEntity templateEntity = new TemplateMasterEntity();
                    templateEntity.TemplateId = Convert.ToInt32(id);
                    TemplateMasterBl templateMasterBl = new TemplateMasterBl();
                    templateMasterBl.DeleteMasterTemplate(templateEntity);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }

        /// <summary>
        /// btnSave Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Template.json"));
                DataTable templateTable = JsonConvert.DeserializeObject<DataTable>(json);
                if (templateTable.Rows.Count == 0)
                {
                    templateTable.Columns.Add("ID");
                    templateTable.Columns.Add("TemplateName");
                    templateTable.Columns.Add("DateAdded");
                    templateTable.Columns.Add("ProjectName");
                    templateTable.Columns.Add("Email");
                }
                int _templateID = templateTable.Rows.Count + 1;
                string today = DateTime.Now.ToShortDateString();
                DataRow dr = templateTable.Rows.Add(_templateID, ddlTemplates.Text, today, ddlProjects.Text, ddlUserEmail.SelectedItem.Text);
                templateTable.AcceptChanges();
                dr.SetModified();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(templateTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/Template.json"), output);
                string _message = "Selected Template Assigned Successfully)";
                BindTemplate();

                BindUsers();

                ddlTemplates.Items.Clear();
                ddlTemplates.Items.Insert(0, new ListItem("--Select a Template--"));

                BindTemplateToAssign();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowAssignTemplate();", _message), true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }


        /// <summary>
        /// btnCreate Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            TemplateMasterEntity tempalteEntity = new TemplateMasterEntity();
            string createdBy = Session["UserEmail"].ToString();
            string templateName = ddlMasterTemplate.SelectedItem.Text;
            string instruction = txtInstruction.Value;           
            string _message = "Template Created Successfully.";
            try
            {
                tempalteEntity.TemplateName = templateName;
                tempalteEntity.Instruction = instruction;
                tempalteEntity.CreatedBy = createdBy;

                TemplateMasterBl templateMasterBl = new TemplateMasterBl();
                if (hdnTID.Value != "")
                {
                    tempalteEntity.TemplateId = Convert.ToInt32(hdnTID.Value);
                    templateMasterBl.UpdateTemplateMaster(tempalteEntity);
                    _message = "Template Updated Successfully";
                }
                else
                {  
                    templateMasterBl.SaveTemplateMaster(tempalteEntity);
                    _message = "Template Created Successfully";
                }


            }
            catch (Exception)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
            BindMasterTemplateGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate(); ", _message), true);
        }

        /// <summary>
        /// btnUploader Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void SaveUploadMasterTemplateFile(string filePath, string fileName)
        {
            try
            {
                if (fileName != null)
                {
                    TemplateMasterEntity templateEntity = new TemplateMasterEntity();
                    templateEntity.FileName = fileName;
                    templateEntity.FilePath = filePath;
                    templateEntity.CreatedBy = Session["UserEmail"].ToString();
                    TemplateMasterBl templateMasterBl = new TemplateMasterBl();
                    templateMasterBl.SaveTemplateMaster(templateEntity);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// grdMasterTemplate RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMasterTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string template_name = e.Row.Cells[0].Text;

                    string template_id = grdMasterTemplate.DataKeys[e.Row.RowIndex].Value.ToString();
                    string instuction = e.Row.Cells[2].Text.Replace("\n", "_#_");
                    string masterTemplateId = GetMaterTemplateId(template_name);
                    foreach (Button editbutton in e.Row.Cells[3].Controls.OfType<Button>())
                    {

                        editbutton.UseSubmitBehavior = false;
                        editbutton.Attributes["onclick"] = "return PullDataToEdit('" + template_id + "','" + template_name + "','" + instuction + "','" + masterTemplateId + "');";

                    }
                    foreach (Button delbutton in e.Row.Cells[4].Controls.OfType<Button>())
                    {

                        delbutton.UseSubmitBehavior = false;
                        delbutton.Attributes["onclick"] = "javascript:In2InGlobalConfirm('" + template_id + "');return false;";

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        private string GetMaterTemplateId(string template_name)
        {
            string masterTemplateId = "";
            try
            {
                DataTable masterTemplateDDLDT = GetMasterTemplates().Tables[0];
                if (masterTemplateDDLDT != null)
                {
                    if (masterTemplateDDLDT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in masterTemplateDDLDT.Rows)
                        {
                            if (dr[1].ToString() == template_name)
                            {
                                masterTemplateId = dr[0].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
            return masterTemplateId;
        }

        /// <summary>
        /// grd Template RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[0].Text;
                    foreach (LinkButton button in e.Row.Cells[4].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// ddlProjects_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Users.json"));
                DataTable userTable = JsonConvert.DeserializeObject<DataTable>(json);
                DataRow[] userRows = userTable.Select("ProjectID='" + ddlProjects.SelectedValue + "'");
                if (userRows.Length > 0)
                {
                    ddlUserEmail.Items.Clear();
                    ddlUserEmail.DataSource = userRows.CopyToDataTable();
                    ddlUserEmail.DataBind();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowAssignTemplate();", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        protected void hdnDelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(hdnTID.Value != "")
                {
                    DeleteMasterTemplate(hdnTID.Value);
                    BindMasterTemplateGrid();
                }
                 
                string _message = "Template deleted successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            BindMasterTemplate();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowCreateTemplate();", true);
        }
    }

}
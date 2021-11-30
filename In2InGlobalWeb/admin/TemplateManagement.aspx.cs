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
                        //BindProjects();
                        //BindUsers();
                        //BindTemplate();
                        BindMasterTemplate();
                        BindMasterTemplateGrid();
                        //BindTemplateToAssign();                                             
                        //txtcreatedBy = Session["UserEmail"].ToString();
                        txtcreatedB.InnerText = Session["UserEmail"].ToString();

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
            if (Request.Form["__EVENTTARGET"] != null)
            {
                if (Request.Form["__EVENTTARGET"].ToString().IndexOf("grdMasterTemplate") == 0)
                {
                    int extraComa = Request.Form["__EVENTTARGET"].ToString().Replace("grdMasterTemplate", "").Length;
                    // Fire event
                    DeleteMasterTemplate(Request.Form["__EVENTARGUMENT"].Substring(0, Request.Form["__EVENTARGUMENT"].Length - extraComa));
                    BindMasterTemplateGrid();
                    string _message = "Template deleted Successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);
                }
            }
        }

        /// <summary>
        /// Bind Master Template Grid
        /// </summary>
        private void BindMasterTemplateGrid()
        {
            DataSet dsloadTemplare = new DataSet();
            TemplateMasterBl templateMasterBL = new TemplateMasterBl();
            try
            {
                dsloadTemplare = templateMasterBL.PopulateTemplateGrid();
                grdMasterTemplate.DataSource = dsloadTemplare.Tables[0];
                grdMasterTemplate.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// populate template name on create template screen
        /// </summary>
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
                newRow["TemplateName"] = fileName.Substring(0, fileName.Length - 4);

                dtTemplate.Rows.Add(newRow);
            }

            foreach (DataRow dr in dtMasterTemplate.Rows)
            {
                if (dtTemplate.Select("TemplateName='" + dr["TemplateName"] + "'").Length > 0)
                {
                    dtTemplate.Select("TemplateName='" + dr["TemplateName"] + "'")[0].Delete();
                }
            }
            ddlMasterTemplate.Items.Clear();
            ddlMasterTemplate.Items.Add("--Select a Template--");
            ddlMasterTemplate.DataSource = dtTemplate;
            ddlMasterTemplate.DataBind();

        }

        /// <summary>
        /// Bind Template To Assign
        /// </summary>
        private void BindTemplateToAssign()
        {
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue);
            int userId = Convert.ToInt32(ddlUserEmail.SelectedValue);
            DataSet dsUserDetails = new DataSet();
            AssignedTemplateBL projectBL = new AssignedTemplateBL();
            try
            {
                dsUserDetails = projectBL.PopulateTemplateNameForAssignedProjectAndUser(projectId, userId);
                ddlTemplates.DataSource = dsUserDetails;
                ddlTemplates.DataTextField = "user_name";
                ddlTemplates.DataValueField = "user_id";
                ddlTemplates.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// Bind Projects
        /// </summary>
        private void BindProjects()
        {
            DataSet dsUserDetails = new DataSet();
            AssignedTemplateBL projectBL = new AssignedTemplateBL();
            try
            {

                dsUserDetails = projectBL.PopulateProjectNameForTemplate();
                ddlProjects.DataSource = dsUserDetails;
                ddlProjects.DataTextField = "project_name";
                ddlProjects.DataValueField = "project_id";
                ddlProjects.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// Bind Users
        /// </summary>
        private void BindUsers()
        {
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue);
            DataSet dsUserDetails = new DataSet();
            AssignedTemplateBL projectBL = new AssignedTemplateBL();
            try
            {

                dsUserDetails = projectBL.PopulateAllUserEmailForAssignedProject(projectId);
                ddlUserEmail.DataSource = dsUserDetails;
                ddlUserEmail.DataTextField = "user_name";
                ddlUserEmail.DataValueField = "user_id";
                ddlUserEmail.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// grdTemplate_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindTemplate();
            grdTemplate.PageIndex = e.NewPageIndex;
            grdTemplate.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowAssignTemplate();", true);
        }

        /// <summary>
        /// grdTemplate RowDeleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grdTemplate.DataKeys[e.RowIndex].Value.ToString();
            DeleteTemplate(ID);
            BindTemplate();

            ddlTemplates.Items.Clear();
            ddlTemplates.Items.Add(new ListItem("--Select a Template--"));
            BindTemplateToAssign();
            string _message = "Template assignment removed.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowAssignTemplate();", _message), true);
        }

        /// <summary>
        /// grdMasterTemplate PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMasterTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindMasterTemplateGrid();
            grdMasterTemplate.PageIndex = e.NewPageIndex;
            grdMasterTemplate.DataBind();
            //
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowCreateTemplate();", true);

        }

        /// <summary>
        /// grdMasterTemplate RowDeleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMasterTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grdMasterTemplate.DataKeys[e.RowIndex].Value.ToString();
            DeleteMasterTemplate(ID);
            BindMasterTemplateGrid();
            BindMasterTemplate();
            string _message = "Template removed successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
        }

        /// <summary>
        /// Delete Master Template
        /// </summary>
        /// <param name="iD"></param>
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

        /// <summary>
        /// Delete Template
        /// </summary>
        /// <param name="iD"></param>
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

        /// <summary>
        /// btnSave Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
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
            ddlTemplates.Items.Add(new ListItem("--Select a Template--"));

            BindTemplateToAssign();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowAssignTemplate();", _message), true);

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
            string templateName = ddlMasterTemplate.Text;
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
                    tempalteEntity.TemplateId = Convert.ToInt64(hdnTID.Value);
                    _message = "Company Updated Successfully";
                    //templateMasterBl.UpdateTemplateMaster(tempalteEntity); Nabin :Ganesh write this method to update  master template.
                }
                else
                {
                    templateMasterBl.SaveTemplateMaster(tempalteEntity);
                }

                BindMasterTemplateGrid();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            //BindMasterTemplate();
            //BindTemplateToAssign();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate(); ", _message), true);
        }

        /// <summary>
        /// btnUploader Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploader_Click(object sender, EventArgs e)
        {
            string fileName = "";

            string filePath = Server.MapPath("MasterTemplate\\");
            string uploadedBy = "";
            string today = DateTime.Now.ToShortDateString();
            Session["servermessage"] = null;
            hdnFake.Text = "";
            if (Session["UserRow"] != null)
            {
                DataRow usrDataRow = (DataRow)Session["UserRow"];
                
                uploadedBy = usrDataRow["first_name"].ToString() + " " + usrDataRow["last_name"].ToString();
                try
                {
                    if (templateFileUpload.HasFile)
                    {
                        fileName = templateFileUpload.FileName;
                        //fileName = fileName.Replace(".csv", "~" + uploadedBy.Replace(" ", "") + "~" + ddlAssignedProject.SelectedValue + ".csv");


                        string pathToCheck = filePath + fileName;
                        //if (!System.IO.File.Exists(pathToCheck))
                        //{
                        using (StreamReader uploadedFS = new StreamReader(templateFileUpload.PostedFile.InputStream))
                        {
                            TextReader uploaderFileTextReader = new StreamReader(uploadedFS.BaseStream);

                            if (CheckUploadedFileHaveOnlyHeader(uploaderFileTextReader))
                            {

                                templateFileUpload.SaveAs(System.IO.Path.Combine(filePath, fileName));
                                Session["servermessge"] = "File uploaded Successfully.";

                            }
                        }

                        //}
                    }
                    else
                    {
                        string _message = "Please choose a file again.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    string _message = "Failed to upload choosed file.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate();", _message), true);
                }
            }
            else { Response.Redirect("Login.aspx"); }
        }

        /// <summary>
        /// Check Uploaded File Have Only Header
        /// </summary>
        /// <param name="trold"></param>
        /// <returns></returns>
        private bool CheckUploadedFileHaveOnlyHeader(TextReader trold)
        {
            bool _result = true;
            using (DataTable table = new CSVReader(trold).CreateDataTable(true))
            {

                if (table.Rows.Count > 0)
                {
                    return false;
                }
            }
            return _result;
        }

        /// <summary>
        /// hdnFake_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hdnFake_Click(object sender, EventArgs e)
        {
            if (Session["servermessage"] != null)
            {
                string servermessge = Session["servermessage"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowCreateTemplate();ShowServerMessage('{0}'); ", servermessge), true);

            }
            Session["servermessage"] = null;
            hdnFake.Text = "";
        }

        /// <summary>
        /// grdMasterTemplate RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMasterTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;

                string ID = grdMasterTemplate.DataKeys[e.Row.RowIndex].Value.ToString();
                string instuction = e.Row.Cells[2].Text.Replace("\n", "\\#");
                foreach (LinkButton button in e.Row.Cells[3].Controls.OfType<LinkButton>())
                {
                    if (button.ID == "lnkDel")
                    {
                        button.OnClientClick = "In2InGlobalConfirm('" + ID + "');";
                    }
                    if (button.ID == "lnkEdit")
                    {
                        button.OnClientClick = "PullDataToEdit('" + ID + "','" + item + "','" + instuction + "');";
                        button.Attributes["href"] = "#";
                    }
                }
            }
        }

        /// <summary>
        /// grd Template RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
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

        /// <summary>
        /// ddlProjects_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
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
    }

}
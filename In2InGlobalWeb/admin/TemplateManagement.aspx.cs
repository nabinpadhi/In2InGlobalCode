﻿using In2InGlobal.presentation.Tools;
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

                        BindProjects();
                        BindUsers();
                        BindTemplate();
                        BindMasterTemplate();
                        BindMasterTemplateGrid();
                        BindTemplateToAssign();                                             
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
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }


            }

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
            ddlUserEmail.Items.Clear();
            ddlUserEmail.Items.Add("--Select an Email");
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
            
            ddlTemplates.Items.Clear();
            ddlTemplates.Items.Add(new ListItem("--Select a Template--"));
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
        protected void btnCreate_Click(object sender, EventArgs e)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/MasterTemplate.json"));
            DataTable masterTemplateTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (hdnMTName.Value == "")
            {
                if (masterTemplateTable.Rows.Count == 0)
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

                DataRow dr = masterTemplateTable.Rows.Add(_templateID, templateName, createdBy, instruction);
                masterTemplateTable.AcceptChanges();
                dr.SetModified();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(masterTemplateTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/MasterTemplate.json"), output);
            }
            else
            {
                DataRow masterTemplateRow = masterTemplateTable.Select("TemplateName='" + hdnMTName.Value + "'")[0];
                masterTemplateRow["Instruction"] = txtInstruction.Value;                
                masterTemplateTable.AcceptChanges();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(masterTemplateTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/MasterTemplate.json"), output);
            }
            BindMasterTemplateGrid();
            BindMasterTemplate();
            BindTemplateToAssign();
            string _message = "Template Updated Successfully.)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowCreateTemplate(); ", _message), true);
        }

       
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
                uploadedBy = usrDataRow["FirstName"].ToString() + " " + usrDataRow["LastName"].ToString();
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

        private bool CheckUploadedFileHaveOnlyHeader(TextReader trold)
        {
            bool _result = true;
            using (DataTable table = new CSVReader(trold).CreateDataTable(true))
            {

                if (table.Rows.Count < 1)
                {
                    return false;
                }
            }
            return _result;
        }


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
      

        protected void grdMasterTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                
                string ID = grdMasterTemplate.DataKeys[e.Row.RowIndex].Value.ToString();
                string instuction = e.Row.Cells[2].Text.Replace("\n", "\\#");
                foreach (LinkButton button in e.Row.Cells[3].Controls.OfType<LinkButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                    if(button.CommandName == "Edit")
                    {
                        button.Attributes["onclick"] = "PullDataToEdit('" + ID+"','"+ item + "','"+ instuction + "');";
                        button.Attributes["href"] = "#";
                    }
                }
            }
        }

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
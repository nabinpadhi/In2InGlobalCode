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
                    hdnPName.Value = spnProjectName.InnerText;
                    BindProjectGrid();

                    if (usrRole == "Admin")
                    {
                        usrEmailTR.Visible = true;
                        tblTemplateDetail.Visible = true;
                        BindTemplateGrid(ddlProjects.SelectedValue, "");


                    }
                    else
                    {
                        ddlUsrEmailId.Text = Session["UserEmail"].ToString();
                        ddlUsrEmailId.Enabled = false;
                        if (Session["ProjectID"] != null)
                        {
                            ddlProjects.SelectedValue = Session["ProjectID"].ToString();
                        }
                        tblTemplateDetail.Visible = true;
                        BindTemplateGrid("", ddlUsrEmailId.Text);
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        private void BindProjects()
        {
            string userEmail = Session["UserEmail"].ToString();
            string userRole = Session["UserRole"].ToString();

            DataSet dsUserDetails = new DataSet();
            ProjectMasterBL projectBL = new ProjectMasterBL();
            dsUserDetails = projectBL.getAssignedProject(userRole, userEmail);

            ddlProjects.DataSource = dsUserDetails;
            ddlProjects.DataTextField = "project_name";
            ddlProjects.DataValueField = "project_id";
            ddlProjects.DataBind();
        }

        private void BindAssignedProjects()
        {
            string userEmail = Session["UserEmail"].ToString();
            string userRole = Session["UserRole"].ToString();

            DataSet dsUserDetails = new DataSet();
            ProjectMasterBL projectBL = new ProjectMasterBL();
            dsUserDetails = projectBL.getAssignedProject(userRole, userEmail);

            ddlAssignedProject.DataSource = dsUserDetails;
            ddlAssignedProject.DataTextField = "project_name";
            ddlAssignedProject.DataValueField = "project_id";
            ddlAssignedProject.DataBind();
        }


        private void BindAssignedUser()
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



        private void LoadTemplates()
        {
            DataTable dtTemplate = new DataTable();
            dtTemplate.Columns.Add("TemplateName");
            dtTemplate.Columns.Add("FilePath");
            DataTable tblAssignedTemplate = new DataTable();
            string AssignedTemplateJson = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Template.json");
            tblAssignedTemplate = JsonConvert.DeserializeObject<DataTable>(AssignedTemplateJson);
            if (tblAssignedTemplate.Rows.Count > 0)
            {
                if (ddlAssignedProject.SelectedIndex > 0)
                {
                    if (JsonConvert.DeserializeObject<DataTable>(AssignedTemplateJson).Select("ProjectName='" + ddlAssignedProject.SelectedValue + "'").Length > 0)
                    {
                        tblAssignedTemplate = tblAssignedTemplate.Select("ProjectName='" + ddlAssignedProject.SelectedValue + "'").CopyToDataTable();
                    }
                }
                else
                {
                    if (JsonConvert.DeserializeObject<DataTable>(AssignedTemplateJson).Select("Email='" + Session["UserEmail"].ToString() + "'").Length > 0)
                    {
                        tblAssignedTemplate = tblAssignedTemplate.Select("Email='" + Session["UserEmail"].ToString() + "'").CopyToDataTable();
                    }
                }
            }
            tblAssignedTemplate.Columns.Add("FilePath");
            foreach (DataRow dr in tblAssignedTemplate.Rows)
            {
                dr.BeginEdit();
                dr["FilePath"] = Server.MapPath("MasterTemplate") + "\\" + dr["TemplateName"] + ".csv";
                dr.EndEdit();
                dr.AcceptChanges();
            }
            tblAssignedTemplate.AcceptChanges();
            ddlTemplate.DataSource = tblAssignedTemplate;
            ddlTemplate.DataTextField = "TemplateName";
            ddlTemplate.DataValueField = "FilePath";
            ddlTemplate.DataBind();
        }
        private void BindFileGrid(string pid)
        {
            try
            {
                string userEmail = Session["UserEmail"].ToString();
                string userRole = Session["UserRole"].ToString();

                DataSet dsUserDetails = new DataSet();
                UploadTemplateBL projectBL = new UploadTemplateBL();
                dsUserDetails = projectBL.LoadUploadFileTemplateGrid(userRole, userEmail, pid);
                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    DataTable tblUploadedFiles = dsUserDetails.Tables[0];
                    // DataRow _usrRow = (DataRow)Session["UserRow"];
                    // string userName = _usrRow["FirstName"] + " " + _usrRow["LastName"];
                    string userName = tblUploadedFiles.Rows[0]["user_email"].ToString();
                    if (tblUploadedFiles.Rows.Count > 0)
                    {
                        if (pid != "")
                        {

                            if (tblUploadedFiles.Select("project_name='" + pid + "'").Length > 0)
                            {
                                tblUploadedFiles = tblUploadedFiles.Select("project_name='" + pid + "'").CopyToDataTable();
                            }
                            else
                            {
                                tblUploadedFiles = null;
                                grdTemplate.EmptyDataText = "No file(s) uploaded for selected project. ";
                            }
                        }
                        else
                        {

                            if (tblUploadedFiles.Select("uploaded_by='" + userName + "'").Length > 0)
                            {
                                tblUploadedFiles = tblUploadedFiles.Select("uploaded_by='" + userName + "'").CopyToDataTable();

                            }
                            else
                            {
                                tblUploadedFiles = null;
                                grdTemplate.EmptyDataText = "No file(s) uploaded for selected project. ";
                            }

                        }
                    }
                    else
                    {
                        tblUploadedFiles = null;
                        grdTemplate.EmptyDataText = "No file(s) uploaded for selected project. ";
                    }
                    grdUploadedFiles.DataSource = tblUploadedFiles;
                    grdUploadedFiles.DataBind();
                }
                else
                {
                    grdUploadedFiles.DataSource = null;
                    grdUploadedFiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void BindTemplateGrid(string _pid, string _email)
        {
            try
            {
                grdTemplate.Visible = true;
                string userRole = Session["UserRole"].ToString();

                DataSet dsUserDetails = new DataSet();
                UploadTemplateBL projectBL = new UploadTemplateBL();
                dsUserDetails = projectBL.LoadSearchTemplateGrid(userRole, _email, _pid);
                if (dsUserDetails.Tables[0].Rows.Count == 0)
                {
                    DataTable tblTemplate = dsUserDetails.Tables[0];

                    if (_pid != "" || _email != "")
                    {
                        string _target = "";
                        if (_pid != "")
                        {
                            _target = "ProjectName = '" + _pid + "'";
                        }
                        else if (_email != "")
                        {
                            _target = "Email = '" + _email + "'";
                        }
                        if (tblTemplate.Select(_target).Length > 0)
                        {
                            tblTemplate = tblTemplate.Select(_target).CopyToDataTable();
                            grdTemplate.DataSource = tblTemplate;
                            grdTemplate.DataBind();

                        }
                        else
                        {
                            grdTemplate.DataSource = null;
                            grdTemplate.DataBind();
                        }
                    }
                }
                else
                {
                    grdTemplate.DataSource = null;
                    grdTemplate.DataBind();
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
            // ddlUsrEmailId.Text = "";
            BindTemplateGrid(ddlProjects.SelectedValue, "");
            //string _message = "Project removed successfully.";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowFileMgnt();", _message), true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }

        protected void btnUploader_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string _message = "";
            string filePath = Server.MapPath("uploadedfiles\\");
            string uploadedBy = "";
            string today = DateTime.Now.ToShortDateString();
            if (Session["UserRow"] != null)
            {
                DataRow usrDataRow = (DataRow)Session["UserRow"];
                uploadedBy = usrDataRow["first_name"].ToString() + " " + usrDataRow["last_name"].ToString();
                try
                {
                    if (fileUploader.HasFile)
                    {
                        fileName = fileUploader.FileName;
                        fileName = fileName.Replace(".csv", "~" + uploadedBy.Replace(" ", "") + "~" + ddlAssignedProject.SelectedItem.Text + ".csv");
                        string pathToCheck = filePath + fileName;
                        if (!System.IO.File.Exists(pathToCheck))
                        {
                            fileUploader.SaveAs(System.IO.Path.Combine(filePath, fileName));
                            SaveFileDetails(fileName, uploadedBy, today);
                             _message = "File uploaded Successfully.";
                           // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("B"), string.Format("ShowServerMessage('{0}');ShowFileMgnt();", _message), true);
                        }
                        else
                        {
                            using (StreamReader uploadedFS = new StreamReader(fileUploader.PostedFile.InputStream))
                            {
                                TextReader uploaderFileTextReader = new StreamReader(uploadedFS.BaseStream);

                                if (CheckUploadedFileHaveOnlyHeader(uploaderFileTextReader))
                                {
                                     _message = "Uploaded Template contains only header.";
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowFileMgnt(); ", _message), true);
                                }
                                else
                                {
                                    string _existingFilePath = System.IO.Path.Combine(filePath, fileName);
                                    if (IsBothCSVFileDataAreSame(_existingFilePath))
                                    {
                                        //replace the file                                       
                                        fileUploader.SaveAs(System.IO.Path.Combine(filePath, fileName));
                                        //updating uploadedon field in JSON row data 
                                        UpdateUploadedFile(fileName, uploadedBy, today);
                                         _message = "File uploaded Successfully.";
                                       // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowFileMgnt(); ", _message), true);
                                    }
                                    else
                                    {

                                        string tempfileName = "";
                                        int counter = 2;
                                        while (System.IO.File.Exists(pathToCheck))
                                        {

                                            tempfileName = "V-" + counter.ToString() + "-" + fileName;
                                            pathToCheck = filePath + tempfileName;
                                            counter++;
                                        }
                                        fileName = tempfileName;
                                        fileUploader.SaveAs(Server.MapPath(System.IO.Path.Combine("/admin/uploadedfiles/", fileName)));

                                        SaveFileDetails(fileName, uploadedBy, today);

                                        SaveUploadTemplateInformationInDB(fileName, ddlAssignedProject.SelectedItem.Text, "True");

                                        _message = "File uploaded Successfully.";
                                        
                                    }
                                }
                                uploaderFileTextReader.Close();
                            }
                        }
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString("X"), "<script type=\"text/javascript\">FUcallBack('" + _message + "');</script> ");
                    
                }
                catch (System.IO.IOException ex)
                {
                    throw ex;
                }
            }

        }

        private void SaveUploadTemplateInformationInDB(string FileName, string ProjectName, string Status)
        {
            UploadTemplateEntity templateEntity = new UploadTemplateEntity();
            try
            {
                if (ProjectName != null)
                {
                    templateEntity.FileName = FileName;
                    templateEntity.ProjectName = ProjectName;
                    templateEntity.CreatedBy = Session["UserEmail"].ToString(); ;
                    templateEntity.RoleName = Session["UserRole"].ToString(); ;
                    templateEntity.UserEmail = Session["UserEmail"].ToString(); ;
                    templateEntity.Status = Status;

                    UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
                    uploadTemplateBl.SaveAssignedTemplate(templateEntity);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private bool IsBothCSVFileDataAreSame(string fileName)
        {
            bool _result = true;
            StreamReader fsOld = new StreamReader(fileName);
            string _existingData = fsOld.ReadToEnd();
            string _uploadedData = GetUploadedContent();
            if (_existingData != null && _uploadedData != null)
            {
                if (_existingData == _uploadedData)
                {
                    _result = true;
                }
                else
                {
                    _result = false;
                }
            }

            fsOld.Close();
            return _result;
        }

        private string GetUploadedContent()
        {
            int BUFFER_SIZE = fileUploader.PostedFile.ContentLength;
            int nBytesRead = 0;
            Byte[] Buffer = new Byte[BUFFER_SIZE];
            StringBuilder strUploadedContent = new StringBuilder("");
            fileUploader.PostedFile.InputStream.Position = 0;
            Stream theStream = fileUploader.PostedFile.InputStream;
            nBytesRead = theStream.Read(Buffer, 0, BUFFER_SIZE);

            while (0 != nBytesRead)
            {
                strUploadedContent.Append(Encoding.ASCII.GetString(Buffer, 0, nBytesRead));
                nBytesRead = theStream.Read(Buffer, 0, BUFFER_SIZE);
            }
            return strUploadedContent.ToString();
        }

        private bool ValidateUploadedFileExists(string filePath, string fileName, ref int recordCount)
        {

            bool _result = false;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/UploadedFiles.json"));
            DataTable fileTable = JsonConvert.DeserializeObject<DataTable>(json);
            recordCount = fileTable.Select("FileName='" + fileName + "'").Length;
            if (recordCount > 0)
            {
                _result = true;
            }
            return _result;
        }

        private bool CheckUploadedFileHaveOnlyHeader(TextReader tr)
        {
            bool _result = true;
            using (DataTable table = new CSVReader(tr).CreateDataTable(true))
            {

                if (table.Rows.Count > 0)
                {
                    _result = false;
                }
            }
            return _result;
        }

        private static byte[] GetFileHash(FileStream fs1)
        {
            using (var md5Hasher = new MD5CryptoServiceProvider())
            {
                return md5Hasher.ComputeHash(fs1);
            }


        }
        private static byte[] GetFileHash(StreamReader fs2)
        {

            var md5Hasher = new MD5CryptoServiceProvider();
            return md5Hasher.ComputeHash(fs2.BaseStream);

        }
        private void SaveFileDetails(string fileName, string uploadedBy, string uploadedOn)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/UploadedFiles.json"));
            DataTable fileTable = JsonConvert.DeserializeObject<DataTable>(json);
            int columnIDvalue = GetUniqueID(fileTable, "uploadedfiles");
            DataRow dr = fileTable.Rows.Add(columnIDvalue, fileName, ddlAssignedProject.SelectedItem.Text, uploadedBy, uploadedOn, "img/success.png");
            fileTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(fileTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/UploadedFiles.json"), output);
            BindFileGrid(ddlAssignedProject.SelectedValue);
        }
        private void UpdateUploadedFile(string fileName, string uploadedBy, string uploadedOn)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/UploadedFiles.json"));
            DataTable fileTable = JsonConvert.DeserializeObject<DataTable>(json);
            int columnIDvalue = GetUniqueID(fileTable, "uploadedfiles");
            DataRow dr = fileTable.Select("FileName='" + fileName + "' AND ProjectName='" + ddlAssignedProject.SelectedItem.Text + "' AND UploadedBy ='" + uploadedBy + "'")[0];
            dr["Date"] = uploadedOn;
            fileTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(fileTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/UploadedFiles.json"), output);
            BindFileGrid(ddlAssignedProject.SelectedValue);
        }
        private int GetUniqueID(DataTable fileTable, string uploadedfiles)
        {
            int _localNewID = fileTable.Rows.Count;
            if (_localNewID == 0)
            {
                fileTable = AddColumnsToTargetTable(fileTable, uploadedfiles);
            }
            _localNewID = _localNewID + 1;
            if (_localNewID == 1)
            {
                if (fileTable.Select("ID ='" + _localNewID + "'").Length > 0)
                {
                    _localNewID = _localNewID + 1;
                }
            }
            return _localNewID;
        }

        private DataTable AddColumnsToTargetTable(DataTable fileTable, string targetTable)
        {
            string columns = "";
            switch (targetTable)
            {
                case "uploadedfiles":
                    columns = "ID:FileName:ProjectName:UploadedBy:Date:UploadedStatus";
                    break;
                default:
                    columns = "";
                    break;
            }
            foreach (string columnName in columns.Split(':'))
            {
                DataColumn dc = new DataColumn(columnName);
                fileTable.Columns.Add(dc);
            }
            fileTable.AcceptChanges();
            return fileTable;

        }

        private void DeleteUploadedFile(string iD)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/UploadedFiles.json"));
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (usrTable.Select("ID ='" + iD + "'").Length > 0)
            {
                usrTable.Select("ID ='" + iD + "'")[0].Delete();
                usrTable.AcceptChanges();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/UploadedFiles.json"), output);
            }
        }

        protected void ddlusrEmailId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTemplateGrid("", ddlUsrEmailId.SelectedValue);

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowFileMgnt();", true);
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string _message="";
            try
            {
                System.IO.FileStream fs = null;
                fs = System.IO.File.Open(ddlTemplate.SelectedValue, System.IO.FileMode.Open);
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
            

        protected void LoadInstruction(object sender, EventArgs e)
        {
            tplInstruction.InnerHtml = "";
            string templateName = ddlTemplate.SelectedItem.Text;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/MasterTemplate.json");

            DataTable tblMasterTemple = JsonConvert.DeserializeObject<DataTable>(json);
            if (tblMasterTemple.Rows.Count > 0)
            {
                if (tblMasterTemple.Select("TemplateName='" + templateName + "'").Length > 0)
                {
                    string instruction = tblMasterTemple.Select("TemplateName='" + templateName + "'")[0]["Instruction"].ToString();
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

        protected void ddlAssignedProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAssignedProject.SelectedIndex > 0)
            {
                btnUploader.Enabled = true;
                fileUploader.Enabled = true;
                ddlTemplate.Enabled = true;
                btnDownload.Enabled = true;
            }
            else
            {
                btnUploader.Enabled = false;
                fileUploader.Enabled = false;
                ddlTemplate.SelectedIndex = 0;
                ddlTemplate.Enabled = false;
                btnDownload.Enabled = false;
            }

            BindFileGrid(ddlAssignedProject.SelectedValue);
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
            ViewState["dirProject"] = dsUserDetails.Tables[0];
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
                projectEntitiy.ProjectName = spnProjectName.InnerText;
                projectEntitiy.CreatedBy = Session["UserEmail"].ToString();
                projectEntitiy.Description = txtDescription.Value;
                projectEntitiy.UserRole = Session["UserRole"].ToString();
                projectEntitiy.UserEmail = Session["UserEmail"].ToString();
                if (hdnProjectToEdit.Value == "")
                {
                    projectBL.SaveProjectMaster(projectEntitiy);
                    _message = "Project Created Successfully.)";
                }
                else
                {
                    projectBL.UpdateProjectMaster(projectEntitiy);
                    _message = "Project updated Successfully.)";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            BindProjectGrid();
            if (grdProject.PageCount > 1)
                grdProject.PageIndex = grdProject.PageCount - 1;
            spnProjectName.InnerText = GenerateProjectName();

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt(); ", _message), true);
        }

        protected void grdProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            
            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = "project_name";
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
                grdProject.PageIndex = e.NewPageIndex;
                grdProject.DataSource = dtrslt;
                grdProject.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "ShowProjectMgnt();", true);

        }

        protected void grdProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DeleteProject(((System.Web.UI.WebControls.Label)grdProject.Rows[e.RowIndex].Cells[0].Controls[1]).Text);
            BindProjectGrid();
            grdProject.PageIndex = grdProject.PageCount - 1;
            spnProjectName.InnerText = GenerateProjectName();
            string _message = "Project removed successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt();", _message), true);
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
               
                string instuction = e.Row.Cells[2].Text.Replace("\n", "<br>");
                foreach (Button editButton in e.Row.Cells[3].Controls.OfType<Button>())
                {
                    editButton.UseSubmitBehavior = false;
                    editButton.Attributes["onclick"] = "return PullDataToEdit('" + projectname + "','" + updatedBy + "','" + instuction + "');";
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
            string _message = "Project deleted successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');ShowProjectMgnt(); ", _message), true);

        }

        protected void btnFUCalbk_Click(object sender, EventArgs e)
        {
            string _message = hdnFUCalBkMsg.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowFileMgnt(); ", _message), true);
            hdnFUCalBkMsg.Value = "";
        }
    }
}



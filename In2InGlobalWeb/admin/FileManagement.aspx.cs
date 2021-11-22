using In2InGlobal.presentation.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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
                    string usrRole = Session["UserRole"].ToString();
                    if (usrRole == "Admin")
                    {
                        usrEmailTR.Visible = true;
                        tblTemplateDetail.Visible = true;
                        // BindTemplateGrid(ddlProjects.SelectedValue, "");


                    }
                    else
                    {
                        usrEmailId.Text = Session["UserEmail"].ToString();
                        usrEmailId.ReadOnly = true;
                        if (Session["ProjectID"] != null)
                        {
                            ddlProjects.SelectedValue = Session["ProjectID"].ToString();
                        }
                        tblTemplateDetail.Visible = true;
                        BindTemplateGrid("", usrEmailId.Text);
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
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Projects.json");
            ddlProjects.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlProjects.DataBind();
        }

        private void BindAssignedProjects()
        {
            string _email = Session["UserEmail"].ToString();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Projects.json");
            ddlAssignedProject.DataSource = JsonConvert.DeserializeObject<DataTable>(json).Select("CreatedBy='" + _email + "'").CopyToDataTable();
            ddlAssignedProject.DataBind();
        }

        private void LoadTemplates()
        {
            DataTable dtTemplate = new DataTable();
            dtTemplate.Columns.Add("TemplateName");
            dtTemplate.Columns.Add("FilePath");

            /*foreach (string s in System.IO.Directory.GetFiles(Server.MapPath("MasterTemplate")))
            {
                string fileName = System.IO.Path.GetFileName(s);
                DataRow newRow = dtTemplate.NewRow();
                newRow["TemplateName"] = fileName.Substring(0, fileName.Length - 4);
                newRow["FilePath"] = fileName;

                dtTemplate.Rows.Add(newRow);
            }*/

            DataTable tblAssignedTemplate = new DataTable();
            string AssignedTemplateJson = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Template.json");

            if (ddlAssignedProject.SelectedIndex > 0)
            {
                if (JsonConvert.DeserializeObject<DataTable>(AssignedTemplateJson).Select("ProjectName='" + ddlAssignedProject.SelectedValue + "'").Length > 0)
                {
                    tblAssignedTemplate = JsonConvert.DeserializeObject<DataTable>(AssignedTemplateJson).Select("ProjectName='" + ddlAssignedProject.SelectedValue + "'").CopyToDataTable();
                }
            }
            else
            {
                if (JsonConvert.DeserializeObject<DataTable>(AssignedTemplateJson).Select("Email='" + Session["UserEmail"].ToString() + "'").Length > 0)
                {
                    tblAssignedTemplate = JsonConvert.DeserializeObject<DataTable>(AssignedTemplateJson).Select("Email='" + Session["UserEmail"].ToString() + "'").CopyToDataTable();
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

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/UploadedFiles.json");

            DataTable tblUploadedFiles = JsonConvert.DeserializeObject<DataTable>(json);
            DataRow _usrRow = (DataRow)Session["UserRow"];
            string userName = _usrRow["FirstName"] + " " + _usrRow["LastName"];
            if (tblUploadedFiles.Rows.Count > 0)
            {
                if (pid != "")
                {

                    if (tblUploadedFiles.Select("ProjectName='" + pid + "'").Length > 0)
                    {
                        tblUploadedFiles = tblUploadedFiles.Select("ProjectName='" + pid + "'").CopyToDataTable();
                    }
                    else
                    {
                        tblUploadedFiles = null;
                        grdTemplate.EmptyDataText = "No file(s) uploaded for selected project. ";
                    }
                }
                else
                {

                    if (tblUploadedFiles.Select("UploadedBy='" + userName + "'").Length > 0)
                    {
                        tblUploadedFiles = tblUploadedFiles.Select("UploadedBy='" + userName + "'").CopyToDataTable();

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
        private void BindTemplateGrid(string _pid, string _email)
        {
            grdTemplate.Visible = true;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Template.json");
            DataTable tblTemplate = JsonConvert.DeserializeObject<DataTable>(json);
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
        protected void grdUploadedFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            BindFileGrid(ddlAssignedProject.SelectedValue);
            grdUploadedFiles.PageIndex = e.NewPageIndex;
            grdUploadedFiles.DataBind();

        }
        protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindTemplateGrid("", Session["UserEmail"].ToString());
            grdTemplate.PageIndex = e.NewPageIndex;
            grdTemplate.DataBind();

        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            usrEmailId.Text = "";
            BindTemplateGrid(ddlProjects.SelectedValue, "");
        }

        protected void btnUploader_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string filePath = Server.MapPath("uploadedfiles\\");
            string uploadedBy = "";
            string today = DateTime.Now.ToShortDateString();
            if (Session["UserRow"] != null)
            {
                DataRow usrDataRow = (DataRow)Session["UserRow"];
                uploadedBy = usrDataRow["FirstName"].ToString() + " " + usrDataRow["LastName"].ToString();
                try
                {
                    if (fileUploader.HasFile)
                    {
                        fileName = fileUploader.FileName;
                        fileName = fileName.Replace(".csv","~"+ uploadedBy.Replace(" ","")+"~"+ ddlAssignedProject.SelectedValue + ".csv");

                        //check whether file exists,if no write the file into folder & database 
                        //If yes 2nd validation uploaded file only contain header,if yes then exit with error message
                        //if no then verify whether both the files contain same data if same then exit with warning message
                        //if both files are having different data then write the file with different version name
                        
                        string pathToCheck = filePath + fileName;
                        if (!System.IO.File.Exists(pathToCheck))
                        {
                            fileUploader.SaveAs(System.IO.Path.Combine(filePath, fileName));
                            SaveFileDetails(fileName, uploadedBy, today);                           
                            string _message = "File Uploaded Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}');", _message), true);
                        }
                        else
                        {
                            using (StreamReader uploadedFS = new StreamReader(fileUploader.PostedFile.InputStream))
                            {
                                TextReader uploaderFileTextReader= new StreamReader(uploadedFS.BaseStream);

                                if (CheckUploadedFileHaveOnlyHeader(uploaderFileTextReader))
                                {
                                    string _message = "Uploaded Template contains only header.";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("B"), string.Format("ShowServerMessage('{0}');", _message), true);
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
                                        string _message = "File uploaded Successfully.";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("P"), string.Format("ShowServerMessage('{0}');", _message), true);
                                    }
                                    else
                                    {
                                        
                                        string tempfileName = "";
                                        int counter = 2;
                                        while (System.IO.File.Exists(pathToCheck))
                                        {

                                            tempfileName = "V-"+counter.ToString() +"-" + fileName;
                                            pathToCheck = filePath + tempfileName;
                                            counter++;
                                        }
                                        fileName = tempfileName;
                                        fileUploader.SaveAs(Server.MapPath(System.IO.Path.Combine("/admin/uploadedfiles/", fileName)));
                                        SaveFileDetails(fileName, uploadedBy, today);
                                        string _message = "File uploaded Successfully.";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');", _message), true);
                                    }
                                }
                                uploaderFileTextReader.Close();
                            } 
                        }
                    }
                }
                catch (System.IO.IOException ex)
                {
                    throw ex;
                }
            }

        }

        private bool IsBothCSVFileDataAreSame(string fileName)
        {
            bool _result = true;
            StreamReader fsOld = new StreamReader(fileName);
            //StreamReader uploadedFS = new StreamReader(fileUploader.PostedFile.InputStream);
            string _existingData = fsOld.ReadToEnd();
            string _uploadedData = GetUploadedContent(); //uploadedFS.ReadToEnd();
            if(_existingData !=null && _uploadedData != null)
            {
                if (_existingData == _uploadedData)
                {
                    _result = true;
                }
                else {
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

        private bool ValidateUploadedFileExists(string filePath, string fileName,ref int recordCount)
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
             
                if (table.Rows.Count > 1)
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
            int columnIDvalue = GetUniqueID(fileTable,"uploadedfiles");
            DataRow dr = fileTable.Rows.Add(columnIDvalue, fileName, ddlAssignedProject.SelectedValue, uploadedBy, uploadedOn, "img/success.png");
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
            DataRow dr = fileTable.Select("FileName='" + fileName + "' AND ProjectName='" + ddlAssignedProject.SelectedValue + "' AND UploadedBy ='" + uploadedBy + "'")[0];
            dr["Date"] = uploadedOn;
            fileTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(fileTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/UploadedFiles.json"), output);
            BindFileGrid(ddlAssignedProject.SelectedValue);
        }
        private int GetUniqueID(DataTable fileTable,string uploadedfiles)
        {
            int _localNewID = fileTable.Rows.Count;
            if(_localNewID ==0)
            {
                fileTable = AddColumnsToTargetTable(fileTable,uploadedfiles);
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
            switch(targetTable)
            {
                case "uploadedfiles":
                    columns = "ID:FileName:ProjectName:UploadedBy:Date:UploadedStatus";
                   break;
                default:
                  columns = "";
                    break;
            }
            foreach(string columnName in columns.Split(':'))
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

        protected void usrEmailId_TextChanged(object sender, EventArgs e)
        {
            BindTemplateGrid("", usrEmailId.Text);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/Projects.json"));
            DataTable tblProject = JsonConvert.DeserializeObject<DataTable>(json);
            DataRow[] usrProjects = tblProject.Select("CreatedBy='" + usrEmailId.Text + "'");
            if (usrProjects.Length > 0)
            {
                ddlProjects.Items.Clear();
                ddlProjects.Items.Add(new ListItem("--Select a Project--"));
                ddlProjects.DataSource = usrProjects.CopyToDataTable();
                ddlProjects.DataBind();
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("MasterTemplate/" + ddlTemplate.SelectedValue), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" + ddlTemplate.SelectedValue);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
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

        }
    }
}



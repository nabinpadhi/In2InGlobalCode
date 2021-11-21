using In2InGlobal.presentation.Tools;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
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
            string filePath = Server.MapPath("uploadedfiles");
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
                        fileName = fileName.Replace(".csv","~"+ usrDataRow["Email"] + ".csv");
                        //if (ValidateUploadedFile(filePath, fileName))
                        //{                            
                            fileUploader.SaveAs(System.IO.Path.Combine(filePath, fileName));
                            SaveFileDetails(fileName, uploadedBy, today);
                            //Response.Redirect(Request.RawUrl);
                            Response.Redirect(Request.Url.AbsoluteUri, true);
                        //}
                    }
                }
                catch (System.IO.IOException ex)
                {
                    throw ex;
                }
            }

        }

        private bool ValidateUploadedFile(string filePath, string fileName)
        {

            bool _result = true;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/UploadedFiles.json"));
            DataTable fileTable = JsonConvert.DeserializeObject<DataTable>(json);
            if(fileTable.Select("FileName='"+fileName+"'").Length >0)
            {
                //write the uploaded file into temp folder
                using (FileStream fsOld = File.OpenWrite(Server.MapPath(System.IO.Path.Combine("uploadedfiles/", fileName))))
                {

                    using (FileStream fs = File.OpenWrite(System.IO.Path.Combine(filePath, fileName)))
                    {
                        fileUploader.PostedFile.InputStream.CopyTo(fs);
                        if (!CheckUploadedFileHaveOnlyHeader(fileUploader.FileName))
                        {
                            var uploadedfileHash = GetFileHash(fs);
                            var existingFileHash = GetFileHash(fsOld);
                            if(!uploadedfileHash.Equals(existingFileHash))
                            {
                                fileUploader.SaveAs(System.IO.Path.Combine("/uploadedfiles/", fileName.Replace(".csv", Session["UserEmail"] + ".csv")));
                            }
                            else
                            {
                                _result = false;
                            }
                        }
                        else
                        {
                            _result = false;
                        }
                        fs.Flush();
                    }
                }
            }
            return _result;
        }

        private bool CheckUploadedFileHaveOnlyHeader(string fileName)
        {
            DataTable table = CSVReader.ReadCSVFile(fileName, true);
            bool _result = true;
            if (table.Rows.Count > 1)
            {
                _result = false;
            }
            return _result;
        }

        private static byte[] GetFileHash(FileStream fs)
        {
           
                var md5Hasher = new MD5CryptoServiceProvider();
                return md5Hasher.ComputeHash(fs);
           
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



using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
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
                    BindFileGrid();
                    LoadTemplates();
                    BindProjects();
                    projectid.InnerText = Session["ProjectID"].ToString();
                    string usrRole = Session["UserRole"].ToString();
                    if (usrRole == "Admin")
                    {
                        usrEmailTR.Visible = true;
                        tblTemplateDetail.Visible = true;
                        BindTemplateGrid(ddlProjects.SelectedValue, "");


                    }
                    else
                    {
                        usrEmailTR.Visible = false;
                        tblTemplateDetail.Visible = false;
                    }
                }
                else{
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

        private void LoadTemplates()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Template.json");
            ddlTemplate.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlTemplate.DataTextField = "TemplateName";
            ddlTemplate.DataValueField = "TemplateFileName";
            ddlTemplate.DataBind();
        }

        private void BindFileGrid()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/UploadedFiles.json");
            grdUploadedFiles.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdUploadedFiles.DataBind();
        }
        private void BindTemplateGrid(string _pid,string _email)
        {

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
                else if(_email != "")
                {
                    _target = "Email = '" + _email + "'";
                }
                if (tblTemplate.Select(_target).Length > 0)
                {
                    tblTemplate = tblTemplate.Select(_target).CopyToDataTable();
                    grdTemplate.DataSource = tblTemplate;
                    grdTemplate.DataBind();

                }
                else { grdTemplate.DataSource = null; }
            }

        }
        protected void grdUploadedFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindFileGrid();
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
            BindTemplateGrid(ddlProjects.SelectedValue,"");
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
                uploadedBy = usrDataRow["FirstName"].ToString() + "  " + usrDataRow["LastName"].ToString();
                try
                {
                    if (fileUploader.HasFile)
                    {
                        fileName = fileUploader.FileName;
                        fileUploader.SaveAs(System.IO.Path.Combine(filePath, fileName));
                        SaveFileDetails(fileName,uploadedBy, today);
                        //Response.Redirect(Request.RawUrl);
                        Response.Redirect(Request.Url.AbsoluteUri, true);
                    }
                }
                catch (System.IO.IOException ex)
                {
                    throw ex;
                }
            }
           
        }

        private void SaveFileDetails(string fileName, string uploadedBy, string uploadedOn)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(Server.MapPath("json-data/UploadedFiles.json"));
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            DataRow dr = usrTable.Rows.Add(fileName,uploadedBy, uploadedOn, "img/success.png");
            usrTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/UploadedFiles.json"), output);
            BindFileGrid();
        }

        protected void grdUploadedFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grdUploadedFiles.DataKeys[e.RowIndex].Value.ToString();
            DeleteUploadedFile(ID);
            BindFileGrid();
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
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("TemplateFiles/" +ddlTemplate.SelectedValue), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" + ddlTemplate.SelectedValue);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
        }
    }
}



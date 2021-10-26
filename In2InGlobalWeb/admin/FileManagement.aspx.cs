using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class FileManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string target = Request.QueryString.Get("t");
            if (target == "a")
            {
                usrEmailTR.Visible = true;
                tblTemplateDetail.Visible = true;
                BindFileGrid();
            }
            else
            {
                usrEmailTR.Visible = false;
                tblTemplateDetail.Visible = false;
            }
        }
        private void BindFileGrid()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/UploadedFiles.json");
            grdUploadedFiles.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdUploadedFiles.DataBind();
        }

        protected void grdUploadedFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindFileGrid();
                grdUploadedFiles.PageIndex = e.NewPageIndex;
                grdUploadedFiles.DataBind();
            
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;

namespace In2InGlobal.presentation.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindActivity();                

            }
            else
            {
                if (hdnPageAction.Value=="Login")
                {
                    string emailid = Request.Form["email"];
                    string pwd = Request.Form["password"];
                    string companyname = Request.Form["companyname"];
                    string activity = ddlActivity.SelectedValue.ToString();
                }
            }
            
        }
        private void BindActivity()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Activity.json");
            ddlActivity.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlActivity.DataValueField = "ActivityName";
            ddlActivity.DataBind();
        }
    }
}
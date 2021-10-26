using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class UserManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string target = Request.QueryString.Get("t");
            if (target != "a")
            {
                Response.Redirect("Login.aspx");

            }
            else
            {
                BindUsers();
                BindCompany();
                BindRoles();
            }

        }

        private void BindUsers()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/users.json");
            grdUsers.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdUsers.DataBind();
        }
        private void BindCompany()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Companies.json");
            ddlCompanyName.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlCompanyName.DataValueField = "CompanyName";
            ddlCompanyName.DataBind();
        }
        private void BindRoles()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Roles.json");
            ddlRoleName.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlRoleName.DataValueField = "RoleName";
            ddlRoleName.DataBind();
        }

        protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindUsers();
            grdUsers.PageIndex = e.NewPageIndex;
            grdUsers.DataBind();
        }
    }
}
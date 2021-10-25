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
            }
          
        }

        private void BindUsers()
        {
            //grdUsers.DataSource = GetUsers();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/users.json");           
            grdUsers.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdUsers.DataBind();
        }
    }
}
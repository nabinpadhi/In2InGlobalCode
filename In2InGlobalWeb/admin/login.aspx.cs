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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindActivity();
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
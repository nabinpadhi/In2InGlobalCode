using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Services;

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
        [WebMethod]
        public static string GetUserDetails(string emailid)
        {
            string companyNameandrole = "";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (usrTable.Select("Email ='" + emailid + "'").Length > 0)
            {
                DataRow dr = usrTable.Select("Email ='" + emailid + "'")[0];
                companyNameandrole = dr["Company"].ToString()+","+dr["Role"].ToString();                
            }
            else
            {
                companyNameandrole = "";                
            }
            return companyNameandrole;

        }
        [WebMethod(EnableSession = true)]
        public static string DoLogin(string emailid,string password)
        {
            string result = "";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (usrTable.Select("Email ='" + emailid + "' and Password = '"+password+"'").Length > 0)
            {
                DataRow userRow = usrTable.Select("Email ='" + emailid + "' and Password = '" + password + "'")[0];
                result = "Success";
                HttpContext.Current.Session["UserRole"] = userRow["Role"].ToString();
                HttpContext.Current.Session["UserEmail"] = userRow["Email"].ToString();
                HttpContext.Current.Session["UserRow"] = userRow;
            }
            else
            {
                result = "Invalid email / password";
                HttpContext.Current.Session["UserRole"] = null;
                HttpContext.Current.Session["UserRow"] = null;
                HttpContext.Current.Session["UserEmail"] = null;
            }
            return result;

        }

    }
}
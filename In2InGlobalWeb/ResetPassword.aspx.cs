using InGlobal.presentation;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;

namespace In2InGlobal.presentation.admin
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string forwhom = Request.QueryString["fw"];
                if (forwhom != null)
                {
                    string userData = StringUtil.Decrypt(forwhom);

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    string json = (new WebClient()).DownloadString(Server.MapPath("admin/json-data/Users.json"));
                    DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);                 
                    DataRow[] userRows = usrTable.Select("Email ='" + userData + "'");
                    
                    if (userRows.Length == 0)
                    {
                        Response.Redirect("admin/login.aspx");
                    }
                    else
                    {
                        ps_user_id.Value = userData;
                    }
                    ps_user_id.Value = userData;

                }
            }
        }
        [WebMethod(EnableSession = true)]
        public static string ChangePass(string emailid, string password)
        {
            string result = "";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString(HttpContext.Current.Server.MapPath("admin/json-data/Users.json"));
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            password = new EncryptField().Encrypt(password);
            DataRow[] userRow = usrTable.Select("Email ='" + emailid + "'");
            if (userRow.Length > 0)
            {
                userRow[0]["Password"] = password;                
                usrTable.AcceptChanges();
                userRow[0].SetModified();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(HttpContext.Current.Server.MapPath("admin/json-data/Users.json"), output);
                result = "{\"msg\":\"Password changed Successfully\",\"status\":\"1\" }";

            }
            
            return result;
        }

    }
}
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Services;
using In2InGlobal.presentation;
using InGlobal.presentation;
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

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
        public static string SendPassword(string emailid)
        {
            string result = "";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            DataRow[] userRows = usrTable.Select("Email ='" + emailid + "'");
            if (userRows.Length > 0)
            {
                string password = userRows[0]["Password"].ToString();
                password = new EncryptField().Decrypt(password);
                string userName = userRows[0]["FirstName"].ToString();
                string bodyText = GetForgotPasswordHTMLBody(userName,emailid);// "Dear " + userName + ",<br><p>As requested here we are sending your password to login In2In Global App.</p><br><b>Password :</b><i>" + password + "</i>";
                try
                {
                    MailMessage message = new MailMessage("in2inglobalapp@gmail.com", emailid, "In2In Global Login Credential", bodyText);
                    message.IsBodyHtml = true;
                     SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("in2inglobalapp@gmail.com", "%TGB6yhn^YHN5tgb");
                    client.Send(message);
                    result = "{\"msg\" : \"Password was sent successfully to your email ID.\",\"status\":\"1\"}";
                }
                catch (Exception ex)
                {
                    result = ex.StackTrace;
                }
            }
            else
            {
                result = "Provided email not found.";               
            }
            return result;

        }

        private static string GetForgotPasswordHTMLBody(string userName,string emailid)
        {
           
            string htmlBody="";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            htmlBody = (new WebClient()).DownloadString("http://localhost:26677/tools/fgpwdhtmlformat.html");
            htmlBody = htmlBody.Replace("${user-name}", userName);
            htmlBody = htmlBody.Replace("${site-url}", "http://localhost:26677/admin/login.aspx");
            htmlBody = htmlBody.Replace("${site-name}", "In2In Global pvt. ltd.");
            htmlBody = htmlBody.Replace("${customer-service-email}", "in2inglobalapp@gmail.com");
            htmlBody = htmlBody.Replace("${site-toll-free-number}", "1200-987654");
            htmlBody = htmlBody.Replace("${site-logo}", "cid:MyImage");
            htmlBody = htmlBody.Replace("${reset-password-url}",GetResetURL(emailid));
            return htmlBody;
        }

        private static string GetResetURL(string emailid)
        {            
            string result = "http://localhost:26677/ResetPassword.aspx?fw=" + StringUtil.Crypt(emailid);
            return result;
        }
       

        [WebMethod(EnableSession = true)]
        public static string DoLogin(string emailid,string password)
        {
            string result = "";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            password = new EncryptField().Encrypt(password);
            if (usrTable.Select("Email ='" + emailid + "' and Password = '"+password+"'").Length > 0)
            {
                DataRow userRow = usrTable.Select("Email ='" + emailid + "' and Password = '" + password + "'")[0];
                result = "Success";
                HttpContext.Current.Session["UserRole"] = userRow["Role"].ToString();
                HttpContext.Current.Session["UserEmail"] = userRow["Email"].ToString();
                HttpContext.Current.Session["UserRow"] = userRow;

                string projson = (new WebClient()).DownloadString(HttpContext.Current.Server.MapPath("json-data/Projects.json"));
                DataTable proTable = JsonConvert.DeserializeObject<DataTable>(projson);
                DataRow[] UserProjects = proTable.Select("CreatedBy ='" + userRow["Email"].ToString() + "'");
                HttpContext.Current.Session["ProjectTable"] = UserProjects;

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
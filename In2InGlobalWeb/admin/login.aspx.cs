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
using In2InGlobal.businesslogic;

namespace In2InGlobal.presentation.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindActivity(Request.Form["email"]);
            }
            else
            {
                if (hdnPageAction.Value == "Login")
                {
                    Session["email"] = Request.Form["email"];

                    string emailid = Request.Form["email"];
                    string pwd = Request.Form["password"];
                    string companyname = Request.Form["companyname"];
                    BindActivity(emailid);
                }
            }

        }



        /// <summary>
        /// Send Password
        /// </summary>
        /// <param name="emailid"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static string SendPassword(string emailid)
        {
            string result = "";
            LoginBl loginbL = new LoginBl();
            DataSet dsUser = loginbL.getMyLogin(emailid);

            if (dsUser.Tables.Count > 0)
            {
                DataTable usrTable = dsUser.Tables[0];


                string password = usrTable.Rows[0]["paawrd"].ToString();
                password = new EncryptField().Decrypt(password);
                string userName = usrTable.Rows[0]["first_name"].ToString();
                string bodyText = GetForgotPasswordHTMLBody(userName, emailid);
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

        /// <summary>
        /// Get Forgot Password HTMLBody
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailid"></param>
        /// <returns></returns>
        private static string GetForgotPasswordHTMLBody(string userName, string emailid)
        {

            string htmlBody = "";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            htmlBody = (new WebClient()).DownloadString("http://localhost:26677/tools/fgpwdhtmlformat.html");
            htmlBody = htmlBody.Replace("${user-name}", userName);
            htmlBody = htmlBody.Replace("${site-url}", "http://localhost:26677/admin/login.aspx");
            htmlBody = htmlBody.Replace("${site-name}", "In2In Global pvt. ltd.");
            htmlBody = htmlBody.Replace("${customer-service-email}", "in2inglobalapp@gmail.com");
            htmlBody = htmlBody.Replace("${site-toll-free-number}", "1200-987654");
            htmlBody = htmlBody.Replace("${site-logo}", "cid:MyImage");
            htmlBody = htmlBody.Replace("${reset-password-url}", GetResetURL(emailid));
            return htmlBody;
        }

        /// <summary>
        /// Get Reset Url
        /// </summary>
        /// <param name="emailid"></param>
        /// <returns></returns>
        private static string GetResetURL(string emailid)
        {
            string result = "http://localhost:26677/ResetPassword.aspx?fw=" + StringUtil.Crypt(emailid);
            return result;
        }

        /// <summary>
        /// Do Login
        /// </summary>
        /// <param name="emailid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static string DoLogin(string emailid, string password)
        {
            string result = "";
           
            LoginBl userMasterBL = new LoginBl();
            DataSet dsUser = new DataSet();            
            dsUser = userMasterBL.getMyLogin(emailid);
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                DataTable usrTable = dsUser.Tables[0];
            ///and paawrd = '" + password + "'"
            password = new EncryptField().Encrypt(password);

                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    DataRow userRow = dsUser.Tables[0].Rows[0];

                    result = "Success";
                    HttpContext.Current.Session["UserRole"] = dsUser.Tables[0].Rows[0]["role_name"].ToString();
                    HttpContext.Current.Session["UserEmail"] = dsUser.Tables[0].Rows[0]["user_email"].ToString();
                    HttpContext.Current.Session["UserRow"] = userRow;
                    HttpContext.Current.Session["dsUser"] = dsUser;
                }
                else
                {
                    result = "Invalid email / password";
                    HttpContext.Current.Session["UserRole"] = null;
                    HttpContext.Current.Session["UserRow"] = null;
                    HttpContext.Current.Session["UserEmail"] = null;
                    HttpContext.Current.Session["dsUser"] = null;
                }

               
            }
            else
            {
                result = "Invalid email / password";
                HttpContext.Current.Session["UserRole"] = null;
                HttpContext.Current.Session["UserRow"] = null;
                HttpContext.Current.Session["UserEmail"] = null;
                HttpContext.Current.Session["dsUser"] = null;

            }
            return result;
        }

            /// <summary>
            /// txtEmailId TextChanged
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void txtEmailId_TextChanged(object sender, EventArgs e)
            {
            loginbtn.Disabled = false;
            var email = txtEmailId.Text;
               BindActivity(email);
                password.Value = "";
                password.Focus();
            
            }


        /// <summary>
        /// Bind Activity
        /// </summary>
        /// <param name="useremail"></param>
        private void BindActivity(string useremail)
        {
            //  string result = "";
            if (useremail != null)
            {
                LoginBl userMasterBL = new LoginBl();
                DataSet dsUser = new DataSet();
                dsUser = userMasterBL.getMyLogin(useremail);
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    companyname.Text = dsUser.Tables[0].Rows[0]["company_name"].ToString();
                    Session["CompanyName"]= dsUser.Tables[0].Rows[0]["company_name"].ToString();                   
                    companyname.Enabled = false;
                    Session["dsUser"] = dsUser;
                }
                else
                {
                    companyname.Text = "No Company";
                    loginbtn.Disabled = true;
                    HttpContext.Current.Session["UserRole"] = null;
                    HttpContext.Current.Session["UserRow"] = null;
                    HttpContext.Current.Session["UserEmail"] = null;
                }
            }
        }
    }
}
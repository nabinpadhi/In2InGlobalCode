using In2InGlobal.businesslogic;
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
                    string userEmail = StringUtil.Decrypt(forwhom);
                    LoginBl loginbL = new LoginBl();
                    DataSet dsUser = loginbL.getMyLogin(userEmail);

                    if (dsUser.Tables.Count == 0)
                    {
                        Response.Redirect("admin/login.aspx");
                    }
                    else
                    {
                        ps_user_id.Value = dsUser.Tables[0].Rows[0]["user_email"].ToString();
                    }
                  
                }
            }
        }
        [WebMethod(EnableSession = true)]
        public static string ChangePass(string emailid, string password)
        {
            string result = "";
            
            password = new EncryptField().Encrypt(password);

            LoginBl loginbl = new LoginBl();
            loginbl.UpdateUserLoginPwd(emailid, password);
            
            result = "{\"msg\":\"Password changed Successfully\",\"status\":\"1\" }";

            return result;
        }

    }
}
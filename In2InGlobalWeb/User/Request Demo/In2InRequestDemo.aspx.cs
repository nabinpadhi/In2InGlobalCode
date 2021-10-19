using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InGlobal.BusinessLogic;
using System.Net.Mail;
using System.IO;
using System.Web.Configuration;
using System.Net;
namespace InGlobal.presentation
{
    public partial class tpmsignup : System.Web.UI.Page
    {

        XSSValidator _validatorObject = new XSSValidator("");
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void cmdSignup_Click(object sender, EventArgs e)
        {
            TPMUserBL _userObject = new TPMUserBL();
            MapObject(_userObject);
            string _autoPWD;
            _autoPWD = System.Web.Security.Membership.GeneratePassword(6,2);
            _userObject.ps_EncryptedPassword = new EncryptField().Encrypt(_autoPWD);
            if(_validatorObject.StartValidate(_userObject))
            {
                if (_userObject.AddTPM())
                {
                    SendWelComeMail(_userObject.ps_CompanyEmail, _userObject.ps_FullName, _autoPWD);
                    hdnServerResponse.Value = "next";
                }
                else{
                     hdnServerResponse.Value = "Signing up failed. Please try again later.";
                }
            }
            else
            {
                hdnServerResponse.Value = _validatorObject.ErrorMessage;
            }
        }
        static string GetWelcomeTPMMailTemplate()
        {
            StreamReader sr = File.OpenText(HttpContext.Current.Server.MapPath("../../MailTemplates/WelcomeTPMMailTemplate.txt"));
            string message = sr.ReadToEnd();
            sr.Close();
            return message;
        }
        static void SendWelComeMail(string _tpmMail, string _userFullName,string _tempPWD)
        {
            
            using (MailMessage mail = new MailMessage())
            {
                mail.To.Add(new MailAddress(_tpmMail));
                mail.From = new MailAddress("no-reply@ittpm.com", "ITTPM");
                mail.Subject = "Welcome to TPM family - http:\\www.ittpm.com";
                mail.IsBodyHtml = true;
                string ms = GetWelcomeTPMMailTemplate();
                mail.Body = ms.Replace("$USERNAME",_userFullName.TrimEnd()).Replace("$ToEmail", _tpmMail).Replace("$PASSWORD", _tempPWD);
                SmtpClient client = new SmtpClient();
                string nruserC = new EncryptField().Decrypt(WebConfigurationManager.AppSettings["NO_REPLY_UC"]);
                client.Credentials = new NetworkCredential("no-reply@ittpm.com", "iYkl31#7");
                client.Port = 25;
                client.Host = "ittpm.com";
                try
                {
                    client.Send(mail);
                }
                catch (SmtpException)
                {
 
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void MapObject(TPMUserBL _userObject)
        {
           
            
        }
    }
}
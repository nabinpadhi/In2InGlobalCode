using System.Net.Mail;
using System.Web;
namespace InGlobal.presentation
{
    public static class Common
    {
        public static bool HasSession()
        {
            if (HttpContext.Current.Session.Count != 0 && (null != HttpContext.Current.Session["UserID"]))
            {
                return true;
            }
            else
                return false;
        }

        public static void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("nabinpadhi@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "in2inglobalapp@gmail.com";
                NetworkCred.Password = "%TGB6yhn^YHN5tgb";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }
    }

}
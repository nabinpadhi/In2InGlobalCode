using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;

namespace InGlobal.presentation
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*class="latest-stories"><li>
            //string _newsHTML  = GetNewsHTML(@"http://ibnlive.in.com/newstopics/it-sector.html");
            string _newsHTML = GetNewsHTML(@"http://www.techspot.com/category/industry/");
        
            _newsHTML = _newsHTML.Replace("class=\"latest-stories\"><li>", "class=\"latest-stories\"><ul><li>");
            _newsHTML = _newsHTML.Replace("<div id=\"pointstable\"></div>", "</ul><div id=\"pointstable\"></div>");
            _newsHTML = _newsHTML.Replace("href=\"", "class=\"newsPop\"  data-skin=\"winter\" data-caption=\"TPM - IT News\" data-width=\"720px\" data-height=\"450px\" data-css3effect=\"rollIn\" href=\"#\" data-newssrc=\"");
            divNewsCapture.InnerHtml = _newsHTML;*/
        }
        private string GetNewsHTML(string _Url)
        {
            string result = "";
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(_Url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);
                result = doc.DocumentNode.SelectSingleNode("//div[@id='pane-content']").InnerHtml;

            }
            catch (Exception)
            {
            }
            return result;
        }
        protected void btnLoginNext_click(object sender, EventArgs e)
        {
            Response.Redirect("User/TPMLanding.aspx");
        }
    }
}
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InGlobal.presentation.articles
{
    public partial class ArticleOnJQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            string _newsHTML = GetNewsHTML(@"http://blog.jquery.com/category/jquery-ui/");           
            tdJqueryArticle.InnerHtml = _newsHTML;
        }
        private string GetNewsHTML(string _Url)
        {

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(_Url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            result = doc.DocumentNode.SelectSingleNode("//div[@id='container']").InnerHtml;
            return result;
        }
    }
}
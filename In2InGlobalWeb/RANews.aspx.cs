using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InGlobal.presentation
{
    public partial class RANews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                      
           /* string newsUrl = Request.QueryString["newssrc"];
            string _height = Request.QueryString["popheight"];
            if(newsUrl.StartsWith("/news/"))
            {
                newsUrl = newsUrl.Replace("/news/", @"http://www.securityweek.com");
            }
           string _newsHTML = GetNewsHTML(@"//http://www.securityweek.com");
            
            _newsHTML = "<marquee behavior='scroll' onmouseover='this.stop();' onmouseout='this.start();' direction='up' style='width:100%; height:"+ _height +";background-color: white;' scrollamount='2'><div style='position:relative;top:-"+_height+"';>" + _newsHTML + "<div></marquee>";
            _newsHTML = _newsHTML.Replace("TechSpot News","");
            
            _newsHTML = _newsHTML.Replace("href=\"", "class=\"newsPop\"  data-skin=\"winter\" data-caption=\"TPM - IT News\" data-width=\"920px\" data-height=\"350px\" data-css3effect=\"rollIn\" href=\"#\" data-newssrc=\"");
            _newsHTML = _newsHTML.Replace("�", "'");
            news_container.InnerHtml =  _newsHTML;*/
           
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
                result = result.Replace("<div class=\"view view-recent-user-story view-id-recent_user_story view-display-id-block_1 view-dom-id-2\"", "<div class=\"view view-recent-user-story view-id-recent_user_story view-display-id-block_1 view-dom-id-2\" id=\"pane-content\"");
                sr.Close();
                myResponse.Close();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);
                result = doc.DocumentNode.SelectSingleNode("//div[@id='pane-content']").InnerHtml;

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }
    }
}
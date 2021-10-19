using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InGlobal.presentation.User
{
    public partial class ServerMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ResponseMessage"] != null)
            {
                messageParagrap.InnerHtml = "<b>" + Session["ResponseMessage"].ToString() + "<b>";
                Session["ResponseMessage"] = null;
            }
            
        }
    }
}
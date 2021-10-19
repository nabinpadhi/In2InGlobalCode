using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InGlobal.presentation.User
{
    public partial class TPMLanding : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Common.HasSession())
            {
                Response.Redirect("../index.aspx?a=so");
            }
            
        }
        protected void doNoSession(object sender, EventArgs e)
        {
            Response.Redirect("../index.aspx?a=so");
        }
        protected void Logout(object sender, EventArgs e)
        {
            Response.Redirect("../index.aspx?a=lo");
        }
    }
}
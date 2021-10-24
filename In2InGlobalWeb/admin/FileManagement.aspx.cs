using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class FileManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string target = Request.QueryString.Get("t");
            if (target == "a")
            {
                usrEmailTR.Visible = true;
                tblTemplateDetail.Visible = true;
            }
            else
            {
                usrEmailTR.Visible = false;
                tblTemplateDetail.Visible = false;
            }
        }
    }
}
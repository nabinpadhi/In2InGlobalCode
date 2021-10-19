using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InGlobal.presentation.User
{
    public partial class LandingDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Common.HasSession())
            {
                hdnIsInitialPassword.Value = Session["IsInitialPassword"].ToString();
                hdnSessionState.Value = "alive";
            }
            else
            {
                hdnIsInitialPassword.Value = "";
                hdnSessionState.Value = "dead";
                
            }
        }
    }
}
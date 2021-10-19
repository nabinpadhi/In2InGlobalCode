using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InGlobal.presentation.User.tpm
{
    public partial class qans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Common.HasSession())
            {

                hdnSessionState.Value = "alive";
            }
            else
            {

                hdnSessionState.Value = "dead";

            }
        }
    }
}
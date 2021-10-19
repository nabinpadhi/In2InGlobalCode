using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InGlobal.BusinessLogic;
namespace InGlobal.presentation
{
    public partial class ChangePwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdChangePwd_Click(object sender, EventArgs e)
        {
            TPMUserBL _user = new TPMUserBL();
            _user.pi_ID = (int)Session["UserID"];
            _user.ps_EncryptedPassword = new EncryptField().Encrypt(txtNewPwd.Text);
            if (_user.UpdatePwd() == 0)
            {
                hdnStatus.Value = "failed";
            }
            else{
                hdnStatus.Value = "success";
                Session["IsInitialPassword"] = "false";
            }
        }
    }
}
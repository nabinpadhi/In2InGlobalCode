using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InGlobal.BusinessLogic;
using System.Data;
namespace InGlobal.presentation
{
    public partial class In2InGlobalLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            TPMUserBL _userObject = new TPMUserBL();
            MapObject(_userObject);
            DataSet _result = _userObject.IsValidLogin();
            if (_result.Tables[0].Rows.Count > 0)
            {
                Session["UserID"] = _result.Tables[0].Rows[0]["UserID"];
                Session["IsTPM"] = _result.Tables[0].Rows[0]["IsTPM"];

                Session["IsInitialPassword"] = _result.Tables[0].Rows[0]["IsInitialPassword"];
                hdnServerResponse.Value = "next";
            }
            else
            {
                hdnServerResponse.Value = "pause";
                Session["UserID"] = "0";
            }

        }
        
        private void MapObject(TPMUserBL _userObject)
        {
            _userObject.ps_UserName = txtUserName.Text;
            _userObject.ps_EncryptedPassword = new EncryptField().Encrypt(txtPassword.Text);
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kss.ittpm.BusinessLogic;
namespace kss.ittpm.presentation
{
    public partial class tpmsignup : System.Web.UI.Page
    {

        XSSValidator _validatorObject = new XSSValidator("");
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void cmdSignup_Click(object sender, EventArgs e)
        {
            TPMUserBL _userObject = new TPMUserBL();
            MapObject(_userObject);
            
            _userObject.ps_EncryptedPassword = System.Web.Security.Membership.GeneratePassword(6,2);
            _userObject.ps_EncryptedPassword = new EncryptField().Encrypt(_userObject.ps_EncryptedPassword);
            if(_validatorObject.StartValidate(_userObject))
            {
                if (_userObject == null)//_userObject.AddTPM())
                {
                    hdnServerResponse.Value = "next";
                }
                else{
                     hdnServerResponse.Value = "Signing up failed. Please try again later";
                }
            }
            else
            {
                hdnServerResponse.Value = _validatorObject.ErrorMessage;
            }
        }

        private void MapObject(TPMUserBL _userObject)
        {
            _userObject.ps_FullName = txtFullName.Value;
            _userObject.ps_CompanyName = hdnCompanyName.Value;
            _userObject.pd_DOB = Convert.ToDateTime(txtDOB.Value);
            _userObject.ps_CompanyEmail = txtCompanyEmail.Value;
        }
    }
}
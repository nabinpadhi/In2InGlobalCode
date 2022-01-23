using InGlobal.BusinessLogic;
using System;
using System.Data;

namespace In2InGlobal.presentation.admin
{
    public partial class MyProfile : System.Web.UI.Page
    {
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] != null)
            {
                string usrRole = Session["UserRole"].ToString();
                if (!IsPostBack)
                {
                    FillMyProfileDetails();
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
            }
        }


        /// <summary>
        /// The below FUnction is used to load MyProfileData
        /// </summary>
        private void FillMyProfileDetails()
        {
            DataSet dsMyProfile = new DataSet();

            try
            {
               
                dsMyProfile = (DataSet)Session["dsUser"];
                if (dsMyProfile.Tables[0].Rows.Count > 0)
                {
                    DataRow drMyProfile = dsMyProfile.Tables[0].Rows[0];
                    username.InnerText = drMyProfile["first_name"].ToString() + "  " + drMyProfile["last_name"].ToString();
                    companyname.InnerText = drMyProfile["company_name"].ToString();
                    email.InnerText = drMyProfile["user_email"].ToString();
                    activityaccess.InnerText = drMyProfile["activity_name"].ToString();
                    role.InnerText = drMyProfile["role_name"].ToString();
                    if (drMyProfile["activated"].ToString() == "True")
                    {
                        status.InnerText = "Active";
                    }
                    else
                    {
                        status.InnerText = "InActive";
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }

        }

    }
}
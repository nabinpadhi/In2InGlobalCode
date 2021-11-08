using System;
using System.Data;

namespace In2InGlobal.presentation.admin
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string usrRole = Session["UserRole"].ToString();
            if (!IsPostBack)
            {
                FillUserDetails();

            }
        }

        private void FillUserDetails()
        {
            if (Session["UserRow"] != null)
            {
                DataRow usrDataRow = (DataRow)Session["UserRow"];
                username.Value = usrDataRow["FirstName"].ToString() + "  " + usrDataRow["LastName"].ToString();
                companyname.Value = usrDataRow["Company"].ToString();
                email.Value = usrDataRow["Email"].ToString();
                activityaccess.Value = usrDataRow["ActivityAccess"].ToString();
                role.Value = usrDataRow["Role"].ToString();
                status.Value = usrDataRow["Status"].ToString();
            }
        }
    }
}
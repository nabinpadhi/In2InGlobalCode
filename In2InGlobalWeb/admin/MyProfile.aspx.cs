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
                username.InnerText = usrDataRow["FirstName"].ToString() + "  " + usrDataRow["LastName"].ToString();
                companyname.InnerText = usrDataRow["Company"].ToString();
                email.InnerText = usrDataRow["Email"].ToString();
                activityaccess.InnerText = usrDataRow["ActivityAccess"].ToString();
                role.InnerText = usrDataRow["Role"].ToString();
                status.InnerText = usrDataRow["Status"].ToString();
            }
        }
    }
}
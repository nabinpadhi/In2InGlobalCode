using In2InGlobal.businesslogic;
using In2InGlobalBusinessEL;
using InGlobal.presentation;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    /// <summary>
    /// User Management
    /// </summary>
    public partial class UserManagement : System.Web.UI.Page
    {
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usrRole = Session["UserRole"].ToString();
                if (usrRole != "Admin")
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {
                    BindUsers();
                    BindCompany();
                    BindActivity();
                    BindRoles();
                }
            }

        }

        /// <summary>
        /// Bind User grid
        /// </summary>
        private void BindUsers()
        {
            UserMasterBL userMasterBL = new UserMasterBL();
            DataSet dsUser = new DataSet();
            dsUser = userMasterBL.FillUserGridInfo();
            grdUsers.DataSource = dsUser;
            grdUsers.DataBind();
        }

        /// <summary>
        /// Bind Company
        /// </summary>
        private void BindCompany()
        {
            DataSet dsUserDetails = new DataSet();
            UserMasterBL userMasterBL = new UserMasterBL();
            try
            {

                dsUserDetails = userMasterBL.getCompanyNameForUser();
                ddlCompanyName.DataSource = dsUserDetails;
                ddlCompanyName.DataTextField = "company_name";
                ddlCompanyName.DataValueField = "company_id";
                ddlCompanyName.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// Bind Roles
        /// </summary>
        private void BindRoles()
        {
            DataSet dsUserDetails = new DataSet();
            UserMasterBL userMasterBL = new UserMasterBL();
            try
            {
                dsUserDetails = userMasterBL.getRoleNameForUser();
                ddlRoleName.DataSource = dsUserDetails;
                ddlRoleName.DataTextField = "role_name";
                ddlRoleName.DataValueField = "role_id";
                ddlRoleName.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// grdUsers PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindUsers();
            grdUsers.PageIndex = e.NewPageIndex;
            grdUsers.DataBind();
        }
        private void BindActivity()
        {
            DataSet dsUserDetails = new DataSet();
            UserMasterBL userMasterBL = new UserMasterBL();
            try
            {
                dsUserDetails = userMasterBL.getActivityNameForUser();
                ddlActivityAccess.DataSource = dsUserDetails;
                ddlActivityAccess.DataTextField = "activity_name";
                ddlActivityAccess.DataValueField = "activity_id";
                ddlActivityAccess.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// grdUsers RowEditing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        /// <summary>
        /// grdUsers RowDeleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string email = grdUsers.DataKeys[e.RowIndex].Value.ToString();
            DeleteUser(email);
            BindUsers();
        }

        /// <summary>
        /// grdUsers RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string useremailid = grdUsers.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)grdUsers.Rows[e.RowIndex];

            TextBox textFirstName = (TextBox)row.Cells[0].Controls[0];
            TextBox textLastName = (TextBox)row.Cells[1].Controls[0];
            TextBox textCompany = (TextBox)row.Cells[2].Controls[0];
            TextBox textPhone = (TextBox)row.Cells[6].Controls[0];
            UpdateUser(textFirstName.Text, textLastName.Text, textCompany.Text, useremailid, textPhone.Text);
            grdUsers.EditIndex = -1;

            BindUsers();
        }

        /// <summary>
        /// Update  User
        /// </summary>
        /// <param name="Fname"></param>
        /// <param name="Lname"></param>
        /// <param name="company"></param>
        /// <param name="email"></param>
        private void UpdateUser(string Fname, string Lname, string company, string email, String Phone)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = Fname;
            userEntity.LastName = Lname;
            userEntity.Email = email;
            userEntity.CompanyName = company;
            userEntity.PhoneNumber = Phone;
            UserMasterBL userMasterBl = new UserMasterBL();
            userMasterBl.UpdateUser(userEntity);
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="email"></param>
        private void DeleteUser(string email)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.Email = email;

            UserMasterBL companyMasterBl = new UserMasterBL();
            companyMasterBl.DeleteUser(userEntity);
        }

        /// <summary>
        /// grdUsers RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsers.EditIndex = -1;
            BindUsers();
        }

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNewUser(object sender, EventArgs e)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = txtFName.Value;
            userEntity.LastName = txtLName.Value;
            userEntity.Email = txtEmail.Value;
            userEntity.PhoneNumber = txtPhoneNo.Value;
            userEntity.RoleId = 1; //ddRole.SelectedValue;
            userEntity.ActivityId = 1;// ddlActivityAccess.SelectedValue;
            userEntity.CompanyId = 1;//ddlCompanyName.SelectedValue; 
            userEntity.Password = new EncryptField().Encrypt(txtPassword.Value);
            userEntity.CreatedBy = "gsahoo2011@gmail.com";

            UserMasterBL companyMasterBl = new UserMasterBL();
            companyMasterBl.SaveUserMasterDetails(userEntity);

            BindUsers();
            ClearAll();
            string _message = "User Created Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);
        }

        /// <summary>
        /// Clear all data when cancel click
        /// </summary>
        private void ClearAll()
        {
            txtFName.Value = "";
            txtLName.Value = "";
            ddlCompanyName.SelectedIndex = 0;
            ddlActivityAccess.SelectedIndex = 0;
            ddlRoleName.SelectedIndex = 0;
            txtPhoneNo.Value = "";
            txtPassword.Value = "";
        }
    }
}
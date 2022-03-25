using In2InGlobal.businesslogic;
using In2InGlobalBusinessEL;
using InGlobal.presentation;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
                if (Session["UserRole"] != null)
                {
                    string usrRole = Session["UserRole"].ToString();
                    if (usrRole != "Admin")
                    {
                        Response.Redirect("Login.aspx");

                    }
                    else
                    {
                        try
                        {
                            BindUsers();
                            BindCompany();
                            BindRoles();
                            BindActivity();
                        }
                        catch (Exception ex)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
                        }
                    }
                }
                else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);
            }
        }

        /// <summary>
        /// Bind User grid
        /// </summary>
        private void BindUsers()
        {
            try
            {
                UserMasterBL userMasterBL = new UserMasterBL();
                DataSet dsUser = new DataSet();
                dsUser = userMasterBL.FillUserGridInfo();
                grdUsers.DataSource = dsUser;
                grdUsers.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// Bind Company
        /// </summary>
        private void BindCompany()
        {
            DataSet dsCompanies = new DataSet();
            try
            {

                dsCompanies = (DataSet)Session["dsCompanies"];
                ddlCompanyName.DataSource = dsCompanies;
                ddlCompanyName.DataTextField = "company_name";
                ddlCompanyName.DataValueField = "company_id";
                ddlCompanyName.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }
        private DataSet GetCompany()
        {
            DataSet dsCompanies = new DataSet();
            UserMasterBL userMasterBL = new UserMasterBL();
            try
            {

                dsCompanies = userMasterBL.getCompanyNameForUser();
                Session["dsCompanies"] = dsCompanies;

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
            return dsCompanies;
        }
        private DataSet GetActivityAccess()
        {
            DataSet dsActivityAccess = new DataSet();
            UserMasterBL userMasterBL = new UserMasterBL();
            try
            {
                dsActivityAccess = userMasterBL.getActivityNameForUser();
                Session["dsActivityAccess"] = dsActivityAccess;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
            return dsActivityAccess;
        }
        private DataSet GetRoles()
        {
            DataSet dsRoles = new DataSet();
            UserMasterBL userMasterBL = new UserMasterBL();
            try
            {
                dsRoles = userMasterBL.getRoleNameForUser();
                Session["dsRoles"] = dsRoles;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
            return dsRoles;
        }

        /// <summary>
        /// Bind Roles
        /// </summary>
        private void BindRoles()
        {
            DataSet dsRoles = new DataSet();
            try
            {
                dsRoles = (DataSet)Session["dsRoles"];
                ddlRoleName.DataSource = dsRoles;
                ddlRoleName.DataTextField = "role_name";
                ddlRoleName.DataValueField = "role_id";
                ddlRoleName.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
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


            try
            {
                DataSet dsUserDetails = (DataSet)Session["dsActivityAccess"];
                ddlActivityAccess.DataSource = dsUserDetails;
                ddlActivityAccess.DataTextField = "activity_name";
                ddlActivityAccess.DataValueField = "activity_id";
                ddlActivityAccess.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
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
        /// Update  User
        /// </summary>
        /// <param name="Fname"></param>
        /// <param name="Lname"></param>
        /// <param name="company"></param>
        /// <param name="email"></param>
        private void UpdateUser(UserEntity userEntity)
        {
            try
            {
                UserMasterBL userMasterBl = new UserMasterBL();
                userMasterBl.UpdateUser(userEntity);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="email"></param>
        private void DeleteUser(string email)
        {
            DataSet userDs = new DataSet();
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.Email = email;

                UserMasterBL companyMasterBl = new UserMasterBL();
                companyMasterBl.DeleteUser(userEntity);
                BindUsers();
                string _message = "User Deleted Successfully";
                hdnUserEmail.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }


        /// <summary>
        /// Get Project For User
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private DataSet GetProjectForUser(string email)
        {
            DataSet dsUsers = new DataSet();
            UserEntity userEntity = new UserEntity();
            userEntity.Email = email;

            UserMasterBL companyMasterBl = new UserMasterBL();

            dsUsers = companyMasterBl.GetProjectForUser(userEntity);

            return dsUsers;
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

        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataTable dtCompany = GetCompany().Tables[0];
                    DataTable dtRole = GetRoles().Tables[0];
                    DataTable dtActivity = GetActivityAccess().Tables[0];

                    string fname = e.Row.Cells[0].Text;
                    string lname = e.Row.Cells[1].Text;
                    string companyid = dtCompany.Select("company_name = '" + e.Row.Cells[2].Text + "'")[0]["company_id"].ToString();
                    string email = grdUsers.DataKeys[e.Row.RowIndex].Value.ToString();
                    string roleid = dtRole.Select("role_name = '" + e.Row.Cells[4].Text + "'")[0]["role_id"].ToString(); //e.Row.Cells[4].Text; ;
                    string activityid = dtActivity.Select("activity_name = '" + e.Row.Cells[5].Text + "'")[0]["activity_id"].ToString(); //e.Row.Cells[5].Text; ;                


                    foreach (Button editbutton in e.Row.Cells[6].Controls.OfType<Button>())
                    {
                        if (email == Session["UserEmail"].ToString())
                        {
                            editbutton.Enabled = false;
                        }
                        else
                        {
                            editbutton.UseSubmitBehavior = false;
                            editbutton.Attributes["onclick"] = "return PullDataToEdit('" + fname + "', '" + lname + "', '" + companyid + "', '" + email + "', '" + roleid + "', '" + activityid + "'); ";
                        }

                    }
                    foreach (Button delbutton in e.Row.Cells[7].Controls.OfType<Button>())
                    {

                        if (email == Session["UserEmail"].ToString())
                        {
                            delbutton.Enabled = false;
                        }
                        else
                        {
                            delbutton.UseSubmitBehavior = false;
                            delbutton.Attributes["onclick"] = "javascript:In2InGlobalConfirm('" + email + "');return false;";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNewUser(object sender, EventArgs e)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = txtFName.Value;
                userEntity.LastName = txtLName.Value;
                userEntity.Email = txtEmail.Value;
                userEntity.PhoneNumber = "";
                userEntity.RoleId = Convert.ToInt64(ddlRoleName.SelectedValue);
                userEntity.ActivityId = Convert.ToInt64(ddlActivityAccess.SelectedValue);
                userEntity.CompanyId = Convert.ToInt64(ddlCompanyName.SelectedValue);
                userEntity.CreatedBy = Session["UserEmail"].ToString();//"gsahoo2011@gmail.com";
                string _message = "";
                if (hdnUserEmail.Value == "")
                {
                    userEntity.Password = new EncryptField().Encrypt(txtPassword.Value);
                    UserMasterBL companyMasterBl = new UserMasterBL();
                    companyMasterBl.SaveUserMasterDetails(userEntity);
                    BindUsers();
                    ClearAll();
                    _message = "User Created Successfully";
                }
                else
                {
                    userEntity.CompanyName = ddlCompanyName.SelectedItem.Text;
                    UpdateUser(userEntity);
                    BindUsers();
                    ClearAll();
                    hdnUserEmail.Value = "";
                    _message = "User Updated Successfully";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.ShowException();", true);
            }
        }

        /// <summary>
        /// Clear all data when cancel click
        /// </summary>
        private void ClearAll()
        {
            txtFName.Value = "";
            txtLName.Value = "";
            txtEmail.Value = "";
            ddlCompanyName.SelectedIndex = 0;
            ddlActivityAccess.SelectedIndex = 0;
            ddlRoleName.SelectedIndex = 0;

            txtPassword.Value = "";
        }
        protected void hdnDelBtn_Click(object sender, EventArgs e)
        {
            DataSet userDs = new DataSet();
            try
            {
                if (hdnUserEmail.Value != "")
                {
                    userDs = GetProjectForUser(hdnUserEmail.Value);

                    if (userDs.Tables[0].Rows.Count > 0)
                    {
                        string _message = "User delete failed.User mapped to a project";
                        hdnUserEmail.Value = "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);
                    }
                    else
                    {
                        DeleteUser(hdnUserEmail.Value);                      
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
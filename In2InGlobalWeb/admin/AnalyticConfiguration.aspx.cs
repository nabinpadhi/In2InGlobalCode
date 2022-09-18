using In2InGlobal.businesslogic;
using In2InGlobalBL;
using In2InGlobalBusinessEL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class AnalyticConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] != null)
            {
                if (!IsPostBack)
                {
                    BindCompany();
                    BindDashboardGrid();
                }
            }
            else
                Page. ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);


        }

        private void BindCompany()
        {
            DataSet dsCompany = new DataSet();
            AnalyticsBL analyticsBL = new AnalyticsBL();
            dsCompany = analyticsBL.getCompanyName();
            ddlCompany.Items.Clear();

            ddlCompany.DataTextField = "company_name";
            ddlCompany.DataValueField = "company_id";
            ddlCompany.DataSource = dsCompany.Tables[0];
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select a Company--");

            ddlCompanyProcess.Items.Clear();

            ddlCompanyProcess.DataTextField = "company_name";
            ddlCompanyProcess.DataValueField = "company_id";
            ddlCompanyProcess.DataSource = dsCompany.Tables[0];
            ddlCompanyProcess.DataBind();
            ddlCompanyProcess.Items.Insert(0, "--Select a Company--");
        }

        private void BindUsers(long companyID,string ForTab)
        {
            try
            {
                DataSet dsUsers = new DataSet();
                AnalyticsBL userBL = new AnalyticsBL();
                dsUsers = userBL.getUserEmailByCompany(companyID);
                if (ForTab == "AnaConf")
                {
                    ddlUser.Items.Clear();
                    ddlUser.DataTextField = "user_email";
                    ddlUser.DataValueField = "user_id";
                    ddlUser.DataSource = dsUsers.Tables[0];
                    ddlUser.DataBind();
                    ddlUser.Items.Insert(0, "--Select a User--");
                }
                else if (ForTab == "ProConf")
                {
                    ddlUserProcess.Items.Clear();
                    ddlUserProcess.DataTextField = "user_email";
                    ddlUserProcess.DataValueField = "user_id";
                    ddlUserProcess.DataSource = dsUsers.Tables[0];
                    ddlUserProcess.DataBind();
                    ddlUserProcess.Items.Insert(0, "--Select a User--");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        private void BindProjects(string userEmail, string ForTab)
        {
            int companyId = 0;
            try
            {
                DataSet dsUsersPrject = new DataSet();
                AnalyticsBL projectBL = new AnalyticsBL();
                dsUsersPrject = projectBL.getProjectNameByUserEmail(companyId, userEmail);
                if (ForTab == "AnaConf")
                {
                    ddlProject.DataTextField = "project_name";
                    ddlProject.DataValueField = "project_id";
                    ddlProject.Items.Clear();
                    ddlProject.DataSource = dsUsersPrject.Tables[0];
                    ddlProject.DataBind();
                    ddlProject.Items.Insert(0, "--Select a Project--");
                }
                else if (ForTab == "ProConf")
                {
                    ddlProjectProcess.DataTextField = "project_name";
                    ddlProjectProcess.DataValueField = "project_id";
                    ddlProjectProcess.Items.Clear();
                    ddlProjectProcess.DataSource = dsUsersPrject.Tables[0];
                    ddlProjectProcess.DataBind();
                    ddlProjectProcess.Items.Insert(0, "--Select a Project--");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void BindDashboardGrid()
        {
            DataSet dsUsersPrject = new DataSet();
            AnalyticsBL projectBL = new AnalyticsBL();
            dsUsersPrject = projectBL.getAnalyticsGridDetails();
            grdAnalyticsLink.DataSource = dsUsersPrject.Tables[0];
            grdAnalyticsLink.DataBind();
        }
        protected void grdAnalyticsLink_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdAnalyticsLink.PageIndex = e.NewPageIndex;
            BindDashboardGrid();
            grdAnalyticsLink.DataBind();

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUsers(Convert.ToInt64(ddlCompany.SelectedValue), "AnaConf");
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowAnaConf();", true);
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjects(ddlUser.SelectedItem.Text, "AnaConf");
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowAnaConf();", true);
        }
        protected void ddlCompanyProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUsers(Convert.ToInt64(ddlCompanyProcess.SelectedValue),"ProConf");
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowProConf();", true);
        }

        protected void ddlUserProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjects(ddlUserProcess.SelectedItem.Text, "ProConf");
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), "ShowProConf();", true);
        }
        //btnUpdateProcess_Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AnalyticsEntity analyticsEntity = new AnalyticsEntity();
            string _message = "Analytics Configuration Saved Successfully";
            try
            {
                analyticsEntity.DashboardUrl = txtlink.Value;

                AnalyticsBL analyticsBl = new AnalyticsBL();

                if (hdnDBID.Value != "")
                {
                    _message = "Analytics Configuration Updated Successfully";
                    analyticsEntity.Id = Convert.ToInt64(hdnDBID.Value);
                    analyticsBl.UpdateAnalyticConfiguration(analyticsEntity);
                }
                else
                {
                    analyticsEntity.ProjectId = Convert.ToInt32(ddlProject.SelectedValue);
                    analyticsEntity.UserId = Convert.ToInt32(ddlUser.SelectedValue);
                    analyticsEntity.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    analyticsEntity.CreatedBy = Session["UserEmail"].ToString();
                    analyticsBl.SaveAnalyticConfiguration(analyticsEntity);
                }
                txtlink.Value = "";
                ddlUser.SelectedIndex = 0;
                ddlProject.SelectedIndex = 0;
                ddlCompany.SelectedIndex = 0;
                hdnDBID.Value = "";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            BindDashboardGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowAnaConf(); ", _message), true);
        }

        private void DeleteAnalyticConfiguration(int _id)
        {
            AnalyticsEntity analyticsEntity = new AnalyticsEntity();

            if (_id != 0)
            {
                try
                {

                    analyticsEntity.Id = Convert.ToInt64(_id);
                    AnalyticsBL analyticsBL = new AnalyticsBL();
                    analyticsBL.DeleteAnalyticConfiguration(analyticsEntity);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        protected void grdAnalyticsLink_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string dashboardID = grdAnalyticsLink.DataKeys[e.Row.RowIndex].Value.ToString();

                    string project = e.Row.Cells[5].Text;
                    string user = e.Row.Cells[4].Text;
                    string company = e.Row.Cells[3].Text;

                    string link = e.Row.Cells[6].Text;

                    foreach (Button editbutton in e.Row.Cells[7].Controls.OfType<Button>())
                    {

                        editbutton.UseSubmitBehavior = false;
                        editbutton.Attributes["onclick"] = "return PullDataToEdit('" + link + "','" + dashboardID + "','"+user+"','"+company+"','"+project+"');";

                    }
                    foreach (Button delbutton in e.Row.Cells[8].Controls.OfType<Button>())
                    {

                        delbutton.UseSubmitBehavior = false;
                        delbutton.Attributes["onclick"] = "javascript:In2InGlobalConfirm('" + dashboardID + "');return false;";

                    }
                    string ancLink = " onclick = \"window.parent.ShowZohoAnalytics('" + link + "');\"";

                    e.Row.Cells[6].Text = "<a href='#' "+ancLink+">" + link + "</a>";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void hdnDelBtn_Click(object sender, EventArgs e)
        {
            DeleteAnalyticConfiguration(Convert.ToInt32(hdnDBID.Value));
            string _message = "Analytics Configuration Deleted Successfully.";
            BindDashboardGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}'); ", _message), true);
        }

        protected void btnUpdateProcess_Click(object sender, EventArgs e)
        {
            
            string _message = "Processed successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("D"), string.Format("ShowServerMessage('{0}');ShowProConf(); ", _message), true);
           // ScriptManager.RegisterStartupScript(analyticconfigurationscriptmanager, analyticconfigurationscriptmanager.GetType(), "ShowServerMessage", string.Format("ShowServerMessage('{0}');ShowProConf();", _message), true);
        }
    }
}
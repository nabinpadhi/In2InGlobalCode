using In2InGlobal.businesslogic;
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
    public partial class CompanyManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] != null)
            {
                if (!IsPostBack)
                {
                    string usrRole = Session["UserRole"].ToString();
                    if (usrRole != "Admin")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);

                    }
                    else
                    {
                        BindCompany();
                        BindLOB();
                    }
                }
                else
                {
                    if (Request.Form["__EVENTTARGET"] != null)
                    {
                        if (Request.Form["__EVENTTARGET"].ToString().IndexOf("grdCompany") == 0)
                        {
                            int extraComa = Request.Form["__EVENTTARGET"].ToString().Replace("grdCompany", "").Length;
                            // Fire event
                            DeleteCompany(Request.Form["__EVENTARGUMENT"].Substring(0, Request.Form["__EVENTARGUMENT"].Length - extraComa));
                            BindCompany();
                            string _message = "Company deleted Successfully";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);
                        }
                    }
                }
            }
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);


        }
        private void BindCompany()
        {
            DataSet dsCompanyDetails = new DataSet();
            CompanyMasterBL objCompanyBL = new CompanyMasterBL();

            try
            {
                dsCompanyDetails = objCompanyBL.getCompanyDetails();
                grdCompany.DataSource = dsCompanyDetails;
                grdCompany.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
       
        protected void grdCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                string companyid = grdCompany.DataKeys[e.Row.RowIndex].Value.ToString();
                string companyname = e.Row.Cells[1].Text;
                string lob = e.Row.Cells[2].Text;

                foreach (LinkButton button in e.Row.Cells[3].Controls.OfType<LinkButton>())
                {

                    if (button.ID == "lnkDel")
                    {
                        if (companyname == Session["CompanyName"].ToString())
                        {
                            button.Enabled = false;
                            button.Visible = false;
                        }
                        else
                        {
                            button.OnClientClick = "In2InGlobalConfirm('" + companyid + "');";
                        }
                    }
                    if (button.ID == "lnkEdit")
                    {
                        button.OnClientClick = "PullDataToEdit('"+companyid + "','" + companyname + "','" + lob + "'); ";
                        button.Attributes["href"] = "#";
                    }
                }
            }

        }

        protected void grdCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindCompany();
            grdCompany.PageIndex = e.NewPageIndex;
            grdCompany.DataBind();


        }
        
        private void BindLOB()
        {
            DataTable dtLOB = GetLOB();
            ddlLOB.DataSource = dtLOB;
            ddlLOB.DataTextField = "lob_name";
            ddlLOB.DataValueField = "lob_name";
            ddlLOB.DataBind();
        }
        private DataTable GetLOB()
        {
            DataTable dtLOB = new DataTable();
            dtLOB.Columns.Add("lob_id"); dtLOB.Columns.Add("lob_name");
            dtLOB.Rows.Add("1", "Software"); dtLOB.Rows.Add("2", "Marketing"); dtLOB.Rows.Add("3", "Manufacturing");
            dtLOB.Rows.Add("4", "Hospitality"); dtLOB.Rows.Add("5", "Realestate");
            Session["dtLOB"]=dtLOB;
            return dtLOB;            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            string _message = "Company Created Successfully";
            try
            {
                companyEntity.CompanyName = txtCompanyName.Value;
                companyEntity.LOB = ddlLOB.SelectedItem.Text;
                companyEntity.CreatedBy = Session["UserEmail"].ToString();
                
                CompanyMasterBL companyMasterBl = new CompanyMasterBL();
                
                if (hdnCompanyID.Value != "")
                {
                    _message = "Company Updated Successfully";
                    companyEntity.CompanyId = Convert.ToInt64(hdnCompanyID.Value);
                    companyMasterBl.UpdateCompany(companyEntity);
                }
                else
                {
                    companyMasterBl.SaveCompanyMaster(companyEntity);
                }
               
                BindCompany();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }         
          
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);

        }

        protected void grdCompany_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCompany.EditIndex = e.NewEditIndex;
            BindCompany();
        }

        protected void grdCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string companyID = grdCompany.DataKeys[e.RowIndex].Value.ToString();
            DeleteCompany(companyID);
            BindCompany();
        }

        private void DeleteCompany(string companyID)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            
            if (companyID != null)
            {
                try
                {
                    companyEntity.CompanyId = Convert.ToInt32(companyID);
                    CompanyMasterBL companyMasterBl = new CompanyMasterBL();
                    companyMasterBl.DeleteCompany(companyEntity);
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
            }            
        }

        protected void grdCompany_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string companyID = grdCompany.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)grdCompany.Rows[e.RowIndex];
            TextBox textCompanyName = (TextBox)row.Cells[1].Controls[0];
            TextBox textLOB = (TextBox)row.Cells[2].Controls[0];            
            UpdateCompany(textCompanyName.Text, textLOB.Text, companyID);
            grdCompany.EditIndex = -1;

            BindCompany();
        }

        protected void UpdateCompany(string companyname, string lob, string companyID)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            if(companyID!=null)
            {
                companyEntity.CompanyName = companyname;
                companyEntity.LOB = lob;
                companyEntity.CompanyId = Convert.ToInt32(companyID);

                CompanyMasterBL companyMasterBl = new CompanyMasterBL();
                companyMasterBl.UpdateCompany(companyEntity);
            }           
        }

        protected void grdCompany_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCompany.EditIndex = -1;
            BindCompany();
        }
    }
}
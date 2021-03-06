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
                    //if (usrRole != "Admin")
                    //{
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", "window.parent.location='login.aspx';", true);

                    //}
                    //else
                    //{
                        BindCompany();
                        BindLOB();
                    //}
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

               
                foreach (Button editbutton in e.Row.Cells[3].Controls.OfType<Button>())
                {
                    if (companyname == Session["CompanyName"].ToString())
                    {
                        editbutton.Enabled = false;

                    }
                    else
                    {
                        editbutton.UseSubmitBehavior = false;
                        editbutton.Attributes["onclick"] = "return PullDataToEdit('" + companyid + "','" + companyname + "','" + lob + "');";
                    }
                }
                foreach (Button delbutton in e.Row.Cells[4].Controls.OfType<Button>())
                {
                    if (companyname == Session["CompanyName"].ToString())
                    {
                        delbutton.Enabled = false;
                        
                    }
                    else
                    {
                        delbutton.UseSubmitBehavior = false;
                        delbutton.Attributes["onclick"] = "javascript:In2InGlobalConfirm('" + companyname + "','" + companyid + "');return false;";
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
            dtLOB.Rows.Add("1", "Automobiles");
            dtLOB.Rows.Add("2", "Aviation");
            dtLOB.Rows.Add("3", "Finance");
            dtLOB.Rows.Add("4", "Chemical");
            dtLOB.Rows.Add("5", "Electricity");
            dtLOB.Rows.Add("6", "Education");
            dtLOB.Rows.Add("7", "Food & Beverages");
            dtLOB.Rows.Add("8", "Transportation");
            dtLOB.Rows.Add("9", "Logistics");
            dtLOB.Rows.Add("10", "Information Technology");
            dtLOB.Rows.Add("11", "Medical");
            dtLOB.Rows.Add("12", "Mining");
            dtLOB.Rows.Add("13", "Pharmaceutical");
            dtLOB.Rows.Add("14", "Rent & Leasing");
            dtLOB.Rows.Add("15", "Supermarket");
            dtLOB.Rows.Add("16", "Steel");
            dtLOB.Rows.Add("17", "Telecom");
            dtLOB.Rows.Add("18", "Textile");
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
                txtCompanyName.Value = "";
                ddlLOB.SelectedIndex = 0;
                hdnCompanyID.Value = "";
                BindCompany();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }         
          
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}'); ", _message), true);

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


        private DataSet GetUserForCompany(string companyID) 
        {
            DataSet dsUsers = new DataSet();
            CompanyMasterBL objCompanyBL = new CompanyMasterBL();
            long companyid= Convert.ToInt32(companyID);

            if (companyID != null)
            {
                try
                {
                    dsUsers = objCompanyBL.GetUsers(companyid);
                    
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }               
            }
            return dsUsers;
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
        protected void hdnDelBtn_Click(object sender, EventArgs e)
        {
            DataSet userDs = new DataSet();
           
            if (hdnCompanyID.Value != "")
            {
                userDs = GetUserForCompany(hdnCompanyID.Value);
                if (userDs.Tables[0].Rows.Count > 0)
                {
                    hdnCName.Value = "";
                    hdnCompanyID.Value = "";
                    string _message = "Delete failed.User exists for the Company";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}')", _message), true);
                }
                else
                {
                    DeleteCompany(hdnCompanyID.Value);
                    BindCompany();
                    hdnCName.Value = "";
                    hdnCompanyID.Value = "";
                    string _message = "Company deleted successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), string.Format("ShowServerMessage('{0}')", _message), true);
                }
            }
          

        }
    }
    
}
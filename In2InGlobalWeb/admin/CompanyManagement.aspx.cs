using In2InGlobal.businesslogic;
using In2InGlobalBusinessEL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
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
            if (!IsPostBack)
            {
                string usrRole = Session["UserRole"].ToString();
                if (usrRole != "Admin")
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {
                    BindCompany();
                }
            }

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
            GridViewRowEventArgs ea = e as GridViewRowEventArgs;
            if (ea.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = ea.Row.DataItem as DataRowView;
                Object ob = drv["company_phone"];
                if (!Convert.IsDBNull(ob))
                {
                    TableCell cell2 = ea.Row.Cells[3];
                    if (cell2.Text.Length > 1)
                    {
                        cell2.Text = "<img src='assets/img/mobile.png' style='width:25px;height:20px;'></span>" + " " + cell2.Text;
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
        [WebMethod(EnableSession = true)]
        public static string AddNewCompany(string companyname, string lob, string phoneno)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            try
            {
                companyEntity.CompanyName = companyname;
                companyEntity.LOB = lob;
                companyEntity.CompanyPhone = phoneno;
                companyEntity.CreatedBy = "Admin";
                companyEntity.CompanyAddress = "Bangalore";
                CompanyMasterBL companyMasterBl = new CompanyMasterBL();
                companyMasterBl.SaveCompanyMaster(companyEntity);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }       
            return "Success";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            try
            {
                companyEntity.CompanyName = txtCompanyName.Value;
                companyEntity.LOB = txtLOB.Value;
                companyEntity.CompanyPhone = txtPhoneNo.Value;
                companyEntity.CreatedBy = "Admin";
                companyEntity.CompanyAddress = "Bangalore";

                CompanyMasterBL companyMasterBl = new CompanyMasterBL();

                companyMasterBl.SaveCompanyMaster(companyEntity);
                BindCompany();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }         
          
            string _message = "Company Created Successfully";
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
            TextBox textPhoneNo = (TextBox)row.Cells[3].Controls[0];
            UpdateCompany(textCompanyName.Text, textLOB.Text, textPhoneNo.Text, companyID);
            grdCompany.EditIndex = -1;

            BindCompany();
        }

        protected void UpdateCompany(string companyname, string email, string phoneno, string companyID)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            if(companyID!=null)
            {
                companyEntity.CompanyName = companyname;
                companyEntity.LOB = email;
                companyEntity.CompanyPhone = phoneno;
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
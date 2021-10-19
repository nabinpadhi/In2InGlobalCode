using InGlobal.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trirand.Web.UI.WebControls;

namespace InGlobal.presentation.User.tpm
{
    public partial class UploadDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Common.HasSession())
            {                
                hdnSessionState.Value = "alive";
            }
            else
            {
                
                hdnSessionState.Value = "dead";

            }
            
        }
        protected void jqProjectDocuments_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                 
                }
                else
                    hdnSessionState.Value = "dead";
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }

        protected void jqProjectDocuments_RowDeleting(object sender, JQGridRowDeleteEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    try
                    {
                       
                    }
                    catch (Exception exfailed)
                    {
                        Session["so_Exp"] = exfailed;
                        Response.AddHeader("DeleteFailed", "Error while deleting selected task(s).");
                    }
                }
                else
                {
                    Response.AddHeader("ErrorMsg", "No Session");
                    hdnSessionState.Value = "dead";
                }
            }

            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }

    }
    
}
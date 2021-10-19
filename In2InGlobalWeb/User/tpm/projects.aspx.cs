using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trirand.Web.UI.WebControls;
using InGlobal.BusinessLogic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.Configuration;
using System.Net;
namespace InGlobal.presentation.User.tpm
{
    public partial class projects : System.Web.UI.Page
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
        #region "jqGrid Events"
        protected void jqGridTPMProjects_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            try
            {                
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    BindTPMProjects();

                }
                else
                {
                    hdnSessionState.Value = "dead";
                }
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "Error while loading project details.");
            }
        }

        protected void jqGridTPMProjects_CellBinding(object sender, JQGridCellBindEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    if (e.ColumnIndex == 7)
                    {

                        DateTime _startDate = Convert.ToDateTime(e.RowValues.GetValue(4).ToString());
                        DateTime _endDate = Convert.ToDateTime("01/01/1900");
                        if (Convert.ToString(e.RowValues.GetValue(5)) != "")
                        {
                            if (Convert.ToDateTime(e.RowValues.GetValue(5).ToString()).ToShortDateString() == "01/01/1753" || Convert.ToDateTime(e.RowValues.GetValue(5).ToString()).ToShortDateString() == "01/01/1900")
                            {
                                _endDate = Convert.ToDateTime("01/01/1900");
                            }
                            else
                            {
                                _endDate = Convert.ToDateTime(e.RowValues.GetValue(5).ToString());
                            }
                        }
                        DateTime _targetDate = Convert.ToDateTime(e.RowValues.GetValue(6).ToString());
                        string _cellValue = "<div style='width:100%;height:100%;background-color:@bgVal;'>&nbsp;</div>";
                        if (_startDate > DateTime.Today || _endDate.ToShortDateString() == "01/01/1900")
                        {
                            _cellValue = _cellValue.Replace("@bgVal","Orange");
                        }
                        else if (_endDate.ToShortDateString() != "01/01/1900" && _endDate > _targetDate)
                        {
                            _cellValue = _cellValue.Replace("@bgVal","Red");
                        }
                        else if (_endDate.ToShortDateString() != "01/01/1900" && _endDate <= _targetDate)
                        {
                            _cellValue = _cellValue.Replace("@bgVal", "Green");
                        }
                        else if (_endDate.ToShortDateString() != "01/01/1900" && _targetDate <= DateTime.Today)
                        {
                            _cellValue = _cellValue.Replace("@bgVal", "Red");
                        }
                        e.CellHtml = _cellValue;
                    }
                    if (e.ColumnIndex == 5)
                    {
                        if (Convert.ToDateTime(e.RowValues.GetValue(5).ToString()).ToShortDateString() == "01/01/1753" || Convert.ToDateTime(e.RowValues.GetValue(5).ToString()).ToShortDateString() == "01/01/1900")
                        {
                            e.CellHtml = "";
                        }
                    }
                    if(e.ColumnIndex==9)
                    {
                        if (Convert.ToBoolean(e.RowValues.GetValue(8).ToString()))
                        {
                            e.CellHtml = "<a href='#' data-projectID='" + e.RowKey + "' class='DocumentDownload'>Download</a></br><a href='#' data-projectID='" + e.RowKey + "' class='UploadDocument'>Upload</a>";
                        }
                        else
                        {
                            e.CellHtml = "<a href='#' data-projectID='" + e.RowKey + "' class='UploadDocument'>Upload</a>";
                        }

                    }
                }
                else
                {
                    hdnSessionState.Value = "dead";
                    Response.AddHeader("ErrorMsg", "No Session");
                }
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }
        protected void jqGridTPMProjects_RowEditing(object sender, JQGridRowEditEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    DataSet _TPMProjects = (DataSet)Session["TPMProject"];
                    if (Convert.ToString(_TPMProjects.Tables[0].Select("ID=" + e.RowKey)[0][e.RowData.GetKey(0)]) != e.RowData.Get(0))
                    {
                        TPMProjectBL _tpmProject = new TPMProjectBL();
                        _tpmProject.pi_ID = Convert.ToInt32(e.RowKey);
                        _tpmProject.ps_ColumnName = e.RowData.GetKey(0);
                        _tpmProject.ps_NewValue = e.RowData.Get(0);
                        XSSValidator validatorObject = new XSSValidator(string.Empty);
                        if (ValidateProjectEdit(_tpmProject, ref validatorObject))
                        {
                            _tpmProject.UpdateProject();
                        }
                        else {
                            string ErrMsg = "";
                           
                            foreach (string msg in validatorObject.pa_ErrorMessage)
                            {
                                ErrMsg += "<li>" + msg +"</li>";

                            }
                            ErrMsg = "<ul>" + ErrMsg + "</ul>";
                            Response.AddHeader("ErrorMsg",ErrMsg);
                        }
                    }
                    
                   // Session["ResponseMessage"] = "Project with same name already exist";
                }
                else
                {
                    hdnSessionState.Value = "dead";
                    Response.AddHeader("ErrorMsg", "SessionDead");
                }
                jqGridTPMProjects.DataBind();
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;                
                Response.AddHeader("ErrorMsg", "Exception");
            }

        }

        protected void jqGridTPMProjects_RowDeleting(object sender, JQGridRowDeleteEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    try
                    {
                        if (DeleteProject(e.RowKey) > 0)
                        {
                            Response.AddHeader("DeleteSuccess", "Project deleted successfully.");
                        }
                    }
                    catch (Exception exfailed)
                    {
                        Session["so_Exp"] = exfailed;      
                        Response.AddHeader("DeleteFailed", "Error while deleting select project(s).");
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

        protected void jqTeamMember_CellBinding(object sender, JQGridCellBindEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    if (e.ColumnIndex == 4)
                    {
                        string RoleValue="";
                        switch (Convert.ToString(e.RowValues.GetValue(3)))
                        {
                            case "JRDEV":
                                RoleValue = "Jr. Developer";
                                break;
                            case "SRDEV":
                                RoleValue = "Sr. Developer";
                                break;
                            case "TPM":
                                RoleValue = "Tech. Project Manager";
                                break;
                            case "PM":
                                RoleValue = "Project Manager";
                                break;
                            case "PL":
                                RoleValue = "Project Lead";
                                break;
                            case "TL":
                                RoleValue = "Team Lead";
                                break;
                            case "ML":
                                RoleValue = "Module Lead";
                                break;
                            case "OTM":
                                RoleValue = "Other Team Member";
                                break;
                        }

                        e.CellHtml = RoleValue;
                    }
                    hdnSessionState.Value = "alive";
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
        protected void jqGridTPMProjects_RowAdding(object sender, JQGridRowAddEventArgs e)
        {
           
             try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    TPMProjectBL _tpmProjectBLObject = new TPMProjectBL();

                    _tpmProjectBLObject.ps_Name = e.RowData["Name"];
                    _tpmProjectBLObject.ps_ClientName = e.RowData["ClientName"];
                    _tpmProjectBLObject.pi_TeamSize = Convert.ToInt16(e.RowData["TeamSize"]);
                    _tpmProjectBLObject.pd_StartDate = Convert.ToDateTime(e.RowData["StartDate"]);
                    if (e.RowData["EndDate"] == "")
                          _tpmProjectBLObject.pd_EndDate = Convert.ToDateTime("01/01/1111");
                    else
                        _tpmProjectBLObject.pd_EndDate = Convert.ToDateTime(e.RowData["EndDate"]);
                    _tpmProjectBLObject.pd_TargetEndDate = Convert.ToDateTime(e.RowData["TargetEndDate"]);
                    _tpmProjectBLObject.pi_UserID = (int)HttpContext.Current.Session["UserID"];
                    XSSValidator validatorObject = new XSSValidator(string.Empty);
                    validatorObject.pa_ExculdeException = new System.Collections.ArrayList();
                    validatorObject.pa_ExculdeException.Add("?");
                    validatorObject.pa_ExculdeException.Add(":");
                    validatorObject.pa_ExculdeException.Add("/");

                    if (validatorObject.StartValidate(_tpmProjectBLObject))
                    {
                        if (e.RowData["EndDate"] != "")
                        {
                            if (_tpmProjectBLObject.pd_StartDate >= _tpmProjectBLObject.pd_EndDate)
                            {
                                Response.AddHeader("ErrorMsg", "Project end date cannot be faster to project start date.");
                                //jqGridTPMProjects.

                            }
                            else
                            {
                                _tpmProjectBLObject.AddProject();
                                jqGridTPMProjects.DataBind();
                            }
                        }
                        else
                        {
                            _tpmProjectBLObject.AddProject();
                            jqGridTPMProjects.DataBind();
                        }
                        
                    }
                    else
                    {
                       
                        Response.AddHeader("ErrorMsg", "Err_296");
                    }

                }
                else
                {
                    hdnSessionState.Value = "alive";
                    Response.AddHeader("ErrorMsg", "No Session");
                }
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }
        protected void jqTeamMember_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    int _projectID = Convert.ToInt32(e.ParentRowKey);
                    BindProjectMembers(_projectID);
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
        protected void jqTeamMember_RowEditing(object sender, JQGridRowEditEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    DataSet _TPMProjects = (DataSet)Session["TPMProject"];
                    if (_TPMProjects.Tables[1].Select("UserID=" + e.RowKey)[0][e.RowData.GetKey(0)] != e.RowData.Get(0))
                    {
                        TeamMemberBL _teamMember = new TeamMemberBL();
                        _teamMember.pi_ID = Convert.ToInt32(e.RowKey);
                        _teamMember.ps_ColumnName = e.RowData.GetKey(0);
                        _teamMember.ps_NewValue = e.RowData.Get(0);
                        XSSValidator validatorObject = new XSSValidator(string.Empty);
                        if (ValidateTeamMemberEdit(_teamMember, ref validatorObject))
                        {
                            _teamMember.UpdateTeamMember();
                        }
                        else
                        {
                            Response.AddHeader("ErrorMsg", validatorObject.pa_ErrorMessage.ToString());
                        }
                    }

                    // Session["ResponseMessage"] = "Project with same name already exist";
                }
                else
                {
                    hdnSessionState.Value = "dead";
                    Response.AddHeader("ErrorMsg", "SessionDead");
                }
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Response.AddHeader("ErrorMsg", "Exception");
            }

        }

        private bool ValidateTeamMemberEdit(TeamMemberBL _teamMember, ref XSSValidator _validatorObject)
        {
            bool _result = true;

            _validatorObject.pa_ExculdeException = new System.Collections.ArrayList();
            _validatorObject.pa_ExculdeException.Add("?");
            _validatorObject.pa_ExculdeException.Add(":");
            _validatorObject.pa_ExculdeException.Add("/");
            _validatorObject.pa_ExculdeException.Add("-");
            _result = _validatorObject.StartValidate(_teamMember);
           
            return _result;
        }

        protected void jqTeamMember_RowDeleting(object sender, JQGridRowDeleteEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    try
                    {
                        if (ExcludeTeamMember(e.RowKey) > 0)
                        {
                            Response.AddHeader("DeleteSuccess", "Team Member has been excluded successfully.");
                            jqTeamMember.DataBind();
                        }
                    }
                    catch (Exception exfailed)
                    {
                        Session["so_Exp"] = exfailed;
                        Response.AddHeader("DeleteFailed", "Error while excluding selected team member(s).");
                    }
                }
                else
                {
                    hdnSessionState.Value = "dead";
                    Response.AddHeader("ErrorMsg", "No Session");
                }
            }

            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }

        private int ExcludeTeamMember(string _selectedTeamMembers)
        {
            TeamMemberBL _TeamMemberBLObject = new TeamMemberBL();
            _TeamMemberBLObject.ps_SelectedTeammembers = _selectedTeamMembers;
            return _TeamMemberBLObject.ExcludeTeamMember();
        }


        protected void jqTeamMember_RowAdding(object sender, JQGridRowAddEventArgs e)
        {

            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    TeamMemberBL _TeamMemberBLObject = new TeamMemberBL();

                    _TeamMemberBLObject.ps_Name = e.RowData["Name"];
                    _TeamMemberBLObject.ps_Email = e.RowData["EmailID"];
                    _TeamMemberBLObject.pi_YOE = Convert.ToInt16(e.RowData["YOE"]);
                    _TeamMemberBLObject.ps_Role = e.RowData["Role"];

                    _TeamMemberBLObject.pi_ProjectID = Convert.ToInt16(e.ParentRowKey);

                    XSSValidator validatorObject = new XSSValidator(string.Empty);
                    validatorObject.pa_ExculdeException = new System.Collections.ArrayList();
                    validatorObject.pa_ExculdeException.Add("?");
                    validatorObject.pa_ExculdeException.Add("@");
                    validatorObject.pa_ExculdeException.Add("-");
                    validatorObject.pa_ExculdeException.Add(".");
                    validatorObject.pa_ExculdeException.Add(":");
                    validatorObject.pa_ExculdeException.Add("/");

                    if (validatorObject.StartValidate(_TeamMemberBLObject))
                    {
                        string _autoPWD;
                        _autoPWD = System.Web.Security.Membership.GeneratePassword(6, 2);
                        _TeamMemberBLObject.ps_EncryptedPassword = new EncryptField().Encrypt(_autoPWD);
                        _TeamMemberBLObject.AddTeamMember();
                        GetTPMProjects();
                        jqGridTPMProjects.DataBind();
                        SendWelComeMail(_TeamMemberBLObject.ps_Email, _TeamMemberBLObject.ps_Name, _autoPWD);
                        Response.AddHeader("ErrorMsg", "Success-Team member included successfully.");
                        
                    }
                    else
                    {

                        Response.AddHeader("ErrorMsg", "Err_296");
                    }

                }
                else
                {
                    hdnSessionState.Value = "dead";
                    Response.AddHeader("ErrorMsg", "No Session");
                }
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "Failed-Error while including team error.");
            }
        }
        static string GetWelcomeTPMMailTemplate()
        {
            StreamReader sr = File.OpenText(HttpContext.Current.Server.MapPath("../../MailTemplates/WelcomeTPM-TMMailTemplate.txt"));
            string message = sr.ReadToEnd();
            sr.Close();
            return message;
        }
        static void SendWelComeMail(string _tpmMail, string _userFullName, string _tempPWD)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.To.Add(new MailAddress(_tpmMail));
                mail.From = new MailAddress("no-reply@ittpm.com", "ITTPM");
                mail.Subject = "Welcome to TPM family - http:\\www.ittpm.com";
                mail.IsBodyHtml = true;
                string ms = GetWelcomeTPMMailTemplate();
                mail.Body = ms.Replace("$USERNAME", _userFullName.TrimEnd()).Replace("$ToEmail", _tpmMail).Replace("$PASSWORD", _tempPWD);
                SmtpClient client = new SmtpClient();
                string nruserC = new EncryptField().Decrypt(WebConfigurationManager.AppSettings["NO_REPLY_UC"]);
                client.Credentials = new NetworkCredential("no-reply@ittpm.com", "iYkl31#7");
                client.Port = 25;
                client.Host = "ittpm.com";
                try
                {
                    client.Send(mail);
                }
                catch (SmtpException)
                {

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion
        #region "Internal Functions/Methods"
        private DataSet GetTPMProjects()
        {
            DataSet _result;
            Session["RoleName"] = "TPM";
            TPMProjectBL tpmProjectbLObject = new TPMProjectBL();
            if (Session["RoleName"].ToString() == "TPM")
            {
                tpmProjectbLObject.pi_UserID = (int)HttpContext.Current.Session["UserID"];
                /*

                string ls_serachText = string.Empty;
                if (Context.Request["searchtext"] != null)
                    ls_serachText = Convert.ToString(Context.Request["searchtext"]);
                if (ls_serachText == "Search")
                    ls_serachText = "";
                scopeunitBLObject.ps_SearchText = ls_serachText;
                scopeunitBLObject.pi_RAID = Convert.ToInt32(Session["RAID"]);
                scopeunitBLObject.pi_LastModifiedBy = Convert.ToInt32(Session["UserID"]);*/
                _result = tpmProjectbLObject.GetProjects();
                Session["TPMProject"] = _result;
                
            }
            else
            {
                throw new Exception("Error While Processing Request...<br>");
            }
            return _result;
        }
        private void BindTPMProjects()
        {
            DataSet _Source = GetTPMProjects();
            jqGridTPMProjects.DataSource = _Source;
            jqGridTPMProjects.DataBind();
            jqGridTPMProjects.PagerSettings.PageSize = _Source.Tables[0].Rows.Count;
            jqGridTPMProjects.PagerSettings.PageSizeOptions = "{}";
            
        }
        private void BindProjectMembers(int _projectID)
        {
            
            DataSet TPMProjects = (DataSet) Session["TPMProject"];
            if (TPMProjects.Tables[1].Select("ID = " + _projectID).Length > 0)
            {
                jqTeamMember.DataSource = TPMProjects.Tables[1].Select("ID = " + _projectID).CopyToDataTable();
                jqTeamMember.DataBind();
            }            
            jqTeamMember.PagerSettings.PageSize = TPMProjects.Tables[1].Rows.Count;
            jqTeamMember.PagerSettings.PageSizeOptions = "{}";
            
            
        }
       
        private bool ValidateProjectEdit(TPMProjectBL _tpmProject, ref XSSValidator _validatorObject)
        {
            bool _result = true;

            _validatorObject.pa_ExculdeException = new System.Collections.ArrayList();
            _validatorObject.pa_ExculdeException.Add("?");
            _validatorObject.pa_ExculdeException.Add(":");
            _validatorObject.pa_ExculdeException.Add("/");
            _validatorObject.pa_ExculdeException.Add("-");
            _result = _validatorObject.StartValidate(_tpmProject);
            if (_tpmProject.ps_ColumnName == "EndDate" && _tpmProject.ps_NewValue != "")
            {
                DataSet _projectSource = (DataSet)jqGridTPMProjects.DataSource;
                if (Convert.ToDateTime(_projectSource.Tables[0].Select("ID = " + _tpmProject.pi_ID.ToString())[0]["StartDate"].ToString()) >= Convert.ToDateTime(_tpmProject.ps_NewValue))
                {
                    _validatorObject.pa_ErrorMessage.Add("End date not matching start date.");
                    _result = false;
                }
            }
            if (_tpmProject.ps_ColumnName == "StartDate" && _tpmProject.ps_NewValue != "")
            {
                DataSet _projectSource = (DataSet)jqGridTPMProjects.DataSource;
                if (Convert.ToDateTime(_projectSource.Tables[0].Select("ID = " + _tpmProject.pi_ID.ToString())[0]["EndDate"].ToString()) <= Convert.ToDateTime(_tpmProject.ps_NewValue))
                {
                    _validatorObject.pa_ErrorMessage.Add("Start date not matching with end date.");
                    
                    _result = false;
                }
                 if (Convert.ToDateTime(_projectSource.Tables[0].Select("ID = " + _tpmProject.pi_ID.ToString())[0]["TargetEndDate"].ToString()) <= Convert.ToDateTime(_tpmProject.ps_NewValue))
                {
                    _validatorObject.pa_ErrorMessage.Add("Start date not matching with target end date.");
                    _result = false;
                }
            }
            if (_tpmProject.ps_ColumnName == "TargetEndDate" && _tpmProject.ps_NewValue != "")
            {
                DataSet _projectSource = (DataSet)jqGridTPMProjects.DataSource;
                if (Convert.ToDateTime(_projectSource.Tables[0].Select("ID = " + _tpmProject.pi_ID.ToString())[0]["StartDate"].ToString()) >= Convert.ToDateTime(_tpmProject.ps_NewValue))
                {
                    _validatorObject.pa_ErrorMessage.Add("Target end date not matching start date.");
                    _result = false;
                }
            }
            return _result;
        }
        private int DeleteProject(string _slectedProjectIDs)
        {
            TPMProjectBL _tpmProjectObject = new TPMProjectBL();
            _tpmProjectObject.ps_SelectedIDs = _slectedProjectIDs;
            return _tpmProjectObject.DeleteProject();
        }
       
        #endregion
    }
}
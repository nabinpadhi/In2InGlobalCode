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
    public partial class task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Common.HasSession())
            {
                if (!IsPostBack)
                {
                    LoadProjects();
                }
                hdnSessionState.Value = "alive";
            }
            else
            {
                
                hdnSessionState.Value = "dead";

            }
            
        }
        protected void jqTasks_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    int _memberID = Convert.ToInt32(e.ParentRowKey);
                    BindMemberTask(_memberID);
                }
                else
                    hdnSessionState.Value = "alive";
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }
        private void BindMemberTask(int _memberID)
        {

            DataSet _MemberAndTask = GetMemberAndTasks();
            if (_MemberAndTask.Tables[1].Select("UserID = " + _memberID).Length > 0)
            {
                jqTasks.DataSource = _MemberAndTask.Tables[1].Select("UserID = " + _memberID).CopyToDataTable();
                jqTasks.DataBind();
            }
            jqTasks.PagerSettings.PageSize = _MemberAndTask.Tables[1].Select("UserID = " + _memberID).Length;
            jqTasks.PagerSettings.PageSizeOptions = "{}";
            Session["MemberAndTask"] = _MemberAndTask;
        }
        protected void jqTeamMember_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {                  
                    BindProjectMembers();
                }
                else
                    hdnSessionState.Value = "alive";
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }
        private void BindProjectMembers()
        {

            DataSet TPMProjects = GetMemberAndTasks();         

            jqTeamMember.DataSource = TPMProjects.Tables[0];
            jqTeamMember.DataBind();
            jqTeamMember.PagerSettings.PageSize = TPMProjects.Tables[0].Rows.Count;
            jqTeamMember.PagerSettings.PageSizeOptions = "{}";


        }
        private void LoadProjects()
        {
            ddlProject.DataSource = GetProjects((int)HttpContext.Current.Session["UserID"]);
            ddlProject.DataTextField = "Name";
            ddlProject.DataValueField = "ID";
            ddlProject.DataBind();
        }

        private DataSet GetProjects(int _TPMID)
        {
            MemberTaskBL _memberTaskBLObject = new MemberTaskBL();
            return _memberTaskBLObject.GetTPMProjects(_TPMID);
        }
        private DataSet GetMemberAndTasks()
        {
            
            DataSet _result;
            
            TeamMemberBL teamMemberBLObject = new TeamMemberBL();
            if (Convert.ToBoolean(Session["IsTPM"].ToString()))
            {
                teamMemberBLObject.pi_TPMID = (int)HttpContext.Current.Session["UserID"];
                _result = teamMemberBLObject.GetMemberAndTask();                

            }
            else
            {
                throw new Exception("Error While Processing Request...<br>");
            }
            return _result;

        }
        protected void jqTeamMember_CellBinding(object sender, JQGridCellBindEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    if (e.ColumnIndex == 4)
                    {
                        string RoleValue = "";
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

                }
                else
                {
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
        /*
        protected void jqTasks_RowEditing(object sender, JQGridRowEditEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    DataSet _TPMProjects = (DataSet)Session["TPMProject"];
                    if (Convert.ToString(_TPMProjects.Tables[0].Select("ID=" + e.RowKey)[0][e.RowData.GetKey(0)]) != e.RowData.Get(0))
                    {
                        TeamMemberBL _tpmProject = new TPMProjectBL();
                        _tpmProject.pi_ID = Convert.ToInt32(e.RowKey);
                        _tpmProject.ps_ColumnName = e.RowData.GetKey(0);
                        _tpmProject.ps_NewValue = e.RowData.Get(0);
                        XSSValidator validatorObject = new XSSValidator(string.Empty);
                        if (ValidateTaskEdit(_tpmProject, ref validatorObject))
                        {
                            _tpmProject.UpdateProject();
                        }
                        else
                        {
                            string ErrMsg = "";

                            foreach (string msg in validatorObject.pa_ErrorMessage)
                            {
                                ErrMsg += "<li>" + msg + "</li>";

                            }
                            ErrMsg = "<ul>" + ErrMsg + "</ul>";
                            Response.AddHeader("ErrorMsg", ErrMsg);
                        }
                    }

                    // Session["ResponseMessage"] = "Project with same name already exist";
                }
                else
                {
                    hdnSessionState.Value = "dead";
                    Response.AddHeader("ErrorMsg", "SessionDead");
                }
                jqTasks.DataBind();
            }
            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Response.AddHeader("ErrorMsg", "Exception");
            }

        }

        protected void jqTasks_RowDeleting(object sender, JQGridRowDeleteEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
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
                }
                jqTasks.DataBind();
            }

            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }*/
        protected void jqTasks_RowAdding(object sender, JQGridRowAddEventArgs e)
        {

            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    MemberTaskBL _memberTaskBLObject = new MemberTaskBL();

                    _memberTaskBLObject.pi_UserID = Convert.ToInt32(e.ParentRowKey);
                    _memberTaskBLObject.pi_AssignedBy = (int)HttpContext.Current.Session["UserID"];
                    _memberTaskBLObject.pi_LastModifiedBy = (int)HttpContext.Current.Session["UserID"];
                    _memberTaskBLObject.pi_ProjectID = Convert.ToInt32(e.RowData["ProjectName"]);
                    _memberTaskBLObject.ps_TaskDetails = e.RowData["TaskDetails"];
                    _memberTaskBLObject.ps_TaskHeader = e.RowData["TaskHeader"];
                    _memberTaskBLObject.ps_Status = e.RowData["Status"];
                    XSSValidator validatorObject = new XSSValidator(string.Empty);
                    validatorObject.pa_ExculdeException = new System.Collections.ArrayList();
                    validatorObject.pa_ExculdeException.Add("?");
                    validatorObject.pa_ExculdeException.Add(":");
                    validatorObject.pa_ExculdeException.Add("/");

                    if (validatorObject.StartValidate(_memberTaskBLObject))
                    {
                        try
                        {
                            _memberTaskBLObject.AddMemberTask();
                            jqTasks.DataBind();
                            Response.AddHeader("AddSuccess", "Task has been added successfully.");
                        }
                        catch (Exception)
                        {
                            Response.AddHeader("AddFailed", "Task has not been added successfully.");
                        }
                    }
                    else
                    {

                        Response.AddHeader("ErrorMsg", "TPM_9");
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

        protected void jqTasks_RowEditing(object sender, JQGridRowEditEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    hdnSessionState.Value = "alive";
                    DataSet _MemberAndTask = (DataSet)Session["MemberAndTask"];
                    if (Convert.ToString(_MemberAndTask.Tables[1].Select("ID=" + e.RowKey)[0][e.RowData.GetKey(0)]) != e.RowData.Get(0))
                    {
                        MemberTaskBL _memberTaskBLObject = new MemberTaskBL();

                        _memberTaskBLObject.pi_ID = Convert.ToInt32(e.RowKey);
                        _memberTaskBLObject.ps_ColumnName = e.RowData.GetKey(0);
                        _memberTaskBLObject.ps_NewValue = e.RowData.Get(0);

                        XSSValidator validatorObject = new XSSValidator(string.Empty);
                        validatorObject.pa_ExculdeException = new System.Collections.ArrayList();
                        validatorObject.pa_ExculdeException.Add("?");
                        validatorObject.pa_ExculdeException.Add(":");
                        validatorObject.pa_ExculdeException.Add("/");

                        if (validatorObject.StartValidate(_memberTaskBLObject))
                        {

                            _memberTaskBLObject.UpdateMemberTask();
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

        protected void jqTasks_RowDeleting(object sender, JQGridRowDeleteEventArgs e)
        {
            try
            {
                if (Common.HasSession())
                {
                    try
                    {
                        if (DeleteTask(e.RowKey) > 0)
                        {
                            Response.AddHeader("DeleteSuccess", "Task has been deleted successfully.");
                            jqTasks.DataBind();
                        }
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
                }
            }

            catch (Exception ex)
            {
                Session["so_Exp"] = ex;
                Session["isLoginPage"] = "False";
                Response.AddHeader("ErrorMsg", "../standards/PageException.aspx?src=pop&ipl=0");
            }
        }

        private int DeleteTask(string _selectedTasks)
        {
            MemberTaskBL _memberTaskBLObject = new MemberTaskBL();
            _memberTaskBLObject.ps_SelectedTasks = _selectedTasks;
            return _memberTaskBLObject.DeleteTask();
        }
    }
    
}
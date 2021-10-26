using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InGlobal.DataAccess;
using InGlobal.Interface;
using System.Data.SqlClient;
using System.Data;
namespace InGlobal.DataLink
{
    public class TeamMemberDL
    {
       
        public int AddTeamMember(ITeamMember _teamMember)
        {   
            In2InGlobalDataAccessHelper dataAccessObject = new In2InGlobalDataAccessHelper();
            return (int)dataAccessObject.UpdateObject(_teamMember, "sp_AddTeamMember");
        }
        public ITeamMember GetTeamMember(ITeamMember _teamMember)
        {
            In2InGlobalDataAccessHelper dataAccessObject = new In2InGlobalDataAccessHelper();
            _teamMember = (ITeamMember)dataAccessObject.GetObject("sp_GetTeamMember", _teamMember);
            return _teamMember;
        }

        public int ExcludeTeamMember(string _slectedTeamMembers)
        {
            In2InGlobalDataAccessHelper _dataaccessObject = new In2InGlobalDataAccessHelper();
            return _dataaccessObject.ExecuteNonQuery("sp_ExcludeTeamMeber", new SqlParameter[] { new SqlParameter("@SelectedTeamMembers", _slectedTeamMembers) });
        }

        public void UpdateTeamMember(ITeamMember _teamMember)
        {
            In2InGlobalDataAccessHelper _dataaccessObject = new In2InGlobalDataAccessHelper();
            _dataaccessObject.UpdateObject(_teamMember, "sp_UpdateTeamMember");
        }

        public DataSet GetMemberAndTask(ITeamMember _teamMember)
        {
            In2InGlobalDataAccessHelper dataAccessObject = new In2InGlobalDataAccessHelper();
            return (DataSet)dataAccessObject.GetDataSet("sp_GetMemberAndTasks",new SqlParameter[] {  new SqlParameter("@UserID", _teamMember.pi_TPMID)});
            
        }
    }
}

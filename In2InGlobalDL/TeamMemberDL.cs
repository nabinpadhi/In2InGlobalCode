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
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            return (int)dataAccessObject.UpdateObject(_teamMember, "sp_AddTeamMember");
        }
        public ITeamMember GetTeamMember(ITeamMember _teamMember)
        {
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            _teamMember = (ITeamMember)dataAccessObject.GetObject("sp_GetTeamMember", _teamMember);
            return _teamMember;
        }

        public int ExcludeTeamMember(string _slectedTeamMembers)
        {
            KSSITTPMDataAccessHelper _dataaccessObject = new KSSITTPMDataAccessHelper();
            return _dataaccessObject.ExecuteNonQuery("sp_ExcludeTeamMeber", new SqlParameter[] { new SqlParameter("@SelectedTeamMembers", _slectedTeamMembers) });
        }

        public void UpdateTeamMember(ITeamMember _teamMember)
        {
            KSSITTPMDataAccessHelper _dataaccessObject = new KSSITTPMDataAccessHelper();
            _dataaccessObject.UpdateObject(_teamMember, "sp_UpdateTeamMember");
        }

        public DataSet GetMemberAndTask(ITeamMember _teamMember)
        {
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            return (DataSet)dataAccessObject.GetDataSet("sp_GetMemberAndTasks",new SqlParameter[] {  new SqlParameter("@UserID", _teamMember.pi_TPMID)});
            
        }
    }
}

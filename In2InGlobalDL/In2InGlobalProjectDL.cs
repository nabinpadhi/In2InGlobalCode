using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InGlobal.Interface;
using System.Data.SqlClient;
using InGlobal.DataAccess;
namespace InGlobal.DataLink
{
    public class In2InGlobalProjectDL
    {
        public DataSet GetProjects(IProject _In2InGlobalProjectObject)
        {
            In2InGlobalDataAccessHelper _dataaccessObject = new In2InGlobalDataAccessHelper();
            return _dataaccessObject.GetDataSet("sp_GetIn2InGlobalProjects", new SqlParameter[] { new SqlParameter("@UserID", _In2InGlobalProjectObject.pi_UserID) });

        }

        public void AddProject(IProject _In2InGlobalProjectObject)
        {
            In2InGlobalDataAccessHelper _dataaccessObject = new In2InGlobalDataAccessHelper();
            _dataaccessObject.UpdateObject(_In2InGlobalProjectObject, "sp_AddProject");
        }

        public void UpdateProject(IProject _In2InGlobalProjectObject)
        {
            In2InGlobalDataAccessHelper _dataaccessObject = new In2InGlobalDataAccessHelper();
            _dataaccessObject.UpdateObject(_In2InGlobalProjectObject, "sp_UpdateProject");
        }

        public int DeleteProject(string _slectedProjectIDs)
        {
            In2InGlobalDataAccessHelper _dataaccessObject = new In2InGlobalDataAccessHelper();
            return _dataaccessObject.ExecuteNonQuery("sp_DeleteProject", new SqlParameter[] { new SqlParameter("@SelectedProjects", _slectedProjectIDs) });
        }
    }
}

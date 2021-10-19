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
    public class TPMProjectDL
    {
        public DataSet GetProjects(IProject _tpmProjectObject)
        {
            KSSITTPMDataAccessHelper _dataaccessObject = new KSSITTPMDataAccessHelper();
            return _dataaccessObject.GetDataSet("sp_GetTPMProjects", new SqlParameter[] { new SqlParameter("@UserID", _tpmProjectObject.pi_UserID) });

        }

        public void AddProject(IProject _tpmProjectObject)
        {
            KSSITTPMDataAccessHelper _dataaccessObject = new KSSITTPMDataAccessHelper();
            _dataaccessObject.UpdateObject(_tpmProjectObject, "sp_AddProject");
        }

        public void UpdateProject(IProject _tpmProjectObject)
        {
            KSSITTPMDataAccessHelper _dataaccessObject = new KSSITTPMDataAccessHelper();
            _dataaccessObject.UpdateObject(_tpmProjectObject, "sp_UpdateProject");
        }

        public int DeleteProject(string _slectedProjectIDs)
        {
            KSSITTPMDataAccessHelper _dataaccessObject = new KSSITTPMDataAccessHelper();
            return _dataaccessObject.ExecuteNonQuery("sp_DeleteProject", new SqlParameter[] { new SqlParameter("@SelectedProjects", _slectedProjectIDs) });
        }
    }
}

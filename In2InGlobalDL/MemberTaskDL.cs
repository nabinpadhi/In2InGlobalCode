using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InGlobal.DataAccess;
using InGlobal.Interface;
using System.Data;
using System.Data.SqlClient;
namespace InGlobal.DataLink
{
    public class MemberTaskDL
    {
        public DataSet AddMemberTask(IMemberTask _memberTask)
        {
            DataSet _result;
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            dataAccessObject.UpdateObject(_memberTask, "sp_AddMemberTask");
            _result = (DataSet)dataAccessObject.GetDataSet("sp_GetMemberAndTasks", new SqlParameter[] { new SqlParameter("@UserID", _memberTask.pi_LastModifiedBy) });
            return _result;
        }

        public DataSet GetTPMProjects(int _TPMID)
        {
            DataSet _result;
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            _result = (DataSet)dataAccessObject.GetDataSet("sp_GetTPMProjects", new SqlParameter[] { new SqlParameter("@UserID", _TPMID) });
            return _result;
        }

        public void UpdateMemberTask(IMemberTask _memberTaskObject)
        {
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            dataAccessObject.UpdateObject(_memberTaskObject, "sp_UpdateMemberTask");
        }

        public int DeleteTask(IMemberTask _memberTaskObject)
        {
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            return dataAccessObject.ExecuteNonQuery("sp_DeleteTasks", new SqlParameter[] { new SqlParameter("@SelectedTasks", _memberTaskObject.ps_SelectedTasks) });
        }
    }
}

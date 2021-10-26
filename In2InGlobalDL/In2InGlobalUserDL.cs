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
    public class In2InGlobalUserDL
    {
        public DataSet ValidateLogin(IUser _user)
        {
            
            DataSet _userObject = null;
            In2InGlobalDataAccessHelper dataAccessObject = new In2InGlobalDataAccessHelper();
            _userObject = dataAccessObject.GetDataSet("sp_IsValidUser", new SqlParameter[] { new SqlParameter("@UserName", _user.ps_UserName), new SqlParameter("@EncryptedPassword", _user.ps_EncryptedPassword)});
            return _userObject;
        }

        public int AddTPM(IUser _user)
        {   
            In2InGlobalDataAccessHelper dataAccessObject = new In2InGlobalDataAccessHelper();
            return (int)dataAccessObject.UpdateObject(_user, "sp_AddTPM");
        }
        public  IUser GetTPM(IUser _user)
        {
            In2InGlobalDataAccessHelper dataAccessObject = new In2InGlobalDataAccessHelper();
           _user = (IUser)dataAccessObject.GetObject("sp_GetTPM",_user);
           return _user;
        }

        public int UpdatePwd(IUser _user)
        {
            In2InGlobalDataAccessHelper dataAccessObject = new In2InGlobalDataAccessHelper();
            return (int)dataAccessObject.UpdateObject(_user, "sp_UpdatePWD");
        }
    }
}

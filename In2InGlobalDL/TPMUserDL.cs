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
    public class TPMUserDL
    {
        public DataSet ValidateLogin(IUser _user)
        {
            
            DataSet _userObject = null;
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            _userObject = dataAccessObject.GetDataSet("sp_IsValidUser", new SqlParameter[] { new SqlParameter("@UserName", _user.ps_UserName), new SqlParameter("@EncryptedPassword", _user.ps_EncryptedPassword)});
            return _userObject;
        }

        public int AddTPM(IUser _user)
        {   
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            return (int)dataAccessObject.UpdateObject(_user, "sp_AddTPM");
        }
        public  IUser GetTPM(IUser _user)
        {
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
           _user = (IUser)dataAccessObject.GetObject("sp_GetTPM",_user);
           return _user;
        }

        public int UpdatePwd(IUser _user)
        {
            KSSITTPMDataAccessHelper dataAccessObject = new KSSITTPMDataAccessHelper();
            return (int)dataAccessObject.UpdateObject(_user, "sp_UpdatePWD");
        }
    }
}

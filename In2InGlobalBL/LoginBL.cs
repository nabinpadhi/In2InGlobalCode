using In2InGlobal.datalink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.businesslogic
{
    public class LoginBl
    {
        public DataSet getMyLogin(string email) 
        {
            DataSet dsLogin = new DataSet();
            try
            {
                LoginDL objLogin = new LoginDL();
                dsLogin = objLogin.getMyLogin(email);  
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dsLogin;
        }
        public long UpdateUserLoginPwd(string email,string paawrd)
        {
            long _result = 0;
            try
            {
                LoginDL objLogin = new LoginDL();
                _result = objLogin.UpdateUserLoginPwd(email, paawrd);
            }
            catch(Exception ex)
            {

            }
            return _result;
        }
    }
}

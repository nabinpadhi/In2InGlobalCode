using In2InGlobal.datalink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InGlobal.BusinessLogic
{
    public class MyProfileBL
    {
        /// <summary>
        /// The Function which is used for get the MyProfile Data
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataSet getMyProfile(string email)
        { 
            DataSet dsMyProfile = new DataSet();
            try
            {
                MyProfileDL objMyprofile = new MyProfileDL();
                dsMyProfile = objMyprofile.getMyProfile(email);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return dsMyProfile;
        }
    }
}

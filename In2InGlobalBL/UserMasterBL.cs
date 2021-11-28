using In2InGlobal.datalink;
using In2InGlobalBusinessEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.businesslogic
{
    public class UserMasterBL
    {
        /// <summary>
        /// This function is used to load company name
        /// </summary>
        /// <returns></returns>
        public DataSet getCompanyNameForUser() 
        {
            DataSet dsobjcompanyname = new DataSet();
            try
            {
                UserMasterDL objcompanyname = new UserMasterDL();
                dsobjcompanyname = objcompanyname.getCompanyNameForUser(); 
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsobjcompanyname;
        }

        /// <summary>
        /// This function is used to load activity name
        /// </summary>
        /// <returns></returns>
        public DataSet getActivityNameForUser()
        {
            DataSet dsactivityname = new DataSet(); 
            try
            {
                UserMasterDL objactivityname = new UserMasterDL();
                dsactivityname = objactivityname.getActivityNameForUser();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsactivityname;
        }

        /// <summary>
        /// This function is used to load role name
        /// </summary>
        /// <returns></returns>
        public DataSet getRoleNameForUser()
        {
            DataSet dsrolename = new DataSet(); 
            try
            {
                UserMasterDL objuserrolename = new UserMasterDL();
                dsrolename = objuserrolename.getRoleNameForUser();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsrolename; 
        }

        public DataSet FillUserGridInfo()
        {
            DataSet dsrolename = new DataSet();
            try
            {
                UserMasterDL objuserrolename = new UserMasterDL();
                dsrolename = objuserrolename.FillUserGridInfo();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dsrolename;
        }


        /// <summary>
        /// This function is used to insert user information
        /// </summary>
        public UserDto SaveUserMasterDetails(UserEntity userEntity)     
        {
            UserDto response = null;
            UserMasterDL userMasterDL = new UserMasterDL();

            if (userEntity == null) return response;

            var varUserId = userMasterDL.SaveUserMaster(userEntity);

            response = new UserDto
            {
                UserId = varUserId 
            };
           
            return response;
        }

        /// <summary>
        /// This function is used to insert user information
        /// </summary>
        public UserDto UpdateUser(UserEntity userEntity) 
        {
            UserDto response = null;
            UserMasterDL userMasterDL = new UserMasterDL();

            if (userEntity == null) return response;

            var varUserId = userMasterDL.UpdateUser(userEntity); 

            response = new UserDto
            {
                UserId = varUserId
            };

            return response;
        }

        /// <summary>
        /// This function is used to insert user information
        /// </summary>
        public UserDto DeleteUser(UserEntity userEntity) 
        {
            UserDto response = null;
            UserMasterDL userMasterDL = new UserMasterDL();

            if (userEntity == null) return response;

            var varUserId = userMasterDL.DeleteUser(userEntity); 

            response = new UserDto
            {
                UserId = varUserId
            };

            return response;
        }

    }
}

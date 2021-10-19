using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using InGlobal.Interface;
using InGlobal.DataLink;

namespace InGlobal.BusinessLogic
{
    public class TPMUserBL:IUser
    {
        #region "Private Members"
        int _id;
        string _companyEmail;
        string _companyName;
        
        string _encryptedPassword;
        string _fullName;
        string _userName;
        bool _isInitialPassword;
        string _technology;
        DateTime _dob;
        DataSet _users;
        #endregion

        #region "Properties"
        public int pi_ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string ps_CompanyEmail
        {
            get
            {
                return _companyEmail;
            }
            set
            {
                _companyEmail = value;
            }
        }

        public string ps_CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }

        public string ps_FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        public string ps_EncryptedPassword
        {
            get
            {
                return _encryptedPassword;
            }
            set
            {
                _encryptedPassword = value;
            }
        }

        public string ps_UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public bool pb_IsInitialPassword
        {
            get
            {
                return _isInitialPassword;
            }
            set
            {
                _isInitialPassword = value;
            }
        }

        public string ps_Technology
        {
            get
            {
                return _technology;
            }
            set
            {
                _technology = value;
            }
        }

        public DateTime pd_DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
            }
        }

        public DataSet po_Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }
        #endregion

        #region "Methods"
        public DataSet IsValidLogin()
        {
            DataSet _result;
            TPMUserDL _tpmuserDL = new TPMUserDL();
            _result = _tpmuserDL.ValidateLogin(this);
            return _result;
        }
        #endregion

        public bool AddTPM()
        {
            bool _result = true;
            TPMUserDL _userDLObject = new TPMUserDL();
            if (_userDLObject.AddTPM(this) == 0)
            {
                _result = false;
            }
            return _result;
        }
        public TPMUserBL GetTPM()
        {            
            TPMUserDL _userDLObject = new TPMUserDL();
            return (TPMUserBL)_userDLObject.GetTPM(this);
            
        }

        public int UpdatePwd()
        {
            TPMUserDL _userDLObject = new TPMUserDL();
            return _userDLObject.UpdatePwd(this);
        }
    }
}

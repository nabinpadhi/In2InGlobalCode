using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using InGlobal.Interface;
using InGlobal.DataLink;

namespace InGlobal.BusinessLogic
{
    public class TeamMemberBL : ITeamMember
    {
        #region "Private Members"
        int _id;
        int _tpmid;
        private int _projectID;        
        string _Email;
        string _Name;
        string _EncryptedPassword;
        string _columnName;
        string _newValue;
        string _role;
        string _Technology;
        int _YOE;
        DateTime _DOB;
        DataSet _teamMembers;
        string _selectedTeammembers;
        TeamMemberDL _teamMemberDLObject = new TeamMemberDL();
        #endregion

        #region "Properties"
        public string ps_SelectedTeammembers
        {
            get
            {
                return _selectedTeammembers;
            }
            set
            {
                _selectedTeammembers = value;
            }
        }
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
        public int pi_TPMID
        {
            get
            {
                return _tpmid;
            }
            set
            {
                _tpmid = value;
            }
        }
        public int pi_ProjectID
        {
            get
            {
                return _projectID;
            }
            set
            {
                _projectID = value;
            }
        }
        public string ps_Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public string ps_Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }
        public string ps_Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        public int pi_YOE
        {
            get
            {
                return _YOE;
            }
            set
            {
                _YOE = value;
            }
        }

        public DataSet po_TeamMembers
        {
            get
            {
                return _teamMembers;
            }
            set
            {
                _teamMembers = value;
            }
        }
        public string ps_ColumnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                _columnName = value;
            }
        }
        public string ps_NewValue
        {
            get
            {
                return _newValue;
            }
            set
            {
                _newValue = value;
            }
        }
        public string ps_EncryptedPassword
        {
            get
            {
                return _EncryptedPassword;
            }
            set
            {
                _EncryptedPassword = value;
            }
        }
        public string ps_Technology
        {
            get
            {
                return _Technology;
            }
            set
            {
                _Technology = value;
            }
        }
        public DateTime pd_DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
            }
        }
        #endregion

        #region "Methods"
        public bool AddTeamMember()
        {
            bool _result = true;

            if (_teamMemberDLObject.AddTeamMember(this) == 0)
            {
                _result = false;
            }
            return _result;
        }
        public TeamMemberBL GetTeamMember()
        {

            return (TeamMemberBL)_teamMemberDLObject.GetTeamMember(this);

        }
        public DataSet GetMemberAndTask()
        {

            return _teamMemberDLObject.GetMemberAndTask(this);

        }

        #endregion



        public void UpdateTeamMember()
        {

            TeamMemberDL tpmProjectDLObject = new TeamMemberDL();
            tpmProjectDLObject.UpdateTeamMember(this);

        }

        public int ExcludeTeamMember()
        {
            return _teamMemberDLObject.ExcludeTeamMember(this.ps_SelectedTeammembers);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InGlobal.DataLink;
using InGlobal.Interface;
namespace InGlobal.BusinessLogic
{
    public class MemberTaskBL:IMemberTask
    {
        int _ID;
        int _userID;
        int _projectID;
        int _assignedBy;
        string _taskHeader;
        string _taskDetails;
        int _lastModifiedBy;
        string _status;
        private string _columnName;
        private string _newValue;
        private string _selectedTasks;
        public int pi_ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        public int pi_UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
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

        public int pi_AssignedBy
        {
            get
            {
                return _assignedBy;
            }
            set
            {
                _assignedBy = value;
            }
        }

        public string ps_TaskHeader
        {
            get
            {
                return _taskHeader;
            }
            set
            {
                _taskHeader = value;
            }
        }

        public string ps_TaskDetails
        {
            get
            {
                return _taskDetails;
            }
            set
            {
                _taskDetails = value;
            }
        }

        public int pi_LastModifiedBy
        {
            get
            {
                return _lastModifiedBy;
            }
            set
            {
                _lastModifiedBy = value;
            }
        }

        public string ps_Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        public string ps_SelectedTasks
        {
            get
            {
                return _selectedTasks;
            }
            set
            {
                _selectedTasks = value;
            }
        }
        public string ps_ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }

        }
        public string ps_NewValue
        {
            get { return _newValue; }
            set { _newValue = value; }

        }

        public DataSet AddMemberTask()
        {
            MemberTaskDL _memberTaskDL = new MemberTaskDL();
            return _memberTaskDL.AddMemberTask(this);
        }

        public DataSet GetIn2InGlobalProjects(int _TPMID)
        {
            MemberTaskDL _memberTaskDL = new MemberTaskDL();
            return _memberTaskDL.GetIn2InGlobalProjects(_TPMID);
        }

        public void UpdateMemberTask()
        {
            MemberTaskDL _memberTaskDL = new MemberTaskDL();
            _memberTaskDL.UpdateMemberTask(this);
        }

        public int DeleteTask()
        {
            MemberTaskDL _memberTaskDL = new MemberTaskDL();
            return _memberTaskDL.DeleteTask(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InGlobal.Interface;
using InGlobal.DataLink;
namespace InGlobal.BusinessLogic
{
    public class In2InGlobalProjectBL:IProject
    {
        #region "Private Members"
        private int _userID;
        private int _id;
        private string _selectedIDs;
        private string _name;
        private int _teamSize;
        private string _clientName;
        private DateTime _startDate;
        private DateTime _endDate;
        private DateTime _targetDate;
        private int _status;
        private DataSet _projects;
        private string _columnName;
        private string _newValue;
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
       
        public string ps_SelectedIDs
        {
            get
            {
                return _selectedIDs;
            }
            set
            {
                _selectedIDs = value;
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
        public string ps_Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int pi_TeamSize
        {
            get
            {
                return _teamSize;
            }
            set
            {
                _teamSize = value;
            }
        }

        public string ps_ClientName
        {
            get
            {
                return _clientName;
            }
            set
            {
                _clientName = value;
            }
        }

        public DateTime pd_StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }

        public DateTime pd_EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }

        public DateTime pd_TargetEndDate
        {
            get
            {
                return _targetDate;
            }
            set
            {
                _targetDate = value;
            }
        }

        public int pi_Status
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

        public DataSet po_Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
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
        #endregion

        public DataSet GetProjects()
        {
            In2InGlobalProjectDL In2InGlobalProjectDLObject = new In2InGlobalProjectDL();
            return In2InGlobalProjectDLObject.GetProjects(this);
        }

        public void AddProject()
        {
            In2InGlobalProjectDL In2InGlobalProjectDLObject = new In2InGlobalProjectDL();
            In2InGlobalProjectDLObject.AddProject(this);
        }
        public void UpdateProject()
        {
            In2InGlobalProjectDL In2InGlobalProjectDLObject = new In2InGlobalProjectDL();
            In2InGlobalProjectDLObject.UpdateProject(this);
        }

        public int DeleteProject()
        {
            In2InGlobalProjectDL In2InGlobalProjectDLObject = new In2InGlobalProjectDL();
            return In2InGlobalProjectDLObject.DeleteProject(this.ps_SelectedIDs);
        }
    }
}

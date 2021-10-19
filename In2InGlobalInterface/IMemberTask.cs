using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InGlobal.Interface
{
    public interface IMemberTask
    {
        int pi_ID
        {
            get;
            set;
        }
        int pi_UserID
        {
            get;
            set;
        }
          int pi_ProjectID
               {
            get;
            set;
        }
         int pi_AssignedBy
              {
            get;
            set;
        }
           string ps_TaskHeader
                {
            get;
            set;
        }
         string ps_TaskDetails
              {
            get;
            set;
        }         
         int pi_LastModifiedBy
              {
            get;
            set;
        }
          string ps_Status
         {
             get;
             set;
         }
          string ps_SelectedTasks
          {
              get;
              set;
          }
    }
}

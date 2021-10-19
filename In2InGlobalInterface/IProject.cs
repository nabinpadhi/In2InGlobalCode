using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InGlobal.Interface
{
    public interface IProject
    {
        int pi_ID { get; set; }
        int pi_UserID { get; set; }
        string ps_Name { get; set; }
        int pi_TeamSize { get; set; }
        string ps_ClientName { get; set; }
        DateTime pd_StartDate { get; set; }
        DateTime pd_EndDate { get; set; }
        DateTime pd_TargetEndDate { get; set; }
        int pi_Status { get; set; }
        DataSet po_Projects { get; set; }
        string ps_ColumnName { get; set; }
        string ps_NewValue { get; set; }
        string ps_SelectedIDs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace InGlobal.Interface
{
    public interface ITeamMember
    {
         
        int pi_ID { get; set; }
        int pi_TPMID { get; set; }
        int pi_ProjectID { get; set; }
        string ps_Email { get; set; }
        string ps_Name { get; set; }
        string ps_SelectedTeammembers { get; set; }
        int pi_YOE { get; set; }
        string ps_Role { get; set; }
        DataSet po_TeamMembers { get; set; }
       
    }
}

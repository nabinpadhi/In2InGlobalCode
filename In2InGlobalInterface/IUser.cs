using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace InGlobal.Interface
{
    public interface IUser
    {
         
        int pi_ID { get; set; }
        
        string ps_CompanyEmail { get; set; }
        string ps_CompanyName { get; set; }
        string ps_FullName { get; set; }
        string ps_EncryptedPassword { get; set; }
        string ps_UserName { get; set; }
        bool pb_IsInitialPassword { get; set; }       
        string ps_Technology { get; set; }
        DateTime pd_DOB { get; set; }

        DataSet po_Users { get; set; }
       
    }
}

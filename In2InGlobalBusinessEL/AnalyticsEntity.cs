using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobalBusinessEL
{
    public class AnalyticsEntity
    {
        public long Id { get; set; } 
        public long CompanyId { get; set; }
        public long ProjectId { get; set; }  
        public long UserId { get; set; }       
        public string CreatedBy { get; set; }
        public long DashboardId { get; set; }
        public long TemplateId { get; set; } 
        public long  WorkspaceId { get; set; }
        public string DashboardUrl { get; set; }       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobalBusinessEL
{
    public class AssignedTemplateEntity
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public long TemplateId { get; set; }
        public string AssignedBy { get; set; } 
    }
}

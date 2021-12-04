using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobalBusinessEL
{
    public class UploadTemplateEntity
    {
        public long Id { get; set; }
        public long  TemplateId { get; set; }
        public string FileName { get; set; }
        public string ProjectName { get; set; }
        public string CreatedBy { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public string RoleName { get; set; }

    }
}

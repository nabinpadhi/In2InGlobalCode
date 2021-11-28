using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace In2InGlobalBusinessEntity
{
    public class TemplateEntity
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }  
        public string? Instruction { get; set; }          
        public string? CreatedBy { get; set; }
    }
}
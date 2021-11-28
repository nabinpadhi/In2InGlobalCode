using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace In2InGlobalBusinessEL
{
    public class CompanyEntity
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string LOB { get; set; }
        public string CompanyAddress{ get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }

    }
}
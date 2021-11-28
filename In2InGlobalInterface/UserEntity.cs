using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace In2InGlobal.presentation.EntityDetails
{
    public class UserEntity
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public long CompanyId { get; set; }
        public long RoleId { get; set; }
        public long ActivityId { get; set; } 
        public string Password { get; set; }
        public string CreatedBy { get; set; }
    }
}
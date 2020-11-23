using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public partial class UserSecretQuestions
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public string Emailaddress { get; set; }
        public int? UserId { get; set; }
        public string Movie { get; set; }
        public string City { get; set; }
        public string Hero { get; set; }
        public string Book { get; set; }
        public string Cartoon { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual UserMaster UserMaster { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableUserMaster
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public int LangId { get; set; }
        public int MembershipId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual TableLanguage Lang { get; set; }
        public virtual TableMembership TableMembership { get; set; }
    }
}

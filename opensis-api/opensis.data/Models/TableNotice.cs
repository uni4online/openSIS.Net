using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableNotice
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int NoticeId { get; set; }
        public string TargetMembershipIds { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool Isactive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

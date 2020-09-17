using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableMembership
    {
        public TableMembership()
        {
            TableUserMaster = new HashSet<TableUserMaster>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int MembershipId { get; set; }
        public string Profile { get; set; }
        public string Title { get; set; }
        public string Access { get; set; }
        public bool? WeeklyUpdate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual TableSchoolMaster TableSchoolMaster { get; set; }
        public virtual ICollection<TableUserMaster> TableUserMaster { get; set; }
    }
}

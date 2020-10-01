using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Plans
    {
        public Plans()
        {
            SchoolMaster = new HashSet<SchoolMaster>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int PlanId { get; set; }
        public string Name { get; set; }
        public int? MaxApiChecks { get; set; }
        public byte[] Features { get; set; }

        public virtual ICollection<SchoolMaster> SchoolMaster { get; set; }
    }
}

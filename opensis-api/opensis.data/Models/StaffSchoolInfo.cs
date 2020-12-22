using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public partial class StaffSchoolInfo
    {
        public int Id { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? StaffId { get; set; }
        public int? SchoolAttachedId { get; set; }
        public string SchoolAttachedName { get; set; }
        public string Profile { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual StaffMaster StaffMaster { get; set; }
    }
}

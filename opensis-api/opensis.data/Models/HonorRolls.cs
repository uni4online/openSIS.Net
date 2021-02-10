using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class HonorRolls
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int MarkingPeriodId { get; set; }
        public int HonorRollId { get; set; }
        public string HonorRoll { get; set; }
        public int? Breakoff { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual SchoolYears SchoolYears { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class BlockPeriod
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int BlockId { get; set; }
        public int PeriodId { get; set; }
        public string PeriodTitle { get; set; }
        public string PeriodShortName { get; set; }
        public string PeriodStartTime { get; set; }
        public string PeriodEndTime { get; set; }
        public int? PeriodSortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Block Block { get; set; }
    }
}

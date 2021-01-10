using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class EffortGradeScale
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int EffortGradeScaleId { get; set; }
        public int? GradeScaleValue { get; set; }
        public string GradeScaleComment { get; set; }
        public int? SortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Grade
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int GradeScaleId { get; set; }
        public int GradeId { get; set; }
        public string Tite { get; set; }
        public int? Breakoff { get; set; }
        public decimal? WeightedGpValue { get; set; }
        public decimal? UnweightedGpValue { get; set; }
        public string Comment { get; set; }
        public int? SortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GradeScale GradeScale { get; set; }
    }
}

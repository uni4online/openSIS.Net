using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class GradeScale
    {
        public GradeScale()
        {
            Grade = new HashSet<Grade>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int GradeScaleId { get; set; }
        public string GradeScaleName { get; set; }
        public decimal? GradeScaleValue { get; set; }
        public string GradeScaleComment { get; set; }
        public bool? CalculateGpa { get; set; }
        public bool? UseAsStandardGradeScale { get; set; }
        public int? SortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual SchoolMaster SchoolMaster { get; set; }
        public virtual ICollection<Grade> Grade { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class CourseStandard
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int CourseId { get; set; }
        public string StandardRefNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual Course Course { get; set; }
        public virtual GradeUsStandard GradeUsStandard { get; set; }
    }
}

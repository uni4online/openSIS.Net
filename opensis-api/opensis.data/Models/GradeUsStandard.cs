using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class GradeUsStandard
    {
        public GradeUsStandard()
        {
            CourseStandard = new HashSet<CourseStandard>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public string StandardRefNo { get; set; }
        public int? GradeStandardId { get; set; }
        public string GradeLevel { get; set; }
        public string Domain { get; set; }
        public string Subject { get; set; }
        public string Course { get; set; }
        public string Topic { get; set; }
        public string StandardDetails { get; set; }
        public bool? IsSchoolSpecific { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<CourseStandard> CourseStandard { get; set; }
    }
}

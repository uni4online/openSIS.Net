using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class StudentEnrollmentCode
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int EnrollmentCode { get; set; }
        public decimal? AcademicYear { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int? SortOrder { get; set; }
        public string Type { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public virtual SchoolMaster SchoolMaster { get; set; }
    }
}

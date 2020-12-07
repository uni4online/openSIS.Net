using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Gradelevels
    {
       
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int GradeId { get; set; }
        public string ShortName { get; set; }
        public string Title { get; set; }
        public string IscedGradeLevel { get; set; }
        public int? NextGradeId { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual GradeEquivalency IscedGradeLevelNavigation { get; set; }
        public virtual SchoolMaster SchoolMaster { get; set; }
       
    }
}

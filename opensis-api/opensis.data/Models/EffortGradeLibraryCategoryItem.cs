using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class EffortGradeLibraryCategoryItem
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int EffortCategoryId { get; set; }
        public int EffortItemId { get; set; }
        public string EffortItemTitle { get; set; }
        public int? SortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual EffortGradeLibraryCategory EffortGradeLibraryCategory { get; set; }
    }
}

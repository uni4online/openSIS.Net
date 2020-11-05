using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class FieldsCategory
    {
        public FieldsCategory()
        {
            CustomFields = new HashSet<CustomFields>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int CategoryId { get; set; }
        public bool? IsSystemCategory { get; set; }
        public bool? Search { get; set; }
        public string Title { get; set; }
        public string Module { get; set; }
        public int? SortOrder { get; set; }
        public bool? Required { get; set; }
        public bool? Hide { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual SchoolMaster SchoolMaster { get; set; }
        public virtual ICollection<CustomFields> CustomFields { get; set; }
    }
}

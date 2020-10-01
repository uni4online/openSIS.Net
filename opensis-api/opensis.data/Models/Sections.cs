using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Sections
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int SectionId { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
    }
}

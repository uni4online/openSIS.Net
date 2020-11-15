using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class CustomFieldsValue
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int CategoryId { get; set; }
        public int FieldId { get; set; }
        public int TargetId { get; set; }
        public string Module { get; set; }
        public string CustomFieldTitle { get; set; }
        public string CustomFieldType { get; set; }
        public string CustomFieldValue { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomFields CustomFields { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Subject
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

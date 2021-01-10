using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Programs
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

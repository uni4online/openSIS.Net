using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public partial class DpdownValuelist
    {
        public long Id { get; set; }
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public string LovName { get; set; }
        public string LovColumnValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual SchoolMaster SchoolMaster { get; set; }
    }
}

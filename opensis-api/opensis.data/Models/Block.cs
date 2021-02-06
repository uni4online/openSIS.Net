using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Block
    {
        public Block()
        {
            
            CourseBlockSchedule = new HashSet<CourseBlockSchedule>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int BlockId { get; set; }
        public string BlockTitle { get; set; }
        public long? BlockSortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual SchoolMaster SchoolMaster { get; set; }
      
        public virtual ICollection<CourseBlockSchedule> CourseBlockSchedule { get; set; }
    }
}

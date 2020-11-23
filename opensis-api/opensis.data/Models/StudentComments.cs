using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public partial class StudentComments
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public virtual StudentMaster StudentMaster { get; set; }
    }
}

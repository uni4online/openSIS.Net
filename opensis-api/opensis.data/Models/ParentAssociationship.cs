using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public partial class ParentAssociationship
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public bool Associationship { get; set; }
        public string Relationship { get; set; }
        public bool? IsCustodian { get; set; }
        public string ContactType { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
    }
}

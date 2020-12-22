using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public partial class ParentAddress
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public bool StudentAddressSame { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ParentInfo ParentInfo { get; set; }
        public virtual StudentMaster StudentMaster { get; set; }
    }
}

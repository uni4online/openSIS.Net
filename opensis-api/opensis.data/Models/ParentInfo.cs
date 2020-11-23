using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class ParentInfo
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public string Relationship { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool? StudentAddressSame { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsCustodian { get; set; }
        public bool IsPortalUser { get; set; }
        public string PortalUserId { get; set; }
        public string BusNo { get; set; }
        public bool? BusPickup { get; set; }
        public bool? BusDropoff { get; set; }
        public string ContactType { get; set; }
        public string Associationship { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual StudentMaster StudentMaster { get; set; }
    }
}

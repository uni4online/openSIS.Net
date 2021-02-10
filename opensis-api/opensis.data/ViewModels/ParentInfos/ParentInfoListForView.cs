using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.ParentInfos
{
    public class ParentInfoListForView 
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int ParentId { get; set; }
        public byte[] ParentPhoto { get; set; }
        public string Salutation { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Mobile { get; set; }
        public string PersonalEmail { get; set; }
        public string WorkEmail { get; set; }
        public string UserProfile { get; set; }
        public bool IsPortalUser { get; set; }
        public string LoginEmail { get; set; }
        public string Suffix { get; set; }
        public string BusNo { get; set; }
        public bool? BusPickup { get; set; }
        public bool? BusDropoff { get; set; }
        public string ContactType { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }      
        public string Relationship { get; set; }
        public bool? IsCustodian { get; set; }
        public ParentAddress ParentAddress { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
    public class GetStaffListForView
    {
        public Guid TenantId { get; set; }
        public int StaffId { get; set; }
        public string FirstGivenName { get; set; }
        public string MiddleName { get; set; }
        public string LastFamilyName { get; set; }
        public string StaffInternalId { get; set; }
        public string Profile { get; set; }
        public string JobTitle { get; set; }
        public string SchoolEmail { get; set; }
        public string MobilePhone { get; set; }
    }
}

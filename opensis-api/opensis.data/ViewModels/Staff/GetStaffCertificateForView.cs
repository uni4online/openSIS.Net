using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
    public class GetStaffCertificateForView
    {
        public int Id { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? StaffId { get; set; }
        public string CertificationName { get; set; }
        public string ShortName { get; set; }
        public string CertificationCode { get; set; }
        public bool? PrimaryCertification { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? CertificationExpiryDate { get; set; }
        public string CertificationDescription { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
    public class StaffCertificateInfoListModel : CommonFields
    {
        public List<StaffCertificateInfo> staffCertificateInfoList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? StaffId { get; set; }

    }
}

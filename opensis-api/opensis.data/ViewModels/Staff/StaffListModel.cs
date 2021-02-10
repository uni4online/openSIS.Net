using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
    public class StaffListModel : CommonFields
    {
        public List<StaffMaster> staffMaster { get; set; }
        public List<GetStaffListForView> getStaffListForView { get; set; }
        public Guid? TenantId { get; set; }
        public int? TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public int? _pageSize { get; set; }

    }
}

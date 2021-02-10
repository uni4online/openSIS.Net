using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
    public class CheckStaffInternalIdViewModel : CommonFields
    {
        public Guid TenantId { get; set; }
        public string StaffInternalId { get; set; }
        public bool IsValidInternalId { get; set; }
    }
}

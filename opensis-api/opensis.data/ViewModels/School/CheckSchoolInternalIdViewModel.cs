using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.School
{
    public class CheckSchoolInternalIdViewModel : CommonFields
    {
        public Guid TenantId { get; set; }
        public string SchoolInternalId { get; set; }
        public bool IsValidInternalId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Grades
{
    public class CheckStandardRefNoViewModel : CommonFields
    {
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string StandardRefNo { get; set; }
        public bool IsValidStandardRefNo { get; set; }
    }
}

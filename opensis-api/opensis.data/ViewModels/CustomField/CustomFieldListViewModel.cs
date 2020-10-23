using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CustomField
{
    public class CustomFieldListViewModel : CommonFields
    {
        public List<CustomFields> customFieldsList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
    }
}

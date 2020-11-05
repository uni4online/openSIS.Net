using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CustomField
{
    public class FieldsCategoryListViewModel : CommonFields
    {
        public List<FieldsCategory> fieldsCategoryList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string Module { get; set; }
    }
}

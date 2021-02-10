using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CustomField
{
    public class CustomFieldSortOrderModel : CommonFields
    {
        public int SchoolId { get; set; }
        public int? CategoryId { get; set; }
        public int? PreviousSortOrder { get; set; }
        public int? CurrentSortOrder { get; set; }
    }
}

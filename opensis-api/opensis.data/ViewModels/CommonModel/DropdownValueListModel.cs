using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CommonModel
{
    public class DropdownValueListModel : CommonFields
    {
        public List<DpdownValuelist> dropdownList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string LovName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Period
{
    public class BlockPeriodSortOrderViewModel : CommonFields
    {
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? BlockId { get; set; }
        public int? PreviousSortOrder { get; set; }
        public int? CurrentSortOrder { get; set; }
    }
}

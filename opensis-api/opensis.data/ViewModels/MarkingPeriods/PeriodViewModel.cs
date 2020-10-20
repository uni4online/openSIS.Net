using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.MarkingPeriods
{
    public class PeriodViewModel : CommonFields
    {
        public int SchoolId { get; set; }
        public Guid TenantId { get; set; }
        public decimal AcademicYear { get; set; }
        public List<PeriodView> period { get; set; }
    }
}

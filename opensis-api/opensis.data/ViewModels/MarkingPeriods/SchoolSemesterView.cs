using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.MarkingPeriods
{
    public class SchoolSemesterView
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int MarkingPeriodId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int YearId { get; set; }
        public bool IsParent { get; set; }
        public List<SchoolQuarterView> Children { get; set; }
    }
}

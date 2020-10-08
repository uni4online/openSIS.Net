using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.MarkingPeriods
{
    public class SchoolQuarterView
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int MarkingPeriodId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int SemesterId { get; set; }
        public bool IsParent { get; set; }
        public List<SchoolProgressPeriodView> Children { get; set; }
    }
}

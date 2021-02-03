using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.MarkingPeriods
{
    public class MarkingPeriod: CommonFields
    {
        public MarkingPeriod()
        {
            schoolYearsView = new List<SchoolYearView>();
        }
        public List<SchoolYearView> schoolYearsView { get; set; }
        public int SchoolId { get; set; }
        public Guid TenantId { get; set; }
        public decimal? AcademicYear { get; set; }
    }
}

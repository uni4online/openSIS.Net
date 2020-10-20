using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.MarkingPeriods
{
    public class DropDownViewModel: CommonFields
    {
        public List<AcademicYear> AcademicYears { get; set; }
        public int SchoolId { get; set; }
        public Guid TenantId { get; set; }
    }
}

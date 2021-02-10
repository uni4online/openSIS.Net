using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Grades
{
    public class GetEffortGradeScaleForView
    {
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? EffortGradeScaleId { get; set; }
        public int? GradeScaleValue { get; set; }
        public string GradeScaleComment { get; set; }
        public int? SortOrder { get; set; }
    }
}

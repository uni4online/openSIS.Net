using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Grades
{
    public class GradeSortOrderModel : CommonFields
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int GradeScaleId { get; set; }
        public int? PreviousSortOrder { get; set; }
        public int? CurrentSortOrder { get; set; }
    }
}

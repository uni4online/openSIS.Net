using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Grades
{
    public class EffortGradeLlibraryCategoryListViewModel : CommonFields
    {
        public List<EffortGradeLibraryCategory> effortGradeLibraryCategoryList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
    }
}


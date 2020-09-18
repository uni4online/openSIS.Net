using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.School
{
    public class SchoolListModel: CommonFields
    {
        public List<GetSchoolForView> GetSchoolForView { get; set; }
        public Guid? TenantId { get; set; }
        public int? TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public int? _pageSize { get; set; }
    }
}

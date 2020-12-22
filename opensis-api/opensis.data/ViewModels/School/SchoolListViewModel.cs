using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.School
{
    public class SchoolListViewModel: CommonFields
    {
        public List<SchoolMaster> schoolMaster { get; set; }
        public Guid? TenantId { get; set; }
    }
}

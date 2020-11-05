using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class SearchContactViewModel:CommonFields
    {
        public List<StudentMaster> studentMaster { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string FilterValue { get; set; }

    }
}

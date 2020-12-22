using opensis.data.Models;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.ParentInfos
{
    public class GetAllParentInfoListForView : CommonFields
    {
        public List<GetParentInfoForView> parentInfoForView { get; set; }
        
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }      
        public int? StudentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }      
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public int? _pageSize { get; set; }
    }
}

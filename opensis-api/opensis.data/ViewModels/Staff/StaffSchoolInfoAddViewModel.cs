using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
    public class StaffSchoolInfoAddViewModel : CommonFields
    {
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? StaffId { get; set; }
        public string Profile { get; set; }
        public string JobTitle { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? HomeroomTeacher { get; set; }
        public string PrimaryGradeLevelTaught { get; set; }
        public string PrimarySubjectTaught { get; set; }
        public string OtherGradeLevelTaught { get; set; }
        public string OtherSubjectTaught { get; set; }
        public List<StaffSchoolInfo> staffSchoolInfoList { get; set; }
    }
}

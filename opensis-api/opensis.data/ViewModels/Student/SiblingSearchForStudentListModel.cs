using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class SiblingSearchForStudentListModel : CommonFields
    {
        public List<StudentMaster> studentMasterList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string FirstGivenName { get; set; }
        public string LastFamilyName { get; set; }
    }
}

using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class SiblingSearchForStudentListModel : CommonFields
    {
        public List<GetStudentForView> getStudentForView { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string StudentInternalId { get; set; }
        public int? GradeId { get; set; }
        public string FirstGivenName { get; set; }
        public string LastFamilyName { get; set; }
        public DateTime? Dob { get; set; }
    }
}

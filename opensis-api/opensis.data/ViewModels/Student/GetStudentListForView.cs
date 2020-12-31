using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class GetStudentListForView
    {
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? StudentId { get; set; }
        public string AlternateId { get; set; }
        public string StudentInternalId { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string FirstGivenName { get; set; }
        public string MiddleName { get; set; }
        public string LastFamilyName { get; set; }
        public string PersonalEmail { get; set; }  
        public string GradeLevelTitle { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public Guid StudentGuid { get; set; }

    }
}

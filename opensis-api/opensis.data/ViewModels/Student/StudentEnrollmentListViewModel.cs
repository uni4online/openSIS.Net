using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class StudentEnrollmentListViewModel : CommonFields
    {
        public List<StudentEnrollmentListForView> studentEnrollmentListForView { get; set; }
        public Guid? TenantId { get; set; }
        public int StudentId { get; set; }
        public int? CalenderId { get; set; }
        public string AcademicYear { get; set; }
        public string RollingOption { get; set; }
        public int? SchoolId { get; set; }
        public Guid? StudentGuid { get; set; }
    }
}

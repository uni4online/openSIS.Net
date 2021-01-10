using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class StudentEnrollmentListModel : CommonFields
    {
        public List<StudentEnrollment> studentEnrollments { get; set; }
        public Guid? TenantId { get; set; }
        public int StudentId { get; set; }
        public Guid StudentGuid { get; set; }
        public int SchoolId { get; set; }
        public int? CalenderId { get; set; }
        public string RollingOption { get; set; }
        public decimal? AcademicYear { get; set; }
    }
}

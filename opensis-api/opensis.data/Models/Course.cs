using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Course
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseShortName { get; set; }
        public string CourseGradeLevel { get; set; }
        public string CourseProgram { get; set; }
        public string CourseSubject { get; set; }
        public string CourseCategory { get; set; }
        public string CreditHours { get; set; }
        public string CourseDescription { get; set; }
        public bool? IsCourseActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

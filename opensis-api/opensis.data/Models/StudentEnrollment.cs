using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class StudentEnrollment
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int? EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int? GradeId { get; set; }
        public int? SectionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EnrollmentCode { get; set; }
        public int? DropCode { get; set; }
        public int? NextSchool { get; set; }
        public int? CalendarId { get; set; }
        public int? LastSchool { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public virtual StudentMaster StudentMaster { get; set; }
        public virtual Gradelevels Gradelevels { get; set; }
        public virtual Sections Sections { get; set; }
    }
}

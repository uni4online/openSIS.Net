using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class AttendanceCodeCategories
    {
        public AttendanceCodeCategories()
        {
            AttendanceCode = new HashSet<AttendanceCode>();
            CourseSection = new HashSet<CourseSection>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int AttendanceCategoryId { get; set; }
        public decimal? AcademicYear { get; set; }
        public string Title { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual SchoolMaster SchoolMaster { get; set; }
        public virtual ICollection<AttendanceCode> AttendanceCode { get; set; }
        public virtual ICollection<CourseSection> CourseSection { get; set; }
    }
}

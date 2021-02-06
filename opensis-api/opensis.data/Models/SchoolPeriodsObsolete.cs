using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class SchoolPeriodsObsolete
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int PeriodId { get; set; }
        public decimal? AcademicYear { get; set; }
        public int? SortOrder { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public decimal? Length { get; set; }
        public string Block { get; set; }
        public string IgnoreScheduling { get; set; }
        public bool? Attendance { get; set; }
        public int? RolloverId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
    }

}

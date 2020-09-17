using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableSchoolPeriods
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int PeriodId { get; set; }
        public decimal? AcademicYear { get; set; }
        public decimal? SortOrder { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public decimal? Length { get; set; }
        public string Block { get; set; }
        public string IgnoreScheduling { get; set; }
        public string Attendance { get; set; }
        public decimal? RolloverId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual TableSchoolMaster TableSchoolMaster { get; set; }
    }
}

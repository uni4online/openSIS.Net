using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
  public partial  class CalendarEvents
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int CalendarId { get; set; }
        public int EventId { get; set; }
        public decimal? AcademicYear { get; set; }
        public DateTime? SchoolDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VisibleToMembershipId { get; set; }
        public string EventColor { get; set; }
        public bool? SystemWideEvent { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
    }
}

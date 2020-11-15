using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.CalendarEvents
{
    public class CalendarEventListViewModel : CommonFields
    {
        public List<opensis.data.Models.CalendarEvents> calendarEventList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? CalendarId { get; set; }
        public int? AcademicYear { get; set; }
    }
}

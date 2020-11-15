using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Calendar
{
    public class CalendarListModel : CommonFields
    {
        public List<SchoolCalendars> CalendarList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? AcademicYear { get; set; }
    }
}

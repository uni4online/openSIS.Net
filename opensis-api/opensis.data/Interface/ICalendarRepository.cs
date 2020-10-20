using opensis.data.ViewModels.Calendar;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface ICalendarRepository
    {
        CalendarAddViewModel AddCalendar(CalendarAddViewModel calendar);
        CalendarAddViewModel ViewCalendar(CalendarAddViewModel calendar);
        CalendarAddViewModel UpdateCalendar(CalendarAddViewModel calendar);
        CalendarListModel GetAllCalendar(CalendarListModel calendarList);
        CalendarAddViewModel DeleteCalendar(CalendarAddViewModel calendar);

    }
}

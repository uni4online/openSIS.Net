using opensis.data.ViewModels.CalendarEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.CalendarEvents.Interfaces
{
    public interface ICalendarEventService
    {
        CalendarEventAddViewModel AddCalendarEvent(CalendarEventAddViewModel calendarEvent);
        CalendarEventAddViewModel ViewCalendarEvent(CalendarEventAddViewModel calendarEvent);
        CalendarEventAddViewModel UpdateCalendarEvent(CalendarEventAddViewModel calendarEvent);
        CalendarEventListViewModel GetAllCalendarEvent(CalendarEventListViewModel calendarEventList);
        CalendarEventAddViewModel DeleteCalendarEvent(CalendarEventAddViewModel calendarEvent);
    }
}

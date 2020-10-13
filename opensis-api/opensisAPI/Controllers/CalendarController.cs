using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Calender.Interfaces;
using opensis.data.ViewModels.Calendar;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private ICalendarService _calendarService;
        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpPost("addCalendar")]
        public ActionResult<CalendarAddViewModel> AddCalendar(CalendarAddViewModel calendar)
        {
            CalendarAddViewModel calendarAdd = new CalendarAddViewModel();
            try
            {
                calendarAdd = _calendarService.AddCalendar(calendar);
            }
            catch (Exception es)
            {
                calendarAdd._failure = true;
                calendarAdd._message = es.Message;
            }
            return calendarAdd;
        }

        [HttpPost("viewCalendar")]
        public ActionResult<CalendarAddViewModel> ViewCalendar(CalendarAddViewModel room)
        {
            CalendarAddViewModel viewCalendar = new CalendarAddViewModel();
            try
            {
                viewCalendar = _calendarService.ViewCalendar(room);
            }
            catch (Exception es)
            {
                viewCalendar._failure = true;
                viewCalendar._message = es.Message;
            }
            return viewCalendar;
        }

        [HttpPut("updateCalendar")]
        public ActionResult<CalendarAddViewModel> UpdateCalendar(CalendarAddViewModel calendar)
        {
            CalendarAddViewModel calendarUpdate = new CalendarAddViewModel();
            try
            {
                calendarUpdate = _calendarService.UpdateCalendar(calendar);
            }
            catch (Exception es)
            {
                calendarUpdate._failure = true;
                calendarUpdate._message = es.Message;
            }
            return calendarUpdate;
        }

        [HttpPost("getAllCalendar")]
        public ActionResult<CalendarListModel> GetAllCalendar(CalendarListModel calendarList)
        {
            CalendarListModel calendarListModel = new CalendarListModel();
            try
            {
                calendarListModel = _calendarService.GetAllCalendar(calendarList);
            }
            catch (Exception es)
            {
                calendarListModel._message = es.Message;
                calendarListModel._failure = true;
            }
            return calendarListModel;
        }
    }
}

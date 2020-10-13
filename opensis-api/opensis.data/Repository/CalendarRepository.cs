using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class CalendarRepository : ICalendarRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public CalendarRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }


        /// <summary>
        /// Add calendar
        /// </summary>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public CalendarAddViewModel AddCalendar(CalendarAddViewModel calendar)
        {
            int? calenderId = Utility.GetMaxPK(this.context, new Func<SchoolCalendars, int>(x => x.CalenderId));
            calendar.schoolCalendar.CalenderId = (int)calenderId;

            if (calendar.schoolCalendar.DefaultCalender == true)
            {
                (from p in this.context?.SchoolCalendars
                 where p.TenantId == calendar.schoolCalendar.TenantId && p.SchoolId == calendar.schoolCalendar.SchoolId
                 select p).ToList().ForEach(x => x.DefaultCalender = false);
            }
            calendar.schoolCalendar.DefaultCalender = calendar.schoolCalendar.DefaultCalender;
            this.context?.SchoolCalendars.Add(calendar.schoolCalendar);
            this.context?.SaveChanges();

            return calendar;
        }

        /// <summary>
        /// Get Calender By Id
        /// </summary>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public CalendarAddViewModel ViewCalendar(CalendarAddViewModel calendar)
        {
            try
            {
                CalendarAddViewModel calendarAddViewModel = new CalendarAddViewModel();
                var calendarRepository = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == calendar.schoolCalendar.TenantId && x.SchoolId == calendar.schoolCalendar.SchoolId && x.CalenderId == calendar.schoolCalendar.CalenderId);
                if (calendarRepository != null)
                {
                    calendar.schoolCalendar = calendarRepository;
                    calendar._tenantName = calendar._tenantName;
                    calendar._failure = false;
                    return calendar;
                }
                else
                {
                    calendarAddViewModel._failure = true;
                    calendarAddViewModel._message = NORECORDFOUND;
                    return calendarAddViewModel;
                }
            }
            catch (Exception es)
            {

                throw;
            }
        }

        /// <summary>
        /// Update Calendar
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public CalendarAddViewModel UpdateCalendar(CalendarAddViewModel calendar)
        {
            try
            {
                var calendarRepository = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == calendar.schoolCalendar.TenantId && x.SchoolId == calendar.schoolCalendar.SchoolId && x.CalenderId == calendar.schoolCalendar.CalenderId);
                calendarRepository.SchoolId = calendar.schoolCalendar.SchoolId;
                calendarRepository.TenantId = calendar.schoolCalendar.TenantId;
                calendarRepository.Title = calendar.schoolCalendar.Title;
                calendarRepository.AcademicYear = calendar.schoolCalendar.AcademicYear;
                if (calendar.schoolCalendar.DefaultCalender == true)
                {
                    (from p in this.context?.SchoolCalendars
                     where p.CalenderId != calendarRepository.CalenderId && p.TenantId == calendar.schoolCalendar.TenantId && p.SchoolId == calendar.schoolCalendar.SchoolId
                     select p).ToList().ForEach(x => x.DefaultCalender = false);

                }
                calendarRepository.DefaultCalender = calendar.schoolCalendar.DefaultCalender;
                calendarRepository.Days = calendar.schoolCalendar.Days;
                calendarRepository.LastUpdated = DateTime.UtcNow;
                calendarRepository.VisibleToMembershipId = calendar.schoolCalendar.VisibleToMembershipId;
                calendarRepository.UpdatedBy = calendar.schoolCalendar.UpdatedBy;
                calendarRepository.StartDate = calendar.schoolCalendar.StartDate;
                calendarRepository.StartDate = calendar.schoolCalendar.EndDate;
                this.context?.SaveChanges();
                calendar._failure = false;
                return calendar;
            }
            catch (Exception ex)
            {
                calendar.schoolCalendar = null;
                calendar._failure = true;
                calendar._message = NORECORDFOUND;
                return calendar;
            }
        }
        /// <summary>
        /// Get All Calendar
        /// </summary>
        /// <param name="calendarList"></param>
        /// <returns></returns>
        public CalendarListModel GetAllCalendar(CalendarListModel calendarList)
        {
            CalendarListModel calendarListModel = new CalendarListModel();
            try
            {
                var calendarRepository = this.context?.SchoolCalendars.Where(x => x.TenantId == calendarList.TenantId && x.SchoolId == calendarList.SchoolId).OrderBy(x => x.Title).ToList();
                calendarListModel.CalendarList = calendarRepository;
                calendarListModel._tenantName = calendarList._tenantName;
                calendarListModel._token = calendarList._token;
                calendarListModel._failure = false;
            }
            catch (Exception es)
            {
                calendarListModel._message = es.Message;
                calendarListModel._failure = true;
                calendarListModel._tenantName = calendarList._tenantName;
                calendarListModel._token = calendarList._token;
            }
            return calendarListModel;

        }
    }
}

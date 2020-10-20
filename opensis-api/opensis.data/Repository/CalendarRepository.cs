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
            calendar.schoolCalendar.LastUpdated = DateTime.UtcNow;
            var checkDefaultCalendar = this.context?.SchoolCalendars.Where(x => x.AcademicYear == calendar.schoolCalendar.AcademicYear && x.TenantId == calendar.schoolCalendar.TenantId && x.SchoolId == calendar.schoolCalendar.SchoolId).ToList().Find(x => x.DefaultCalender == true);
            if (checkDefaultCalendar == null)
            {
                calendar.schoolCalendar.DefaultCalender = true;
            }
            if (calendar.schoolCalendar.DefaultCalender == true)
            {
                (from p in this.context?.SchoolCalendars
                 where p.TenantId == calendar.schoolCalendar.TenantId && p.SchoolId == calendar.schoolCalendar.SchoolId && p.AcademicYear == calendar.schoolCalendar.AcademicYear
                 select p).ToList().ForEach(x => x.DefaultCalender = false);
            }

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
        /// <param name="calendar"></param>
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
                var checkDefaultCalendar = this.context?.SchoolCalendars.Where(x => x.AcademicYear == calendar.schoolCalendar.AcademicYear && x.TenantId == calendar.schoolCalendar.TenantId && x.SchoolId == calendar.schoolCalendar.SchoolId && x.CalenderId != calendar.schoolCalendar.CalenderId).ToList().Find(x => x.DefaultCalender == true);
                if (checkDefaultCalendar == null)
                {
                    calendarRepository.DefaultCalender = true;
                }
                else
                {
                    calendarRepository.DefaultCalender = calendar.schoolCalendar.DefaultCalender;
                }
                if (calendar.schoolCalendar.DefaultCalender == true)
                {
                    (from p in this.context?.SchoolCalendars
                     where p.CalenderId != calendarRepository.CalenderId && p.AcademicYear == calendar.schoolCalendar.AcademicYear && p.TenantId == calendar.schoolCalendar.TenantId && p.SchoolId == calendar.schoolCalendar.SchoolId
                     select p).ToList().ForEach(x => x.DefaultCalender = false);

                }

                calendarRepository.Days = calendar.schoolCalendar.Days;
                calendarRepository.LastUpdated = DateTime.UtcNow;
                calendarRepository.VisibleToMembershipId = calendar.schoolCalendar.VisibleToMembershipId;
                calendarRepository.UpdatedBy = calendar.schoolCalendar.UpdatedBy;
                calendarRepository.StartDate = calendar.schoolCalendar.StartDate;
                calendarRepository.EndDate = calendar.schoolCalendar.EndDate;
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


        /// <summary>
        /// Delete Calendar
        /// </summary>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public CalendarAddViewModel DeleteCalendar(CalendarAddViewModel calendar)
        {
            try
            {
                var calendarRepository = this.context?.SchoolCalendars.Where(x => x.CalenderId == calendar.schoolCalendar.CalenderId).ToList().OrderBy(x => x.CalenderId).LastOrDefault();
                if (calendarRepository != null)
                {
                    var eventsExist = this.context?.CalendarEvents.FirstOrDefault(x => x.TenantId == calendarRepository.TenantId && x.SchoolId == calendarRepository.SchoolId && x.CalendarId == calendarRepository.CalenderId);
                    if (eventsExist != null)
                    {
                        calendar._message = "Calendar cannot deleted because it has event.";
                        calendar._failure = true;
                    }
                    else
                    {
                        this.context?.SchoolCalendars.Remove(calendarRepository);
                        this.context?.SaveChanges();
                        calendar._failure = false;
                        calendar._message = "Deleted";
                    }
                }

                return calendar;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}

using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.AttendanceCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class AttendanceCodeRepository : IAttendanceCodeRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public AttendanceCodeRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        /// <summary>
        /// Add AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel AddAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            int? AttendanceCodeId = Utility.GetMaxPK(this.context, new Func<AttendanceCode, int>(x => x.AttendanceCode1));
            attendanceCodeAddViewModel.attendanceCode.AttendanceCode1 = (int)AttendanceCodeId;
            attendanceCodeAddViewModel.attendanceCode.LastUpdated = DateTime.UtcNow;            
            this.context?.AttendanceCode.Add(attendanceCodeAddViewModel.attendanceCode);
            this.context?.SaveChanges();
            attendanceCodeAddViewModel._failure = false;

            return attendanceCodeAddViewModel;
        }
        /// <summary>
        /// Get AttendanceCode By Id
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel ViewAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeModel = new AttendanceCodeAddViewModel();
            try
            {                
                var attendanceCodeMaster = this.context?.AttendanceCode.FirstOrDefault(x => x.TenantId == attendanceCodeAddViewModel.attendanceCode.TenantId && x.SchoolId == attendanceCodeAddViewModel.attendanceCode.SchoolId && x.AttendanceCode1 == attendanceCodeAddViewModel.attendanceCode.AttendanceCode1);
                if (attendanceCodeMaster != null)
                {
                    attendanceCodeModel.attendanceCode = attendanceCodeMaster;
                    attendanceCodeAddViewModel._failure = false;                    
                }
                else
                {
                    attendanceCodeModel._failure = true;
                    attendanceCodeModel._message = NORECORDFOUND;                    
                }
            }
            catch (Exception es)
            {

                attendanceCodeModel._failure = true;
                attendanceCodeModel._message = es.Message;
            }
            return attendanceCodeModel;
        }
        /// <summary>
        /// Update AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel UpdateAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeUpdate = new AttendanceCodeAddViewModel();
            try
            {
                var AttendanceCode = this.context?.AttendanceCode.FirstOrDefault(x => x.TenantId == attendanceCodeAddViewModel.attendanceCode.TenantId && x.SchoolId == attendanceCodeAddViewModel.attendanceCode.SchoolId && x.AttendanceCode1 == attendanceCodeAddViewModel.attendanceCode.AttendanceCode1);
                AttendanceCode.AcademicYear = attendanceCodeAddViewModel.attendanceCode.AcademicYear;
                AttendanceCode.Title = attendanceCodeAddViewModel.attendanceCode.Title;
                AttendanceCode.ShortName = attendanceCodeAddViewModel.attendanceCode.ShortName;
                AttendanceCode.Type = attendanceCodeAddViewModel.attendanceCode.Type;
                AttendanceCode.StateCode = attendanceCodeAddViewModel.attendanceCode.StateCode;
                AttendanceCode.DefaultCode = attendanceCodeAddViewModel.attendanceCode.DefaultCode;
                AttendanceCode.AllowEntryBy = attendanceCodeAddViewModel.attendanceCode.AllowEntryBy;
                AttendanceCode.SortOrder = attendanceCodeAddViewModel.attendanceCode.SortOrder;                
                AttendanceCode.LastUpdated = DateTime.UtcNow;
                AttendanceCode.UpdatedBy = attendanceCodeAddViewModel.attendanceCode.UpdatedBy;
                this.context?.SaveChanges();
                attendanceCodeAddViewModel._failure = false;
                attendanceCodeAddViewModel._message = "Entity Updated";
            }
            catch (Exception es)
            {
                attendanceCodeAddViewModel._failure = true;
                attendanceCodeAddViewModel._message = es.Message;
            }
            return attendanceCodeAddViewModel;
        }
        /// <summary>
        /// Delete AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel DeleteAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            try
            {
                var AttendanceCode = this.context?.AttendanceCode.FirstOrDefault(x => x.TenantId == attendanceCodeAddViewModel.attendanceCode.TenantId && x.SchoolId == attendanceCodeAddViewModel.attendanceCode.SchoolId && x.AttendanceCode1 == attendanceCodeAddViewModel.attendanceCode.AttendanceCode1);
                this.context?.AttendanceCode.Remove(AttendanceCode);
                this.context?.SaveChanges();
                attendanceCodeAddViewModel._failure = false;
                attendanceCodeAddViewModel._message = "Deleted";
            }
            catch (Exception es)
            {
                attendanceCodeAddViewModel._failure = true;
                attendanceCodeAddViewModel._message = es.Message;
            }
            return attendanceCodeAddViewModel;
        }
    }
}

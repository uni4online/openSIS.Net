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
            AttendanceCodeAddViewModel attendanceCodeViewModel = new AttendanceCodeAddViewModel();
            try
            {                
                var attendanceCodeView = this.context?.AttendanceCode.FirstOrDefault(x => x.TenantId == attendanceCodeAddViewModel.attendanceCode.TenantId && x.SchoolId == attendanceCodeAddViewModel.attendanceCode.SchoolId && x.AttendanceCode1 == attendanceCodeAddViewModel.attendanceCode.AttendanceCode1);
                if (attendanceCodeView != null)
                {
                    attendanceCodeViewModel.attendanceCode = attendanceCodeView;
                    attendanceCodeAddViewModel._failure = false;                    
                }
                else
                {
                    attendanceCodeViewModel._failure = true;
                    attendanceCodeViewModel._message = NORECORDFOUND;                    
                }
            }
            catch (Exception es)
            {

                attendanceCodeViewModel._failure = true;
                attendanceCodeViewModel._message = es.Message;
            }
            return attendanceCodeViewModel;
        }
        /// <summary>
        /// Update AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeAddViewModel UpdateAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeUpdateModel = new AttendanceCodeAddViewModel();
            try
            {
                var attendanceCodeUpdate = this.context?.AttendanceCode.FirstOrDefault(x => x.TenantId == attendanceCodeAddViewModel.attendanceCode.TenantId && x.SchoolId == attendanceCodeAddViewModel.attendanceCode.SchoolId && x.AttendanceCode1 == attendanceCodeAddViewModel.attendanceCode.AttendanceCode1);
                attendanceCodeUpdate.AttendanceCategoryId = attendanceCodeAddViewModel.attendanceCode.AttendanceCategoryId;
                attendanceCodeUpdate.AcademicYear = attendanceCodeAddViewModel.attendanceCode.AcademicYear;
                attendanceCodeUpdate.Title = attendanceCodeAddViewModel.attendanceCode.Title;
                attendanceCodeUpdate.ShortName = attendanceCodeAddViewModel.attendanceCode.ShortName;
                attendanceCodeUpdate.Type = attendanceCodeAddViewModel.attendanceCode.Type;
                attendanceCodeUpdate.StateCode = attendanceCodeAddViewModel.attendanceCode.StateCode;
                attendanceCodeUpdate.DefaultCode = attendanceCodeAddViewModel.attendanceCode.DefaultCode;
                attendanceCodeUpdate.AllowEntryBy = attendanceCodeAddViewModel.attendanceCode.AllowEntryBy;
                attendanceCodeUpdate.SortOrder = attendanceCodeAddViewModel.attendanceCode.SortOrder;
                attendanceCodeUpdate.LastUpdated = DateTime.UtcNow;
                attendanceCodeUpdate.UpdatedBy = attendanceCodeAddViewModel.attendanceCode.UpdatedBy;
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
        /// Get All AttendanceCode
        /// </summary>
        /// <param name="attendanceCodeListViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeListViewModel GetAllAttendanceCode(AttendanceCodeListViewModel attendanceCodeListViewModel)
        {
            AttendanceCodeListViewModel attendanceCodeListModel = new AttendanceCodeListViewModel();
            try
            {

                var attendanceCodeList = this.context?.AttendanceCode.Where(x => x.TenantId == attendanceCodeListViewModel.TenantId && x.SchoolId == attendanceCodeListViewModel.SchoolId && x.AttendanceCategoryId== attendanceCodeListViewModel.AttendanceCategoryId).OrderBy(x => x.SortOrder).ToList();
                attendanceCodeListModel.attendanceCodeList = attendanceCodeList;
                attendanceCodeListModel._tenantName = attendanceCodeListViewModel._tenantName;
                attendanceCodeListModel._token = attendanceCodeListViewModel._token;
                attendanceCodeListModel._failure = false;
            }
            catch (Exception es)
            {
                attendanceCodeListModel._message = es.Message;
                attendanceCodeListModel._failure = true;
                attendanceCodeListModel._tenantName = attendanceCodeListViewModel._tenantName;
                attendanceCodeListModel._token = attendanceCodeListViewModel._token;
            }
            return attendanceCodeListModel;
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
                var attendanceCodeDelete = this.context?.AttendanceCode.FirstOrDefault(x => x.TenantId == attendanceCodeAddViewModel.attendanceCode.TenantId && x.SchoolId == attendanceCodeAddViewModel.attendanceCode.SchoolId && x.AttendanceCode1 == attendanceCodeAddViewModel.attendanceCode.AttendanceCode1);
                this.context?.AttendanceCode.Remove(attendanceCodeDelete);
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
        /// <summary>
        /// Add AttendanceCodeCategories
        /// </summary>
        /// <param name="attendanceCodeCategoriesAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeCategoriesAddViewModel AddAttendanceCodeCategories(AttendanceCodeCategoriesAddViewModel attendanceCodeCategoriesAddViewModel)
        {
            int? AttendanceCodeCategoryId = Utility.GetMaxPK(this.context, new Func<AttendanceCodeCategories, int>(x => x.AttendanceCategoryId));
            attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.AttendanceCategoryId = (int)AttendanceCodeCategoryId;
            attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.LastUpdated = DateTime.UtcNow;
            this.context?.AttendanceCodeCategories.Add(attendanceCodeCategoriesAddViewModel.attendanceCodeCategories);
            this.context?.SaveChanges();
            attendanceCodeCategoriesAddViewModel._failure = false;

            return attendanceCodeCategoriesAddViewModel;
        }
        /// <summary>
        /// Get AttendanceCodeCategories By Id
        /// </summary>
        /// <param name="attendanceCodeCategoriesAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeCategoriesAddViewModel ViewAttendanceCodeCategories(AttendanceCodeCategoriesAddViewModel attendanceCodeCategoriesAddViewModel)
        {
            AttendanceCodeCategoriesAddViewModel attendanceCodeCategoriesViewModel = new AttendanceCodeCategoriesAddViewModel();
            try
            {
                var attendanceCodeCategoriesView = this.context?.AttendanceCodeCategories.FirstOrDefault(x => x.TenantId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.TenantId && x.SchoolId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.SchoolId && x.AttendanceCategoryId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.AttendanceCategoryId);
                if (attendanceCodeCategoriesView != null)
                {
                    attendanceCodeCategoriesViewModel.attendanceCodeCategories = attendanceCodeCategoriesView;
                    attendanceCodeCategoriesViewModel._failure = false;
                }
                else
                {
                    attendanceCodeCategoriesViewModel._failure = true;
                    attendanceCodeCategoriesViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                attendanceCodeCategoriesViewModel._failure = true;
                attendanceCodeCategoriesViewModel._message = es.Message;
            }
            return attendanceCodeCategoriesViewModel;
        }
        /// <summary>
        /// Update AttendanceCodeCategories
        /// </summary>
        /// <param name="attendanceCodeCategoriesAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeCategoriesAddViewModel UpdateAttendanceCodeCategories(AttendanceCodeCategoriesAddViewModel attendanceCodeCategoriesAddViewModel)
        {
            AttendanceCodeCategoriesAddViewModel attendanceCodeCategoriesUpdateModel = new AttendanceCodeCategoriesAddViewModel();
            try
            {
                var attendanceCodeCategoriesUpdate = this.context?.AttendanceCodeCategories.FirstOrDefault(x => x.TenantId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.TenantId && x.SchoolId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.SchoolId && x.AttendanceCategoryId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.AttendanceCategoryId);
                attendanceCodeCategoriesUpdate.AcademicYear = attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.AcademicYear;
                attendanceCodeCategoriesUpdate.Title = attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.Title;
                attendanceCodeCategoriesUpdate.LastUpdated = DateTime.UtcNow;
                attendanceCodeCategoriesUpdate.UpdatedBy = attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.UpdatedBy;
                this.context?.SaveChanges();
                attendanceCodeCategoriesAddViewModel._failure = false;
                attendanceCodeCategoriesAddViewModel._message = "Entity Updated";
            }
            catch (Exception es)
            {
                attendanceCodeCategoriesAddViewModel._failure = true;
                attendanceCodeCategoriesAddViewModel._message = es.Message;
            }
            return attendanceCodeCategoriesAddViewModel;
        }
        /// <summary>
        /// Get All  AttendanceCodeCategories
        /// </summary>
        /// <param name="attendanceCodeCategoriesListViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeCategoriesListViewModel GetAllAttendanceCodeCategories(AttendanceCodeCategoriesListViewModel attendanceCodeCategoriesListViewModel)
        {
            AttendanceCodeCategoriesListViewModel attendanceCodeCategoriesListModel = new AttendanceCodeCategoriesListViewModel();
            try
            {

                var attendanceCodeCategoriesList = this.context?.AttendanceCodeCategories.Where(x => x.TenantId == attendanceCodeCategoriesListViewModel.TenantId && x.SchoolId == attendanceCodeCategoriesListViewModel.SchoolId).ToList();
                attendanceCodeCategoriesListModel.attendanceCodeCategoriesList = attendanceCodeCategoriesList;
                attendanceCodeCategoriesListModel._tenantName = attendanceCodeCategoriesListViewModel._tenantName;
                attendanceCodeCategoriesListModel._token = attendanceCodeCategoriesListViewModel._token;
                attendanceCodeCategoriesListModel._failure = false;
            }
            catch (Exception es)
            {
                attendanceCodeCategoriesListModel._message = es.Message;
                attendanceCodeCategoriesListModel._failure = true;
                attendanceCodeCategoriesListModel._tenantName = attendanceCodeCategoriesListViewModel._tenantName;
                attendanceCodeCategoriesListModel._token = attendanceCodeCategoriesListViewModel._token;
            }
            return attendanceCodeCategoriesListModel;
        }
        /// <summary>
        /// Delete AttendanceCodeCategories
        /// </summary>
        /// <param name="attendanceCodeCategoriesAddViewModel"></param>
        /// <returns></returns>
        public AttendanceCodeCategoriesAddViewModel DeleteAttendanceCodeCategories(AttendanceCodeCategoriesAddViewModel attendanceCodeCategoriesAddViewModel)
        {
            try
            {
                var attendanceCodeCategoriesDelete = this.context?.AttendanceCodeCategories.FirstOrDefault(x => x.TenantId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.TenantId && x.SchoolId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.SchoolId && x.AttendanceCategoryId == attendanceCodeCategoriesAddViewModel.attendanceCodeCategories.AttendanceCategoryId);
                var AttendanceCodeExist = this.context?.AttendanceCode.FirstOrDefault(x => x.TenantId == attendanceCodeCategoriesDelete.TenantId && x.SchoolId == attendanceCodeCategoriesDelete.SchoolId && x.AttendanceCategoryId == attendanceCodeCategoriesDelete.AttendanceCategoryId);
                if (AttendanceCodeExist != null)
                {
                    attendanceCodeCategoriesAddViewModel.attendanceCodeCategories = null;
                    attendanceCodeCategoriesAddViewModel._message = "AttendanceCodeCategory cannot be deleted because it has its association";
                    attendanceCodeCategoriesAddViewModel._failure = true;
                }
                else
                {
                    this.context?.AttendanceCodeCategories.Remove(attendanceCodeCategoriesDelete);
                    this.context?.SaveChanges();
                    attendanceCodeCategoriesAddViewModel._failure = false;
                    attendanceCodeCategoriesAddViewModel._message = "Deleted";
                }                
            }
            catch (Exception es)
            {
                attendanceCodeCategoriesAddViewModel._failure = true;
                attendanceCodeCategoriesAddViewModel._message = es.Message;
            }
            return attendanceCodeCategoriesAddViewModel;
        }
    }
}

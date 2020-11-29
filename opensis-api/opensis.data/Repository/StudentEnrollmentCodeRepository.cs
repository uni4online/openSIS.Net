using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.StudentEnrollmentCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class StudentEnrollmentCodeRepository: IStudentEnrollmentCodeRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public StudentEnrollmentCodeRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Add StudentEnrollmentCode
        /// </summary>
        /// <param name="studentEnrollmentCodeAddViewModel"></param>
        /// <returns></returns>

        public StudentEnrollmentCodeAddViewModel AddStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {

            int? MasterEnrollmentCode = Utility.GetMaxPK(this.context, new Func<StudentEnrollmentCode, int>(x => x.EnrollmentCode));
            studentEnrollmentCodeAddViewModel.studentEnrollmentCode.EnrollmentCode = (int)MasterEnrollmentCode;

            studentEnrollmentCodeAddViewModel.studentEnrollmentCode.LastUpdated = DateTime.UtcNow;
            this.context?.StudentEnrollmentCode.Add(studentEnrollmentCodeAddViewModel.studentEnrollmentCode);
            this.context?.SaveChanges();
            studentEnrollmentCodeAddViewModel._failure = false;
            return studentEnrollmentCodeAddViewModel;
        }

        /// <summary>
        /// View By Id StudentEnrollmentCode
        /// </summary>
        /// <param name="studentEnrollmentCodeAddViewModel"></param>
        /// <returns></returns>

        public StudentEnrollmentCodeAddViewModel ViewStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {
            StudentEnrollmentCodeAddViewModel studentEnrollmentCodeView = new StudentEnrollmentCodeAddViewModel();
            try
            {
               
                var studentEnrollmentCodeData = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.TenantId && x.SchoolId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.SchoolId && x.EnrollmentCode == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.EnrollmentCode);
                if (studentEnrollmentCodeData != null)
                {
                    studentEnrollmentCodeView.studentEnrollmentCode = studentEnrollmentCodeData;
                }
                else
                {
                    studentEnrollmentCodeView._failure = true;
                    studentEnrollmentCodeView._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                studentEnrollmentCodeView._failure = true;
                studentEnrollmentCodeView._message = es.Message;
            }
            return studentEnrollmentCodeView;
        }

        /// <summary>
        /// Delete StudentEnrollmentCode
        /// </summary>
        /// <param name="studentEnrollmentCodeAddViewModel"></param>
        /// <returns></returns>

        public StudentEnrollmentCodeAddViewModel DeleteStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {
            try
            {
                var studentEnrollmentCodeDelete = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.TenantId && x.SchoolId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.SchoolId && x.EnrollmentCode == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.EnrollmentCode);

                this.context?.StudentEnrollmentCode.Remove(studentEnrollmentCodeDelete);
                this.context?.SaveChanges();
                studentEnrollmentCodeAddViewModel._failure = false;
                studentEnrollmentCodeAddViewModel._message = "Deleted";
            }

            catch (Exception es)
            {
                studentEnrollmentCodeAddViewModel._failure = true;
                studentEnrollmentCodeAddViewModel._message = es.Message;
            }
            return studentEnrollmentCodeAddViewModel;
        }

        /// <summary>
        /// Update StudentEnrollmentCode
        /// </summary>
        /// <param name="studentEnrollmentCodeAddViewModel"></param>
        /// <returns></returns>
        public StudentEnrollmentCodeAddViewModel UpdateStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {
            try
            {
                var studentEnrollmentCodeUpdate = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.TenantId && x.SchoolId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.SchoolId && x.EnrollmentCode == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.EnrollmentCode);

                studentEnrollmentCodeUpdate.TenantId = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.TenantId;
                studentEnrollmentCodeUpdate.SchoolId = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.SchoolId;
                studentEnrollmentCodeUpdate.EnrollmentCode = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.EnrollmentCode;
                studentEnrollmentCodeUpdate.AcademicYear = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.AcademicYear;
                studentEnrollmentCodeUpdate.Title = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.Title;
                studentEnrollmentCodeUpdate.ShortName = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.ShortName;
                studentEnrollmentCodeUpdate.SortOrder = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.SortOrder;
                studentEnrollmentCodeUpdate.Type = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.Type;
                studentEnrollmentCodeUpdate.LastUpdated = DateTime.UtcNow;
                studentEnrollmentCodeUpdate.UpdatedBy = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.UpdatedBy;

                this.context?.SaveChanges();

                studentEnrollmentCodeAddViewModel._failure = false;
            }
            catch (Exception ex)
            {
                studentEnrollmentCodeAddViewModel.studentEnrollmentCode = null;
                studentEnrollmentCodeAddViewModel._failure = true;
                studentEnrollmentCodeAddViewModel._message = ex.Message;
            }
            return studentEnrollmentCodeAddViewModel;
        }

        /// <summary>
        /// Get All Student Enrollment Code
        /// </summary>
        /// <param name="studentEnrollmentCodeListView"></param>
        /// <returns></returns>
        public StudentEnrollmentCodeListViewModel GetAllStudentEnrollmentCode(StudentEnrollmentCodeListViewModel studentEnrollmentCodeListView)
        {
            StudentEnrollmentCodeListViewModel studentEnrollmentCodeList = new StudentEnrollmentCodeListViewModel();
            try
            {

                var StudentEnrollmentCodeAll = this.context?.StudentEnrollmentCode.Where(x => x.TenantId == studentEnrollmentCodeListView.TenantId && x.SchoolId == studentEnrollmentCodeListView.SchoolId).ToList();
                studentEnrollmentCodeList.studentEnrollmentCodeList = StudentEnrollmentCodeAll;
                studentEnrollmentCodeList._tenantName = studentEnrollmentCodeListView._tenantName;
                studentEnrollmentCodeList._token = studentEnrollmentCodeListView._token;
                studentEnrollmentCodeList._failure = false;
            }
            catch (Exception es)
            {
                studentEnrollmentCodeList._message = es.Message;
                studentEnrollmentCodeList._failure = true;
                studentEnrollmentCodeList._tenantName = studentEnrollmentCodeListView._tenantName;
                studentEnrollmentCodeList._token = studentEnrollmentCodeListView._token;
            }
            return studentEnrollmentCodeList;

        }



    }
}

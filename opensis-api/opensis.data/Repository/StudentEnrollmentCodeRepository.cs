﻿using opensis.data.Helper;
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

            //int? MasterEnrollmentCode = Utility.GetMaxPK(this.context, new Func<StudentEnrollmentCode, int>(x => x.EnrollmentCode));

            int? MasterEnrollmentCode = 1;

            var EnrollmentCodeData = this.context?.StudentEnrollmentCode.Where(x => x.SchoolId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.SchoolId && x.TenantId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.TenantId).OrderByDescending(x => x.EnrollmentCode).FirstOrDefault();

            if (EnrollmentCodeData != null)
            {
                MasterEnrollmentCode = EnrollmentCodeData.EnrollmentCode + 1;
            }

            studentEnrollmentCodeAddViewModel.studentEnrollmentCode.EnrollmentCode = (int)MasterEnrollmentCode;

            var studentEnrollmentCodeData = this.context?.StudentEnrollmentCode.Where(x => x.TenantId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.TenantId && x.SchoolId == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.SchoolId && x.Type.ToLower() == studentEnrollmentCodeAddViewModel.studentEnrollmentCode.Type.ToLower() && x.Type.ToLower() != "DROP".ToLower() && x.Type.ToLower() != "Add".ToLower()).ToList();
            if (studentEnrollmentCodeData.Count > 0)
            {
                studentEnrollmentCodeAddViewModel._failure = true;
                studentEnrollmentCodeAddViewModel._message = "You can't add any enrollment code in this type";
                return studentEnrollmentCodeAddViewModel;
            }
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

                if (studentEnrollmentCodeDelete != null)
                {
                    if (studentEnrollmentCodeDelete.Type.ToLower() == "Roll over".ToLower() || studentEnrollmentCodeDelete.Type.ToLower() == "Enroll (Transfer)".ToLower() || studentEnrollmentCodeDelete.Type.ToLower() == "Drop (Transfer)".ToLower())
                    {
                        studentEnrollmentCodeAddViewModel._failure = true;
                        studentEnrollmentCodeAddViewModel._message = "Cannot delete because it is not deletable.";
                    }
                    else
                    {
                        var studentEnrollmentExits = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == studentEnrollmentCodeDelete.TenantId && x.SchoolId == studentEnrollmentCodeDelete.SchoolId && (x.EnrollmentCode == studentEnrollmentCodeDelete.Title || x.ExitCode == studentEnrollmentCodeDelete.Title));
                        if (studentEnrollmentExits != null)
                        {
                            studentEnrollmentCodeAddViewModel._failure = true;
                            studentEnrollmentCodeAddViewModel._message = "Cannot delete because enrollment codes are associated.";
                        }
                        else
                        {
                            this.context?.StudentEnrollmentCode.Remove(studentEnrollmentCodeDelete);
                            this.context?.SaveChanges();
                            studentEnrollmentCodeAddViewModel._failure = false;
                            studentEnrollmentCodeAddViewModel._message = "Deleted";                           
                        }
                    }
                }
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

                var sameTitleExits = this.context?.StudentEnrollment.Where(x => x.SchoolId == studentEnrollmentCodeUpdate.SchoolId && x.TenantId == studentEnrollmentCodeUpdate.TenantId && (x.EnrollmentCode.ToLower() == studentEnrollmentCodeUpdate.Title.ToLower() || x.ExitCode.ToLower() == studentEnrollmentCodeUpdate.Title.ToLower())).ToList();
                if (sameTitleExits.Count > 0)
                {
                    foreach (var title in sameTitleExits)
                    {
                        if(title.EnrollmentCode != null && title.EnrollmentCode.ToLower()== studentEnrollmentCodeUpdate.Title.ToLower())
                        {
                            title.EnrollmentCode = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.Title;
                        }
                        if (title.ExitCode != null && title.ExitCode.ToLower() == studentEnrollmentCodeUpdate.Title.ToLower())
                        {
                            title.ExitCode = studentEnrollmentCodeAddViewModel.studentEnrollmentCode.Title;
                        }                                             
                    }
                    this.context?.StudentEnrollment.UpdateRange(sameTitleExits);
                }

                if (studentEnrollmentCodeAddViewModel.studentEnrollmentCode.Type.ToLower() != studentEnrollmentCodeUpdate.Type.ToLower())
                {
                    studentEnrollmentCodeAddViewModel._message = "Can't edit Type because it is not editable";
                }
                else
                {
                    studentEnrollmentCodeAddViewModel.studentEnrollmentCode.LastUpdated = DateTime.UtcNow;
                    this.context.Entry(studentEnrollmentCodeUpdate).CurrentValues.SetValues(studentEnrollmentCodeAddViewModel.studentEnrollmentCode);
                    this.context?.SaveChanges();
                    studentEnrollmentCodeAddViewModel._failure = false;
                }
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
                if (StudentEnrollmentCodeAll.Count > 0)
                {
                    studentEnrollmentCodeList.studentEnrollmentCodeList = StudentEnrollmentCodeAll;
                    studentEnrollmentCodeList._tenantName = studentEnrollmentCodeListView._tenantName;
                    studentEnrollmentCodeList._token = studentEnrollmentCodeListView._token;
                    studentEnrollmentCodeList._failure = false;
                }
                else
                {
                    studentEnrollmentCodeList.studentEnrollmentCodeList = null;
                    studentEnrollmentCodeList._tenantName = studentEnrollmentCodeListView._tenantName;
                    studentEnrollmentCodeList._token = studentEnrollmentCodeListView._token;
                    studentEnrollmentCodeList._failure = true;
                    studentEnrollmentCodeList._message = NORECORDFOUND;

                }
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

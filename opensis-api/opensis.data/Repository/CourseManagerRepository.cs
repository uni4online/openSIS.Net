using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.CourseManager;

namespace opensis.data.Repository
{
    public class CourseManagerRepository : ICourseManagerRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public CourseManagerRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        /// <summary>
        /// Add Program
        /// </summary>
        /// <param name="programAddViewModel"></param>
        /// <returns></returns>
        public ProgramAddViewModel AddProgram(ProgramAddViewModel programAddViewModel)
        {
            //int? ProgramId = Utility.GetMaxPK(this.context, new Func<Programs, int>(x => x.ProgramId));

            int? ProgramId = 0;

            var programData = this.context?.Programs.Where(x => x.SchoolId == programAddViewModel.programs.SchoolId && x.TenantId == programAddViewModel.programs.TenantId).OrderByDescending(x => x.ProgramId).FirstOrDefault();

            if (programData != null)
            {
                ProgramId = programData.ProgramId + 1;
            }
            else
            {
                ProgramId = 1;
            }

            programAddViewModel.programs.ProgramId = (int)ProgramId;
            programAddViewModel.programs.CreatedOn = DateTime.UtcNow;
            this.context?.Programs.Add(programAddViewModel.programs);
            this.context?.SaveChanges();
            programAddViewModel._failure = false;

            return programAddViewModel;
        }
        /// <summary>
        /// Get All Program
        /// </summary>
        /// <param name="programListViewModel"></param>
        /// <returns></returns>
        public ProgramListViewModel GetAllProgram(ProgramListViewModel programListViewModel)
        {
            ProgramListViewModel programListModel = new ProgramListViewModel();
            try
            {

                var programList = this.context?.Programs.Where(x => x.TenantId == programListViewModel.TenantId && x.SchoolId == programListViewModel.SchoolId).ToList();
                if (programList.Count > 0)
                {
                    programListModel.programList = programList;
                    programListModel._tenantName = programListViewModel._tenantName;
                    programListModel._token = programListViewModel._token;
                    programListModel._failure = false;
                }
                else
                {
                    programListModel.programList = null;
                    programListModel._tenantName = programListViewModel._tenantName;
                    programListModel._token = programListViewModel._token;
                    programListModel._failure = true;
                    programListModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                programListModel._message = es.Message;
                programListModel._failure = true;
                programListModel._tenantName = programListViewModel._tenantName;
                programListModel._token = programListViewModel._token;
            }
            return programListModel;
        }
        /// <summary>
        /// Update Program
        /// </summary>
        /// <param name="programAddViewModel"></param>
        /// <returns></returns>
        public ProgramAddViewModel UpdateProgram(ProgramAddViewModel programAddViewModel)
        {
            ProgramAddViewModel programUpdateModel = new ProgramAddViewModel();
            try
            {
                var programUpdate = this.context?.Programs.FirstOrDefault(x => x.TenantId == programAddViewModel.programs.TenantId && x.SchoolId == programAddViewModel.programs.SchoolId && x.ProgramId == programAddViewModel.programs.ProgramId);
                
                if (programUpdate != null)
                {
                    var courseList = this.context?.Course.Where(x => x.TenantId == programUpdate.TenantId && x.SchoolId == programUpdate.SchoolId && x.CourseProgram.ToLower() == programUpdate.ProgramName.ToLower()).ToList();
                
                    programUpdate.ProgramName = programAddViewModel.programs.ProgramName;
                    programUpdate.UpdatedBy = programAddViewModel.programs.UpdatedBy;
                    programUpdate.UpdatedOn = DateTime.UtcNow;

                    if (courseList.Count > 0)
                    {
                        courseList.ForEach(x => x.CourseProgram = programAddViewModel.programs.ProgramName);
                    }
                    this.context?.SaveChanges();
                    programAddViewModel._message = "Program Updated Successfully";
                    programAddViewModel._failure = false;
                }
                else
                {
                    programUpdateModel._failure = true;
                    programUpdateModel._message = NORECORDFOUND;
                }                
                
            }
            catch (Exception es)
            {

                programUpdateModel._message = es.Message;
                programUpdateModel._failure = true;
                programUpdateModel._tenantName = programAddViewModel._tenantName;
                programUpdateModel._token = programAddViewModel._token;
            }
            return programAddViewModel;
        }
        /// <summary>
        /// Delete Program
        /// </summary>
        /// <param name="programAddViewModel"></param>
        /// <returns></returns>
        public ProgramAddViewModel DeleteProgram(ProgramAddViewModel programAddViewModel)
        {
            try
            {
                var programDelete = this.context?.Programs.FirstOrDefault(x => x.TenantId == programAddViewModel.programs.TenantId && x.SchoolId == programAddViewModel.programs.SchoolId && x.ProgramId == programAddViewModel.programs.ProgramId);
                var CourseList = this.context?.Course.Where(e => e.CourseProgram.ToLower() == programDelete.ProgramName.ToLower() && e.SchoolId== programDelete.SchoolId && e.TenantId== programDelete.TenantId).ToList();
                
                if (CourseList.Count>0)
                {
                    programAddViewModel._message = "It Has Associationship";
                    programAddViewModel._failure = true;
                }
                else
                {
                    this.context?.Programs.Remove(programDelete);
                    this.context?.SaveChanges();
                    programAddViewModel._failure = false;
                    programAddViewModel._message = "Deleted";
                }                
            }
            catch (Exception es)
            {
                programAddViewModel._failure = true;
                programAddViewModel._message = es.Message;
            }
            return programAddViewModel;
        }

        /// <summary>
        /// Add Subject
        /// </summary>
        /// <param name="subjectAddViewModel"></param>
        /// <returns></returns>
        public SubjectAddViewModel AddSubject(SubjectAddViewModel subjectAddViewModel)
        {
            try
            {
                //int? MasterSubjectId = Utility.GetMaxPK(this.context, new Func<Subject, int>(x => x.SubjectId));
                int? MasterSubjectId = 1;

                var subjectData = this.context?.Subject.Where(x => x.SchoolId == subjectAddViewModel.subject.SchoolId && x.TenantId == subjectAddViewModel.subject.TenantId).OrderByDescending(x => x.SubjectId).FirstOrDefault();

                if (subjectData != null)
                {
                    MasterSubjectId = subjectData.SubjectId + 1;
                }
                subjectAddViewModel.subject.SubjectId = (int)MasterSubjectId;
                subjectAddViewModel.subject.CreatedOn = DateTime.UtcNow;
                this.context?.Subject.Add(subjectAddViewModel.subject);
                this.context?.SaveChanges();
                subjectAddViewModel._failure = false;
            }
            catch (Exception es)
            {
                subjectAddViewModel._failure = true;
                subjectAddViewModel._message = es.Message;
            }
            return subjectAddViewModel;
        }

        /// <summary>
        /// Update Subject
        /// </summary>
        /// <param name="subjectAddViewModel"></param>
        /// <returns></returns>
        public SubjectAddViewModel UpdateSubject(SubjectAddViewModel subjectAddViewModel)
        {
            try
            {
                var Subject = this.context?.Subject.FirstOrDefault(x => x.TenantId == subjectAddViewModel.subject.TenantId && x.SchoolId == subjectAddViewModel.subject.SchoolId && x.SubjectId == subjectAddViewModel.subject.SubjectId);

                var sameSubjectNameExits = this.context?.Course.Where(x => x.SchoolId == Subject.SchoolId && x.TenantId == Subject.TenantId && x.CourseSubject.ToLower() == Subject.SubjectName.ToLower()).ToList();

                if (sameSubjectNameExits.Count > 0)
                {
                    sameSubjectNameExits.ForEach(x => x.CourseSubject = subjectAddViewModel.subject.SubjectName);
                }
                Subject.SubjectName = subjectAddViewModel.subject.SubjectName;
                Subject.UpdatedBy = subjectAddViewModel.subject.UpdatedBy;
                Subject.UpdatedOn = DateTime.UtcNow;
                this.context?.SaveChanges();
                subjectAddViewModel._failure = false;
                subjectAddViewModel._message = "Subject Updated Successfully";
            }
            catch (Exception es)
            {
                subjectAddViewModel._failure = true;
                subjectAddViewModel._message = es.Message;
            }
            return subjectAddViewModel;
        }

        /// <summary>
        /// Get All Subject List
        /// </summary>
        /// <param name="subjectListViewModel"></param>
        /// <returns></returns>
        public SubjectListViewModel GetAllSubjectList(SubjectListViewModel subjectListViewModel)
        {
            SubjectListViewModel subjectList = new SubjectListViewModel();
            try
            {
                var Subjectdata = this.context?.Subject.Where(x => x.TenantId == subjectListViewModel.TenantId && x.SchoolId == subjectListViewModel.SchoolId).ToList();
                if (Subjectdata.Count > 0)
                {
                    subjectList.subjectList = Subjectdata;
                    subjectList._tenantName = subjectListViewModel._tenantName;
                    subjectList._token = subjectListViewModel._token;
                    subjectList._failure = false;
                }
                else
                {
                    subjectList.subjectList = null;
                    subjectList._tenantName = subjectListViewModel._tenantName;
                    subjectList._token = subjectListViewModel._token;
                    subjectList._failure = true;
                    subjectList._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                subjectList._message = es.Message;
                subjectList._failure = true;
                subjectList._tenantName = subjectListViewModel._tenantName;
                subjectList._token = subjectListViewModel._token;
            }
            return subjectList;
        }

        /// <summary>
        /// Delete Subject
        /// </summary>
        /// <param name="subjectAddViewModel"></param>
        /// <returns></returns>
        public SubjectAddViewModel DeleteSubject(SubjectAddViewModel subjectAddViewModel)
        {
            try
            {
                var subjectDelete = this.context?.Subject.FirstOrDefault(x => x.TenantId == subjectAddViewModel.subject.TenantId && x.SchoolId == subjectAddViewModel.subject.SchoolId && x.SubjectId == subjectAddViewModel.subject.SubjectId);

                if (subjectDelete != null)
                {
                    var courseExits = this.context?.Course.FirstOrDefault(x => x.TenantId == subjectDelete.TenantId && x.SchoolId == subjectDelete.SchoolId && x.CourseSubject == subjectDelete.SubjectName);
                    if (courseExits != null)
                    {
                        subjectAddViewModel._failure = true;
                        subjectAddViewModel._message = "Cannot delete because it has association.";
                    }
                    else
                    {
                        this.context?.Subject.Remove(subjectDelete);
                        this.context?.SaveChanges();
                        subjectAddViewModel._failure = false;
                        subjectAddViewModel._message = "Deleted Successfully";
                    }
                }
            }
            catch (Exception es)
            {
                subjectAddViewModel._failure = true;
                subjectAddViewModel._message = es.Message;
            }
            return subjectAddViewModel;
        }
    }
}

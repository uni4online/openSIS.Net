using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
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
        //public ProgramAddViewModel AddProgram(ProgramAddViewModel programAddViewModel)
        //{
        //    //int? ProgramId = Utility.GetMaxPK(this.context, new Func<Programs, int>(x => x.ProgramId));

        //    int? ProgramId = 0;

        //    var programData = this.context?.Programs.Where(x => x.SchoolId == programAddViewModel.programs.SchoolId && x.TenantId == programAddViewModel.programs.TenantId).OrderByDescending(x => x.ProgramId).FirstOrDefault();

        //    if (programData != null)
        //    {
        //        ProgramId = programData.ProgramId + 1;
        //    }
        //    else
        //    {
        //        ProgramId = 1;
        //    }

        //    programAddViewModel.programs.ProgramId = (int)ProgramId;
        //    programAddViewModel.programs.CreatedOn = DateTime.UtcNow;
        //    this.context?.Programs.Add(programAddViewModel.programs);
        //    this.context?.SaveChanges();
        //    programAddViewModel._failure = false;

        //    return programAddViewModel;
        //}
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
        public ProgramListViewModel AddEditProgram(ProgramListViewModel programListViewModel)
        {
            ProgramListViewModel programUpdateModel = new ProgramListViewModel();
            try
            {
                foreach (var programLists in programListViewModel.programList)
                {
                    if (programLists.ProgramId > 0)
                    {
                        var programUpdate = this.context?.Programs.FirstOrDefault(x => x.TenantId == programLists.TenantId && x.SchoolId == programLists.SchoolId && x.ProgramId == programLists.ProgramId);
                        if (programUpdate!=null)
                        {
                            var program = this.context?.Programs.FirstOrDefault(x => x.TenantId == programLists.TenantId && x.SchoolId == programLists.SchoolId && x.ProgramId != programLists.ProgramId && x.ProgramName.ToLower() == programLists.ProgramName.ToLower());
                            if (program != null)
                            {
                                programUpdateModel._message = "Program Name Already Exists";
                                programUpdateModel._failure = true;
                                return programUpdateModel;
                            }
                            else
                            {
                                var courseList = this.context?.Course.Where(x => x.TenantId == programUpdate.TenantId && x.SchoolId == programUpdate.SchoolId && x.CourseProgram.ToLower() == programUpdate.ProgramName.ToLower()).ToList();                               

                                programLists.CreatedBy = programUpdate.CreatedBy;
                                programLists.CreatedOn = programUpdate.CreatedOn;
                                programLists.UpdatedOn = DateTime.Now;
                                this.context.Entry(programUpdate).CurrentValues.SetValues(programLists);
                                this.context?.SaveChanges();

                                if (courseList.Count > 0)
                                {
                                    courseList.ForEach(x => x.CourseProgram = programLists.ProgramName);
                                }
                            }
                        }
                    }
                    else
                    {
                        int? ProgramId = 1;

                        var programData = this.context?.Programs.Where(x => x.SchoolId == programLists.SchoolId && x.TenantId == programLists.TenantId).OrderByDescending(x => x.ProgramId).FirstOrDefault();

                        if (programData != null)
                        {
                            ProgramId = programData.ProgramId + 1;
                        }

                        var program = this.context?.Programs.Where(x => x.SchoolId == programLists.SchoolId && x.TenantId == programLists.TenantId && x.ProgramName.ToLower() == programLists.ProgramName.ToLower()).FirstOrDefault();
                        if (program!= null)
                        {
                            programUpdateModel._failure = true;
                            programUpdateModel._message = "Program Name Already Exists";
                            return programUpdateModel;
                        }
                        programLists.ProgramId = (int)ProgramId;
                        programLists.CreatedOn = DateTime.UtcNow;
                        this.context?.Programs.AddRange(programLists);
                        
                    }                    
                }
                this.context?.SaveChanges();
                programUpdateModel._message = "Program Updated Successfully";
                programUpdateModel._failure = false;
            }
            catch (Exception es)
            {

                programUpdateModel._message = es.Message;
                programUpdateModel._failure = true;
                programUpdateModel._tenantName = programListViewModel._tenantName;
                programUpdateModel._token = programListViewModel._token;
            }
            return programUpdateModel;
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
        //public SubjectAddViewModel AddSubject(SubjectAddViewModel subjectAddViewModel)
        //{
        //    try
        //    {
        //        //int? MasterSubjectId = Utility.GetMaxPK(this.context, new Func<Subject, int>(x => x.SubjectId));
        //        int? MasterSubjectId = 1;

        //        var subjectData = this.context?.Subject.Where(x => x.SchoolId == subjectAddViewModel.subject.SchoolId && x.TenantId == subjectAddViewModel.subject.TenantId).OrderByDescending(x => x.SubjectId).FirstOrDefault();

        //        if (subjectData != null)
        //        {
        //            MasterSubjectId = subjectData.SubjectId + 1;
        //        }
        //        subjectAddViewModel.subject.SubjectId = (int)MasterSubjectId;
        //        subjectAddViewModel.subject.CreatedOn = DateTime.UtcNow;
        //        this.context?.Subject.Add(subjectAddViewModel.subject);
        //        this.context?.SaveChanges();
        //        subjectAddViewModel._failure = false;
        //    }
        //    catch (Exception es)
        //    {
        //        subjectAddViewModel._failure = true;
        //        subjectAddViewModel._message = es.Message;
        //    }
        //    return subjectAddViewModel;
        //}

        /// <summary>
        /// Add & Update Subject
        /// </summary>
        /// <param name="subjectListViewModel"></param>
        /// <returns></returns>
        public SubjectListViewModel AddEditSubject(SubjectListViewModel subjectListViewModel)
        {
            try
            {
                foreach (var subject in subjectListViewModel.subjectList)
                {
                    if (subject.SubjectId > 0)
                    {
                        var SubjectUpdate = this.context?.Subject.FirstOrDefault(x => x.TenantId == subject.TenantId && x.SchoolId == subject.SchoolId && x.SubjectId == subject.SubjectId);

                        if (SubjectUpdate != null)
                        {
                            var subjectName = this.context?.Subject.FirstOrDefault(x => x.TenantId == subject.TenantId && x.SchoolId == subject.SchoolId && x.SubjectName.ToLower() == subject.SubjectName.ToLower() && x.SubjectId != subject.SubjectId);

                            if (subjectName == null)
                            {
                                var sameSubjectNameExits = this.context?.Course.Where(x => x.SchoolId == SubjectUpdate.SchoolId && x.TenantId == SubjectUpdate.TenantId && x.CourseSubject.ToLower() == SubjectUpdate.SubjectName.ToLower()).ToList();
                                
                                if (sameSubjectNameExits.Count > 0)
                                {
                                    sameSubjectNameExits.ForEach(x => x.CourseSubject = subject.SubjectName);
                                }

                                subject.CreatedBy = SubjectUpdate.CreatedBy;
                                subject.CreatedOn = SubjectUpdate.CreatedOn;
                                subject.UpdatedOn = DateTime.Now;
                                this.context.Entry(SubjectUpdate).CurrentValues.SetValues(subject);
                                this.context?.SaveChanges();
                            }
                            else
                            {
                                subjectListViewModel._failure = true;
                                subjectListViewModel._message = "Subject Name Already Exits";
                                return subjectListViewModel;
                            }
                        }
                        else
                        {
                            subjectListViewModel._failure = true;
                            subjectListViewModel._message = NORECORDFOUND;
                        }
                    }
                    else
                    {
                        var subjectName = this.context?.Subject.FirstOrDefault(x => x.TenantId == subject.TenantId && x.SchoolId == subject.SchoolId && x.SubjectName.ToLower() == subject.SubjectName.ToLower());

                        if (subjectName == null)
                        {
                            int? SubjectId = 1;

                            var subjectData = this.context?.Subject.Where(x => x.SchoolId == subject.SchoolId && x.TenantId == subject.TenantId).OrderByDescending(x => x.SubjectId).FirstOrDefault();

                            if (subjectData != null)
                            {
                                SubjectId = subjectData.SubjectId + 1;
                            }
                            subject.SubjectId = (int)SubjectId;
                            subject.CreatedOn = DateTime.UtcNow;
                            this.context?.Subject.Add(subject);
                        }
                        else
                        {
                            subjectListViewModel._failure = true;
                            subjectListViewModel._message = "Subject Name Already Exits";
                            return subjectListViewModel;
                        }
                    }
                }
                this.context?.SaveChanges();
                subjectListViewModel._failure = false;
                subjectListViewModel._message = "Subject Updated Successfully";
            }
            catch (Exception es)
            {
                subjectListViewModel._failure = true;
                subjectListViewModel._message = es.Message;
            }
            return subjectListViewModel;
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

        /// <summary>
        /// Add Course
        /// </summary>
        /// <param name="courseAddViewModel"></param>
        /// <returns></returns>
        public CourseAddViewModel AddCourse(CourseAddViewModel courseAddViewModel)
        {
            try
            {
                if (courseAddViewModel.ProgramId == 0)
                {
                    int? ProgramId = 1;

                    var programData = this.context?.Programs.Where(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId).OrderByDescending(x => x.ProgramId).FirstOrDefault();

                    if (programData != null)
                    {
                        ProgramId = programData.ProgramId + 1;
                    }

                    var programName = this.context?.Programs.FirstOrDefault(x => x.SchoolId == courseAddViewModel.course.SchoolId && x.TenantId == courseAddViewModel.course.TenantId && x.ProgramName.ToLower() == courseAddViewModel.course.CourseProgram.ToLower());
                    if (programName != null)
                    {
                        courseAddViewModel._failure = true;
                        courseAddViewModel._message = "Program Name Already Exists";
                        return courseAddViewModel;
                    }
                    var programAdd = new Programs() { TenantId = courseAddViewModel.course.TenantId, SchoolId = courseAddViewModel.course.SchoolId, ProgramId = (int)ProgramId, ProgramName = courseAddViewModel.course.CourseProgram, CreatedOn = DateTime.UtcNow, CreatedBy = courseAddViewModel.course.CreatedBy };
                    this.context?.Programs.Add(programAdd);
                }
                if (courseAddViewModel.SubjectId == 0)
                {
                    int? SubjectId = 1;

                    var subjectData = this.context?.Subject.Where(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId).OrderByDescending(x => x.SubjectId).FirstOrDefault();

                    if (subjectData != null)
                    {
                        SubjectId = subjectData.SubjectId + 1;
                    }

                    var subjectName = this.context?.Subject.FirstOrDefault(x => x.SchoolId == courseAddViewModel.course.SchoolId && x.TenantId == courseAddViewModel.course.TenantId && x.SubjectName.ToLower() == courseAddViewModel.course.CourseSubject.ToLower());
                    if (subjectName != null)
                    {
                        courseAddViewModel._failure = true;
                        courseAddViewModel._message = "Subject Name Already Exists";
                        return courseAddViewModel;
                    }
                    var subjectAdd = new Subject() { TenantId = courseAddViewModel.course.TenantId, SchoolId = courseAddViewModel.course.SchoolId, SubjectId = (int)SubjectId, SubjectName = courseAddViewModel.course.CourseSubject, CreatedOn = DateTime.UtcNow, CreatedBy = courseAddViewModel.course.CreatedBy };
                    this.context?.Subject.Add(subjectAdd);
                }

                var courseTitle = this.context?.Course.FirstOrDefault(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId && x.CourseTitle.ToLower() == courseAddViewModel.course.CourseTitle.ToLower());

                if (courseTitle == null)
                {
                    int? CourseId = 1;

                    var courseData = this.context?.Course.Where(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId).OrderByDescending(x => x.CourseId).FirstOrDefault();

                    if (courseData != null)
                    {
                        CourseId = courseData.CourseId + 1;
                    }
                    courseAddViewModel.course.CourseId = (int)CourseId;
                    courseAddViewModel.course.CreatedOn = DateTime.UtcNow;
                    courseAddViewModel.course.IsCourseActive = true;

                    if(courseAddViewModel.course.CourseStandard.ToList().Count>0)
                    {
                        courseAddViewModel.course.CourseStandard.ToList().ForEach(x => x.CreatedOn = DateTime.UtcNow);
                    }
                    
                    this.context?.Course.Add(courseAddViewModel.course);
                }
                else
                {
                    courseAddViewModel._failure = true;
                    courseAddViewModel._message = "Course Title Already Exits";
                    return courseAddViewModel;
                }
                this.context.SaveChanges();
                courseAddViewModel._failure = false;
            }
            catch (Exception es)
            {
                courseAddViewModel._failure = true;
                courseAddViewModel._message = es.Message;
            }
            return courseAddViewModel;
        }

        /// <summary>
        /// Update Course
        /// </summary>
        /// <param name="courseAddViewModel"></param>
        /// <returns></returns>
        public CourseAddViewModel UpdateCourse(CourseAddViewModel courseAddViewModel)
        {
            try
            {
                if (courseAddViewModel.ProgramId == 0)
                {
                    int? ProgramId = 1;

                    var programData = this.context?.Programs.Where(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId).OrderByDescending(x => x.ProgramId).FirstOrDefault();

                    if (programData != null)
                    {
                        ProgramId = programData.ProgramId + 1;
                    }

                    var programName = this.context?.Programs.FirstOrDefault(x => x.SchoolId == courseAddViewModel.course.SchoolId && x.TenantId == courseAddViewModel.course.TenantId && x.ProgramName.ToLower() == courseAddViewModel.course.CourseProgram.ToLower());

                    if (programName != null)
                    {
                        courseAddViewModel._failure = true;
                        courseAddViewModel._message = "Program Name Already Exists";
                        return courseAddViewModel;
                    }

                    var programAdd = new Programs() { TenantId = courseAddViewModel.course.TenantId, SchoolId = courseAddViewModel.course.SchoolId, ProgramId = (int)ProgramId, ProgramName = courseAddViewModel.course.CourseProgram, CreatedOn = DateTime.UtcNow, CreatedBy = courseAddViewModel.course.CreatedBy };
                    this.context?.Programs.Add(programAdd);
                }

                if (courseAddViewModel.SubjectId == 0)
                {
                    int? SubjectId = 1;

                    var subjectData = this.context?.Subject.Where(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId).OrderByDescending(x => x.SubjectId).FirstOrDefault();

                    if (subjectData != null)
                    {
                        SubjectId = subjectData.SubjectId + 1;
                    }

                    var subjectName = this.context?.Subject.FirstOrDefault(x => x.SchoolId == courseAddViewModel.course.SchoolId && x.TenantId == courseAddViewModel.course.TenantId && x.SubjectName.ToLower() == courseAddViewModel.course.CourseSubject.ToLower());
                    if (subjectName != null)
                    {
                        courseAddViewModel._failure = true;
                        courseAddViewModel._message = "Subject Name Already Exists";
                        return courseAddViewModel;
                    }

                    var subjectAdd = new Subject() { TenantId = courseAddViewModel.course.TenantId, SchoolId = courseAddViewModel.course.SchoolId, SubjectId = (int)SubjectId, SubjectName = courseAddViewModel.course.CourseSubject, CreatedOn = DateTime.UtcNow, CreatedBy = courseAddViewModel.course.CreatedBy };
                    this.context?.Subject.Add(subjectAdd);
                }

                var courseUpdate = this.context?.Course.Include(x => x.CourseStandard).FirstOrDefault(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId && x.CourseId == courseAddViewModel.course.CourseId);
                
                if (courseUpdate != null)
                {
                    var courseTitle = this.context?.Course.FirstOrDefault(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId && x.CourseTitle.ToLower() == courseAddViewModel.course.CourseTitle.ToLower() && x.CourseId != courseAddViewModel.course.CourseId);

                    if (courseTitle == null)
                    {
                        courseAddViewModel.course.CreatedBy = courseUpdate.CreatedBy;
                        courseAddViewModel.course.CreatedOn = courseUpdate.CreatedOn;
                        courseAddViewModel.course.UpdatedOn = DateTime.Now;
                        this.context.Entry(courseUpdate).CurrentValues.SetValues(courseAddViewModel.course);
                        courseAddViewModel._message = "Course Updated Successfully";

                        if (courseAddViewModel.course.CourseStandard.ToList().Count > 0)
                        {
                            this.context?.CourseStandard.RemoveRange(courseUpdate.CourseStandard);
                            courseAddViewModel.course.CourseStandard.ToList().ForEach(x => x.UpdatedOn = DateTime.UtcNow);
                            this.context?.CourseStandard.AddRange(courseAddViewModel.course.CourseStandard);
                        }
                    }
                    else
                    {
                        courseAddViewModel._failure = true;
                        courseAddViewModel._message = "Course Title Already Exits";
                        return courseAddViewModel;
                    }
                }
                this.context.SaveChanges();
                courseAddViewModel._failure = false;
            }
            catch(Exception es)
            {
                courseAddViewModel._failure = true;
                courseAddViewModel._message = es.Message;
            }
            return courseAddViewModel;
        }

        /// <summary>
        /// Delete Course
        /// </summary>
        /// <param name="courseAddViewModel"></param>
        /// <returns></returns>
        public CourseAddViewModel DeleteCourse(CourseAddViewModel courseAddViewModel)
        {
            try
            {
                var courseDelete = this.context?.Course.Include(x=>x.CourseStandard).FirstOrDefault(x => x.TenantId == courseAddViewModel.course.TenantId && x.SchoolId == courseAddViewModel.course.SchoolId && x.CourseId == courseAddViewModel.course.CourseId);

                if (courseDelete != null)
                {
                    this.context?.CourseStandard.RemoveRange(courseDelete.CourseStandard);
                    this.context?.Course.Remove(courseDelete);
                    this.context?.SaveChanges();
                    courseAddViewModel._failure = false;
                    courseAddViewModel._message = "Deleted Successfully";
                }
                else
                {
                    courseAddViewModel._failure = true;
                    courseAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                courseAddViewModel._failure = true;
                courseAddViewModel._message = es.Message;
            }
            return courseAddViewModel;
        }

        /// <summary>
        /// Get All Course List
        /// </summary>
        /// <param name="courseListViewModel"></param>
        /// <returns></returns>
        public CourseListViewModel GetAllCourseList(CourseListViewModel courseListViewModel)
        {
            CourseListViewModel courseListModel = new CourseListViewModel();
            try
            {
                var courseRecords = this.context?.Course.Include(e=>e.CourseStandard).ThenInclude(c=>c.GradeUsStandard).Where(x => x.TenantId == courseListViewModel.TenantId && x.SchoolId == courseListViewModel.SchoolId).ToList();               
                if (courseRecords.Count > 0)
                {
                    courseListModel.courseList = courseRecords;
                    courseListModel._tenantName = courseListViewModel._tenantName;
                    courseListModel._token = courseListViewModel._token;
                    courseListModel._failure = false;
                }
                else
                {
                    courseListModel.courseList = null;
                    courseListModel._tenantName = courseListViewModel._tenantName;
                    courseListModel._token = courseListViewModel._token;
                    courseListModel._failure = true;
                    courseListModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                courseListModel._message = es.Message;
                courseListModel._failure = true;
                courseListModel._tenantName = courseListViewModel._tenantName;
                courseListModel._token = courseListViewModel._token;
            }
            return courseListModel;
        }
    }
}

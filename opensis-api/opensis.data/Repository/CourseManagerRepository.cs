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
        /// <summary>
        /// Add Course Section
        /// </summary>
        /// <param name="courseSectionAddViewModel"></param>
        /// <returns></returns>
        public CourseSectionAddViewModel AddCourseSection(CourseSectionAddViewModel courseSectionAddViewModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    List<CourseBlockSchedule> courseBlockScheduleList = new List<CourseBlockSchedule>();
                    List<CourseVariableSchedule> courseVariableScheduleList = new List<CourseVariableSchedule>();

                    if (!(bool)courseSectionAddViewModel.courseSection.DurationBasedOnPeriod)
                    {
                        var CalenderData = this.context?.SchoolCalendars.FirstOrDefault(x => x.CalenderId == courseSectionAddViewModel.courseSection.CalendarId);


                        if (CalenderData != null)
                        {
                            if (CalenderData.StartDate >= courseSectionAddViewModel.courseSection.DurationStartDate || CalenderData.EndDate <= courseSectionAddViewModel.courseSection.DurationEndDate)
                            {                                
                                courseSectionAddViewModel._message = "Start Date And End Date of Course Section Should Be Between Start Date And End Date of School Calender";
                                courseSectionAddViewModel._failure = true;
                                return courseSectionAddViewModel;
                            }
                        }
                    }

                    int? CourseSectionId = 1;

                    var CourseSectionData = this.context?.CourseSection.Where(x => x.TenantId == courseSectionAddViewModel.courseSection.TenantId && x.SchoolId == courseSectionAddViewModel.courseSection.SchoolId).OrderByDescending(x => x.CourseSectionId).FirstOrDefault();


                    if (CourseSectionData != null)
                    {
                        CourseSectionId = CourseSectionData.CourseSectionId + 1;
                    }

                    if (courseSectionAddViewModel.courseSection.ScheduleType != null)
                    {
                        switch (courseSectionAddViewModel.courseSection.ScheduleType.ToLower())
                        {
                            case "fixedschedule":

                                if (courseSectionAddViewModel.courseFixedSchedule != null)
                                {
                                    var roomCapacity = this.context?.Rooms.FirstOrDefault(e => e.TenantId == courseSectionAddViewModel.courseFixedSchedule.TenantId && e.SchoolId == courseSectionAddViewModel.courseFixedSchedule.SchoolId && e.RoomId == courseSectionAddViewModel.courseFixedSchedule.RoomId)?.Capacity;

                                    if (roomCapacity < courseSectionAddViewModel.courseSection.Seats)
                                    {
                                        courseSectionAddViewModel._message = "Invalid Seat Capacity";
                                        courseSectionAddViewModel._failure = true;
                                        return courseSectionAddViewModel;
                                    }
                                    else
                                    {
                                        int? fixedscheduleSerial = 1;

                                        var CourseSectionfixedscheduleData = this.context?.CourseFixedSchedule.Where(x => x.TenantId == courseSectionAddViewModel.courseSection.TenantId && x.SchoolId == courseSectionAddViewModel.courseSection.SchoolId).OrderByDescending(x => x.Serial).FirstOrDefault();


                                        if (CourseSectionfixedscheduleData != null)
                                        {
                                            fixedscheduleSerial = CourseSectionfixedscheduleData.Serial + 1;
                                        }

                                        var courseFixedSchedule = new CourseFixedSchedule()
                                        {
                                            TenantId = courseSectionAddViewModel.courseSection.TenantId,
                                            SchoolId = courseSectionAddViewModel.courseSection.SchoolId,
                                            CourseId = courseSectionAddViewModel.courseSection.CourseId,
                                            CourseSectionId = (int)CourseSectionId,
                                            GradeScaleId = courseSectionAddViewModel.courseSection.GradeScaleId,
                                            Serial = (int)fixedscheduleSerial,
                                            RoomId = courseSectionAddViewModel.courseFixedSchedule.RoomId,
                                            PeriodId = courseSectionAddViewModel.courseFixedSchedule.PeriodId,
                                            CreatedBy = courseSectionAddViewModel.courseFixedSchedule.CreatedBy,
                                            CreatedOn = DateTime.UtcNow
                                        };
                                        this.context?.CourseFixedSchedule.Add(courseFixedSchedule);
                                        courseSectionAddViewModel.courseSection.ScheduleType = "Fixed Schedule (1)";
                                    }
                                }
                                break;

                            case "variableschedule":

                                if (courseSectionAddViewModel.courseVariableScheduleList.Count > 0)
                                {
                                    int? variablescheduleSerial = 1;

                                    var CourseSectionvariablescheduleData = this.context?.CourseVariableSchedule.Where(x => x.TenantId == courseSectionAddViewModel.courseSection.TenantId && x.SchoolId == courseSectionAddViewModel.courseSection.SchoolId).OrderByDescending(x => x.Serial).FirstOrDefault();

                                    if (CourseSectionvariablescheduleData != null)
                                    {
                                        variablescheduleSerial = CourseSectionvariablescheduleData.Serial + 1;
                                    }

                                    foreach (var courseVariableSchedules in courseSectionAddViewModel.courseVariableScheduleList)
                                    {
                                        var roomCapacity = this.context?.Rooms.FirstOrDefault(e => e.TenantId == courseSectionAddViewModel.courseFixedSchedule.TenantId && e.SchoolId == courseSectionAddViewModel.courseFixedSchedule.SchoolId && e.RoomId == courseVariableSchedules.RoomId)?.Capacity;

                                        if (roomCapacity < courseSectionAddViewModel.courseSection.Seats)
                                        {
                                            courseSectionAddViewModel._message = "Invalid Seat Capacity";
                                            courseSectionAddViewModel._failure = true;
                                            return courseSectionAddViewModel;
                                        }
                                        else
                                        {
                                            var courseeVariableSchedule = new CourseVariableSchedule()
                                            {
                                                TenantId = courseSectionAddViewModel.courseSection.TenantId,
                                                SchoolId = courseSectionAddViewModel.courseSection.SchoolId,
                                                CourseId = courseSectionAddViewModel.courseSection.CourseId,
                                                CourseSectionId = (int)CourseSectionId,
                                                GradeScaleId = courseSectionAddViewModel.courseSection.GradeScaleId,
                                                Serial = (int)variablescheduleSerial,
                                                Day = courseVariableSchedules.Day,
                                                PeriodId = courseVariableSchedules.PeriodId,
                                                RoomId = courseVariableSchedules.RoomId,
                                                TakeAttendance = courseVariableSchedules.TakeAttendance,
                                                CreatedBy = courseSectionAddViewModel.courseSection.CreatedBy,
                                                CreatedOn = DateTime.UtcNow,

                                            };
                                            //this.context?.CourseVariableSchedule.Add(courseeVariableSchedule);
                                            courseVariableScheduleList.Add(courseeVariableSchedule);
                                            variablescheduleSerial++;
                                        }
                                    }
                                    this.context?.CourseVariableSchedule.AddRange(courseVariableScheduleList);
                                    courseSectionAddViewModel.courseSection.ScheduleType = "Variable Schedule (2)";
                                }
                                break;

                            case "calendarschedule":

                                if (courseSectionAddViewModel.courseCalendarSchedule != null)
                                {

                                    var roomCapacity = this.context?.Rooms.FirstOrDefault(e => e.TenantId == courseSectionAddViewModel.courseFixedSchedule.TenantId && e.SchoolId == courseSectionAddViewModel.courseFixedSchedule.SchoolId && e.RoomId == courseSectionAddViewModel.courseCalendarSchedule.RoomId)?.Capacity;

                                    if (roomCapacity < courseSectionAddViewModel.courseSection.Seats)
                                    {
                                        courseSectionAddViewModel._message = "Invalid Seat Capacity";
                                        courseSectionAddViewModel._failure = true;
                                        return courseSectionAddViewModel;
                                    }
                                    else
                                    {
                                        int? calendarscheduleSerial = 1;

                                        var CourseSectioncalendarscheduleData = this.context?.CourseCalendarSchedule.Where(x => x.TenantId == courseSectionAddViewModel.courseSection.TenantId && x.SchoolId == courseSectionAddViewModel.courseSection.SchoolId).OrderByDescending(x => x.Serial).FirstOrDefault();

                                        if (CourseSectioncalendarscheduleData != null)
                                        {
                                            calendarscheduleSerial = CourseSectioncalendarscheduleData.Serial + 1;
                                        }

                                        var calendarschedule = new CourseCalendarSchedule()
                                        {
                                            TenantId = courseSectionAddViewModel.courseSection.TenantId,
                                            SchoolId = courseSectionAddViewModel.courseSection.SchoolId,
                                            CourseId = courseSectionAddViewModel.courseSection.CourseId,
                                            CourseSectionId = (int)CourseSectionId,
                                            GradeScaleId = courseSectionAddViewModel.courseSection.GradeScaleId,
                                            Serial = (int)calendarscheduleSerial,
                                            Date = courseSectionAddViewModel.courseCalendarSchedule.Date,
                                            PeriodId = courseSectionAddViewModel.courseCalendarSchedule.PeriodId,
                                            RoomId = courseSectionAddViewModel.courseCalendarSchedule.RoomId,
                                            TakeAttendance = courseSectionAddViewModel.courseCalendarSchedule.TakeAttendance,
                                            CreatedBy = courseSectionAddViewModel.courseSection.CreatedBy,
                                            CreatedOn = DateTime.UtcNow,
                                        };
                                        this.context?.CourseCalendarSchedule.Add(calendarschedule);
                                        courseSectionAddViewModel.courseSection.ScheduleType = "Calendar Schedule (3)";
                                    }
                                }
                                break;

                            case "blockschedule":

                                if (courseSectionAddViewModel.courseBlockScheduleList.Count > 0)
                                {
                                    int? blockscheduleSerial = 1;

                                    var CourseSectionblockscheduleData = this.context?.CourseBlockSchedule.Where(x => x.TenantId == courseSectionAddViewModel.courseSection.TenantId && x.SchoolId == courseSectionAddViewModel.courseSection.SchoolId).OrderByDescending(x => x.Serial).FirstOrDefault();

                                    if (CourseSectionblockscheduleData != null)
                                    {
                                        blockscheduleSerial = CourseSectionblockscheduleData.Serial + 1;
                                    }

                                    foreach (var courseBlockSchedules in courseSectionAddViewModel.courseBlockScheduleList)
                                    {

                                        var roomCapacity = this.context?.Rooms.FirstOrDefault(e => e.TenantId == courseSectionAddViewModel.courseFixedSchedule.TenantId && e.SchoolId == courseSectionAddViewModel.courseFixedSchedule.SchoolId && e.RoomId == courseBlockSchedules.RoomId)?.Capacity;

                                        if (roomCapacity < courseSectionAddViewModel.courseSection.Seats)
                                        {
                                            courseSectionAddViewModel._message = "Invalid Seat Capacity";
                                            courseSectionAddViewModel._failure = true;
                                            return courseSectionAddViewModel;
                                        }
                                        else
                                        {
                                            var courseBlockSchedule = new CourseBlockSchedule()
                                            {
                                                TenantId = courseSectionAddViewModel.courseSection.TenantId,
                                                SchoolId = courseSectionAddViewModel.courseSection.SchoolId,
                                                CourseId = courseSectionAddViewModel.courseSection.CourseId,
                                                CourseSectionId = (int)CourseSectionId,
                                                GradeScaleId = courseSectionAddViewModel.courseSection.GradeScaleId,
                                                Serial = (int)blockscheduleSerial,
                                                BlockId = courseBlockSchedules.BlockId,
                                                PeriodId = courseBlockSchedules.PeriodId,
                                                RoomId = courseBlockSchedules.RoomId,
                                                TakeAttendance = courseBlockSchedules.TakeAttendance,
                                                CreatedBy = courseSectionAddViewModel.courseSection.CreatedBy,
                                                CreatedOn = DateTime.UtcNow
                                            };
                                            //this.context?.CourseBlockSchedule.AddRange(courseBlockSchedule);
                                            courseBlockScheduleList.Add(courseBlockSchedule);
                                            blockscheduleSerial++;
                                        }
                                    }
                                    this.context?.CourseBlockSchedule.AddRange(courseBlockScheduleList);
                                    courseSectionAddViewModel.courseSection.ScheduleType = "Block Schedule (4)";
                                }
                                break;
                        }
                    }
                    courseSectionAddViewModel.courseSection.CourseSectionId = (int)CourseSectionId;
                    this.context?.CourseSection.Add(courseSectionAddViewModel.courseSection);
                    this.context?.SaveChanges();
                    courseSectionAddViewModel._failure = false;
                    transaction.Commit();
                }
                catch (Exception es)
                {
                    transaction.Rollback();
                    courseSectionAddViewModel._message = es.Message;
                    courseSectionAddViewModel._failure = true;
                }
            }
            return courseSectionAddViewModel;
        }

        /// <summary>
        /// Get All CourseSection
        /// </summary>
        /// <param name="courseSectionViewModel"></param>
        /// <returns></returns>
        public CourseSectionViewModel GetAllCourseSection(CourseSectionViewModel courseSectionViewModel)
        {
            CourseSectionViewModel courseSectionView = new CourseSectionViewModel();
            try
            {
                var courseSectionData = this.context?.CourseSection.Include(x => x.Course).Include(x => x.AttendanceCodeCategories).Include(x => x.GradeScale).Include(x => x.SchoolCalendars).Include(x => x.SchoolYears).Include(x => x.Semesters).Include(x => x.Quarters).Where(x => x.TenantId == courseSectionViewModel.TenantId && x.SchoolId == courseSectionViewModel.SchoolId && x.CourseId == courseSectionViewModel.CourseId).ToList().Select(cs => new CourseSection
                {
                    TenantId = cs.TenantId,
                    SchoolId = cs.SchoolId,
                    CourseId = cs.CourseId,
                    CourseSectionId = cs.CourseSectionId,
                    GradeScaleId = cs.GradeScaleId,
                    CourseSectionName = cs.CourseSectionName,
                    CalendarId = cs.CalendarId,
                    AttendanceCategoryId = cs.AttendanceCategoryId,
                    CreditHours = cs.CreditHours,
                    Seats = cs.Seats,
                    IsWeightedCourse = cs.IsWeightedCourse,
                    AffectsClassRank = cs.AffectsClassRank,
                    AffectsHonorRoll = cs.AffectsHonorRoll,
                    OnlineClassRoom = cs.OnlineClassRoom,
                    OnlineClassroomUrl = cs.OnlineClassroomUrl,
                    OnlineClassroomPassword = cs.OnlineClassroomPassword,
                    StandardGradeScaleId = cs.StandardGradeScaleId,
                    UseStandards = cs.UseStandards,
                    DurationBasedOnPeriod = cs.DurationBasedOnPeriod,
                    DurationStartDate = cs.DurationStartDate,
                    DurationEndDate = cs.DurationEndDate,
                    ScheduleType = cs.ScheduleType,
                    MeetingDays = cs.MeetingDays,
                    AttendanceTaken = cs.AttendanceTaken,
                    IsActive = cs.IsActive,
                    CreatedBy = cs.CreatedBy,
                    CreatedOn = cs.CreatedOn,
                    UpdatedBy = cs.UpdatedBy,
                    UpdatedOn = cs.UpdatedOn,
                    QtrMarkingPeriodId = cs.QtrMarkingPeriodId,
                    SmstrMarkingPeriodId = cs.SmstrMarkingPeriodId,
                    YrMarkingPeriodId = cs.YrMarkingPeriodId,
                    Course=new Course { CourseTitle=cs.Course.CourseTitle},
                    GradeScale = new GradeScale { GradeScaleName = cs.GradeScale.GradeScaleName },
                    AttendanceCodeCategories = new AttendanceCodeCategories { Title = cs.AttendanceCodeCategories.Title },
                    SchoolCalendars = cs.SchoolCalendars != null ? new SchoolCalendars { TenantId = cs.SchoolCalendars.TenantId, SchoolId = cs.SchoolCalendars.SchoolId, CalenderId = cs.SchoolCalendars.CalenderId, Title = cs.SchoolCalendars.Title, StartDate = cs.SchoolCalendars.StartDate, EndDate = cs.SchoolCalendars.EndDate, AcademicYear = cs.SchoolCalendars.AcademicYear, DefaultCalender = cs.SchoolCalendars.DefaultCalender, Days=cs.SchoolCalendars.Days,RolloverId=cs.SchoolCalendars.RolloverId,VisibleToMembershipId=cs.SchoolCalendars.VisibleToMembershipId,LastUpdated=cs.SchoolCalendars.LastUpdated,UpdatedBy=cs.SchoolCalendars.UpdatedBy} : null,
                    Quarters = cs.Quarters != null ? new Quarters { Title = cs.Quarters.Title, StartDate = cs.Quarters.StartDate, EndDate = cs.Quarters.EndDate, } : null,
                    Semesters= cs.Semesters != null ? new Semesters { Title = cs.Semesters.Title, StartDate = cs.Semesters.StartDate, EndDate = cs.Semesters.EndDate, } : null,
                    SchoolYears=cs.SchoolYears != null? new SchoolYears { Title = cs.SchoolYears.Title, StartDate = cs.SchoolYears.StartDate, EndDate = cs.SchoolYears.EndDate, }: null,

                });

                if (courseSectionData.Count()>0)
                {
                    foreach (var courseSection in courseSectionData)
                    {
                        if (courseSection.ScheduleType.ToLower() == "Fixed Schedule".ToLower())
                        {
                            var fixedScheduleData = this.context?.CourseFixedSchedule.Include(f => f.Rooms).Include(f => f.BlockPeriod).Where(x => x.TenantId == courseSection.TenantId && x.SchoolId == courseSection.SchoolId && x.CourseId == courseSection.CourseId && x.CourseSectionId == courseSection.CourseSectionId).Select(s=>new CourseFixedSchedule { Serial = s.Serial, Rooms =new Rooms { Title=s.Rooms.Title},BlockPeriod=new BlockPeriod { PeriodTitle= s.BlockPeriod.PeriodTitle}}).FirstOrDefault();
                            if (fixedScheduleData != null)
                            {
                                GetCourseSectionForView getFixedSchedule = new GetCourseSectionForView
                                {
                                    courseFixedSchedule = fixedScheduleData,
                                    courseSection = courseSection
                                };
                                courseSectionView.getCourseSectionForView.Add(getFixedSchedule);
                            }
                        }

                        if (courseSection.ScheduleType.ToLower() == "Variable Schedule".ToLower())
                        {
                            var variableScheduleData = this.context?.CourseVariableSchedule.Where(x => x.TenantId == courseSection.TenantId && x.SchoolId == courseSection.SchoolId && x.CourseId == courseSection.CourseId && x.CourseSectionId == courseSection.CourseSectionId).Include(f => f.Rooms).Include(f => f.BlockPeriod).Select(s => new CourseVariableSchedule { Serial = s.Serial, Day =s.Day,TakeAttendance=s.TakeAttendance, Rooms = new Rooms { Title = s.Rooms.Title }, BlockPeriod = new BlockPeriod { PeriodTitle = s.BlockPeriod.PeriodTitle } }).ToList();
                           
                            if (variableScheduleData.Count> 0)
                            {
                                GetCourseSectionForView getVariableSchedule = new GetCourseSectionForView
                                {
                                    courseVariableSchedule = variableScheduleData,
                                    courseSection = courseSection
                                };
                                courseSectionView.getCourseSectionForView.Add(getVariableSchedule);
                            }
                        }

                        if (courseSection.ScheduleType.ToLower() == "Calendar Schedule".ToLower())
                        {
                            var calendarScheduleData = this.context?.CourseCalendarSchedule.Where(x => x.TenantId == courseSection.TenantId && x.SchoolId == courseSection.SchoolId && x.CourseId == courseSection.CourseId && x.CourseSectionId == courseSection.CourseSectionId).Include(f => f.Rooms).Include(f => f.BlockPeriod).Select(s => new CourseCalendarSchedule { Serial = s.Serial, Date =s.Date,TakeAttendance=s.TakeAttendance,Rooms = new Rooms { Title = s.Rooms.Title }, BlockPeriod = new BlockPeriod { PeriodTitle = s.BlockPeriod.PeriodTitle } }).FirstOrDefault();
                            if (calendarScheduleData != null)
                            {
                                GetCourseSectionForView getCalendarSchedule = new GetCourseSectionForView
                                {
                                    courseCalendarSchedule = calendarScheduleData,
                                    courseSection = courseSection
                                };
                                courseSectionView.getCourseSectionForView.Add(getCalendarSchedule);
                            }                            
                        }

                        if (courseSection.ScheduleType.ToLower() == "Block/Rotating Schedule".ToLower())
                        {
                            var blockScheduleData = this.context?.CourseBlockSchedule.Where(x => x.TenantId == courseSection.TenantId && x.SchoolId == courseSection.SchoolId && x.CourseId == courseSection.CourseId && x.CourseSectionId == courseSection.CourseSectionId).Include(f => f.Rooms).Include(f => f.BlockPeriod).Select(s => new CourseBlockSchedule { Serial=s.Serial, TakeAttendance = s.TakeAttendance, Rooms = new Rooms { Title = s.Rooms.Title }, BlockPeriod = new BlockPeriod { PeriodTitle = s.BlockPeriod.PeriodTitle } }).ToList();
                            if (blockScheduleData.Count > 0)
                            {
                                GetCourseSectionForView getBlockSchedule = new GetCourseSectionForView
                                {
                                    courseBlockSchedule = blockScheduleData,
                                    courseSection = courseSection
                                };
                                courseSectionView.getCourseSectionForView.Add(getBlockSchedule);
                            }
                        }
                    }
                    courseSectionView._tenantName = courseSectionViewModel._tenantName;
                    courseSectionView._token = courseSectionViewModel._token;
                    courseSectionView._failure = false;     
                }              
                else
                {
                    courseSectionView._failure = true;
                    courseSectionView._message = NORECORDFOUND;
                    courseSectionView._tenantName = courseSectionViewModel._tenantName;
                    courseSectionView._token = courseSectionViewModel._token;
                }               
            }
            catch(Exception es)
            {
                courseSectionView._failure = true;
                courseSectionView._message = es.Message;
            }
            return courseSectionView;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using opensis.core.CourseManager.Interfaces;
using opensis.core.helper;
using opensis.data.Interface;
using opensis.data.ViewModels.CourseManager;

namespace opensis.core.CourseManager.Services
{
    public class CourseManagerService : ICourseManagerService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public ICourseManagerRepository courseManagerRepository;
        public CourseManagerService(ICourseManagerRepository courseManagerRepository)
        {
            this.courseManagerRepository = courseManagerRepository;
        }

        //Required for Unit Testing
        public CourseManagerService() { }
        /// <summary>
        /// Add Program
        /// </summary>
        /// <param name="programAddViewModel"></param>
        /// <returns></returns>
        //public ProgramAddViewModel AddProgram(ProgramAddViewModel programAddViewModel)
        //{
        //    ProgramAddViewModel ProgramAddModel = new ProgramAddViewModel();
        //    try
        //    {
        //        if (TokenManager.CheckToken(programAddViewModel._tenantName, programAddViewModel._token))
        //        {
        //            ProgramAddModel = this.courseManagerRepository.AddProgram(programAddViewModel);
        //        }
        //        else
        //        {
        //            ProgramAddModel._failure = true;
        //            ProgramAddModel._message = TOKENINVALID;
        //        }
        //    }
        //    catch (Exception es)
        //    {

        //        ProgramAddModel._failure = true;
        //        ProgramAddModel._message = es.Message;
        //    }
        //    return ProgramAddModel;
        //}
        /// <summary>
        /// Get All Program
        /// </summary>
        /// <param name="programListViewModel"></param>
        /// <returns></returns>
        public ProgramListViewModel GetAllProgram(ProgramListViewModel programListViewModel)
        {
            ProgramListViewModel ProgramListModel = new ProgramListViewModel();
            try
            {
                if (TokenManager.CheckToken(programListViewModel._tenantName, programListViewModel._token))
                {
                    ProgramListModel = this.courseManagerRepository.GetAllProgram(programListViewModel);
                }
                else
                {
                    ProgramListModel._failure = true;
                    ProgramListModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                ProgramListModel._failure = true;
                ProgramListModel._message = es.Message;
            }

            return ProgramListModel;
        }
        /// <summary>
        /// Update Program
        /// </summary>
        /// <param name="programAddViewModel"></param>
        /// <returns></returns>
        public ProgramListViewModel AddEditProgram(ProgramListViewModel programListViewModel)
        {
            ProgramListViewModel ProgramUpdateModel = new ProgramListViewModel();
            try
            {
                if (TokenManager.CheckToken(programListViewModel._tenantName, programListViewModel._token))
                {
                    ProgramUpdateModel = this.courseManagerRepository.AddEditProgram(programListViewModel);
                }
                else
                {
                    ProgramUpdateModel._failure = true;
                    ProgramUpdateModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {

                ProgramUpdateModel._failure = true;
                ProgramUpdateModel._message = es.Message;
            }
            return ProgramUpdateModel;
        }
        /// <summary>
        /// Delete Program
        /// </summary>
        /// <param name="programAddViewModel"></param>
        /// <returns></returns>
        public ProgramAddViewModel DeleteProgram(ProgramAddViewModel programAddViewModel)
        {
            ProgramAddViewModel programDeleteModel = new ProgramAddViewModel();
            try
            {
                if (TokenManager.CheckToken(programAddViewModel._tenantName, programAddViewModel._token))
                {
                    programDeleteModel = this.courseManagerRepository.DeleteProgram(programAddViewModel);
                }
                else
                {
                    programDeleteModel._failure = true;
                    programDeleteModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                programDeleteModel._failure = true;
                programDeleteModel._message = es.Message;
            }

            return programDeleteModel;
        }
        /// <summary>
        /// Add Subject
        /// </summary>
        /// <param name="subjectAddViewModel"></param>
        /// <returns></returns>
        //public SubjectAddViewModel AddSubject(SubjectAddViewModel subjectAddViewModel)
        //{
        //    SubjectAddViewModel subjectAdd = new SubjectAddViewModel();

        //    if (TokenManager.CheckToken(subjectAddViewModel._tenantName, subjectAddViewModel._token))
        //    {
        //        subjectAdd = this.courseManagerRepository.AddSubject(subjectAddViewModel);
        //    }
        //    else
        //    {
        //        subjectAdd._failure = true;
        //        subjectAdd._message = TOKENINVALID;
        //    }
        //    return subjectAdd;
        //}

        /// <summary>
        /// Add & Update Subject
        /// </summary>
        /// <param name="subjectListViewModel"></param>
        /// <returns></returns>
        public SubjectListViewModel AddEditSubject(SubjectListViewModel subjectListViewModel)
        {
            SubjectListViewModel subjectAddUpdate = new SubjectListViewModel();
            if (TokenManager.CheckToken(subjectListViewModel._tenantName, subjectListViewModel._token))
            {
                subjectAddUpdate = this.courseManagerRepository.AddEditSubject(subjectListViewModel);
            }
            else
            {
                subjectAddUpdate._failure = true;
                subjectAddUpdate._message = TOKENINVALID;
            }
            return subjectAddUpdate;
        }

        /// <summary>
        /// Get All Subject List
        /// </summary>
        /// <param name="subjectListViewModel"></param>
        /// <returns></returns>
        public SubjectListViewModel GetAllSubjectList(SubjectListViewModel subjectListViewModel)
        {
            SubjectListViewModel subjectList = new SubjectListViewModel();
            if (TokenManager.CheckToken(subjectListViewModel._tenantName, subjectListViewModel._token))
            {
                subjectList = this.courseManagerRepository.GetAllSubjectList(subjectListViewModel);
            }
            else
            {
                subjectList._failure = true;
                subjectList._message = TOKENINVALID;
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
            SubjectAddViewModel subjectDelete = new SubjectAddViewModel();
            if (TokenManager.CheckToken(subjectAddViewModel._tenantName, subjectAddViewModel._token))
            {
                subjectDelete = this.courseManagerRepository.DeleteSubject(subjectAddViewModel);
            }
            else
            {
                subjectDelete._failure = true;
                subjectDelete._message = TOKENINVALID;
            }
            return subjectDelete;
        }

        /// <summary>
        /// Add Course
        /// </summary>
        /// <param name="courseAddViewModel"></param>
        /// <returns></returns>
        public CourseAddViewModel AddCourse(CourseAddViewModel courseAddViewModel)
        {
            CourseAddViewModel courseAdd = new CourseAddViewModel();
            try
            {
                if (TokenManager.CheckToken(courseAddViewModel._tenantName, courseAddViewModel._token))
                {
                    courseAdd = this.courseManagerRepository.AddCourse(courseAddViewModel);
                }
                else
                {
                    courseAdd._failure = true;
                    courseAdd._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                courseAdd._failure = true;
                courseAdd._message = es.Message;
            }
            return courseAdd;
        }

        /// <summary>
        /// Update Course
        /// </summary>
        /// <param name="courseAddViewModel"></param>
        /// <returns></returns>
        public CourseAddViewModel UpdateCourse(CourseAddViewModel courseAddViewModel)
        {
            CourseAddViewModel courseUpdate = new CourseAddViewModel();
            try
            {
                if (TokenManager.CheckToken(courseAddViewModel._tenantName, courseAddViewModel._token))
                {
                    courseUpdate = this.courseManagerRepository.UpdateCourse(courseAddViewModel);
                }
                else
                {
                    courseUpdate._failure = true;
                    courseUpdate._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                courseUpdate._failure = true;
                courseUpdate._message = es.Message;
            }
            return courseUpdate;
        }

        /// <summary>
        /// Delete Course
        /// </summary>
        /// <param name="courseAddViewModel"></param>
        /// <returns></returns>
        public CourseAddViewModel DeleteCourse(CourseAddViewModel courseAddViewModel)
        {
            CourseAddViewModel courseDelete = new CourseAddViewModel();
            try
            {
                if (TokenManager.CheckToken(courseAddViewModel._tenantName, courseAddViewModel._token))
                {
                    courseDelete = this.courseManagerRepository.DeleteCourse(courseAddViewModel);
                }
                else
                {
                    courseDelete._failure = true;
                    courseDelete._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                courseDelete._failure = true;
                courseDelete._message = es.Message;
            }
            return courseDelete;
        }

        /// <summary>
        /// Get All Course List
        /// </summary>
        /// <param name="courseListViewModel"></param>
        /// <returns></returns>
        public CourseListViewModel GetAllCourseList(CourseListViewModel courseListViewModel)
        {
            CourseListViewModel CourseListModel = new CourseListViewModel();
            try
            {
                if (TokenManager.CheckToken(courseListViewModel._tenantName, courseListViewModel._token))
                {
                    CourseListModel = this.courseManagerRepository.GetAllCourseList(courseListViewModel);
                }
                else
                {
                    CourseListModel._failure = true;
                    CourseListModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                CourseListModel._failure = true;
                CourseListModel._message = es.Message;
            }

            return CourseListModel;
        }
        /// <summary>
        /// Add Course Section
        /// </summary>
        /// <param name="courseSectionAddViewModel"></param>
        /// <returns></returns>
        public CourseSectionAddViewModel AddCourseSection(CourseSectionAddViewModel courseSectionAddViewModel)
        {
            CourseSectionAddViewModel courseSectionAdd = new CourseSectionAddViewModel();
            try
            {
                if (TokenManager.CheckToken(courseSectionAddViewModel._tenantName, courseSectionAddViewModel._token))
                {
                    courseSectionAdd = this.courseManagerRepository.AddCourseSection(courseSectionAddViewModel);
                }
                else
                {
                    courseSectionAdd._failure = true;
                    courseSectionAdd._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                courseSectionAdd._failure = true;
                courseSectionAdd._message = es.Message;
            }
            return courseSectionAdd;
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
                if (TokenManager.CheckToken(courseSectionViewModel._tenantName, courseSectionViewModel._token))
                {
                    courseSectionView = this.courseManagerRepository.GetAllCourseSection(courseSectionViewModel);
                }
                else
                {
                    courseSectionView._failure = true;
                    courseSectionView._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                courseSectionView._failure = true;
                courseSectionView._message = es.Message;
            }
            return courseSectionView;
        }
    }
}
 


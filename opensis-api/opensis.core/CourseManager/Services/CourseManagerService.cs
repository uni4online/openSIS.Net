using System;
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
        public ProgramAddViewModel AddProgram(ProgramAddViewModel programAddViewModel)
        {
            ProgramAddViewModel ProgramAddModel = new ProgramAddViewModel();
            try
            {
                if (TokenManager.CheckToken(programAddViewModel._tenantName, programAddViewModel._token))
                {
                    ProgramAddModel = this.courseManagerRepository.AddProgram(programAddViewModel);
                }
                else
                {
                    ProgramAddModel._failure = true;
                    ProgramAddModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {

                ProgramAddModel._failure = true;
                ProgramAddModel._message = es.Message;
            }
            return ProgramAddModel;
        }
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
        public ProgramAddViewModel UpdateProgram(ProgramAddViewModel programAddViewModel)
        {
            ProgramAddViewModel ProgramUpdateModel = new ProgramAddViewModel();
            try
            {
                if (TokenManager.CheckToken(programAddViewModel._tenantName, programAddViewModel._token))
                {
                    ProgramUpdateModel = this.courseManagerRepository.UpdateProgram(programAddViewModel);
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
        public SubjectAddViewModel AddSubject(SubjectAddViewModel subjectAddViewModel)
        {
            SubjectAddViewModel subjectAdd = new SubjectAddViewModel();

            if (TokenManager.CheckToken(subjectAddViewModel._tenantName, subjectAddViewModel._token))
            {
                subjectAdd = this.courseManagerRepository.AddSubject(subjectAddViewModel);
            }
            else
            {
                subjectAdd._failure = true;
                subjectAdd._message = TOKENINVALID;
            }
            return subjectAdd;
        }

        /// <summary>
        /// Update Subject
        /// </summary>
        /// <param name="subjectAddViewModel"></param>
        /// <returns></returns>
        public SubjectAddViewModel UpdateSubject(SubjectAddViewModel subjectAddViewModel)
        {
            SubjectAddViewModel subjectUpdate = new SubjectAddViewModel();
            if (TokenManager.CheckToken(subjectAddViewModel._tenantName, subjectAddViewModel._token))
            {
                subjectUpdate = this.courseManagerRepository.UpdateSubject(subjectAddViewModel);
            }
            else
            {
                subjectUpdate._failure = true;
                subjectUpdate._message = TOKENINVALID;
            }
            return subjectUpdate;
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
    }
}
 


using opensis.core.helper;
using opensis.core.Student.Interfaces;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Student.Services
{
    public class StudentService: IStudentService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public IStudentRepository studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        //Required for Unit Testing
        public StudentService() { }

        /// <summary>
        /// Student Add
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel SaveStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentAddViewModel = new StudentAddViewModel();
            if (TokenManager.CheckToken(student._tenantName, student._token))
            {
                studentAddViewModel = this.studentRepository.AddStudent(student);
            }
            else
            {
                studentAddViewModel._failure = true;
                studentAddViewModel._message = TOKENINVALID;
            }
            return studentAddViewModel;
        }

        /// <summary>
        /// Student Update
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel UpdateStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentUpdate = new StudentAddViewModel();
            if (TokenManager.CheckToken(student._tenantName, student._token))
            {

                studentUpdate = this.studentRepository.UpdateStudent(student);
            }
            else
            {
                studentUpdate._failure = true;
                studentUpdate._message = TOKENINVALID;
            }
            return studentUpdate;
        }

        /// <summary>
        /// Get All Student With Pagination,sorting,searching
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>

        public StudentListModel GetAllStudentList(PageResult pageResult)
        {
            logger.Info("Method getAllStudentList called.");
            StudentListModel studentList = new StudentListModel();
            try
            {
                if (TokenManager.CheckToken(pageResult._tenantName, pageResult._token))
                {
                    studentList = this.studentRepository.GetAllStudentList(pageResult);
                    studentList._message = SUCCESS;
                    studentList._failure = false;
                    logger.Info("Method getAllStudentList end with success.");
                }

                else
                {
                    studentList._failure = true;
                    studentList._message = TOKENINVALID;
                    return studentList;
                }
            }
            catch (Exception ex)
            {
                studentList._message = ex.Message;
                studentList._failure = true;
                logger.Error("Method getAllStudent end with error :" + ex.Message);
            }

            return studentList;
        }
        /// <summary>
        /// SearchContact For Student
        /// </summary>
        /// <param name="searchContactViewModel"></param>
        /// <returns></returns>

        public SearchContactViewModel SearchContactForStudent(SearchContactViewModel searchContactViewModel)
        {
            logger.Info("Method SearchContactForStudent called.");
            SearchContactViewModel contactViewModel = new SearchContactViewModel();
            try
            {
                if (TokenManager.CheckToken(searchContactViewModel._tenantName, searchContactViewModel._token))
                {
                    contactViewModel = this.studentRepository.SearchContactForStudent(searchContactViewModel);
                    //contactViewModel._message = SUCCESS;
                    //contactViewModel._failure = false;
                    logger.Info("Method SearchContactForStudent end with success.");
                }

                else
                {
                    contactViewModel._failure = true;
                    contactViewModel._message = TOKENINVALID;
                    return contactViewModel;
                }
            }
            catch (Exception ex)
            {
                contactViewModel._message = ex.Message;
                contactViewModel._failure = true;
                logger.Error("Method SearchContactForStudent end with error :" + ex.Message);
            }

            return contactViewModel;
        }
        /// <summary>
        /// Save StudentDocument
        /// </summary>
        /// <param name="studentDocumentAddViewModel"></param>
        /// <returns></returns>
        public StudentDocumentAddViewModel SaveStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            StudentDocumentAddViewModel studentDocumentAdd = new StudentDocumentAddViewModel();
            if (TokenManager.CheckToken(studentDocumentAddViewModel._tenantName, studentDocumentAddViewModel._token))
            {
                studentDocumentAdd = this.studentRepository.AddStudentDocument(studentDocumentAddViewModel);
            }
            else
            {
                studentDocumentAdd._failure = true;
                studentDocumentAdd._message = TOKENINVALID;
            }
            return studentDocumentAdd;
        }
        /// <summary>
        /// Update StudentDocument
        /// </summary>
        /// <param name="studentDocumentAddViewModel"></param>
        /// <returns></returns>
        public StudentDocumentAddViewModel UpdateStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            StudentDocumentAddViewModel studentDocumentUpdate = new StudentDocumentAddViewModel();
            if (TokenManager.CheckToken(studentDocumentAddViewModel._tenantName, studentDocumentAddViewModel._token))
            {
                studentDocumentUpdate = this.studentRepository.UpdateStudentDocument(studentDocumentAddViewModel);
            }
            else
            {
                studentDocumentUpdate._failure = true;
                studentDocumentUpdate._message = TOKENINVALID;
            }
            return studentDocumentUpdate;
        }
        /// <summary>
        /// Get All StudentDocuments List
        /// </summary>
        /// <param name="studentDocumentListViewModel"></param>
        /// <returns></returns>
        public StudentDocumentListViewModel GetAllStudentDocumentsList(StudentDocumentListViewModel studentDocumentListViewModel)
        {
            StudentDocumentListViewModel studentDocumentsList = new StudentDocumentListViewModel();
            if (TokenManager.CheckToken(studentDocumentListViewModel._tenantName, studentDocumentListViewModel._token))
            {
                studentDocumentsList = this.studentRepository.GetAllStudentDocumentsList(studentDocumentListViewModel);
            }
            else
            {
                studentDocumentsList._failure = true;
                studentDocumentsList._message = TOKENINVALID;
            }
            return studentDocumentsList;
        }
        /// <summary>
        /// Delete StudentDocument
        /// </summary>
        /// <param name="studentDocumentAddViewModel"></param>
        /// <returns></returns>
        public StudentDocumentAddViewModel DeleteStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            StudentDocumentAddViewModel studentDocumentdelete = new StudentDocumentAddViewModel();
            if (TokenManager.CheckToken(studentDocumentAddViewModel._tenantName, studentDocumentAddViewModel._token))
            {
                studentDocumentdelete = this.studentRepository.DeleteStudentDocument(studentDocumentAddViewModel);
            }
            else
            {
                studentDocumentdelete._failure = true;
                studentDocumentdelete._message = TOKENINVALID;
            }
            return studentDocumentdelete;
        }

        /// <summary>
        /// Add Student Login Info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public LoginInfoAddModel AddStudentLoginInfo(LoginInfoAddModel login)
        {
            LoginInfoAddModel loginInfo = new LoginInfoAddModel();
            if (TokenManager.CheckToken(login._tenantName, login._token))
            {

                loginInfo = this.studentRepository.AddStudentLoginInfo(login);
            }
            else
            {
                loginInfo._failure = true;
                loginInfo._message = TOKENINVALID;
            }
            return loginInfo;
        }
        ///// <summary>
        ///// Student View By Id
        ///// </summary>
        ///// <param name="student"></param>
        ///// <returns></returns>
        //public StudentAddViewModel ViewStudent(StudentAddViewModel student)
        //{
        //    StudentAddViewModel studentView = new StudentAddViewModel();
        //    if (TokenManager.CheckToken(student._tenantName, student._token))
        //    {

        //        studentView = this.studentRepository.ViewStudent(student);
        //    }
        //    else
        //    {
        //        studentView._failure = true;
        //        studentView._message = TOKENINVALID;     
        //    }
        //    return studentView;
        //}

        //public StudentAddViewModel DeleteStudent(StudentAddViewModel student)
        //{
        //    StudentAddViewModel studentDelete = new StudentAddViewModel();
        //    try
        //    {
        //        if (TokenManager.CheckToken(student._tenantName, student._token))
        //        {
        //            studentDelete = this.studentRepository.DeleteStudent(student);
        //        }
        //        else
        //        {
        //            studentDelete._failure = true;
        //            studentDelete._message = TOKENINVALID;
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        studentDelete._failure = true;
        //        studentDelete._message = es.Message;
        //    }

        //    return studentDelete;
        //}


    }
}

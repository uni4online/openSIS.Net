using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Student.Interfaces;
using opensis.data.Models;
using opensis.data.ViewModels.Student;

namespace opensisAPI.Controllers
{

    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("addStudent")]
        public ActionResult<StudentAddViewModel> AddStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentAdd = new StudentAddViewModel();
            try
            {
                if (student.studentMaster.SchoolId > 0)
                {
                    studentAdd = _studentService.SaveStudent(student);
                }
                else
                {
                    studentAdd._token = student._token;
                    studentAdd._tenantName = student._tenantName;
                    studentAdd._failure = true;
                    studentAdd._message = "Please enter valid school id";
                }
            }
            catch (Exception es)
            {
                studentAdd._failure = true;
                studentAdd._message = es.Message;
            }
            return studentAdd;
        }

        [HttpPut("updateStudent")]
        public ActionResult<StudentAddViewModel> UpdateStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentUpdate = new StudentAddViewModel();
            try
            {
                if (student.studentMaster.SchoolId > 0)
                {
                    studentUpdate = _studentService.UpdateStudent(student);
                }
                else
                {
                    studentUpdate._token = student._token;
                    studentUpdate._tenantName = student._tenantName;
                    studentUpdate._failure = true;
                    studentUpdate._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                studentUpdate._failure = true;
                studentUpdate._message = es.Message;
            }
            return studentUpdate;
        }

        [HttpPost("getAllStudentList")]
        public ActionResult<StudentListModel> GetAllStudentList(PageResult pageResult)
        {
            StudentListModel studentList = new StudentListModel();
            try
            {
                studentList = _studentService.GetAllStudentList(pageResult);
            }
            catch (Exception es)
            {
                studentList._message = es.Message;
                studentList._failure = true;
            }
            return studentList;
        }

        [HttpPost("searchContactForStudent")]
        public ActionResult<SearchContactViewModel> SearchContactForStudent(SearchContactViewModel searchContactViewModel)
        {
            SearchContactViewModel contactViewModel = new SearchContactViewModel();
            try
            {
                contactViewModel = _studentService.SearchContactForStudent(searchContactViewModel);
            }
            catch (Exception es)
            {
                contactViewModel._message = es.Message;
                contactViewModel._failure = true;
            }
            return contactViewModel;
        }


        [HttpPost("addStudentDocument")]
        public ActionResult<StudentDocumentAddViewModel> AddStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            StudentDocumentAddViewModel studentDocoumentAdd = new StudentDocumentAddViewModel();
            try
            {
                if (studentDocumentAddViewModel.studentDocument.SchoolId > 0)
                {
                    studentDocoumentAdd = _studentService.SaveStudentDocument(studentDocumentAddViewModel);
                }
                else
                {
                    studentDocoumentAdd._token = studentDocumentAddViewModel._token;
                    studentDocoumentAdd._tenantName = studentDocumentAddViewModel._tenantName;
                    studentDocoumentAdd._failure = true;
                    studentDocoumentAdd._message = "Please enter valid school id";
                }
            }
            catch (Exception es)
            {
                studentDocoumentAdd._failure = true;
                studentDocoumentAdd._message = es.Message;
            }
            return studentDocoumentAdd;
        }

        [HttpPut("updateStudentDocument")]

        public ActionResult<StudentDocumentAddViewModel> UpdateStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            StudentDocumentAddViewModel studentDocumentUpdate = new StudentDocumentAddViewModel();
            try
            {
                if (studentDocumentAddViewModel.studentDocument.SchoolId > 0)
                {
                    studentDocumentUpdate = _studentService.UpdateStudentDocument(studentDocumentAddViewModel);
                }
                else
                {
                    studentDocumentUpdate._token = studentDocumentAddViewModel._token;
                    studentDocumentUpdate._tenantName = studentDocumentAddViewModel._tenantName;
                    studentDocumentUpdate._failure = true;
                    studentDocumentUpdate._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                studentDocumentUpdate._failure = true;
                studentDocumentUpdate._message = es.Message;
            }
            return studentDocumentUpdate;
        }


        [HttpPost("getAllStudentDocumentsList")]

        public ActionResult<StudentDocumentListViewModel> GetAllStudentDocumentsList(StudentDocumentListViewModel studentDocumentsListViewModel)
        {
            StudentDocumentListViewModel studentDocumentsList = new StudentDocumentListViewModel();
            try
            {
                studentDocumentsList = _studentService.GetAllStudentDocumentsList(studentDocumentsListViewModel);
            }
            catch (Exception es)
            {
                studentDocumentsList._message = es.Message;
                studentDocumentsList._failure = true;
            }
            return studentDocumentsList;
        }


        [HttpPost("deleteStudentDocument")]
        public ActionResult<StudentDocumentAddViewModel> DeleteStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            StudentDocumentAddViewModel studentDocoumentdelete = new StudentDocumentAddViewModel();
            try
            {
                if (studentDocumentAddViewModel.studentDocument.SchoolId > 0)
                {
                    studentDocoumentdelete = _studentService.DeleteStudentDocument(studentDocumentAddViewModel);
                }
                else
                {
                    studentDocoumentdelete._token = studentDocumentAddViewModel._token;
                    studentDocoumentdelete._tenantName = studentDocumentAddViewModel._tenantName;
                    studentDocoumentdelete._failure = true;
                    studentDocoumentdelete._message = "Please enter valid school id";
                }
            }
            catch (Exception es)
            {
                studentDocoumentdelete._failure = true;
                studentDocoumentdelete._message = es.Message;
            }
            return studentDocoumentdelete;
        }


        [HttpPost("addStudentLoginInfo")]
        public ActionResult<LoginInfoAddModel> AddStudentLoginInfo(LoginInfoAddModel login)
        {
            LoginInfoAddModel loginInfo = new LoginInfoAddModel();
            try
            {
                if (login.userMaster.SchoolId > 0)
                {
                    loginInfo = _studentService.AddStudentLoginInfo(login);
                }
                else
                {
                    loginInfo._token = login._token;
                    loginInfo._tenantName = login._tenantName;
                    loginInfo._failure = true;
                    loginInfo._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                loginInfo._failure = true;
                loginInfo._message = es.Message;
            }
            return loginInfo;
        }

        //[HttpPost("viewStudent")]

        //public ActionResult<StudentAddViewModel> ViewStudent(StudentAddViewModel student)
        //{
        //    StudentAddViewModel studentView = new StudentAddViewModel();
        //    try
        //    {
        //        if (student.studentMaster.SchoolId > 0)
        //        {
        //            studentView = _studentService.ViewStudent(student);
        //        }
        //        else
        //        {
        //            studentView._token = student._token;
        //            studentView._tenantName = student._tenantName;
        //            studentView._failure = true;
        //            studentView._message = "Please enter valid scholl id";
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        studentView._failure = true;
        //        studentView._message = es.Message;
        //    }
        //    return studentView;
        //}



        //[HttpPost("deleteStudent")]

        //public ActionResult<StudentAddViewModel> DeleteStudent(StudentAddViewModel student)
        //{
        //    StudentAddViewModel studentDelete = new StudentAddViewModel();
        //    try
        //    {
        //        if (student.studentMaster.SchoolId > 0)
        //        {
        //            studentDelete = _studentService.DeleteStudent(student);
        //        }
        //        else
        //        {
        //            studentDelete._token = student._token;
        //            studentDelete._tenantName = student._tenantName;
        //            studentDelete._failure = true;
        //            studentDelete._message = "Please enter valid scholl id";
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

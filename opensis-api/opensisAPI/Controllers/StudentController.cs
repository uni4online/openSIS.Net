using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Student.Interfaces;
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
                if (student.studentEnrollment.SchoolId > 0)
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
                if (student.studentEnrollment.SchoolId > 0)
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

        [HttpPost("viewStudent")]

        public ActionResult<StudentAddViewModel> ViewStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentView = new StudentAddViewModel();
            try
            {
                if (student.studentEnrollment.SchoolId > 0)
                {
                    studentView = _studentService.ViewStudent(student);
                }
                else
                {
                    studentView._token = student._token;
                    studentView._tenantName = student._tenantName;
                    studentView._failure = true;
                    studentView._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                studentView._failure = true;
                studentView._message = es.Message;
            }
            return studentView;
        }



        [HttpPost("deleteStudent")]

        public ActionResult<StudentAddViewModel> DeleteStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentDelete = new StudentAddViewModel();
            try
            {
                if (student.studentEnrollment.SchoolId > 0)
                {
                    studentDelete = _studentService.DeleteStudent(student);
                }
                else
                {
                    studentDelete._token = student._token;
                    studentDelete._tenantName = student._tenantName;
                    studentDelete._failure = true;
                    studentDelete._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                studentDelete._failure = true;
                studentDelete._message = es.Message;
            }
            return studentDelete;
        }



    }
}

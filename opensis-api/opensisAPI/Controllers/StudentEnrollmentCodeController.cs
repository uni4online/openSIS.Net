using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.StudentEnrollmentCodes.Interfaces;
using opensis.data.ViewModels.StudentEnrollmentCodes;

namespace opensisAPI.Controllers
{

    [EnableCors("AllowOrigin")]
    [Route("{tenant}/StudentEnrollmentCode")]
    [ApiController]
    public class StudentEnrollmentCodeController : ControllerBase
    {

        private IStudentEnrollmentCodeService _studentEnrollmentCodeService;
        public StudentEnrollmentCodeController(IStudentEnrollmentCodeService studentEnrollmentCodeService)
        {
            _studentEnrollmentCodeService = studentEnrollmentCodeService;
        }

        [HttpPost("addStudentEnrollmentCode")]
        public ActionResult<StudentEnrollmentCodeAddViewModel> AddStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {
            StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAdd = new StudentEnrollmentCodeAddViewModel();
            try
            {
                studentEnrollmentCodeAdd = _studentEnrollmentCodeService.SaveStudentEnrollmentCode(studentEnrollmentCodeAddViewModel);
            }
            catch (Exception es)
            {
                studentEnrollmentCodeAdd._failure = true;
                studentEnrollmentCodeAdd._message = es.Message;
            }
            return studentEnrollmentCodeAdd;
        }

        [HttpPost("viewStudentEnrollmentCode")]

        public ActionResult<StudentEnrollmentCodeAddViewModel> ViewStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {
            StudentEnrollmentCodeAddViewModel studentEnrollmentCodeView = new StudentEnrollmentCodeAddViewModel();
            try
            {
                studentEnrollmentCodeView = _studentEnrollmentCodeService.ViewStudentEnrollmentCode(studentEnrollmentCodeAddViewModel);
            }
            catch (Exception es)
            {
                studentEnrollmentCodeView._failure = true;
                studentEnrollmentCodeView._message = es.Message;
            }
            return studentEnrollmentCodeView;
        }

        [HttpPost("deleteStudentEnrollmentCode")]

        public ActionResult<StudentEnrollmentCodeAddViewModel> DeleteStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {
            StudentEnrollmentCodeAddViewModel studentEnrollmentCodeDelete = new StudentEnrollmentCodeAddViewModel();
            try
            {
                studentEnrollmentCodeDelete = _studentEnrollmentCodeService.DeleteStudentEnrollmentCode(studentEnrollmentCodeAddViewModel);
            }
            catch (Exception es)
            {
                studentEnrollmentCodeDelete._failure = true;
                studentEnrollmentCodeDelete._message = es.Message;
            }
            return studentEnrollmentCodeDelete;
        }

        [HttpPut("updateStudentEnrollmentCode")]

        public ActionResult<StudentEnrollmentCodeAddViewModel> UpdateStudentEnrollmentCode(StudentEnrollmentCodeAddViewModel studentEnrollmentCodeAddViewModel)
        {
            StudentEnrollmentCodeAddViewModel studentEnrollmentCodeUpdate = new StudentEnrollmentCodeAddViewModel();
            try
            {
                studentEnrollmentCodeUpdate = _studentEnrollmentCodeService.UpdateStudentEnrollmentCode(studentEnrollmentCodeAddViewModel);
            }
            catch (Exception es)
            {
                studentEnrollmentCodeUpdate._failure = true;
                studentEnrollmentCodeUpdate._message = es.Message;
            }
            return studentEnrollmentCodeUpdate;
        }

        [HttpPost("getAllStudentEnrollmentCode")]

        public ActionResult<StudentEnrollmentCodeListViewModel> GetAllStudentEnrollmentCode(StudentEnrollmentCodeListViewModel studentEnrollmentCodeListView)
        {
            StudentEnrollmentCodeListViewModel studentEnrollmentCodeList = new StudentEnrollmentCodeListViewModel();
            try
            {
                if (studentEnrollmentCodeListView.SchoolId > 0)
                {
                    studentEnrollmentCodeList = _studentEnrollmentCodeService.GetAllStudentEnrollmentCode(studentEnrollmentCodeListView);
                }
                else
                {
                    studentEnrollmentCodeList._token = studentEnrollmentCodeListView._token;
                    studentEnrollmentCodeList._tenantName = studentEnrollmentCodeListView._tenantName;
                    studentEnrollmentCodeList._failure = true;
                    studentEnrollmentCodeList._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                studentEnrollmentCodeList._failure = true;
                studentEnrollmentCodeList._message = es.Message;
            }
            return studentEnrollmentCodeList;
        }




    }
}

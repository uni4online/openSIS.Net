using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.AttendanceCode.Interfaces;
using opensis.data.ViewModels.AttendanceCodes;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/AttendanceCode")]
    [ApiController]
    public class AttendanceCodeController : ControllerBase
    {
        private IAttendanceCodeRegisterService _attendanceCodeRegisterService;
        public AttendanceCodeController(IAttendanceCodeRegisterService attendanceCodeRegisterService)
        {
            _attendanceCodeRegisterService = attendanceCodeRegisterService;
        }
        [HttpPost("addAttendanceCode")]
        public ActionResult<AttendanceCodeAddViewModel> AddAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeAdd = new AttendanceCodeAddViewModel();
            try
            {
                attendanceCodeAdd = _attendanceCodeRegisterService.SaveAttendanceCode(attendanceCodeAddViewModel);
            }
            catch (Exception es)
            {
                attendanceCodeAdd._failure = true;
                attendanceCodeAdd._message = es.Message;
            }
            return attendanceCodeAdd;
        }
        [HttpPost("viewAttendanceCode")]

        public ActionResult<AttendanceCodeAddViewModel> ViewAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeView = new AttendanceCodeAddViewModel();
            try
            {
                attendanceCodeView = _attendanceCodeRegisterService.ViewAttendanceCode(attendanceCodeAddViewModel);
            }
            catch (Exception es)
            {
                attendanceCodeView._failure = true;
                attendanceCodeView._message = es.Message;
            }
            return attendanceCodeView;
        }
        [HttpPut("updatewAttendanceCode")]

        public ActionResult<AttendanceCodeAddViewModel> UpdateAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodeUpdate = new AttendanceCodeAddViewModel();
            try
            {
                attendanceCodeUpdate = _attendanceCodeRegisterService.UpdateAttendanceCode(attendanceCodeAddViewModel);
            }
            catch (Exception es)
            {
                attendanceCodeUpdate._failure = true;
                attendanceCodeUpdate._message = es.Message;
            }
            return attendanceCodeUpdate;
        }
        [HttpPost("deleteAttendanceCode")]

        public ActionResult<AttendanceCodeAddViewModel> DeleteAttendanceCode(AttendanceCodeAddViewModel attendanceCodeAddViewModel)
        {
            AttendanceCodeAddViewModel attendanceCodelDelete = new AttendanceCodeAddViewModel();
            try
            {
                attendanceCodelDelete = _attendanceCodeRegisterService.DeleteAttendanceCode(attendanceCodeAddViewModel);
            }
            catch (Exception es)
            {
                attendanceCodelDelete._failure = true;
                attendanceCodelDelete._message = es.Message;
            }
            return attendanceCodelDelete;
        }
    }
}

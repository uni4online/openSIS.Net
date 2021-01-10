using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.CourseManager.Interfaces;
using opensis.data.ViewModels.CourseManager;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/CourseManager")]
    [ApiController]
    public class CourseManagerController : ControllerBase
    {
        private ICourseManagerService _courseManagerService;
        public CourseManagerController(ICourseManagerService courseManagerService)
        {
            _courseManagerService = courseManagerService;
        }

        [HttpPost("addProgram")]
        public ActionResult<ProgramAddViewModel> AddProgram(ProgramAddViewModel programAddViewModel)
        {
            ProgramAddViewModel programAdd = new ProgramAddViewModel();
            try
            {
                programAdd = _courseManagerService.AddProgram(programAddViewModel);
            }
            catch (Exception es)
            {
                programAdd._failure = true;
                programAdd._message = es.Message;
            }
            return programAdd;
        }
        
        [HttpPost("GetAllProgram")]
        public ActionResult<ProgramListViewModel> GetAllProgram(ProgramListViewModel programListViewModel)
        {
            ProgramListViewModel programList = new ProgramListViewModel();
            try
            {
                programList = _courseManagerService.GetAllProgram(programListViewModel);               
                
            }
            catch (Exception es)
            {
                programList._message = es.Message;
                programList._failure = true;
            }
            return programList;
        }
        
        [HttpPut("updateProgram")]
        public ActionResult<ProgramAddViewModel> UpdateProgram(ProgramAddViewModel programAddViewModel)
        {
            ProgramAddViewModel programUpdate = new ProgramAddViewModel();
            try
            {
                programUpdate = _courseManagerService.UpdateProgram(programAddViewModel);
            }
            catch (Exception es)
            {
                programUpdate._failure = true;
                programUpdate._message = es.Message;
            }
            return programUpdate;
        }
        
        [HttpPost("deleteProgram")]
        public ActionResult<ProgramAddViewModel> DeleteProgram(ProgramAddViewModel programAddViewModel)
        {
            ProgramAddViewModel programDelete = new ProgramAddViewModel();
            try
            {
                programDelete = _courseManagerService.DeleteProgram(programAddViewModel);
            }
            catch (Exception es)
            {
                programDelete._failure = true;
                programDelete._message = es.Message;
            }
            return programDelete;
        }

        [HttpPost("addSubject")]
        public ActionResult<SubjectAddViewModel> AddSubject(SubjectAddViewModel subjectAddViewModel)
        {
            SubjectAddViewModel subjectAdd = new SubjectAddViewModel();
            try
            {
                subjectAdd = _courseManagerService.AddSubject(subjectAddViewModel);
            }
            catch (Exception es)
            {
                subjectAdd._failure = true;
                subjectAdd._message = es.Message;
            }
            return subjectAdd;
        }

        [HttpPut("updateSubject")]
        public ActionResult<SubjectAddViewModel> UpdateSubject(SubjectAddViewModel subjectAddViewModel)
        {
            SubjectAddViewModel subjectUpdate = new SubjectAddViewModel();
            try
            {
                if (subjectAddViewModel.subject.SchoolId > 0)
                {
                    subjectUpdate = _courseManagerService.UpdateSubject(subjectAddViewModel);
                }
                else
                {
                    subjectUpdate._token = subjectAddViewModel._token;
                    subjectUpdate._tenantName = subjectAddViewModel._tenantName;
                    subjectUpdate._failure = true;
                    subjectUpdate._message = "Please enter valid school id";
                }
            }
            catch (Exception es)
            {
                subjectUpdate._failure = true;
                subjectUpdate._message = es.Message;
            }
            return subjectUpdate;
        }

        [HttpPost("getAllSubjectList")]
        public ActionResult<SubjectListViewModel> GetAllSubjectList(SubjectListViewModel subjectListViewModel)
        {
            SubjectListViewModel subjectList = new SubjectListViewModel();
            try
            {
                subjectList = _courseManagerService.GetAllSubjectList(subjectListViewModel);
            }
            catch (Exception es)
            {
                subjectList._message = es.Message;
                subjectList._failure = true;
            }
            return subjectList;
        }

        [HttpPost("deleteSubject")]
        public ActionResult<SubjectAddViewModel> DeleteSubject(SubjectAddViewModel subjectAddViewModel)
        {
            SubjectAddViewModel subjectDelete = new SubjectAddViewModel();
            try
            {
                if (subjectAddViewModel.subject.SchoolId > 0)
                {
                    subjectDelete = _courseManagerService.DeleteSubject(subjectAddViewModel);
                }
                else
                {
                    subjectDelete._token = subjectAddViewModel._token;
                    subjectDelete._tenantName = subjectAddViewModel._tenantName;
                    subjectDelete._failure = true;
                    subjectDelete._message = "Please enter valid school id";
                }
            }
            catch (Exception es)
            {
                subjectDelete._failure = true;
                subjectDelete._message = es.Message;
            }
            return subjectDelete;
        }
    }
}

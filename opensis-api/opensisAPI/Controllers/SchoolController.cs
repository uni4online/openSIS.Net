using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.School.Interfaces;
using opensis.data.Models;
using opensis.data.ViewModels.School;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/School")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private ISchoolRegisterService _schoolRegisterService;
        public SchoolController(ISchoolRegisterService schoolRegisterService)
        {
            _schoolRegisterService = schoolRegisterService;
        }
       
        [HttpPost("addSchool")]
        public ActionResult<SchoolAddViewModel> AddSchool(SchoolAddViewModel school)
        {
            SchoolAddViewModel schoolAdd = new SchoolAddViewModel();
            try
            {
                schoolAdd= _schoolRegisterService.SaveSchool(school);
            }
            catch (Exception es)
            {
                schoolAdd._failure = true;
                schoolAdd._message = es.Message;
            }
            return schoolAdd;
        }

        [HttpPut("updateSchool")]

        public ActionResult<SchoolAddViewModel> UpdateSchool(SchoolAddViewModel school)
        {
            SchoolAddViewModel schoolAdd = new SchoolAddViewModel();
            try
            {
                schoolAdd =  _schoolRegisterService.UpdateSchool(school);
            }
            catch (Exception es)
            {
                schoolAdd._failure = true;
                schoolAdd._message = es.Message;
            }
            return schoolAdd;
        }

        [HttpPost("viewSchool")]

        public ActionResult<SchoolAddViewModel> ViewSchool(SchoolAddViewModel school)
        {
            SchoolAddViewModel schoolAdd = new SchoolAddViewModel();
            try
            {
                schoolAdd= _schoolRegisterService.ViewSchool(school);
            }
            catch (Exception es)
            {
                schoolAdd._failure = true;
                schoolAdd._message = es.Message;
            }
            return schoolAdd;
        }

        


        [HttpPost("getAllSchools")]

        public ActionResult<SchoolListModel> GetAllSchools(PageResult pageResult)
        {
            SchoolListModel schoolList = new SchoolListModel();
            try
            {
                schoolList =  _schoolRegisterService.GetAllSchools(pageResult);
            }
            catch (Exception es)
            {
                schoolList._message = es.Message;
                schoolList._failure=true;
            }
            return schoolList;
        }

        [HttpPost("getAllSchoolList")]

        public ActionResult<SchoolListModel> GetAllSchoolList(SchoolListModel school)
        {
            SchoolListModel schoolList = new SchoolListModel();
            try
            {
                schoolList = _schoolRegisterService.GetAllSchoolList(school);
            }
            catch (Exception es)
            {
                schoolList._message = es.Message;
                schoolList._failure = true;
            }
            return schoolList;
        }
        //[HttpPost("updateSchoolLogo/{guid}")]
        //public async Task<ActionResult<SchoolLogoUpdateModel>> UpdateSchoolLogo(Guid guid, [FromForm] SchoolLogoUpdateModel schoolLogoUpdateModel)
        //{
        //    var result = await _schoolRegisterService.updateSchoolLogo(guid, schoolLogoUpdateModel);
        //    return result;

        //}
    }
}
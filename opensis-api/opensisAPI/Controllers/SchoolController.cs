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
        //private readonly opensisContext _context;

        //public SchoolController(opensisContext context, ISchoolRegisterService schoolRegisterService)
        public SchoolController(ISchoolRegisterService schoolRegisterService)
        {
            _schoolRegisterService = schoolRegisterService;
           // _context = context;
            //_context.AddSampleData();
        }
       

        [HttpPost("getAllSchools")]

        public ActionResult<SchoolListViewModel> GetAllSchools(SchoolViewModel objModel)
        {
            //return _schoolRegisterService.getAllSchools(_context);
            return _schoolRegisterService.getAllSchools(objModel);
        }


        [HttpPut("addSchools")]

        public ActionResult<SchoolListViewModel> AddSchools(Schools schools)
        {
            return _schoolRegisterService.SaveSchool(schools);
        }

        [HttpPost("addSchool")]

        public async Task<ActionResult<SchoolAddViewMopdel>> AddSchool(SchoolAddViewMopdel school)
        {
            return await _schoolRegisterService.SaveSchool(school);
        }

        [HttpPut("updateSchool")]

        public async Task<ActionResult<SchoolAddViewMopdel>> UpdateSchool(SchoolAddViewMopdel school)
        {
            return await _schoolRegisterService.UpdateSchool(school);
        }

        [HttpPost("viewSchool")]

        public async Task<ActionResult<SchoolAddViewMopdel>> ViewSchool(SchoolAddViewMopdel school)
        {
            return await _schoolRegisterService.ViewSchool(school);
        }

        [HttpPost("getSchool")]
        public async Task<ActionResult<SchoolAddViewMopdel>> GetSchool(SchoolAddViewMopdel school)
        {
            return await _schoolRegisterService.EditSchool(school);
        }

        //[HttpPost("updateSchoolLogo/{guid}")]
        //public async Task<ActionResult<SchoolLogoUpdateModel>> UpdateSchoolLogo(Guid guid, [FromForm] SchoolLogoUpdateModel schoolLogoUpdateModel)
        //{
        //    var result = await _schoolRegisterService.updateSchoolLogo(guid, schoolLogoUpdateModel);
        //    return result;

        //}
    }
}
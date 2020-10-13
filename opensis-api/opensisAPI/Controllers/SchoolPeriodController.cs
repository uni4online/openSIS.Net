using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.SchoolPeriod.Interfaces;
using opensis.data.ViewModels.SchoolPeriod;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/SchoolPeriod")]
    [ApiController]
    public class SchoolPeriodController : ControllerBase
    {
        private ISchoolPeriodService _schoolPeriodService;
        public SchoolPeriodController(ISchoolPeriodService schoolPeriodService)
        {
            _schoolPeriodService = schoolPeriodService;
        }

        [HttpPost("addSchoolPeriod")]
        public ActionResult<SchoolPeriodAddViewModel> AddSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod)
        {
            SchoolPeriodAddViewModel schoolPeriodAdd = new SchoolPeriodAddViewModel();
            try
            {
                schoolPeriodAdd = _schoolPeriodService.SaveSchoolPeriod(schoolPeriod);
            }
            catch (Exception es)
            {
                schoolPeriodAdd._failure = true;
                schoolPeriodAdd._message = es.Message;
            }
            return schoolPeriodAdd;
        }

        [HttpPut("updateSchoolPeriod")]

        public ActionResult<SchoolPeriodAddViewModel> UpdateSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod)
        {
            SchoolPeriodAddViewModel schoolPeriodUpdate = new SchoolPeriodAddViewModel();
            try
            {
                schoolPeriodUpdate = _schoolPeriodService.UpdateSchoolPeriod(schoolPeriod);
            }
            catch (Exception es)
            {
                schoolPeriodUpdate._failure = true;
                schoolPeriodUpdate._message = es.Message;
            }
            return schoolPeriodUpdate;
        }

        [HttpPost("viewSchoolPeriod")]

        public ActionResult<SchoolPeriodAddViewModel> ViewSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod)
        {
            SchoolPeriodAddViewModel schoolPeriodView = new SchoolPeriodAddViewModel();
            try
            {
                schoolPeriodView = _schoolPeriodService.ViewSchoolPeriod(schoolPeriod);
            }
            catch (Exception es)
            {
                schoolPeriodView._failure = true;
                schoolPeriodView._message = es.Message;
            }
            return schoolPeriodView;
        }

        [HttpPost("deleteSchoolPeriod")]

        public ActionResult<SchoolPeriodAddViewModel> DeleteSchoolPeriod(SchoolPeriodAddViewModel schoolPeriod)
        {
            SchoolPeriodAddViewModel schoolPeriodDelete = new SchoolPeriodAddViewModel();
            try
            {
                schoolPeriodDelete = _schoolPeriodService.DeleteSchoolPeriod(schoolPeriod);
            }
            catch (Exception es)
            {
                schoolPeriodDelete._failure = true;
                schoolPeriodDelete._message = es.Message;
            }
            return schoolPeriodDelete;
        }
    }
}

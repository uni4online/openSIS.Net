using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Gradelevel.Interfaces;
using opensis.data.ViewModels.Gradelevel;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Gradelevel")]
    [ApiController]
    public class GradelevelController : ControllerBase
    {
        private IGradelevelService _gradelevelService;
        public GradelevelController(IGradelevelService gradelevelService)
        {
            _gradelevelService = gradelevelService;
        }

        [HttpPost("addGradelevel")]
        public ActionResult<GradelevelViewModel> AddGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelView = new GradelevelViewModel();
            try
            {
                gradelevelView = _gradelevelService.AddGradelevel(gradelevel);
            }
            catch (Exception es)
            {
                gradelevelView._failure = true;
                gradelevelView._message = es.Message;
            }
            return gradelevelView;
        }
        [HttpPost("viewGradelevel")]

        public ActionResult<GradelevelViewModel> ViewGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelView = new GradelevelViewModel();
            try
            {
                gradelevelView = _gradelevelService.ViewGradelevel(gradelevel);
            }
            catch (Exception es)
            {
                gradelevelView._failure = true;
                gradelevelView._message = es.Message;
            }
            return gradelevelView;
        }

        [HttpPut("updateGradelevel")]

        public ActionResult<GradelevelViewModel> UpdateGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelUpdate = new GradelevelViewModel();
            try
            {
                gradelevelUpdate = _gradelevelService.UpdateGradelevel(gradelevel);
            }
            catch (Exception es)
            {
                gradelevelUpdate._failure = true;
                gradelevelUpdate._message = es.Message;
            }
            return gradelevelUpdate;
        }

        [HttpPost("getAllGradeLevels")]

        public ActionResult<GradelevelListViewModel> GetAllGradeLevels(GradelevelListViewModel gradelevel)
        {
            GradelevelListViewModel gradelevelList = new GradelevelListViewModel();
            try
            {
                gradelevelList = _gradelevelService.GetAllGradeLevels(gradelevel);
            }
            catch (Exception es)
            {
                gradelevelList._failure = true;
                gradelevelList._message = es.Message;
            }
            return gradelevelList;
        }
        //[HttpPost("deleteGradelevel")]

        //public ActionResult<GradelevelViewModel> DeleteGradelevel(GradelevelViewModel gradelevel)
        //{
        //    GradelevelViewModel gradelevelDelete = new GradelevelViewModel();
        //    try
        //    {
        //        gradelevelDelete = _gradelevelService.DeleteGradelevel(gradelevel);
        //    }
        //    catch (Exception es)
        //    {
        //        gradelevelDelete._failure = true;
        //        gradelevelDelete._message = es.Message;
        //    }
        //    return gradelevelDelete;
        //}
    }
}

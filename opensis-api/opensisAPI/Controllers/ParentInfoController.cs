using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.ParentInfo.Interfaces;
using opensis.data.ViewModels.ParentInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/ParentInfo")]
    [ApiController]
    public class ParentInfoController : ControllerBase
    {
        private IParentInfoRegisterService _parentInfoRegisterService;
        public ParentInfoController(IParentInfoRegisterService parentInfoRegisterService)
        {
            _parentInfoRegisterService = parentInfoRegisterService;
        }
        [HttpPost("addParentInfo")]
        public ActionResult<ParentInfoAddViewModel> AddParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            ParentInfoAddViewModel parentInfoAdd = new ParentInfoAddViewModel();
            try
            {
                parentInfoAdd = _parentInfoRegisterService.SaveParentInfo(parentInfoAddViewModel);
            }
            catch (Exception es)
            {
                parentInfoAdd._failure = true;
                parentInfoAdd._message = es.Message;
            }
            return parentInfoAdd;
        }
        [HttpPost("viewParentInfo")]

        //public ActionResult<ParentInfoAddViewModel> ViewParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        //{
        //    ParentInfoAddViewModel parentInfoView = new ParentInfoAddViewModel();
        //    try
        //    {
        //        parentInfoView = _parentInfoRegisterService.ViewParentInfo(parentInfoAddViewModel);
        //    }
        //    catch (Exception es)
        //    {
        //        parentInfoView._failure = true;
        //        parentInfoView._message = es.Message;
        //    }
        //    return parentInfoView;
        //}
        [HttpPut("updateParentInfo")]

        public ActionResult<ParentInfoAddViewModel> UpdateParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            ParentInfoAddViewModel parentInfoUpdate = new ParentInfoAddViewModel();
            try
            {
                parentInfoUpdate = _parentInfoRegisterService.UpdateParentInfo(parentInfoAddViewModel);
            }
            catch (Exception es)
            {
                parentInfoUpdate._failure = true;
                parentInfoUpdate._message = es.Message;
            }
            return parentInfoUpdate;
        }
    }
}

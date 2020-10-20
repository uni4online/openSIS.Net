using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Section.Interfaces;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Section;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Section")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private ISectionService _sectionService;
        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpPost("addSection")]
        public ActionResult<SectionAddViewModel> AddSection(SectionAddViewModel section)
        {
            SectionAddViewModel sectionAdd = new SectionAddViewModel();
            try
            {
                if (section.tableSections.SchoolId > 0)
                {
                    sectionAdd = _sectionService.SaveSection(section);
                }
                else
                {
                    sectionAdd._token = section._token;
                    sectionAdd._tenantName = section._tenantName;
                    sectionAdd._failure = true;
                    sectionAdd._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                sectionAdd._failure = true;
                sectionAdd._message = es.Message;
            }
            return sectionAdd;
        }

        [HttpPut("updateSection")]

        public ActionResult<SectionAddViewModel> UpdateSection(SectionAddViewModel section)
        {
            SectionAddViewModel sectionUpdate = new SectionAddViewModel();
            try
            {
                if (section.tableSections.SchoolId > 0)
                {
                    sectionUpdate = _sectionService.UpdateSection(section);
                }
                else
                {
                    sectionUpdate._token = section._token;
                    sectionUpdate._tenantName = section._tenantName;
                    sectionUpdate._failure = true;
                    sectionUpdate._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                sectionUpdate._failure = true;
                sectionUpdate._message = es.Message;
            }
            return sectionUpdate;
        }


        [HttpPost("viewSection")]

        public ActionResult<SectionAddViewModel> ViewSection(SectionAddViewModel section)
        {
            SectionAddViewModel sectionView = new SectionAddViewModel();
            try
            {
                if (section.tableSections.SchoolId > 0)
                {
                    sectionView = _sectionService.ViewSection(section);
                }
                else
                {
                    sectionView._token = section._token;
                    sectionView._tenantName = section._tenantName;
                    sectionView._failure = true;
                    sectionView._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                sectionView._failure = true;
                sectionView._message = es.Message;
            }
            return sectionView;
        }

        
        
        [HttpPost("getAllSection")]

        public ActionResult<SectionListViewModel> GetAllsection(SectionListViewModel section)
        {
            SectionListViewModel sectionList = new SectionListViewModel();
            try
            {
                if (section.SchoolId > 0)
                {
                    sectionList = _sectionService.GetAllsection(section);
                }
                else
                {
                    sectionList._token = section._token;
                    sectionList._tenantName = section._tenantName;
                    sectionList._failure = true;
                    sectionList._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                sectionList._failure = true;
                sectionList._message = es.Message;
            }
            return sectionList;
        }

        [HttpPost("deleteSection")]

        public ActionResult<SectionAddViewModel> DeleteSection(SectionAddViewModel section)
        {
            SectionAddViewModel sectionDelete = new SectionAddViewModel();
            try
            {
                if (section.tableSections.SchoolId > 0)
                {
                    sectionDelete = _sectionService.DeleteSection(section);
                }
                else
                {
                    sectionDelete._token = section._token;
                    sectionDelete._tenantName = section._tenantName;
                    sectionDelete._failure = true;
                    sectionDelete._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                sectionDelete._failure = true;
                sectionDelete._message = es.Message;
            }
            return sectionDelete;
        }
    }
}

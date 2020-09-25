using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using opensis.core.School.Interfaces;
using opensis.data.ViewModels.Notice;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Notice")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        private INoticeService _noticeService;


        public NoticeController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }


        [HttpPost("addNotice")]
        public ActionResult<NoticeAddViewModel> AddNotice(NoticeAddViewModel notice)
        {
            NoticeAddViewModel noticeAdd = new NoticeAddViewModel();
            try
            {
                noticeAdd = _noticeService.SaveNotice(notice);
            }
            catch (Exception es)
            {
                noticeAdd._failure = true;
                noticeAdd._message = es.Message;
            }
            return noticeAdd;
        }

        [HttpPost("deleteNotice")]
        public ActionResult<NoticeDeleteModel> DeleteNotice(NoticeDeleteModel notice)
        {
            NoticeDeleteModel deleteNotice = new NoticeDeleteModel();
            try
            {
                deleteNotice = _noticeService.DeleteNotice(notice);
            }
            catch (Exception es)
            {
                deleteNotice._failure = true;
                deleteNotice._message = es.Message;
            }
            return deleteNotice;
        }


        [HttpPost("updateNotice")]
        public ActionResult<NoticeAddViewModel> UpdateNotice(NoticeAddViewModel notice)
        {
            NoticeAddViewModel noticeAdd = new NoticeAddViewModel();
            try
            {
                noticeAdd = _noticeService.UpdateNotice(notice);
            }
            catch (Exception es)
            {
                noticeAdd._failure = true;
                noticeAdd._message = es.Message;
            }
            return noticeAdd;
        }

        [HttpPost("viewNotice")]

        public ActionResult<NoticeAddViewModel> ViewNotice(NoticeAddViewModel notice)
        {
            NoticeAddViewModel noticeView = new NoticeAddViewModel();
            try
            {
                noticeView = _noticeService.ViewNotice(notice);
            }
            catch (Exception es)
            {
                noticeView._failure = true;
                noticeView._message = es.Message;
            }
            return noticeView;
        }

        [HttpPost("getAllNotice")]
        public ActionResult<NoticeListViewModel> GetAllNotice(NoticeListViewModel notice)
        {
            NoticeListViewModel noticeAdd = new NoticeListViewModel();
            try
            {
                noticeAdd = _noticeService.GetAllNotice(notice);
            }
            catch (Exception es)
            {
                noticeAdd._failure = true;
                noticeAdd._message = es.Message;
            }
            return noticeAdd;
        }
    }
}

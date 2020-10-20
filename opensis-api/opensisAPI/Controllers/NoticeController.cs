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
                if (notice.Notice.SchoolId > 0)
                {
                    noticeAdd = _noticeService.SaveNotice(notice);
                }
                else
                {
                    noticeAdd._token = notice._token;
                    noticeAdd._tenantName = notice._tenantName;
                    noticeAdd._failure = true;
                    noticeAdd._message = "Please enter valid scholl id";
                }
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
                if (notice.Notice.SchoolId > 0)
                {
                    noticeAdd = _noticeService.UpdateNotice(notice);
                }
                else
                {
                    noticeAdd._token = notice._token;
                    noticeAdd._tenantName = notice._tenantName;
                    noticeAdd._failure = true;
                    noticeAdd._message = "Please enter valid scholl id";
                }
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
                if (notice.Notice.SchoolId > 0)
                {
                    noticeView = _noticeService.ViewNotice(notice);
                }
                else
                {
                    noticeView._token = notice._token;
                    noticeView._tenantName = notice._tenantName;
                    noticeView._failure = true;
                    noticeView._message = "Please enter valid scholl id";
                }
            }
            catch (Exception es)
            {
                noticeView._failure = true;
                noticeView._message = es.Message;
            }
            return noticeView;
        }

        [HttpPost("getAllNotice")]
        public ActionResult<NoticeListViewModel> GetAllNotice(NoticeListViewModel noticeList)
        {
            NoticeListViewModel noticeAdd = new NoticeListViewModel();
            try
            {
                if (noticeList.SchoolId > 0)
                {
                    noticeAdd = _noticeService.GetAllNotice(noticeList);
                }
                else
                {
                    noticeAdd._token = noticeList._token;
                    noticeAdd._tenantName = noticeList._tenantName;
                    noticeAdd._failure = true;
                    noticeAdd._message = "Please enter valid scholl id";
                }
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

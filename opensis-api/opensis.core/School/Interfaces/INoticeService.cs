using opensis.data.ViewModels.Notice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opensis.core.School.Interfaces
{
    public interface INoticeService
    {
        NoticeAddViewModel SaveNotice(NoticeAddViewModel notice);

        NoticeListViewModel GetAllNotice(NoticeListViewModel noticeList);

        NoticeAddViewModel UpdateNotice(NoticeAddViewModel notice);
        NoticeDeleteModel DeleteNotice(NoticeDeleteModel notice);

        NoticeAddViewModel ViewNotice(NoticeAddViewModel notice);
    }
}

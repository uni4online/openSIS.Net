using opensis.data.ViewModels.Notice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opensis.data.Interface
{
    public interface INoticeRepository
    {
        NoticeAddViewModel AddNotice(NoticeAddViewModel notice);

        NoticeAddViewModel UpdateNotice(NoticeAddViewModel notice);

        NoticeListViewModel GetAllNotice(NoticeListViewModel noticeList);
        NoticeDeleteModel DeleteNotice(NoticeDeleteModel notice);
        NoticeAddViewModel ViewNotice(NoticeAddViewModel notice);
    }
}

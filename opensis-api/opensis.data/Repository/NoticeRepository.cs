using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Notice;
using opensis.data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opensis.data.Repository
{
    public class NoticeRepository : INoticeRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public NoticeRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Add Notice
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public NoticeAddViewModel AddNotice(NoticeAddViewModel notice)
        {
            try
            {
                int? noticeId = Utility.GetMaxPK(this.context, new Func<TableNotice, int>(x => x.NoticeId));

                notice.Notice.NoticeId = (int)noticeId;

                notice.Notice.TenantId = notice.Notice.TenantId;

                notice.Notice.Isactive = true;

                notice.Notice.CreatedTime = DateTime.UtcNow;

                this.context?.TableNotice.Add(notice.Notice);

                this.context?.SaveChanges();
                notice._failure = false;

                return notice;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Get Notice By Id
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public NoticeAddViewModel ViewNotice(NoticeAddViewModel notice)
        {

            try
            {
                NoticeAddViewModel noticeAddViewModel = new NoticeAddViewModel();
                var noticeModel = this.context?.TableNotice.FirstOrDefault(x => x.TenantId == notice.Notice.TenantId && x.NoticeId == notice.Notice.NoticeId);
                if (noticeModel != null)
                {
                    noticeAddViewModel.Notice = noticeModel;

                }
                else
                {
                    noticeAddViewModel._failure = true;
                    noticeAddViewModel._message = NORECORDFOUND;
                    return noticeAddViewModel;
                }

                return noticeAddViewModel;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Delete Notice
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public NoticeDeleteModel DeleteNotice(NoticeDeleteModel notice)
        {
            try
            {
                var noticeRepository = this.context?.TableNotice.Where(x => x.NoticeId == notice.NoticeId).ToList().OrderBy(x => x.NoticeId).LastOrDefault();

                noticeRepository.Isactive = false;
                this.context?.SaveChanges();
                notice._failure = false;

                return notice;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Update Notice
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public NoticeAddViewModel UpdateNotice(NoticeAddViewModel notice)
        {
            try
            {
                var noticeRepository = this.context?.TableNotice.FirstOrDefault(x => x.TenantId == notice.Notice.TenantId && x.SchoolId == notice.Notice.SchoolId && x.NoticeId == notice.Notice.NoticeId);

                if (noticeRepository != null)
                {
                    noticeRepository.TenantId = notice.Notice.TenantId;
                    noticeRepository.Title = notice.Notice.Title;

                    noticeRepository.TargetMembershipIds = notice.Notice.TargetMembershipIds;
                    noticeRepository.Body = notice.Notice.Body;
                    noticeRepository.ValidFrom = notice.Notice.ValidFrom;
                    noticeRepository.ValidTo = notice.Notice.ValidTo;
                    noticeRepository.Isactive = true;
                    noticeRepository.CreatedTime = DateTime.UtcNow;

                    this.context?.SaveChanges();
                    notice._failure = false;
                    return notice;
                }
                else
                {
                    notice = null;
                    notice._failure = true;
                    notice._message = NORECORDFOUND;
                    return notice;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Get all Notice
        /// </summary>
        /// <returns></returns>
        public NoticeListViewModel GetAllNotice()
        {
            NoticeListViewModel getAllNoticeList = new NoticeListViewModel();
            try
            {
                var noticeRepository = this.context?.TableNotice.OrderBy(x => x.ValidFrom).Where(x => x.Isactive == true).ToList();


                getAllNoticeList.NoticeList = noticeRepository;

                return getAllNoticeList;
            }
            catch (Exception ex)
            {
                getAllNoticeList = null;
                getAllNoticeList._failure = true;
                getAllNoticeList._message = NORECORDFOUND;
                return getAllNoticeList;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Notice
{
    public class NoticeDeleteModel : CommonFields
    {
        public int NoticeId { get; set; }
        public int SchoolId { get; set; }
        public Guid TenantId { get; set; }
    }
}

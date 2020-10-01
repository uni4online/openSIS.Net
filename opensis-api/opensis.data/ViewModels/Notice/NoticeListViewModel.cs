using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Notice
{
   public class NoticeListViewModel : CommonFields
    {
        public List<opensis.data.Models.Notice> NoticeList { get; set; }
    }
}

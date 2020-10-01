using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace opensis.data.ViewModels.Notice
{
    public class NoticeAddViewModel : CommonFields
    {
        public opensis.data.Models.Notice Notice { get; set; }

    }
}

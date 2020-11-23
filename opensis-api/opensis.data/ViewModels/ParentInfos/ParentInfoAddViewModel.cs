using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.ParentInfos
{
    public class ParentInfoAddViewModel : CommonFields
    {
        public ParentInfo parentInfo { get; set; }
        public string PasswordHash { get; set; }
    }
}

using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.ParentInfos
{
    public class ParentInfoListModel : CommonFields
    {
        public List<ParentInfo> parentInfoList { get; set; }        
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int StudentId { get; set; }        
    }
}

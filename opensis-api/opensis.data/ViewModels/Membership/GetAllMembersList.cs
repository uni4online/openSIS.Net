using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Membership
{
   public class GetAllMembersList : CommonFields
    {
        public List<GetAllMembers> GetAllMemberList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }

    }
}

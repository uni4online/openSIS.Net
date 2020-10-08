using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public MembershipRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Get All Members
        /// </summary>
        /// <returns></returns>
        public GetAllMembersList GetAllMemberList(GetAllMembersList membersList)
        {
            GetAllMembersList getAllMembersList = new GetAllMembersList();
            try
            {
                var membershipRepository = this.context?.Membership.Where(x => x.TenantId == membersList.TenantId && x.SchoolId == membersList.SchoolId).ToList();

                getAllMembersList.GetAllMemberList = membershipRepository;

                return getAllMembersList;
            }
            catch (Exception ex)
            {
                getAllMembersList = null;
                getAllMembersList._failure = true;
                getAllMembersList._message = NORECORDFOUND;
                return getAllMembersList;
            }
        }

    }
}
